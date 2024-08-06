USE FoodInspections;
/* 
=========================================================
   EJERCICIO 5: TRIGGERS
========================================================= 
*/
/* 
a) Cada vez que se crea un nuevo establecimiento, se debe crear una licencia de aprobaci�n
con vencimiento 90 d�as, el disparador debe ser escrito teniendo en cuenta la posibilidad de
ingresos m�ltiples.
*/
CREATE TRIGGER Tgr_CtrlLicenciasEstablecimientosNuevos
ON Establecimientos
AFTER INSERT
AS
BEGIN
	INSERT INTO Licencias (estNumero,licFchEmision,licFchVto,licStatus)
	SELECT i.estNumero, GETDATE(), DATEADD(DAY,90,GETDATE()), 'APR'
	FROM inserted i
END;

--TEST
INSERT INTO Establecimientos (estNombre, estDireccion, estTelefono, estLatitud, estLongitud)
VALUES
    ('Establecimiento1', 'Calle Mayor 123', '+34 123 456 789', 40.4168, -3.7038),
    ('Establecimiento2', 'Avenida Libertad 45', '+34 987 654 321', 41.3851, 2.1734);

select * from Licencias l, Establecimientos e
where l.estNumero = e.estNumero
and e.estNombre in ('Establecimiento1','Establecimiento2')
/* 
b) No permitir que se ingresen inspecciones de establecimientos cuya licencia est� pr�xima a
vencer, se entiende por pr�xima a vencer a todas aquellas cuyo vencimiento est� dentro de
los siguientes 5 d�as, el disparador debe tener en cuenta la posibilidad de registros
m�ltiples.
*/
CREATE TRIGGER trg_PrevenirInspeccionesCercaDeVencimiento
ON Inspecciones
INSTEAD OF INSERT
AS
BEGIN
    -- Variable temporal para almacenar los establecimientos con licencia pr�xima a vencer
    DECLARE @Prohibido TABLE (estNumero int);

    -- Insertar los establecimientos con licencia pr�xima a vencer en @Prohibido
    INSERT INTO @Prohibido (estNumero)
    SELECT i.estNumero
    FROM inserted i
	join Licencias l ON l.estNumero = i.estNumero
    WHERE i.inspFecha BETWEEN DATEADD(DAY, -5, l.licFchVto) and l.licFchVto

    -- Verificar si hay establecimientos con licencia pr�xima a vencer
    IF EXISTS (SELECT 1 FROM @Prohibido)
    BEGIN
        -- Si hay establecimientos con licencia pr�xima a vencer, mostrar error y deshacer la transacci�n
        PRINT 'No se puede ingresar la inspecci�n. La licencia del establecimiento est� pr�xima a vencer.';
        ROLLBACK;
    END
    ELSE
    BEGIN
        -- Si no hay establecimientos con licencia pr�xima a vencer, permitir la inserci�n de las inspecciones
        INSERT INTO Inspecciones (inspFecha, estNumero, inspRiesgo, inspResultado, violCodigo, inspComents)
        SELECT inspFecha, estNumero, inspRiesgo, inspResultado, violCodigo, inspComents
        FROM inserted;
    END
END;

-- Insert n� v�lido para probar el trigger:
INSERT INTO Inspecciones (inspFecha, estNumero, inspRiesgo, inspResultado, violCodigo, inspComents)
VALUES ('2024-03-22 12:30:00', 3, 'Bajo', 'Pasa', 1, 'Todo en orden'),
	   ('2024-03-22 12:30:00', 4, 'Bajo', 'Pasa', 1, 'Todo en orden');

select * from Inspecciones

select * from Inspecciones i
where i.inspFecha = '2024-01-12 12:30:00'
and estNumero = 3
and inspRiesgo = 'Bajo'
and inspResultado = 'Pasa'
and violCodigo = 1
and inspComents = 'Todo en orden'