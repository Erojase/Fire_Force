CREATE DATABASE IF NOT EXISTS incendios;
USE incendios;

-- DROPS
DROP TABLE IF EXISTS partidas;
DROP TABLE IF EXISTS usuarios;

-- CREATES

CREATE TABLE if not exists usuarios(
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(25) NOT NULL unique,
    passwd VARCHAR(50) NOT NULL,
    mail VARCHAR(200) NOT NULL
);

CREATE TABLE if not exists partidas(
    id INT PRIMARY KEY AUTO_INCREMENT,
    estado TEXT,
    id_usuario INT NOT NULL,
    FOREIGN KEY (id_usuario) REFERENCES usuarios(id)
);

-- PROCEDURES

DROP PROCEDURE IF EXISTS register;

delimiter $$
CREATE PROCEDURE register(IN _username varchar(25),
                        IN _passwd VARCHAR(50),
                        IN _mail VARCHAR(200))
pa:
begin

DECLARE _result varchar(50);
SET _result =  "";

-- caso -1 nombre de usuario vacio
    if _username is null or _username LIKE "" THEN
        select -1 as "resultado";
        leave pa;
    end if;

-- caso -2 contraseña vacia
    if _passwd is null or _passwd LIKE "" THEN
        select -2 as "resultado";
        leave pa;
    end if;

-- caso -3 contraseña vacia
    if _mail is null or _mail LIKE "" THEN
        select -2 as "resultado";
        leave pa;
    end if;

-- caso 0 todo correcto

   INSERT INTO usuarios (nombre, passwd, mail) VALUES(
        _username, _passwd, _mail
   );
   Select 0 as "resultado";

    
end$$
delimiter ;


DROP PROCEDURE IF EXISTS login;

delimiter $$
CREATE PROCEDURE login(IN _username varchar(25),
                        IN _passwd VARCHAR(50))
pa:
begin

DECLARE _result varchar(50);
SET _result =  "";

-- caso -2 nombre de usuario vacio
    if _username is null or _username LIKE "" THEN
        select -2 as "resultado";
        leave pa;
    end if;

-- caso -3 contraseña vacia
    if _passwd is null or _passwd LIKE "" THEN
        select -3 as "resultado";
        leave pa;
    end if;

-- caso -1 usuario o contraseña no coincidentes

	select COUNT(nombre) from usuarios 
    where nombre like _username and
    passwd like _passwd
    into _result;

-- caso 0 todo correcto

   if _result > 0 then
        select 0 as "resultado";
        leave pa;
	else
		select -1 as "resultado";
        leave pa;
   end if;

    
end$$
delimiter ;