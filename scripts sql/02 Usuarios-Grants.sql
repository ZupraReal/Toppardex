USE 5to_Toppardex;

CREATE USER IF NOT EXISTS 'Admin'@'localhost' IDENTIFIED BY '1PassG!#%';
CREATE USER IF NOT EXISTS 'Empleado'@'localhost' IDENTIFIED BY '1PassC!$%';

GRANT ALL ON 5to_Toppardex.* TO 'Gerente'@'localhost';
