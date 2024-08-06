/* 
=========================================================
   EJERCICIO 1: 
    Creación de índices que considere puedan ser útiles para optimizar las consultas (según criterio
	establecido en el curso).
========================================================= 
*/
--Establecimientos. no requiere indice pues no tiene FK

--Licencias
create index IDX_Licencias_estNumero on Licencias(estNumero);

--TipoViolacion. no requiere indice pues no tiene FK

--Inspecciones. 
create index IDX_Inspecciones_estNumero on Inspecciones(estNumero);
create index IDX_Inspecciones_violCodigo on Inspecciones(violCodigo);




/* 
=========================================================
   EJERCICIO 2: 
    Ingreso de un juego completo de datos de prueba (será más valorada la calidad de los datos que la
	cantidad). 
========================================================= 
*/
--21 ESTABLECIMIENTOS
INSERT INTO Establecimientos (estNombre, estDireccion, estTelefono, estLatitud, estLongitud)
VALUES
    ('Restaurante El Sol', 'Calle Mayor 123', '+34 123 456 789', 40.4168, -3.7038),
    ('Pizzería La Luna', 'Avenida Libertad 45', '+34 987 654 321', 41.3851, 2.1734),
    ('Cafetería La Esquina', 'Plaza España 7', '+34 555 555 555', 37.3894, -5.9822),
    ('Bar El Patio', 'Calle Olivo 12', '+34 666 777 888', 28.4595, -16.2618),
    ('Sushi Fusion', 'Calle Sushi 3', '+34 111 222 333', 25.7617, -80.1918),
    ('Taco Time', 'Avenida de los Tacos 22', '+34 999 888 777', 19.4326, -99.1332),
    ('Burger Palace', 'Calle Burger 8', '+34 333 444 555', 51.5074, -0.1278),
    ('Café Central', 'Plaza Mayor 21', '+34 777 888 999', 48.8566, 2.3522),
    ('Pub Rock&Roll', 'Calle Rock 77', '+34 222 333 444', 40.7128, -74.0060),
    ('Fried Chicken House', 'Avenida Fried 55', '+34 444 555 666', 34.0522, -118.2437),
    ('Vegetarian Garden', 'Calle Veggie 14', '+34 777 999 111', 51.5074, -0.1278),
    ('Seafood Delight', 'Paseo del Mar 9', '+34 888 999 222', 25.7617, -80.1918),
    ('Steakhouse Supreme', 'Calle Steak 25', '+34 333 555 777', 40.7128, -74.0060),
    ('Noodle House', 'Avenida Noodle 17', '+34 666 333 111', 34.0522, -118.2437),
    ('Smoothie Haven', 'Calle Smoothie 31', '+34 999 888 777', 37.7749, -122.4194),
    ('Gourmet Bistro', 'Plaza Gourmet 4', '+34 111 222 333', 51.5074, -0.1278),
    ('Bagel Cafe', 'Calle Bagel 63', '+34 555 444 333', 40.7128, -74.0060),
    ('Thai Palace', 'Avenida Thai 11', '+34 888 777 666', 34.0522, -118.2437),
    ('Creperie Charmante', 'Calle Crepe 28', '+34 333 222 111', 48.8566, 2.3522),
    ('Taco Truck', 'Avenida Taco 6', '+34 222 333 444', 25.7617, -80.1918),
	('Restaurante El Maracanazo', 'Sindrome 123', '+34 123 123 789', 40.7000, -3.6000);


--20 LICENIAS
INSERT INTO Licencias (estNumero, licFchEmision, licFchVto, licStatus)
VALUES
	(2, '2020-01-15', '2021-01-15', 'REV'),
	(1, '2020-01-01', '2021-01-01', 'REV'),
	(1, '2022-01-01', '2025-01-01', 'APR'),
    (2, '2022-02-01', '2025-02-01', 'APR'),
    (3, '2010-03-01', '2021-06-15', 'APR'),
    (4, '2022-04-01', '2023-04-01', 'REV'),
    (5, '2022-05-01', '2023-05-01', 'APR'),
    (6, '2022-06-01', '2023-06-01', 'APR'),
    (7, '2022-07-01', '2023-07-01', 'APR'),
    (8, '2022-08-01', '2023-08-01', 'APR'),
    (9, '2022-09-01', '2023-09-01', 'APR'),
    (10, '2022-10-01', '2023-10-01', 'APR'),
    (11, '2022-11-01', '2023-11-01', 'APR'),
    (12, '2022-12-01', '2023-12-01', 'APR'),
    (13, '2023-01-15', '2024-01-15', 'APR'),
    (14, '2023-02-15', '2024-02-15', 'APR'),
    (15, '2023-03-15', '2024-03-15', 'APR'),
    (16, '2023-04-15', '2024-04-15', 'APR'),
    (17, '2023-05-15', '2024-05-15', 'APR'),
    (18, '2023-06-15', '2024-06-15', 'APR'),
    (19, '2023-07-15', '2024-07-15', 'APR'),
    (20, '2023-08-15', '2024-08-15', 'APR');

