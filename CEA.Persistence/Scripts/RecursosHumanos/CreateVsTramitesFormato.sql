-- Formato oficial impreso de los trámites de RH (NOM_TRAMITES). Igual para todos los tipos de trámite;
-- se consulta solo por ID_TRAMITE:
--   SELECT * FROM VS_TRAMITES_FORMATO     WHERE ID_TRAMITE = :id;               -- 1 renglón (encabezado)
--   SELECT * FROM VS_TRAMITES_FORMATO_DET WHERE ID_TRAMITE = :id ORDER BY ORDEN; -- N renglones (desglose)
--
-- Cada tipo de trámite llena las columnas genéricas (FECHA_INICIO, FECHA_FIN, DIAS, ...) con su propia tabla:
-- vacaciones (TIPO_TRAMITE 1 → NOM_VACAEMP_M / _D) y permisos/justificantes (3 y 4 → NOM_PERMISOS_JUSTIFICANTES). Al agregar un tipo: otro LEFT JOIN + NVL en el
-- encabezado y otro UNION ALL en el detalle. Lo que no aplique al tipo queda NULL y el formato lo oculta.
--
-- PROVISIONAL (no existe en Oracle todavía): MODALIDAD = 'ORDINARIO'.
-- Firma de RH: responsable del departamento 24 (DEPTOS_UE.EMP_RESPON).
-- DIAS de permisos = días hábiles del empleado (FN_DIAS_HABILES_EMP, crearla antes). Justificantes: sin DIAS.
-- SALDO_TOTAL (solo vacaciones) = F_SALDO_VACACIONES.B_DIAS_TOTALES_POR_DISFRUTAR a la fecha de consulta.
-- TIPO_INCIDENCIA / DESC_INCIDENCIA / MOTIVO solo para permisos y justificantes.

CREATE OR REPLACE VIEW VS_TRAMITES_FORMATO AS
SELECT
    T.ID_TRAMITE,
    T.TIPO_TRAMITE,
    TT.NOMBRE_TRAMITE                                   AS DESC_TIPO,
    T.FOLIO,
    T.ANO,
    T.FECHA_SOLICITUD,
    T.ESTATUS,
    CE.DESCRIPCION                                      AS DESC_ESTATUS,
    CAST('ORDINARIO' AS VARCHAR2(30))                   AS MODALIDAD,
    -- Empleado
    E.EMPLEADO                                          AS NOEMP,
    E.NOMBRE || ' ' || E.PATERNO || ' ' || E.MATERNO    AS NOMBRE,
    TRIM(E.RFC || E.HOMOCVE)                            AS RFC,
    E.FECHAALTA                                         AS FECHA_INGRESO,
    (SELECT MAX(D.DIRECCIONNOM) FROM DEPTOS D WHERE D.DEPTO = E.DEPTOCOMI) AS DIRECCION,
    DU.DESCRIPCION                                      AS DEPARTAMENTO,
    P.DESCRIPCION                                       AS PUESTO,
    NC.DESCRIPCION                                      AS CATEGORIA,
    -- Datos genéricos del trámite
    COALESCE(VM.FDESDE, PJ.FECHA_INICIO)                AS FECHA_INICIO,
    COALESCE(VM.FHASTA, PJ.FECHA_FIN)                   AS FECHA_FIN,
    VM.FECHA_REGRESO                                    AS FECHA_REGRESO,
    CASE WHEN T.TIPO_TRAMITE = 1 THEN VM.DIAS_SOLICITADOS
         WHEN T.TIPO_TRAMITE = 3 THEN FN_DIAS_HABILES_EMP(T.ID_EMPLEADO, PJ.FECHA_INICIO, PJ.FECHA_FIN)
    END                                                 AS DIAS,
    -- Vacaciones: saldo total VIGENTE del empleado al consultar (F_SALDO_VACACIONES), no el del día de la solicitud.
    CASE WHEN T.TIPO_TRAMITE = 1 THEN
        (SELECT S.B_DIAS_TOTALES_POR_DISFRUTAR FROM TABLE(F_SALDO_VACACIONES(T.ID_EMPLEADO)) S)
    END                                                 AS SALDO_TOTAL,
    COALESCE(VM.ID_EMPLEADO_CUBRE, PJ.ID_EMPLEADO_CUBRE) AS NOEMP_CUBRE,
    CASE WHEN EC.EMPLEADO IS NOT NULL
         THEN EC.NOMBRE || ' ' || EC.PATERNO || ' ' || EC.MATERNO END AS NOMBRE_CUBRE,
    -- Permisos / justificantes
    PJ.TIPO_INCIDENCIA                                  AS TIPO_INCIDENCIA,
    CAST(CASE PJ.TIPO_INCIDENCIA
         WHEN 1 THEN 'PERMISO CON GOCE DE SUELDO'
         WHEN 2 THEN 'PERMISO SIN GOCE DE SUELDO'
         WHEN 3 THEN 'JUSTIFICACION DE ENTRADA'
         WHEN 4 THEN 'JUSTIFICACION DE SALIDA'
         WHEN 5 THEN 'JUSTIFICACION DE NO CHECADA DE ENTRADA'
         WHEN 6 THEN 'JUSTIFICACION DE NO CHECADA DE SALIDA'
         WHEN 7 THEN 'JUSTIFICACION DE NO CHECADA'
    END AS VARCHAR2(60))                                AS DESC_INCIDENCIA,
    PJ.JUSTIFICACION_MOTIVO                             AS MOTIVO,
    -- Comentarios de la captura: primer movimiento de la bitácora (SOLICITUD GENERADA)
    (SELECT MIN(B.COMENTARIOS) KEEP (DENSE_RANK FIRST ORDER BY B.ID_BITACORA)
       FROM NOM_TRAMITES_BITACORA B WHERE B.ID_TRAMITE = T.ID_TRAMITE) AS OBSERVACIONES,
    -- Firmas: 1 = VALIDA_1 (jefe), 2 = VALIDA_2 (director), 3 = responsable de RH (DEPTOS_UE 24)
    T.VALIDA_1                                          AS FIRMA1_NOEMP,
    CASE WHEN F1.EMPLEADO IS NOT NULL
         THEN F1.NOMBRE || ' ' || F1.PATERNO || ' ' || F1.MATERNO END AS FIRMA1_NOMBRE,
    P1.DESCRIPCION                                      AS FIRMA1_PUESTO,
    T.VALIDA_2                                          AS FIRMA2_NOEMP,
    CASE WHEN F2.EMPLEADO IS NOT NULL
         THEN F2.NOMBRE || ' ' || F2.PATERNO || ' ' || F2.MATERNO END AS FIRMA2_NOMBRE,
    P2.DESCRIPCION                                      AS FIRMA2_PUESTO,
    RH.EMP_RESPON                                       AS FIRMA3_NOEMP,
    CASE WHEN F3.EMPLEADO IS NOT NULL
         THEN F3.NOMBRE || ' ' || F3.PATERNO || ' ' || F3.MATERNO END AS FIRMA3_NOMBRE,
    NVL(P3.DESCRIPCION, RH.DESCRIPCION)                 AS FIRMA3_PUESTO
