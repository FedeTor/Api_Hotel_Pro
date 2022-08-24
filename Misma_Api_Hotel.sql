create table usuario(
idusuario int not null,
nombre char(10),
fecha_nacimiento date,
telefono char(10),
salario number(6,2) --el 6 es la catidad de cifras y el 2 la cantidad de decimales q va a llevar
)
 create sequence secuencia_Usuario
  start with 1
  increment by 1
  maxvalue 99999
  minvalue 1;
  
  select secuencia_Usuario.nextval from dual;
  select secuencia_TokenCancelados.nextval from dual;
  
  delete from Usuarios
  drop table Usuarios
  
  insert into Usuario values(1,'Fede', to_date('10/03/78','dd/mm/yyyy'),'3424234', 232.343);
  
  select * from Usuarios
  

--Tabla Usuarios
create table Usuarios(
Id_Usuarios int primary key not null,
Nombre nvarchar2(100),
Apellido nvarchar2(100),
Email nvarchar2(100),
Contraseña nvarchar2(100),
Rol nvarchar2(20),
constraint nombreRol check(Rol='Administrador' or Rol='AtencionCliente'),
BorradoLogico number
)
  select secuencia_Usuario.nextval from dual;
select * from Usuarios
delete from Usuarios
drop table Usuarios

insert into Usuarios values (49,'Juan', 'Perez', 'juan@gmail.com', '4\m?x?`??e???t???????R?z????^%', 'Administrador',0)
insert into Usuarios values ('Pedro', 'Pedro', 'pedro@gmail.com', 'Contraseña', 'Atencion al Cliente',0)


--Tabla TokenCancelados
create table TokenCancelados(
Id_TokenCancelados int primary key not null,
Hash varchar2(1000)
)
drop table TokenCancelados
select * from TokenCancelados
insert into TokenCancelados values (1,'32434h3jhqwh343oi+¿+3i4ou3o4u3hk3')

select * from TokenCancelados where Hash = @hashS;
  
  
  