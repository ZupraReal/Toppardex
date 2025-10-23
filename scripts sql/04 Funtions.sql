USE 5to_Toppardex

DELIMITER $$
DROP FUNCTION IF EXISTS `StockDisponible` $$
CREATE FUNCTION StockDisponible(xidProducto SMALLINT UNSIGNED) 
RETURNS smallint unsigned
READS SQL DATA
BEGIN
    DECLARE stock SMALLINT UNSIGNED;
    SELECT stock INTO stock
    FROM Producto
    WHERE idProducto = xidProducto;
    RETURN stock;
END $$

DELIMITER ;

DELIMITER $$
DROP FUNCTION IF EXISTS `BuscarZapatilla` $$
CREATE FUNCTION BuscarZapatilla(xnombre VARCHAR(45))
RETURNS VARCHAR(45)
READS SQL DATA
BEGIN 
    Declare Zapatilla VARCHAR(45);
    SELECT nombre INTO Zapatilla
    FROM Producto
    WHERE nombre = xnombre;
    RETURN Zapatilla;
END $$

DELIMITER ;