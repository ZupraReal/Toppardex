select 'Creando Funciones' as 'Estado';


USE 5to_Toppardex;
DELIMITER $$
CREATE FUNCTION StockDisponible(idProducto SMALLINT UNSIGNED) 
RETURNS smallint unsigned
READS SQL DATA
BEGIN
    DECLARE stock SMALLINT UNSIGNED;
    SELECT stock INTO stock
    FROM Productos
    WHERE idProducto = idProducto;
    RETURN stock;
END $$

DELIMITER $$
CREATE FUNCTION BuscarZapatilla(unnombre VARCHAR(45))
RETURNS VARCHAR(45)
READS SQL DATA
BEGIN 
    Declare Zapatilla VARCHAR(45);
    SELECT nombre INTO Zapatilla
    FROM Producto 
    WHERE nombre = unnombre;
    RETURN Zapatilla;
END $$
