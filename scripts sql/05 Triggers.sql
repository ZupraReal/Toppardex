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

DELIMITER $$
   DROP TRIGGER IF EXISTS ActualizarTotalPedido_AfterInsert $$
   CREATE TRIGGER ActualizarTotalPedido_AfterInsert
   AFTER INSERT ON ProductoPedidos
   FOR EACH ROW
   BEGIN
      -- Declara una variable para guardar el nuevo total
      DECLARE totalNuevo DECIMAL(10, 2);

      -- Calcula la suma total de los productos para el pedido afectado
      SELECT SUM(precio * cantidad) INTO totalNuevo
      FROM ProductoPedidos
      WHERE idPedido = NEW.idPedido;

      -- Actualiza la tabla Pedido con el nuevo total calculado
      UPDATE Pedido
      SET total = totalNuevo
      WHERE idPedido = NEW.idPedido;
END$$
DELIMITER ;

DELIMITER $$
DROP TRIGGER IF EXISTS ActualizarTotalPedido_AfterUpdate $$
CREATE TRIGGER ActualizarTotalPedido_AfterUpdate
AFTER UPDATE ON ProductoPedidos
FOR EACH ROW
BEGIN
    UPDATE Pedido p
    JOIN (
        SELECT idPedido, SUM(precio * cantidad) AS totalNuevo
        FROM ProductoPedidos
        WHERE idPedido = NEW.idPedido
        GROUP BY idPedido
    ) t ON t.idPedido = p.idPedido
    SET p.total = t.totalNuevo;
END $$
DELIMITER ;

DELIMITER $$
DROP TRIGGER IF EXISTS ActualizarTotalPedido_AfterDelete $$
CREATE TRIGGER ActualizarTotalPedido_AfterDelete
AFTER DELETE ON ProductoPedidos
FOR EACH ROW
BEGIN
    UPDATE Pedido p
    JOIN (
        SELECT idPedido, SUM(precio * cantidad) AS totalNuevo
        FROM ProductoPedidos
        WHERE idPedido = OLD.idPedido
        GROUP BY idPedido
    ) t ON t.idPedido = p.idPedido
    SET p.total = t.totalNuevo;
END $$
DELIMITER ;