--INSERTS de licencias para query 3.f:
INSERT INTO Licencias (estNumero, licFchEmision, licFchVto, licStatus)
VALUES 
    (3, '2023-03-25', '2024-03-25', 'REV'),
    (4, '2023-04-30', '2024-04-30', 'REV'),
    (5, '2023-05-05', '2024-05-05', 'REV'),
    (6, '2023-06-10', '2024-06-10', 'REV'),
    (7, '2023-07-15', '2024-07-15', 'APR'),
    (8, '2023-08-20', '2024-08-20', 'APR'),
    (9, '2023-09-25', '2024-09-25', 'APR'),
    (10, '2023-10-30', '2024-10-30', 'APR'),
    (11, '2023-11-05', '2024-11-05', 'APR'),
    (12, '2023-12-10', '2024-12-10', 'APR'),
    (13, '2024-01-20', '2025-01-15', 'APR'),
    (14, '2024-02-25', '2025-02-20', 'APR'),
    (15, '2024-03-30', '2025-03-25', 'APR'),
    (16, '2024-04-16', '2025-04-30', 'APR'),
    (17, '2024-05-17', '2025-05-05', 'APR'),
    (18, '2024-06-18', '2025-06-10', 'APR'),
    (19, '2024-07-19', '2025-07-15', 'APR'),
    (20, '2024-08-20', '2025-08-20', 'APR');

--15 VIOLACIONES
INSERT INTO TipoViolacion (violDescrip)
VALUES
    ('Falta de higiene'),
    ('Infracción sanitaria'),
    ('Uso inadecuado de alimentos'),
    ('Mal manejo de desechos'),
    ('Incumplimiento de seguridad'),
    ('Horarios de operación'),
    ('Venta de alcohol a menores'),
    ('Presencia de plagas'),
    ('Contaminación cruzada'),
    ('Falta de licencia'),
    ('Manipulación incorrecta'),
    ('Deficiencias estructurales'),
    ('Inadecuada temperatura'),
    ('No cumplimiento de limpieza'),
    ('Falta de señalización');

