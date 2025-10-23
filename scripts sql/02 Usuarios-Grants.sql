USE 5to_Toppardex;

CREATE USER IF NOT EXISTS 'Gerente'@'localhost' IDENTIFIED BY '1PassG!#%';
CREATE USER IF NOT EXISTS 'Cajero'@'localhost' IDENTIFIED BY '1PassC!$%';

GRANT ALL ON 5to_Toppardex.* TO 'Gerente'@'localhost';
GRANT SELECT, UPDATE(precio, stock) ON 5to_Toppardex.Productos TO 'Cajero'@'localhost';
