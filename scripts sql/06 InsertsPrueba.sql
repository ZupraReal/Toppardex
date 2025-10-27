-- =======================================================
-- DATOS INICIALES
-- =======================================================

USE 5to_Toppardex;
SELECT 'Insertando datos iniciales' AS 'Estado';

/*MArcas*/
INSERT INTO Marca (nombre) VALUES
('Ardidas'),
('Converse'),
('Espuma'),
('Naik'),
('Persona'),
('CD'),
('Fucci'),
('Laposta'),
('Incel');

/*Usuarios*/
CALL AltaCliente('Judas', 'Pin', 'Argentina', '2007-03-15');
CALL AltaCliente('Facundo', 'Perez', 'Argentina', '2006-09-02');
CALL AltaCliente('Maria', 'Lopez', 'Espana', '1985-11-20');
CALL AltaCliente('Carlos', 'Gomez', 'Mexico', '1992-05-10');
CALL AltaCliente('Ana', 'Rodriguez', 'Colombia', '1978-01-25');
CALL AltaCliente('Javier', 'Sanchez', 'Argentina', '2000-08-12');
CALL AltaCliente('Sofia', 'Martinez', 'Chile', '1995-04-03');
CALL AltaCliente('Miguel', 'Fernandez', 'Peru', '1980-07-18');
CALL AltaCliente('Lucia', 'Diaz', 'Uruguay', '2003-12-01');
CALL AltaCliente('Pedro', 'Alvarez', 'Espana', '1965-06-30');
CALL AltaCliente('Elena', 'Vargas', 'Mexico', '1998-02-14');
CALL AltaCliente('Ricardo', 'Flores', 'Ecuador', '1970-10-05');
CALL AltaCliente('Paula', 'Torres', 'Argentina', '2001-03-22');
CALL AltaCliente('Andres', 'Ruiz', 'Colombia', '1988-09-09');
CALL AltaCliente('Valentina', 'Mendez', 'Peru', '2005-01-28');

-- Productos de Ardidas
CALL AltaProducto('Ultraboost 22', 180.00, 45, 1);
CALL AltaProducto('Gazelle Clasica', 85.50, 60, 1);

-- Productos de Converse
CALL AltaProducto('Chuck Taylor All Star', 75.00, 100, 2);
CALL AltaProducto('Run Star Hike', 110.00, 30, 2);

-- Productos de Espuma
CALL AltaProducto('RS-X Tracks', 105.00, 55, 3);
CALL AltaProducto('Cali Dream', 90.00, 40, 3);

-- Productos de Naik
CALL AltaProducto('Air Max 97', 190.99, 40, 4);
CALL AltaProducto('Pegasus 40', 135.00, 75, 4);

-- Productos de CD (Chanel Dior, asumo que es una marca de lujo)
CALL AltaProducto('Low-Top B27', 750.00, 15, 6);


/*-- Crear un pedido vacío
INSERT INTO Pedido (idCliente, fechaVenta, total)
VALUES (1, NOW(), 0);

SET @pedido := LAST_INSERT_ID();

-- Insertar productos
INSERT INTO ProductoPedidos (idPedido, idProducto, precio, cantidad)
VALUES (@pedido, 1, 100, 2),
       (@pedido, 2, 200, 1);

-- Verificar el total actualizado
SELECT * FROM Pedido WHERE idPedido = @pedido;
*/

INSERT INTO Usuario (email, pass, rol)
VALUES 
('admin@empresa.com', SHA2('admin123', 256), 'Admin'),
('empleado@empresa.com', SHA2('empleado123', 256), 'Empleado');
