create database RepitiendoApi_Hotel

--Tabla Usuarios
create table Usuarios(
Id int primary key identity (1,1) not null,
Nombre nvarchar(20),
Apellido nvarchar(20),
Email nvarchar(50),
Contraseña nvarchar(100),
Rol nvarchar(15),
constraint nombreRol check(Rol='Administrador' or Rol='AtencionCliente'),
Borrado bit,
)
drop table Usuarios
select * from Usuarios
delete from Usuarios

INSERT INTO Usuarios VALUES('Juan', 'Perez', 'juan@gmail.com', '4\m?x?`??e???t???????R?z????^%', 'Administrador', 0)
INSERT INTO Usuarios VALUES('Juan', 'Perez', 'juan@gmail.com', 'Contraseña', 'Administrador', 0)
SELECT * FROM Usuarios WHERE Email = 'juan@gmail.com' AND Contraseña = 'Contraseña'

--Tabla EstadosCuartos
create table EstadosCuartos(
Id int primary key identity (1,1) not null,
EstadoCuarto nvarchar(50),
)
--EstadoCuarto: 1-Mantenimiento  2-Ocupado  3-Libre  4-Baja


--Tabla Cuartos
create table Cuartos(
Id int primary key identity (1,1) not null,
Id_EstCuarto int foreign key references EstadosCuartos(Id),
NumeroCuarto int,
CantidadPersonas int,
Fotos varchar(max),
Notas nvarchar(20),
Descripcion nvarchar(100),
Borrado bit,
)

select * from Cuartos
drop table Cuartos


--Tabla EstadosReservaCuartos
create table EstadosReservaCuartos(
Id int primary key identity (1,1) not null,
EstadoReservaCuarto nvarchar(50),
)
--EstadoReserva: 1-activa  2-finalizada  3-cancelada



--Tabla Clientes
create table Clientes(
Id int primary key identity (1,1) not null,
Nombre nvarchar(20),
Apellido nvarchar(20),
Dni int,
Borrado bit
)
drop table Clientes
select * from Clientes


--Tabla ReservaCuartos
create table ReservaCuartos(
Id int primary key identity (1,1) not null,
Id_CuartoFk int foreign key references Cuartos(Id),
Id_ClienteFk int foreign key references Clientes(Id),
Id_EstadoReserva int foreign key references EstadosReservaCuartos(Id),
FechaLlamada datetime,
FechaInicio datetime,
FechaFin datetime
)

select * from ReservaCuartos

--Tabla CancelarToken
create table CancelarToken(
Id int primary key identity (1,1) not null,
Hash nvarchar(max)
)

SELECT * FROM CancelarToken



