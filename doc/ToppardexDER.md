```markdown


erDiagram

Clientes{
   SMALLINTUNSIGNED idCliente PK
   varchar(45) nombre
   varchar(45) apellido
   varchar(45) pais
   fechaDeNacimiento date 
}


Marca{
   SMALLINTUNSIGNED idMarca PK
   varchar(45) nombre
}


Producto{
   SMALLINTUNSIGNED idProducto PK
   varchar(45) nombre
   DECIMAL(10_2) precio
   SMALLINTUNSIGNED stock
   SMALLINTUNSIGNED idMarca FK
}


Pedido{
   SMALLINTUNSIGNED idPedido PK
   SMALLINTUNSIGNED idCliente FK
   DATETIME fechaventa
   DECIMAL(10_2) total 
}

ProductoPedidos{
   SMALLINTUNSIGNED idPedido PK, FK
   SMALLINTUNSIGNED idProducto PK, FK
   DECIMAL(10_2) precio
   SMALLINTUNSIGNED cantidad
}

Clientes||--|{Pedido : ""
Pedido||--|{ProductoPedidos: ""
ProductoPedidos}|--||Producto :""
Producto}|--||Marca :""

```