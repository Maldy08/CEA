-- Adds ESTADO column to AVANCES so each progress entry tracks its own state.
-- Backfills existing rows with the parent TEMA.ESTADO so history is consistent.
ALTER TABLE AVANCES ADD (ESTADO VARCHAR2(20) DEFAULT 'Pendiente');

UPDATE AVANCES a
SET a.ESTADO = (SELECT t.ESTADO FROM TEMAS t WHERE t.ID_TEMA = a.ID_TEMA)
WHERE a.ESTADO IS NULL;

ALTER TABLE AVANCES MODIFY (ESTADO VARCHAR2(20) DEFAULT 'Pendiente' NOT NULL);

COMMIT;
