-- Simulates how a vacation request (P_DIAS days) is distributed across the employee's periods (year/semester),
-- starting from the last period registered in NOM_VACAEMP. Read-only: it does not write anything.
-- Usage (same pattern as F_LISTADOOFICIOS): SELECT * FROM TABLE(F_SIMULA_VACACIONES(7148, 10));

CREATE OR REPLACE TYPE T_SIMULA_VACACIONES_ROW AS OBJECT (
    NOEMP          NUMBER,
    ANO            NUMBER,
    DIAS           NUMBER,
    PERIODO        NUMBER,
    DIASXDISFRUTAR NUMBER
);
/

CREATE OR REPLACE TYPE T_SIMULA_VACACIONES_TAB AS TABLE OF T_SIMULA_VACACIONES_ROW;
/

CREATE OR REPLACE FUNCTION F_SIMULA_VACACIONES (
    P_NOEMP IN NUMBER,
    P_DIAS  IN NUMBER
) RETURN T_SIMULA_VACACIONES_TAB PIPELINED
AS
BEGIN
    FOR R IN (
        WITH
        -- 1. Argumentos de entrada
        Parametros AS (
            SELECT P_NOEMP AS NOEMP, P_DIAS AS DIAS_A_SOLICITAR FROM DUAL
        ),
        -- 2. Datos base del empleado (fecha alta)
        BaseEmpleado AS (
            SELECT E.EMPLEADO, E.FECHAALTA, EXTRACT(YEAR FROM E.FECHAALTA) AS ANO_ALTA
            FROM CEA.EMPLEADOS E, Parametros P
            WHERE E.EMPLEADO = P.NOEMP
        ),
        -- 3. Derechos por año (divididos en 2 periodos)
        DerechosPorAno AS (
            SELECT ANOS, (DIAS / 2) AS DIAS_POR_PERIODO
            FROM CEA.NOM_VACAPRIMA
        ),
        -- 4. Último balance registrado por periodo (su DIASXDISF es el balance oficial)
        UltimoBalanceRegistrado AS (
            SELECT
                V.NOEMP,
                TO_NUMBER(SUBSTR(V.PERVAC, 1, 4)) AS ANO,
                TO_NUMBER(SUBSTR(V.PERVAC, 6, 1)) AS SEMESTRE,
                V.DIASXDISF AS DIAS_BALANCE_RESTANTE
            FROM (
                SELECT V.*,
                       ROW_NUMBER() OVER (PARTITION BY V.NOEMP, V.PERVAC ORDER BY V.FDESDE DESC, V.NOMOV DESC) AS RN
                FROM CEA.NOM_VACAEMP V, Parametros P
                WHERE V.NOEMP = P.NOEMP
            ) V
            WHERE V.RN = 1
        ),
        -- 5. Último periodo que se tocó (punto de partida)
        PuntoDeInicio AS (
            SELECT NVL(MAX(ANO), 0) AS ANO, NVL(MAX(SEMESTRE), 0) AS SEMESTRE
            FROM (
                SELECT ANO, SEMESTRE
                FROM UltimoBalanceRegistrado
                ORDER BY ANO DESC, SEMESTRE DESC
            )
            WHERE ROWNUM = 1
        ),
        -- 6. Años desde el alta hasta el año actual
        GeneradorAnos (ANO, MAX_ANO) AS (
            SELECT B.ANO_ALTA, EXTRACT(YEAR FROM SYSDATE) FROM BaseEmpleado B
            UNION ALL
            SELECT ANO + 1, MAX_ANO FROM GeneradorAnos WHERE ANO < MAX_ANO
        ),
        -- 7. Semestres 1 y 2
        GeneradorSemestres (SEMESTRE) AS (
            SELECT 1 FROM DUAL
            UNION ALL
            SELECT 2 FROM DUAL
        ),
        -- 8. Balance maestro: el registrado si existe; si no, el de la ley
        MasterBalance AS (
            SELECT
                G.ANO,
                S.SEMESTRE,
                (G.ANO - B.ANO_ALTA) AS ANTIGUEDAD,
                NVL(U.DIAS_BALANCE_RESTANTE, D.DIAS_POR_PERIODO) AS DIAS_BALANCE_ACTUAL
            FROM GeneradorAnos G
            CROSS JOIN GeneradorSemestres S
            JOIN BaseEmpleado B ON 1 = 1
            LEFT JOIN DerechosPorAno D ON D.ANOS = (G.ANO - B.ANO_ALTA)
            LEFT JOIN UltimoBalanceRegistrado U ON U.ANO = G.ANO AND U.SEMESTRE = S.SEMESTRE
        ),
        -- 9. Periodos con saldo desde el punto de partida
        PeriodosParaGastar AS (
            SELECT M.*, ROW_NUMBER() OVER (ORDER BY M.ANO, M.SEMESTRE) AS RN
            FROM MasterBalance M
            LEFT JOIN PuntoDeInicio P ON 1 = 1
            WHERE (P.ANO = 0 OR (M.ANO > P.ANO OR (M.ANO = P.ANO AND M.SEMESTRE >= P.SEMESTRE)))
              AND M.DIAS_BALANCE_ACTUAL > 0
        ),
        -- 10. Reparte los días solicitados periodo por periodo
        Simulacion (RN, ANO, SEMESTRE, DIAS_A_TOMAR, NUEVO_BALANCE, DIAS_RESTANTES_SOLICITUD) AS (
            SELECT
                S.RN,
                S.ANO,
                S.SEMESTRE,
                LEAST(S.DIAS_BALANCE_ACTUAL, P.DIAS_A_SOLICITAR),
                S.DIAS_BALANCE_ACTUAL - LEAST(S.DIAS_BALANCE_ACTUAL, P.DIAS_A_SOLICITAR),
                P.DIAS_A_SOLICITAR - LEAST(S.DIAS_BALANCE_ACTUAL, P.DIAS_A_SOLICITAR)
            FROM PeriodosParaGastar S, Parametros P
            WHERE S.RN = 1
            UNION ALL
            SELECT
                S.RN,
                S.ANO,
                S.SEMESTRE,
                LEAST(S.DIAS_BALANCE_ACTUAL, R.DIAS_RESTANTES_SOLICITUD),
                S.DIAS_BALANCE_ACTUAL - LEAST(S.DIAS_BALANCE_ACTUAL, R.DIAS_RESTANTES_SOLICITUD),
                R.DIAS_RESTANTES_SOLICITUD - LEAST(S.DIAS_BALANCE_ACTUAL, R.DIAS_RESTANTES_SOLICITUD)
            FROM Simulacion R
            JOIN PeriodosParaGastar S ON S.RN = R.RN + 1
            WHERE R.DIAS_RESTANTES_SOLICITUD > 0
        )
        SELECT P.NOEMP, S.ANO, S.DIAS_A_TOMAR, S.SEMESTRE, S.NUEVO_BALANCE
        FROM Simulacion S, Parametros P
        ORDER BY S.RN
    ) LOOP
        PIPE ROW (T_SIMULA_VACACIONES_ROW(R.NOEMP, R.ANO, R.DIAS_A_TOMAR, R.SEMESTRE, R.NUEVO_BALANCE));
    END LOOP;
    RETURN;
END F_SIMULA_VACACIONES;
/
