USE 5to_Toppardex

select 'Creando Procedures' as 'Estado';


DELIMITER $$
DROP PROCEDURE IF EXISTS `SumaStock` $$ 
CREATE PROCEDURE SumaStock (xidProducto smallint unsigned,xnombre varchar(45),
xtalla int unsigned, xprecio decimal,xstock smallint unsigned) 
BEGIN
	insert into Producto (idProducto,nombre,precio,stock)
	values (xidProducto,xnombre,xtalla,xprecio,xstock);
END $$
DELIMITER ; 

DELIMITER $$
DROP PROCEDURE IF EXISTS AltaCliente $$
CREATE PROCEDURE AltaCliente(
    IN xnombre VARCHAR(50),
    IN xapellido VARCHAR(50),
    IN xpais VARCHAR(50),
    IN xfecha DATE
)
BEGIN
    INSERT INTO Cliente (Nombre, Apellido, Pais, FechaDeNacimiento)
    VALUES (xnombre, xapellido, xpais, xfecha);
END $$
DELIMITER ;

DELIMITER $$
DROP PROCEDURE IF EXISTS AltaMarca $$
CREATE PROCEDURE AltaMarca(
    IN xnombre VARCHAR(50)
)
BEGIN
    INSERT INTO Marca (Nombre)
    VALUES (xnombre);
END $$
DELIMITER ;

DELIMITER $$
DROP PROCEDURE IF EXISTS AltaProducto $$
CREATE PROCEDURE AltaProducto(
    IN xnombre VARCHAR(50),
    IN xprecio DECIMAL(10,2),
    IN xstock SMALLINT UNSIGNED,
    IN xidmarca SMALLINT UNSIGNED
)
BEGIN
    INSERT INTO Producto (Nombre, Precio, Stock, IdMarca)
    VALUES (xnombre, xprecio, xstock, xidmarca);
END $$
DELIMITER ;

DELIMITER $$
DROP PROCEDURE IF EXISTS AltaPedido $$
CREATE PROCEDURE AltaPedido(
    IN xidcliente SMALLINT UNSIGNED,
    IN xfechaventa DATETIME

)
BEGIN
    INSERT INTO Pedido (idCliente, fechaVenta, total)
    VALUES (xidcliente, xfechaventa, 0.00);

    SELECT LAST_INSERT_ID() AS idPedido;
END $$
DELIMITER ;

DELIMITER $$
DROP PROCEDURE IF EXISTS AltaCliente $$
CREATE PROCEDURE AltaCliente(   
    IN xnombre VARCHAR(45),
    IN xapellido VARCHAR(45),
    IN xpais VARCHAR(45),
    IN xfecha DATE
)
BEGIN
    INSERT INTO Cliente (nombre, apellido, pais, fechaDeNacimiento)
    VALUES (xnombre, xapellido, xpais, xfecha);
END $$


