-- Días hábiles del empleado entre dos fechas (inclusive): se brincan sábados, domingos y los festivos de día
-- completo que le tocan (misma regla que NomDiasFestivosRepository.GetByEmpleadoAsync: tipo B/C/T, sexo del
-- tipo y oficina LUGARTRAB; festivo con HORA_ENTRADA/SALIDA sí se trabaja). Las horas de las fechas se ignoran.
-- Lo usa VS_TRAMITES_FORMATO para los días de los permisos (TIPO_TRAMITE 3).

CREATE OR REPLACE FUNCTION FN_DIAS_HABILES_EMP (
    p_noemp IN NUMBER,
    p_desde IN DATE,
    p_hasta IN DATE
) RETURN NUMBER
IS
    v_dias NUMBER := 0;
    v_dia  DATE   := TRUNC(p_desde);
    v_fest NUMBER;
BEGIN
    IF p_desde IS NULL OR p_hasta IS NULL THEN
        RETURN NULL;
    END IF;
    WHILE v_dia <= TRUNC(p_hasta) LOOP
        IF TO_CHAR(v_dia, 'DY', 'NLS_DATE_LANGUAGE=ENGLISH') NOT IN ('SAT', 'SUN') THEN
            SELECT COUNT(*) INTO v_fest
              FROM NOM_DIAS_FESTIVOS F
              JOIN EMPLEADOS E ON E.EMPLEADO = p_noemp
             WHERE F.ACTIVO = 1
               AND F.FECHA_FESTIVO = v_dia
               AND F.HORA_ENTRADA IS NULL AND F.HORA_SALIDA IS NULL
               AND (   (TRIM(E.ID_TIPOEMP) = 'B' AND F.APLICA_BASE = 1 AND (F.BASE_SEXO IS NULL OR F.BASE_SEXO = E.SEXO))
                    OR (TRIM(E.ID_TIPOEMP) = 'C' AND F.APLICA_CONFIANZA = 1 AND (F.CONF_SEXO IS NULL OR F.CONF_SEXO = E.SEXO))
                    OR (TRIM(E.ID_TIPOEMP) = 'T' AND F.APLICA_CONTRATO = 1 AND (F.CONT_SEXO IS NULL OR F.CONT_SEXO = E.SEXO)))
               AND (NOT EXISTS (SELECT 1 FROM NOM_DIAS_FESTIVOS_OFICINAS O WHERE O.ID_FESTIVO = F.ID_FESTIVO)
                    OR EXISTS (SELECT 1 FROM NOM_DIAS_FESTIVOS_OFICINAS O
                               WHERE O.ID_FESTIVO = F.ID_FESTIVO AND O.ID_OFICINA = E.LUGARTRAB));
            IF v_fest = 0 THEN
                v_dias := v_dias + 1;
            END IF;
        END IF;
        v_dia := v_dia + 1;
    END LOOP;
    RETURN v_dias;
END;
/
