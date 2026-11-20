-- NOM_VACAEMP_M: permite renglones que NO vienen de un trámite (saldo inicial / ajuste capturado por RH).
--   ORIGEN_REGISTRO  'T' = trámite del portal (ID_TRAMITE y FOLIO obligatorios)
--                    'R' = captura directa de RH (sin ID_TRAMITE ni FOLIO)
--   ID_EMPLEADO_CAPTURA / FECHA_CAPTURA: quién y cuándo capturó (los 'R' no tienen bitácora de trámite).
-- F_SALDO_VACACIONES no cambia: toma el renglón M/D con FHASTA más reciente del empleado, sea T o R.
-- Correr ANTES de publicar el backend nuevo (EF lee ORIGEN_REGISTRO).

ALTER TABLE NOM_VACAEMP_M MODIFY (ID_TRAMITE NULL, FOLIO NULL);

ALTER TABLE NOM_VACAEMP_M ADD (
    ORIGEN_REGISTRO     CHAR(1)   DEFAULT 'T' NOT NULL,
    ID_EMPLEADO_CAPTURA NUMBER(4),
    FECHA_CAPTURA       DATE      DEFAULT SYSDATE
);

ALTER TABLE NOM_VACAEMP_M ADD CONSTRAINT CK_NOM_VACAEMP_M_ORIGEN
    CHECK ((ORIGEN_REGISTRO = 'T' AND ID_TRAMITE IS NOT NULL AND FOLIO IS NOT NULL)
        OR (ORIGEN_REGISTRO = 'R' AND ID_TRAMITE IS NULL));

ALTER TABLE NOM_VACAEMP_M ADD CONSTRAINT FK_NOM_VACAEMP_M_CAPTURA
    FOREIGN KEY (ID_EMPLEADO_CAPTURA) REFERENCES EMPLEADOS (EMPLEADO);

COMMENT ON COLUMN NOM_VACAEMP_M.ORIGEN_REGISTRO IS 'T = tramite del portal, R = captura directa de RH (saldo inicial/ajuste)';
COMMENT ON COLUMN NOM_VACAEMP_M.ID_EMPLEADO_CAPTURA IS 'No. de empleado que capturo el renglon';
