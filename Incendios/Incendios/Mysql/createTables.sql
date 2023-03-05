CREATE DATABASE IF NOT EXISTS incendios;
USE incendios;

-- DROPS
DROP TABLE IF EXISTS usuarios;
DROP TABLE IF EXISTS partidas;

-- CREATES

CREATE TABLE usuarios(
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(25) NOT NULL,
    passwd VARCHAR(50) NOT NULL,
    mail VARCHAR(200) NOT NULL
);

CREATE TABLE partidas(
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(25) NOT NULL,
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

SET _result = "";

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

   INSERT INTO usuarios (  ) VALUES(
        your_values
   );

    
end$$




delimiter ;