-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema 5to_Toppardex
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `5to_Toppardex` ;

-- -----------------------------------------------------
-- Schema 5to_Toppardex
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `5to_Toppardex` ;
USE `5to_Toppardex` ;


CREATE TABLE Cliente (
	idCliente smallint unsigned not null AUTO_INCREMENT,
	nombre varchar(45) not null,
	apellido varchar(45) not null,
    pais varchar(45) not null,
	fechaDeNacimiento date not null,
	primary key (idCliente)
);

CREATE TABLE Marca (
	idMarca smallint unsigned AUTO_INCREMENT not null,
	nombre varchar(45) not null,
	primary key (idMarca)
	);

CREATE TABLE Producto (
	idProducto smallint unsigned not null AUTO_INCREMENT,
	nombre varchar(45) not null,
	precio decimal (10, 2) not null,
	stock smallint unsigned not null,
	idMarca smallint unsigned not null,
	primary key (idProducto),
	foreign key (idMarca) references Marca(idMarca) 
);

CREATE TABLE Pedido (
    idPedido smallint unsigned not null AUTO_INCREMENT,
	idCliente smallint unsigned not null,
	fechaVenta datetime not null,
	primary key (idPedido),
	foreign key (idCliente) references Cliente(idCliente)
);


CREATE TABLE ProductoPedidos (
    idPedido smallint unsigned not null,
	idProducto smallint unsigned not null,
	precio decimal (10, 2) not null,
	cantidad smallint unsigned not null,
	numerotalle tinyint unsigned not null,
	primary key (idPedido, idProducto, numerotalle),
	foreign key (idPedido) references Pedido(idPedido)
);
