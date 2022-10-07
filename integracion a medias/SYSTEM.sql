-- creacion del usuario
create user miusuario identified by laclave
default tablespace system 
temporary tablespace temp
quota unlimited on system;

-- PRIVILEGIOS DE USUARIO
grant create session to miusuario;
grant create table to miusuario;
grant create view to miusuario;
grant create procedure to miusuario;
grant create sequence to miusuario;

-- PRIVILEGIOS ADMINISTRAR TABLAS 
grant all on nombretabla to miusuario;

-- revocar privilegios de administracion de tablas 
revoke all on nombretable from miusuario;

--eliminar usuario
drop user miusuario cascade