--40 INSPECCIONES
INSERT INTO Inspecciones (inspFecha, estNumero, inspRiesgo, inspResultado, violCodigo, inspComents)
VALUES
    ('2023-02-05 10:00:00', 1, 'Bajo', 'Pasa', 1, 'Todo en orden'),
    ('2023-03-10 09:30:00', 2, 'Medio', 'Falla', 2, 'Infracción sanitaria detectada'),
    ('2023-04-15 11:45:00', 3, 'Alto', 'Falla', 3, 'Mal manejo de alimentos'),
    ('2023-05-20 14:20:00', 4, 'Medio', 'Pasa con condiciones', 4, 'Se requiere mejorar la gestión de desechos'),
    ('2023-06-25 08:15:00', 5, 'Bajo', 'Pasa', 5, 'No se detectaron irregularidades'),
    ('2023-07-30 12:30:00', 6, 'Medio', 'Falla', 6, 'Violación de horarios de operación'),
    ('2023-08-05 10:00:00', 7, 'Alto', 'Falla', 7, 'Venta de alcohol a menores'),
    ('2023-09-10 09:30:00', 8, 'Medio', 'Pasa con condiciones', 8, 'Se detectaron indicios de plagas'),
    ('2023-10-15 11:45:00', 9, 'Bajo', 'Pasa', 9, 'Todo en orden'),
    ('2023-11-20 14:20:00', 10, 'Alto', 'Falla', 10, 'No se encontró licencia'),
    ('2023-12-25 08:15:00', 11, 'Medio', 'Falla', 11, 'Manipulación incorrecta de alimentos'),
    ('2023-12-05 12:30:00', 12, 'Bajo', 'Pasa', 12, 'Todo en orden'),
    ('2024-03-10 10:00:00', 13, 'Alto', 'Falla', 13, 'Problemas estructurales detectados'),
    ('2024-03-15 09:30:00', 14, 'Medio', 'Pasa con condiciones', 14, 'Temperatura de almacenamiento no óptima'),
    ('2024-03-20 11:45:00', 15, 'Bajo', 'Pasa', 15, 'Todo en orden'),
    ('2024-03-25 14:20:00', 16, 'Medio', 'Falla', 1, 'Falta de higiene'),
    ('2024-04-05 08:15:00', 17, 'Alto', 'Falla', 2, 'Infracción sanitaria detectada'), 
    ('2024-08-10 12:30:00', 18, 'Medio', 'Pasa con condiciones', 3, 'Mal manejo de alimentos'),
    ('2024-08-15 10:00:00', 19, 'Bajo', 'Pasa', 4, 'Se requiere mejorar la gestión de desechos'),
    ('2024-09-20 09:30:00', 20, 'Alto', 'Falla', 5, 'No se detectaron irregularidades'),
	('2024-09-20 09:30:00', 21, 'Alto', 'Falla', 5, 'No se detectaron irregularidades'),
    ('2023-04-25 11:45:00', 1, 'Medio', 'Falla', 6, 'Violación de horarios de operación'),
    ('2023-05-05 14:20:00', 2, 'Alto', 'Falla', 7, 'Venta de alcohol a menores'),
    ('2023-05-10 08:15:00', 3, 'Medio', 'Pasa con condiciones', 8, 'Se detectaron indicios de plagas'),
    ('2023-05-15 12:30:00', 4, 'Bajo', 'Pasa', 9, 'Todo en orden'),
    ('2023-05-20 10:00:00', 5, 'Alto', 'Falla', 10, 'No se encontró licencia'),
    ('2023-05-25 09:30:00', 6, 'Medio', 'Falla', 11, 'Manipulación incorrecta de alimentos'),
    ('2023-06-05 11:45:00', 7, 'Bajo', 'Pasa', 12, 'Todo en orden'),
    ('2023-06-10 14:20:00', 8, 'Alto', 'Falla', 13, 'Problemas estructurales detectados'),
    ('2023-06-15 08:15:00', 9, 'Medio', 'Pasa con condiciones', 14, 'Temperatura de almacenamiento no óptima'),
    ('2023-06-20 12:30:00', 10, 'Bajo', 'Pasa', 15, 'Todo en orden'),
    ('2023-06-25 10:00:00', 11, 'Medio', 'Falla', 1, 'Falta de higiene'),
    ('2023-07-05 09:30:00', 12, 'Alto', 'Falla', 2, 'Infracción sanitaria detectada'),
    ('2024-07-10 11:45:00', 13, 'Medio', 'Pasa con condiciones', 3, 'Mal manejo de alimentos'),
    ('2024-07-15 14:20:00', 14, 'Bajo', 'Pasa', 4, 'Se requiere mejorar la gestión de desechos'),
    ('2024-07-20 08:15:00', 15, 'Alto', 'Falla', 5, 'No se detectaron irregularidades'),
    ('2024-07-25 12:30:00', 16, 'Medio', 'Falla', 6, 'Violación de horarios de operación'),
    ('2024-08-05 10:00:00', 17, 'Alto', 'Falla', 7, 'Venta de alcohol a menores'),
    ('2024-08-10 09:30:00', 18, 'Medio', 'Pasa con condiciones', 8, 'Se detectaron indicios de deshechos animales'),
	('2024-08-15 11:45:00', 19, 'Bajo', 'Pasa', 9, 'Todo en orden'),
    ('2024-08-20 14:20:00', 20, 'Alto', 'Falla', 10, 'No se encontró licencia'),
	('2023-01-05 10:00:00.000', 1, 'Bajo', 'Oficina no encontrada', 1, 'Oficina no encontrada'),
	('2023-01-10 09:30:00.000', 2, 'Medio', 'Oficina no encontrada', 2, 'Oficina no encontrada'),
	('2023-01-15 11:45:00.000', 3, 'Alto', 'Oficina no encontrada', 3, 'Oficina no encontrada'),
	('2023-01-20 14:20:00.000', 4, 'Medio', 'Oficina no encontrada', 4, 'Oficina no encontrada'),
	('2023-01-25 08:15:00.000', 5, 'Bajo', 'Oficina no encontrada', 5, 'Oficina no encontrada'),
	('2023-01-30 12:30:00.000', 6, 'Medio', 'Oficina no encontrada', 6, 'Oficina no encontrada'),
	('2023-02-05 10:00:00.000', 7, 'Alto', 'Oficina no encontrada', 7, 'Oficina no encontrada'),
	('2023-02-10 09:30:00.000', 8, 'Medio', 'Oficina no encontrada', 8, 'Oficina no encontrada'),
	('2023-02-15 11:45:00.000', 9, 'Bajo', 'Oficina no encontrada', 9, 'Oficina no encontrada'),
	('2023-02-20 14:20:00.000', 10, 'Alto', 'Oficina no encontrada', 10, 'Oficina no encontrada'),
	('2023-01-05 10:00:00.000', 1, 'Bajo', 'Oficina no encontrada', 1, NULL),
	('2023-01-10 09:30:00.000', 2, 'Medio', 'Oficina no encontrada', 2, NULL),
	('2023-01-15 11:45:00.000', 3, 'Alto', 'Oficina no encontrada', 3, NULL),
	('2023-01-20 14:20:00.000', 4, 'Medio', 'Oficina no encontrada', 4, NULL),
	('2023-01-25 08:15:00.000', 5, 'Bajo', 'Oficina no encontrada', 5, NULL),
	('2023-01-30 12:30:00.000', 6, 'Medio', 'Oficina no encontrada', 6, NULL),
	('2023-02-05 10:00:00.000', 7, 'Alto', 'Oficina no encontrada', 7, NULL),
	('2023-02-10 09:30:00.000', 8, 'Medio', 'Oficina no encontrada', 8, NULL),
	('2023-02-15 11:45:00.000', 9, 'Bajo', 'Oficina no encontrada', 9, NULL),
	('2023-02-20 14:20:00.000', 10, 'Alto', 'Oficina no encontrada', 10, NULL);