FROM NOM_TRAMITES T
JOIN EMPLEADOS E                ON E.EMPLEADO = T.ID_EMPLEADO
LEFT JOIN RH_CAT_TIPOS_TRAMITE TT ON TT.ID_TIPO_TRAMITE = T.TIPO_TRAMITE
LEFT JOIN RH_CAT_ESTATUS CE     ON CE.ID_ESTATUS = T.ESTATUS
LEFT JOIN DEPTOS_UE DU          ON DU.ID_CEA = E.DEPTOCOMI
LEFT JOIN NOM_PUESTOS P         ON P.ID_PUE = E.ID_PUE
LEFT JOIN NOM_CAT NC            ON NC.ID_CAT = E.ID_CAT
-- Vacaciones
LEFT JOIN NOM_VACAEMP_M VM      ON VM.ID_TRAMITE = T.ID_TRAMITE AND T.TIPO_TRAMITE = 1
-- Permisos (3) y justificantes (4)
LEFT JOIN NOM_PERMISOS_JUSTIFICANTES PJ ON PJ.ID_TRAMITE = T.ID_TRAMITE AND T.TIPO_TRAMITE IN (3, 4)
LEFT JOIN EMPLEADOS EC          ON EC.EMPLEADO = COALESCE(VM.ID_EMPLEADO_CUBRE, PJ.ID_EMPLEADO_CUBRE)
-- Firmas
LEFT JOIN EMPLEADOS F1          ON F1.EMPLEADO = T.VALIDA_1
LEFT JOIN NOM_PUESTOS P1        ON P1.ID_PUE = F1.ID_PUE
LEFT JOIN EMPLEADOS F2          ON F2.EMPLEADO = T.VALIDA_2
LEFT JOIN NOM_PUESTOS P2        ON P2.ID_PUE = F2.ID_PUE
-- Firma 3 = responsable de Recursos Humanos (DEPTOS_UE.ID_CEA = 24 → EMP_RESPON)
LEFT JOIN DEPTOS_UE RH          ON RH.ID_CEA = 24
LEFT JOIN EMPLEADOS F3          ON F3.EMPLEADO = RH.EMP_RESPON
LEFT JOIN NOM_PUESTOS P3        ON P3.ID_PUE = F3.ID_PUE;

-- Desglose del formato, columnas genéricas. Vacaciones (NOM_VACAEMP_D):
--   CONCEPTO_1 = año, CONCEPTO_2 = periodo, VALOR_1 = saldo anterior, VALOR_2 = solicitados, VALOR_3 = restantes.
CREATE OR REPLACE VIEW VS_TRAMITES_FORMATO_DET AS
SELECT
    M.ID_TRAMITE,
    ROW_NUMBER() OVER (PARTITION BY M.ID_TRAMITE ORDER BY D.PERVAC) AS ORDEN,
    TO_CHAR(D.ANO)                                                  AS CONCEPTO_1,
    CASE SUBSTR(D.PERVAC, INSTR(D.PERVAC, '-') + 1)
         WHEN '1' THEN '1ER PERIODO VACACIONAL'
         WHEN '2' THEN '2DO PERIODO VACACIONAL'
         ELSE 'PERIODO ' || SUBSTR(D.PERVAC, INSTR(D.PERVAC, '-') + 1) END AS CONCEPTO_2,
    D.DIASVAC + D.DIASXDISF                                         AS VALOR_1,
    D.DIASVAC                                                       AS VALOR_2,
    D.DIASXDISF                                                     AS VALOR_3
FROM NOM_VACAEMP_M M
JOIN NOM_VACAEMP_D D ON D.ID_VACAEMP = M.ID_VACAEMP;
