select 'Creando Triggers' as 'Estado';
USE 5to_Toppardex;

DELIMITER $$
DROP TRIGGER IF EXISTS BefInsClientes$$ 
CREATE TRIGGER BeforeInsertCliente BEFORE INSERT ON Clientes
FOR EACH ROW
BEGIN
   IF (NEW.fechaDeNacimiento <=17) THEN
   SIGNAL SQLSTATE '45000'
   SET MESSAGE_TEXT ='no sos mayor de edad';
   END IF;
END $$

DELIMITER $$ 
DROP TRIGGER IF EXISTS  BefInsStock$$
CREATE TRIGGER BefInsStock BEFORE INSERT ON ProductoPedidos
FOR EACH ROW 
BEGIN 
DECLARE stock_disponible INT;
SELECT stock INTO stock_disponible 
FROM Productos WHERE idProducto = NEW.idProducto; 
IF (stock_disponible < NEW.cantidad)  THEN 
SIGNAL SQLSTATE '45000' 
SET MESSAGE_TEXT = 'Stock insuficiente para realizar el pedido'; 
END IF; 
END $$

DELIMITER $$
CREATE TRIGGER reducir_stock AFTER INSERT ON ProductoPedidos 
FOR EACH ROW 
BEGIN UPDATE Productos 
SET stock = stock - NEW.cantidad 
WHERE idProducto = NEW.idProducto; 
END $$