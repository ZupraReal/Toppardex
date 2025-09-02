```markdown


erDiagram

Clientes{
   SMALLINTUNSIGNED idCliente PK
   varchar(45) nombre
   varchar(45) apellido
   varchar(45) pais
   date fechaDeNacimiento
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


Pedidos{
   SMALLINTUNSIGNED idPedido PK
   SMALLINTUNSIGNED idCliente FK
   DATETIME fechaventa
}


Talle{
   SMALLINTUNSIGNED idProducto PK, FK
   TINYINTUNSIGNED numerotalle PK
   SMALLINTUNSIGNED cantidadtalle
  
}


ProductoPedidos{
   SMALLINTUNSIGNED idPedido PK, FK
   SMALLINTUNSIGNED idProducto PK, FK
   DECIMAL(10_2) precio
   SMALLINTUNSIGNED cantidad
   TINYINTUNSIGNED numerotalle PK, FK
}




Clientes||--|{Pedidos : ""
Pedidos||--|{ProductoPedidos: ""
ProductoPedidos}|--||Producto :""
Talle ||--|{Producto : ""
Marca ||--|{Producto: ""

```