select 'Creando Users y Grants' as 'Estado';

USE 5to_Toppardex;
DELIMITER ;
DROP USER IF EXISTS 'Gerente'@'localhost';
CREATE USER 'Gerente'@'localhost' IDENTIFIED BY '1PassG!#%';

DROP USER IF EXISTS 'Cajero'@'localhost';
CREATE USER 'Cajero'@'localhost' IDENTIFIED BY '1PassC!$%';

-- ------------------------------------------------------------------

GRANT ALL ON Toppardex.Rubro TO 'Gerente'@'localhost';
GRANT SELECT, UPDATE(precioUnitario, stock) ON Toppardex.Producto TO 'Cajero'@'localhost';

