-- Motor: Mirosoft SQL Server 2012

CREATE DATABASE TPFinal

USE TPFinal

CREATE TABLE Usuarios
(
	usuario VARCHAR(45) NOT NULL,
	contrasenia VARCHAR(45) NOT NULL,
	nombre VARCHAR(45),
	apellido VARCHAR(45),
	PRIMARY KEY (usuario)
);

CREATE TABLE Aerolíneas(
	cod_aerolinea INT NOT NULL IDENTITY(1,1),
	Descripción VARCHAR(45),
	Teléfono INT,
	url VARCHAR(45),
	Consideracion VARCHAR(45),
	PRIMARY KEY (cod_aerolinea)
);
CREATE TABLE Conexiones(
	cod_aerolinea INT NOT NULL,
	cod_conexion INT NOT NULL IDENTITY(1,1),
	ciudad_origen VARCHAR(45),
	ciudad_destino VARCHAR(45),
	pais_origen VARCHAR(45),
	pais_destino VARCHAR(45),
	aeropuerto_origen VARCHAR(45),
	aeropuerto_destino VARCHAR(45),	
	PRIMARY KEY (cod_conexion)
);

CREATE TABLE Vuelos(
	cod_aerolinea INT,
	cod_conexion INT,
	Fecha DATE,
	Precio MONEY, 
	Moneda VARCHAR(25) DEFAULT 'pesos',
	Puntaje DECIMAL(4,2),
	PRIMARY KEY (cod_aerolinea,cod_conexion,Fecha)
	);
	
	
	
	-- Realizo claves foraneas
	
ALTER TABLE Conexiones
	ADD FOREIGN KEY (cod_aerolinea)	REFERENCES Aerolíneas(cod_aerolinea);

ALTER TABLE Vuelos
	ADD FOREIGN KEY (cod_aerolinea) REFERENCES Aerolíneas(cod_aerolinea); 

ALTER TABLE Vuelos
	ADD FOREIGN KEY (cod_conexion) REFERENCES Conexiones(cod_conexion);
	
	
	-- Inserto usuarios en la tabla usuarios
	INSERT INTO Usuarios VALUES ('ADMIN','ADMIN','Administrador','Administrador');
	INSERT INTO Aerolíneas (Descripción, Teléfono, url, Consideracion) VALUES ('Lufthansa',44441111,null,null);
	INSERT INTO Conexiones VALUES (1,'Buenos Aires','Frankfurt','Argentina','Alemania','Ezeiza','Flughafen Frankfurt');
	INSERT INTO Vuelos (cod_aerolinea,cod_conexion,Fecha,Precio,Moneda,Puntaje) VALUES(1,4,'2014-02-10',5000,DEFAULT, 5);
	
	
	-- Muestro los usuarios
	SELECT * FROM Usuarios;
	SELECT * FROM Aerolíneas;
	SELECT * FROM Conexiones;
	SELECT * FROM Vuelos;
	
