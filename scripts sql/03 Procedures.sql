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
    IN xfechaventa DATETIME,
    OUT xidPedido INT
)
BEGIN
    INSERT INTO Pedido (idCliente, fechaVenta, total)
    VALUES (xidcliente, xfechaventa, 0.00);

    SET xidPedido = LAST_INSERT_ID();
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
DELIMITER $$

DROP PROCEDURE IF EXISTS AltaProductoPedido $$
CREATE PROCEDURE AltaProductoPedido(
    IN xidPedido INT,
    IN xidProducto INT,
    IN xcantidad INT
)
BEGIN
    DECLARE xprecio DECIMAL(10,2);

    -- Tomar el precio del producto
    SELECT precio INTO xprecio
    FROM Producto
    WHERE idProducto = xidProducto;

    -- Insertar el producto con ese precio
    INSERT INTO ProductoPedidos (idPedido, idProducto, cantidad, precio)
    VALUES (xidPedido, xidProducto, xcantidad, xprecio);
END $$

DELIMITER ;

-- ✅ Actualizar una marca existente
DROP PROCEDURE IF EXISTS ActualizarMarca;
DELIMITER //
CREATE PROCEDURE ActualizarMarca(IN xidMarca SMALLINT, IN xnombre VARCHAR(50))
BEGIN
    UPDATE Marca SET nombre = xnombre WHERE idMarca = xidMarca;
END //
DELIMITER ;

-- ✅ Eliminar una marca
DROP PROCEDURE IF EXISTS EliminarMarca;
DELIMITER //
CREATE PROCEDURE EliminarMarca(IN xidMarca SMALLINT)
BEGIN
    DECLARE cantProductos INT;
    SELECT COUNT(*) INTO cantProductos FROM Producto WHERE idMarca = xidMarca;

    IF cantProductos > 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'No se puede eliminar la marca porque tiene productos asociados.';
    ELSE
        DELETE FROM Marca WHERE idMarca = xidMarca;
    END IF;
END //
DELIMITER ;

-- ✅ Actualizar un producto
DROP PROCEDURE IF EXISTS ActualizarProducto;
DELIMITER $$
CREATE PROCEDURE ActualizarProducto(
    IN xidProducto SMALLINT,
    IN xnombre VARCHAR(45),
    IN xprecio DECIMAL(10,2),
    IN xstock SMALLINT,
    IN xidMarca SMALLINT
)
BEGIN
    UPDATE Producto
    SET nombre = xnombre,
        precio = xprecio,
        stock = xstock,
        idMarca = xidMarca
    WHERE idProducto = xidProducto;
END $$
DELIMITER ;

-- ✅ Eliminar un producto
DROP PROCEDURE IF EXISTS EliminarProducto;
DELIMITER //
CREATE PROCEDURE EliminarProducto(
    IN xidProducto SMALLINT
)
BEGIN
    DELETE FROM Producto WHERE idProducto = xidProducto;
END //
DELIMITER ;


DROP PROCEDURE IF EXISTS EliminarCliente;
DELIMITER $$
CREATE PROCEDURE EliminarCliente(
    IN xidCliente SMALLINT
)
BEGIN 
    DELETE FROM Cliente WHERE idCliente = xidCliente;
END $$

DELIMITER ;
DROP PROCEDURE IF EXISTS ActualizarCliente;
DELIMITER $$
CREATE PROCEDURE ActualizarCliente(
    IN xidCliente SMALLINT UNSIGNED,
    IN xnombre VARCHAR(45),
    IN xapellido VARCHAR(45),
    IN xpais VARCHAR(45),
    IN xFechaDeNacimiento DATE
)
BEGIN
    UPDATE Cliente
    SET nombre = xnombre,
        apellido = xapellido,
        pais = xpais,
        fechaDeNacimiento = xFechaDeNacimiento
    WHERE idCliente = xidCliente;
END $$
DELIMITER ;

