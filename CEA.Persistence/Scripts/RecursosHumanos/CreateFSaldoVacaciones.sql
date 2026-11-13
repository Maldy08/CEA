-- Vacation balance of an employee as of today, starting from the last period captured in NOM_VACAEMP:
-- a) the first period with days left (year, semester, days) and b) the total days pending up to today.
-- One row per active employee (none if EMPLEADOS.ACTIVO <> 'V'). Read-only: it does not write anything.
-- Usage (same pattern as F_SIMULA_VACACIONES): SELECT * FROM TABLE(F_SALDO_VACACIONES(7148));

CREATE OR REPLACE TYPE T_SALDO_VACACIONES_ROW AS OBJECT (
    EMPLEADO                     NUMBER,
    NOMBRE_COMPLETO              VARCHAR2(200),
    FECHAALTA                    DATE,
    ANTIGUEDAD_ACTUAL_ANOS       NUMBER,
    A_ANO_DISPONIBLE             NUMBER,
    A_PERIODO_DISPONIBLE         NUMBER,
    A_DIAS_POR_DISFRUTAR         NUMBER,
    B_DIAS_TOTALES_POR_DISFRUTAR NUMBER
);
/

CREATE OR REPLACE TYPE T_SALDO_VACACIONES_TAB AS TABLE OF T_SALDO_VACACIONES_ROW;
/

CREATE OR REPLACE FUNCTION F_SALDO_VACACIONES (
    P_NOEMP IN NUMBER
) RETURN T_SALDO_VACACIONES_TAB PIPELINED
AS
BEGIN
    FOR R IN (
        WITH Parametro AS (
            SELECT P_NOEMP AS NOEMP FROM DUAL
        ),
        -- 1. Datos base del empleado
        BaseEmpleado AS (
            SELECT
                E.EMPLEADO,
                E.PATERNO || ' ' || E.MATERNO || ' ' || E.NOMBRE AS NOMBRE_COMPLETO,
                E.FECHAALTA,
                EXTRACT(YEAR FROM E.FECHAALTA) AS ANO_ALTA,
                TRUNC(MONTHS_BETWEEN(SYSDATE, E.FECHAALTA) / 12) AS ANTIGUEDAD_ACTUAL_ANOS
            FROM CEA.EMPLEADOS E
            JOIN Parametro P ON E.EMPLEADO = P.NOEMP
            WHERE E.ACTIVO = 'V'
        ),
        -- 2. Días correspondientes por semestre según la tabla de primas
        DerechosSemestrales AS (
            SELECT ANOS, (DIAS / 2) AS DIAS_POR_PERIODO
            FROM CEA.NOM_VACAPRIMA
        ),
        -- 3. Último registro capturado (FHASTA DESC, NOMOV DESC)
        UltimoRegistroCapturado AS (
            SELECT
                V.NOEMP,
                TO_NUMBER(SUBSTR(V.PERVAC, 1, 4)) AS ANO_PERVAC,
                TO_NUMBER(SUBSTR(V.PERVAC, 6, 1)) AS SEMESTRE_PERVAC,
                V.DIASXDISF,
                V.FHASTA,
                V.NOMOV
            FROM (
                SELECT V.*,
                       ROW_NUMBER() OVER (PARTITION BY V.NOEMP ORDER BY V.FHASTA DESC, V.NOMOV DESC) AS RN
                FROM CEA.NOM_VACAEMP V
                JOIN Parametro P ON V.NOEMP = P.NOEMP
            ) V
            WHERE V.RN = 1
        ),
        -- 4. Años desde el último periodo registrado hasta el año actual
        GeneradorAnos (ANO, MAX_ANO) AS (
            SELECT
                NVL((SELECT U.ANO_PERVAC FROM UltimoRegistroCapturado U), B.ANO_ALTA),
                EXTRACT(YEAR FROM SYSDATE)
            FROM BaseEmpleado B
            UNION ALL
            SELECT ANO + 1, MAX_ANO FROM GeneradorAnos WHERE ANO < MAX_ANO
        ),
        -- 5. Semestres 1 y 2
        GeneradorSemestres AS (
            SELECT 1 AS SEMESTRE FROM DUAL
            UNION ALL
            SELECT 2 AS SEMESTRE FROM DUAL
        ),
        -- 6. Balances de los periodos vigentes y futuros hasta hoy
        PeriodosPendientes AS (
            SELECT
                G.ANO,
                S.SEMESTRE,
                (G.ANO - B.ANO_ALTA) AS ANTIGUEDAD_PERIODO,
                -- Último periodo capturado: su DIASXDISF remanente; posteriores: días completos del tabulador
                CASE
                    WHEN G.ANO = U.ANO_PERVAC AND S.SEMESTRE = U.SEMESTRE_PERVAC THEN U.DIASXDISF
                    ELSE NVL(D.DIAS_POR_PERIODO, 0)
                END AS DIAS_DISPONIBLES
            FROM GeneradorAnos G
            CROSS JOIN GeneradorSemestres S
            CROSS JOIN BaseEmpleado B
            LEFT JOIN UltimoRegistroCapturado U ON 1 = 1
            LEFT JOIN DerechosSemestrales D ON D.ANOS = (G.ANO - B.ANO_ALTA)
            WHERE (U.ANO_PERVAC IS NULL OR (G.ANO > U.ANO_PERVAC OR (G.ANO = U.ANO_PERVAC AND S.SEMESTRE >= U.SEMESTRE_PERVAC)))
              AND (G.ANO - B.ANO_ALTA) <= B.ANTIGUEDAD_ACTUAL_ANOS
        ),
        -- 7. Periodos con días disponibles, en orden cronológico
        PeriodosConSaldo AS (
            SELECT P.*, ROW_NUMBER() OVER (ORDER BY P.ANO ASC, P.SEMESTRE ASC) AS ORDEN_DISPONIBLE
            FROM PeriodosPendientes P
            WHERE P.DIAS_DISPONIBLES > 0
        )
        -- 8. a) primer periodo con saldo; b) total pendiente hasta hoy
        SELECT
            B.EMPLEADO,
            B.NOMBRE_COMPLETO,
            B.FECHAALTA,
            B.ANTIGUEDAD_ACTUAL_ANOS,
            PS.ANO              AS A_ANO_DISPONIBLE,
            PS.SEMESTRE         AS A_PERIODO_DISPONIBLE,
            PS.DIAS_DISPONIBLES AS A_DIAS_POR_DISFRUTAR,
            NVL((SELECT SUM(DIAS_DISPONIBLES) FROM PeriodosConSaldo), 0) AS B_DIAS_TOTALES_POR_DISFRUTAR
        FROM BaseEmpleado B
        LEFT JOIN PeriodosConSaldo PS ON PS.ORDEN_DISPONIBLE = 1
    ) LOOP
        PIPE ROW (T_SALDO_VACACIONES_ROW(
            R.EMPLEADO, R.NOMBRE_COMPLETO, R.FECHAALTA, R.ANTIGUEDAD_ACTUAL_ANOS,
            R.A_ANO_DISPONIBLE, R.A_PERIODO_DISPONIBLE, R.A_DIAS_POR_DISFRUTAR, R.B_DIAS_TOTALES_POR_DISFRUTAR));
    END LOOP;
    RETURN;
END F_SALDO_VACACIONES;
/
