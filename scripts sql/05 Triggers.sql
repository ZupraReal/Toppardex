USE 5to_Toppardex;
select 'Creando Triggers' as 'Estado';


DELIMITER $$
DROP TRIGGER IF EXISTS BeforeInsertCliente$$ 
CREATE TRIGGER BeforeInsertCliente BEFORE INSERT ON Cliente
FOR EACH ROW
BEGIN
   IF (NEW.fechaDeNacimiento > DATE_SUB(CURDATE(), INTERVAL 18 YEAR)) THEN
   SIGNAL SQLSTATE '45000'
   SET MESSAGE_TEXT ='No sos mayor de edad';
   END IF;
END $$

DELIMITER ;

DELIMITER $$
DROP TRIGGER IF EXISTS  BefInsStock$$
CREATE TRIGGER BefInsStock BEFORE INSERT ON ProductoPedidos
FOR EACH ROW 
BEGIN 
   DECLARE stock_disponible INT;
   SELECT stock INTO stock_disponible 
   FROM Producto WHERE idProducto = NEW.idProducto; 
   IF (stock_disponible < NEW.cantidad)  THEN 
      SIGNAL SQLSTATE '45000' 
      SET MESSAGE_TEXT = 'Stock insuficiente para realizar el pedido'; 
   END IF; 
END $$
DELIMITER ;

   DELIMITER $$
   DROP TRIGGER IF EXISTS  reducir_stock$$
   CREATE TRIGGER reducir_stock AFTER INSERT ON ProductoPedidos 
   FOR EACH ROW 
   BEGIN UPDATE Producto
      SET stock = stock - NEW.cantidad 
      WHERE idProducto = NEW.idProducto; 
END $$

DELIMITER ;