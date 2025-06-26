DROP DATABASE IF EXISTS 5to_Toppardex;

CREATE DATABASE 5to_Toppardex;
USE 5to_Toppardex;
select 'Creando BD' as 'Estado';

CREATE TABLE Clientes (
	idCliente smallint unsigned not null,
	nombre varchar(45) not null,
	apellido varchar(45) not null,
    pais varchar(45) not null,
	fechadenacimiento date not null,
	primary key (idCliente)
);

CREATE TABLE Marca (
	idMarca smallint unsigned AUTO_INCREMENT not null,
	nombre varchar(45) not null,
	primary key (idMarca)
  );
  
CREATE TABLE Productos (
	idProducto smallint unsigned not null,
	nombre varchar(45) not null,
	precio decimal (10, 2) not null,
	stock smallint unsigned not null,
	idMarca smallint unsigned not null,
	primary key (idProducto),
	foreign key (idMarca) references Marca(idMarca) 
);

CREATE TABLE Pedidos (
    idPedido smallint unsigned not null,
	idCliente smallint unsigned not null,
	fechaVenta datetime not null,
	primary key (idPedido),
	foreign key (idCliente) references Clientes(idCliente)
);
CREATE TABLE Talle(
	idProducto smallint unsigned not null,
	numerotalle tinyint unsigned not null,
	cantidadtalle smallint unsigned not null, 
	primary key (idProducto, numerotalle),
	foreign key (idProducto) references Productos(idProducto)
); 

CREATE TABLE ProductoPedidos (
    idPedido smallint unsigned not null,
	idProducto smallint unsigned not null,
	precio decimal (10, 2) not null,
	cantidad smallint unsigned not null,
	numerotalle tinyint unsigned not null,
	primary key (idPedido, idProducto, numerotalle),
	foreign key (idPedido) references Pedidos(idPedido),
	foreign key (idProducto, numerotalle) references Talle(idProducto, numerotalle)
   );