--Dos establecimientos que cometen todas las Violaciones:
INSERT INTO Inspecciones (inspFecha, estNumero, inspRiesgo, inspResultado, violCodigo, inspComents)
VALUES
	('2023-04-05 10:00:00', 1, 'Bajo', 'Falla', 1, 'Todo en orden'),
    ('2023-01-10 09:30:00', 1, 'Medio', 'Falla', 2, 'Se encontraron problemas de limpieza'),
    ('2023-01-15 11:45:00', 1, 'Alto', 'Falla', 3, 'No se cumplen los estándares de manipulación de alimentos'),
    ('2023-01-20 14:20:00', 1, 'Medio', 'Pasa con condiciones', 4, 'Se requiere mejorar la gestión de desechos'),
    ('2023-01-25 08:15:00', 1, 'Bajo', 'Pasa', 5, 'Inspección de rutina sin hallazgos'),
	('2023-05-25 11:45:00', 1, 'Medio', 'Falla', 6, 'Violación de horarios de operación'),
    ('2023-02-05 10:00:00', 1, 'Alto', 'Falla', 7, 'Se encontró venta de alcohol a menores'),
    ('2023-02-10 09:30:00', 1, 'Medio', 'Pasa con condiciones', 8, 'Se detectaron signos de infestación de plagas'),
    ('2023-02-15 11:45:00', 1, 'Bajo', 'Pasa', 9, 'Inspección de rutina sin hallazgos'),
    ('2023-02-20 14:20:00', 1, 'Alto', 'Falla', 10, 'No se encontró licencia de funcionamiento'),
    ('2023-02-25 08:15:00', 1, 'Medio', 'Falla', 11, 'Problemas detectados con la manipulación de alimentos'),
    ('2023-03-05 12:30:00', 1, 'Bajo', 'Pasa', 12, 'Inspección de rutina sin hallazgos'),
	('2023-03-10 10:00:00', 1, 'Alto', 'Falla', 13, 'Se encontraron problemas estructurales'),
    ('2023-03-15 09:30:00', 1, 'Medio', 'Pasa con condiciones', 14, 'Temperatura de almacenamiento no adecuada'),
    ('2023-03-20 11:45:00', 1, 'Bajo', 'Pasa', 15, 'Inspección de rutina sin hallazgos'),
	('2023-01-05 10:00:00', 2, 'Bajo', 'Pasa', 1, 'Inspección rutinaria'),
	('2023-05-10 09:30:00', 2, 'Medio', 'Falla', 2, 'Infracción sanitaria detectada'),
	('2023-01-10 11:45:00', 2, 'Alto', 'Falla', 3, 'No se cumplen los estándares de manipulación de alimentos'),
    ('2023-01-20 14:20:00', 2, 'Medio', 'Pasa con condiciones', 4, 'Se requiere mejorar la gestión de desechos'),
    ('2023-01-25 08:15:00', 2, 'Bajo', 'Pasa', 5, 'Inspección de rutina sin hallazgos'),
	('2023-03-30 12:30:00', 2, 'Medio', 'Falla', 6, 'Se detectó incumplimiento de los horarios de operación'),
	('2023-07-05 14:20:00', 2, 'Alto', 'Falla', 7, 'Venta de alcohol a menores'),
    ('2023-02-10 09:30:00', 2, 'Medio', 'Pasa con condiciones', 8, 'Se detectaron signos de infestación de plagas'),
    ('2023-02-15 11:45:00', 2, 'Bajo', 'Pasa', 9, 'Inspección de rutina sin hallazgos'),
    ('2023-02-20 14:20:00', 2, 'Alto', 'Falla', 10, 'No se encontró licencia de funcionamiento'),
    ('2023-02-25 08:15:00', 2, 'Medio', 'Falla', 11, 'Problemas detectados con la manipulación de alimentos'),
    ('2023-03-05 12:30:00', 2, 'Bajo', 'Pasa', 12, 'Inspección de rutina sin hallazgos'),
	('2023-03-10 10:00:00', 2, 'Alto', 'Falla', 13, 'Se encontraron problemas estructurales'),
    ('2023-03-15 09:30:00', 2, 'Medio', 'Pasa con condiciones', 14, 'Temperatura de almacenamiento no adecuada'),
    ('2023-03-20 11:45:00', 2, 'Bajo', 'Pasa', 15, 'Inspección de rutina sin hallazgos');
	
