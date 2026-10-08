-- Access to the "Sistema de Trámites CEA" (next-tramites-cea), same pattern as VIATICOS/VIATICOS_NIVEL, OFICIOS/OFICIOS_NIVEL.
-- DEFAULT 0 fills the existing rows with 0 (no access).
ALTER TABLE USUARIOS ADD (TRAMITES NUMBER(1) DEFAULT 0, TRAMITES_NIVEL NUMBER(1) DEFAULT 0);
