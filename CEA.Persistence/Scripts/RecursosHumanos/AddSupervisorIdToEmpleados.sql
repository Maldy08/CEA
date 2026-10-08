-- Adds SUPERVISOR_ID to EMPLEADOS: the employee's immediate supervisor (another EMPLEADOS.EMPLEADO).
-- Not to be confused with the existing SUPERVISOR NUMBER(1) column, which is a 0/1 flag.
-- Nullable: no backfill; it gets filled in as supervisors are assigned.
ALTER TABLE EMPLEADOS ADD (SUPERVISOR_ID NUMBER(4));

ALTER TABLE EMPLEADOS ADD CONSTRAINT FK_EMPLEADOS_SUPERVISOR
  FOREIGN KEY (SUPERVISOR_ID) REFERENCES EMPLEADOS (EMPLEADO);

COMMENT ON COLUMN EMPLEADOS.SUPERVISOR_ID IS 'Empleado (EMPLEADOS.EMPLEADO) que es supervisor inmediato';