--INSERT de inspecciones para query 3.b:
INSERT INTO Inspecciones (inspFecha, estNumero, inspRiesgo, inspResultado, violCodigo, inspComents)
VALUES
    ('2024-01-05 10:00:00', 1, 'Bajo', 'Falla', 1, 'Falta de higiene'),
    ('2024-01-10 09:30:00', 2, 'Medio', 'Falla', 2, 'Infracción sanitaria detectada'),
    ('2024-01-15 11:45:00', 3, 'Alto', 'Falla', 3, 'Mal manejo de alimentos'),
    ('2024-01-20 14:20:00', 4, 'Medio', 'Falla', 4, 'Mal manejo de desechos'),
    ('2024-01-25 08:15:00', 5, 'Bajo', 'Falla', 5, 'Incumplimiento de seguridad'),
    ('2024-02-05 10:00:00', 6, 'Medio', 'Falla', 1, 'Falta de higiene'),
    ('2024-02-10 09:30:00', 7, 'Alto', 'Falla', 2, 'Infracción sanitaria detectada'),
    ('2024-02-15 11:45:00', 8, 'Medio', 'Falla', 3, 'Mal manejo de alimentos'),
    ('2024-02-20 14:20:00', 9, 'Bajo', 'Falla', 4, 'Mal manejo de desechos'),
    ('2024-02-25 08:15:00', 10, 'Medio', 'Falla', 5, 'Incumplimiento de seguridad'),
    ('2024-03-05 10:00:00', 11, 'Alto', 'Falla', 1, 'Falta de higiene'),
    ('2024-03-10 09:30:00', 12, 'Medio', 'Falla', 2, 'Infracción sanitaria detectada'),
    ('2024-03-15 11:45:00', 13, 'Bajo', 'Falla', 3, 'Mal manejo de alimentos'),
    ('2024-03-20 14:20:00', 14, 'Medio', 'Falla', 4, 'Mal manejo de desechos'),
    ('2024-03-25 08:15:00', 15, 'Alto', 'Falla', 5, 'Incumplimiento de seguridad'),
    ('2024-04-05 10:00:00', 16, 'Medio', 'Falla', 1, 'Falta de higiene'),
    ('2024-04-10 09:30:00', 17, 'Bajo', 'Falla', 2, 'Infracción sanitaria detectada'),
    ('2024-04-15 11:45:00', 18, 'Medio', 'Falla', 3, 'Mal manejo de alimentos'),
    ('2024-04-20 14:20:00', 19, 'Alto', 'Falla', 4, 'Mal manejo de desechos'),
    ('2024-04-25 08:15:00', 20, 'Medio', 'Falla', 5, 'Incumplimiento de seguridad');

--INSERT de inspecciones para query 3.b (estas violaciones no van a salir en la tabla pero entran dentro del filtro de año):
INSERT INTO Inspecciones (inspFecha, estNumero, inspRiesgo, inspResultado, violCodigo, inspComents)
VALUES
    ('2024-05-01 10:00:00', 1, 'Bajo', 'Falla', 6, 'Equipos fuera de norma'),
    ('2024-05-10 11:30:00', 2, 'Medio', 'Falla', 6, 'Equipos fuera de norma'),
    ('2024-05-20 09:45:00', 3, 'Alto', 'Falla', 6, 'Equipos fuera de norma');