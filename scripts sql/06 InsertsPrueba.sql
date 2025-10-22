-- =======================================================
-- DATOS INICIALES
-- =======================================================

USE 5to_Toppardex
SELECT 'Insertando datos iniciales' AS 'Estado';

INSERT INTO Marca (nombre) VALUES
('Ardidas'),
('Converse'),
('Espuma'),
('Naik'),
('Persona'),
('CD'),
('Fucci'),
('Laposta'),
('Incel');

CALL AltaCliente('Judas', 'Pin', 'Argentina', '2007-03-15');
CALL AltaCliente( 'Facundo', 'Perez', 'Argentina', '2006-09-02');

