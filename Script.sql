create database CRUD_API_REST
use CRUD_API_REST
go
create table Productos(
	Id int identity(1,1) not null primary key,
	Nombre varchar(100),
	Precio decimal(12,2),
	Cantidad int,
	Descripcion varchar(100),
	FechaCreacion datetime
)
go 
create procedure InsertarProducto
	@Nombre varchar(100),
	@Precio decimal(12,2),
	@Cantidad int,
	@Descripcion varchar(100),
	@FechaCreacion datetime
	as begin
insert into Productos values 
	(@Nombre, @Precio, @Cantidad, @Descripcion, @FechaCreacion)
end
go
create procedure EliminarProducto
	@Id int
	as begin
delete from Productos where Id = @id
end
go
create procedure ObtenerProductos
	as begin
select * from Productos
end
go
create procedure ActualizarProducto
	@Id int,
	@Nombre varchar(100),
	@Precio decimal(12,2),
	@Cantidad int,
	@Descripcion varchar(100),
	@FechaCreacion datetime
	as begin
update Productos set 
	Nombre=@Nombre, Precio=@Precio, Cantidad=@Cantidad, Descripcion=@Descripcion, FechaCreacion=@FechaCreacion
	where Id=@Id
end
