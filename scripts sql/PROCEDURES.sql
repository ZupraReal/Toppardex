select 'Creando Procedures' as 'Estado';

USE 5to_Toppardex;
DELIMITER $$
DROP PROCEDURE IF EXISTS `SumaStock` $$ 
CREATE PROCEDURE SumaStock (xidProducto smallint unsigned,xnombre varchar(45),
xtalla int unsigned, xprecio decimal,xstock smallint unsigned) 
BEGIN
	insert into Productos (idProducto,nombre,talla,precio,stock)
	values (xidProducto,xnombre,xtalla,xprecio,xstock);
END $$


DELIMITER $$
DROP PROCEDURE IF EXISTS `AgrProducto` $$ 
CREATE PROCEDURE AgrProducto(xidProducto smallint unsigned, xnombre varchar(45), xprecio decimal(10,0), xstock smallint unsigned)
BEGIN
	insert into Productos (idProducto, nombre, precio, stock)
    Values (xidProducto, xnombre, xprecio, xstock);
END$$
