select user from dual;
select * from all_tables;

create table usuario(
idusuario int not null,
nombre char(10),
fecha_nacimiento date,
telefono char(10),
salario number(6,2) --el 6 es la catidad de cifras y el 2 la cantidad de decimales q va a llevar
)
drop table usuario
select * from usuario;

--descripcion de la tabla q consulto
describe usuario

--insert es igual a sql server
-- pero si hay un tipo date se ingresa de la siguiente manera: to_date('10/03/78','dd/mm/yyyy')
-- delete y update es igual a sql server

