USE FoodInspections;
/* 
=========================================================
   EJERCICIO 3: Utilizando SQL implementar las siguientes consultas:
========================================================= 
*/

--a 
--Mostrar nombre, dirección y teléfono de los establecimientos que tuvieron la inspección fallida más reciente.
SELECT est.estNombre AS Establecimientos, est.estTelefono AS Telefono, est.estDireccion AS Direccion
FROM Establecimientos est
WHERE est.estNumero in (SELECT ins.estNumero
						FROM Inspecciones ins
						WHERE ins.inspResultado = 'Falla' and ins.inspFecha = (SELECT MAX(ins2.inspFecha) 
															  FROM Inspecciones ins2 WHERE ins2.inspResultado = 'Falla'));



--b 
--Mostrar los 5 tipos de violaciones mas comunes, el informe debe mostrar código y descripción de la violación y cantidad de inspecciones en el año presente.
SELECT TOP 5 tv.violCodigo, tv.violDescrip, COUNT(i.inspID) AS CantidadInspecciones
FROM TipoViolacion tv
JOIN Inspecciones i ON tv.violCodigo = i.violCodigo
WHERE YEAR(i.inspFecha) = YEAR(GETDATE())
GROUP BY tv.violCodigo, tv.violDescrip
ORDER BY CantidadInspecciones DESC;



--c 
--Mostrar número y nombre de los establecimientos que cometieron todos los tipos de violación que existen.
SELECT e.estNumero AS NumeroEstablecimiento, e.estNombre AS NombreEstablecimiento FROM Establecimientos e
JOIN Inspecciones i ON e.estNumero = i.estNumero
JOIN TipoViolacion v ON i.violCodigo = v.violCodigo
GROUP BY e.estNumero, e.estNombre
HAVING COUNT(DISTINCT i.violCodigo) = (SELECT COUNT(v2.violCodigo) FROM TipoViolacion v2);



--d Mostrar el porcentaje de inspecciones reprobadas por cada establecimiento, incluir dentro de la reprobación las categorías 'Falla', 'Pasa con condiciones'.
SELECT e.estNombre, COUNT(i.inspID) AS TotalInspecciones, 
	SUM(CASE WHEN i.inspResultado IN ('Falla', 'Pasa con condiciones') THEN 1 ELSE 0 END) AS InspeccionesReprobadas, 
	(SUM(CASE WHEN i.inspResultado IN ('Falla', 'Pasa con condiciones') THEN 1 ELSE 0 END) * 100.0) / COUNT(i.inspID) AS PorcentajeReprobadas
FROM Establecimientos e
JOIN Inspecciones i ON e.estNumero = i.estNumero
GROUP BY e.estNombre;

--d Hecha con funciones (están declaradas en la consulta E)
SELECT 
    e.estNombre, 
    ISNULL(dbo.FUNC_ContarInspeccionesTotales(e.estNumero), 0) AS TotalInspecciones,
    ISNULL(dbo.FUNC_ContarInspeccionesRechazadas(e.estNumero), 0) AS InspeccionesReprobadas,
    ISNULL(dbo.FUNC_CalcularPorcentajeRechazadas(e.estNumero), 0) AS PorcentajeReprobadas
FROM 
    Establecimientos e;

--e 
SELECT 
    e.estNumero AS NumeroEstablecimiento, 
    e.estNombre AS NombreEstablecimiento, 
    COUNT(i.inspId) AS TotalInspecciones,
    SUM(CASE WHEN i.inspResultado IN ('Pasa') THEN 1 ELSE 0 END) AS InspeccionesAprobadas, 
    FORMAT((SUM(CASE WHEN i.inspResultado IN ('Pasa') THEN 1 ELSE 0 END) * 100.0) / COUNT(i.inspId), 'N2') AS PorcentajeAprobadas,
    SUM(CASE WHEN i.inspResultado IN ('Falla','Pasa con condiciones') THEN 1 ELSE 0 END) AS InspeccionesRechazadas,
    FORMAT((SUM(CASE WHEN i.inspResultado IN ('Falla','Pasa con condiciones') THEN 1 ELSE 0 END) * 100.0) / COUNT(i.inspId), 'N2') AS PorcentajeRechazadas
FROM Establecimientos e
JOIN Inspecciones i ON e.estNumero = i.estNumero
where e.estNumero in (select l.estNumero from Licencias l where l.licStatus = 'APR' and GETDATE() < l.licFchVto)
GROUP BY e.estNumero, e.estNombre;



--f Mostrar la diferencia de días en que cada establecimiento renovó su licencia.
SELECT e.estNumero, e.estNombre,
    DATEDIFF(day, pl.licFchVto, ul.licFchEmision) AS DiferenciaDiasRenovacion
FROM Establecimientos e
JOIN Licencias ul ON e.estNumero = ul.estNumero
JOIN Licencias pl ON e.estNumero = pl.estNumero
WHERE ul.licFchEmision = (SELECT MAX(ul2.licFchEmision)
							FROM (SELECT TOP 2 ul1.licFchEmision
									FROM Licencias ul1
									WHERE ul1.estNumero = e.estNumero
									ORDER BY ul1.licFchEmision DESC) ul2)
							
    AND pl.licFchEmision = (SELECT MIN(ul2.licFchEmision)
							FROM (SELECT TOP 2 ul1.licFchEmision
									FROM Licencias ul1
									WHERE ul1.estNumero = e.estNumero
									ORDER BY ul1.licFchEmision DESC) ul2)


/* 
#########################################################
   EJERCICIO 3.e SEPARADO EN FUNCIONES
######################################################### 
*/

-- Funcion que consulta: Cantidad de Inspecciones totales
CREATE FUNCTION dbo.FUNC_ContarInspeccionesTotales(@estNumero INT)
RETURNS INT
AS
BEGIN
    DECLARE @TotalInspecciones INT;

    SELECT @TotalInspecciones = COUNT(*)
    FROM dbo.Inspecciones
    WHERE estNumero = @estNumero;

    RETURN @TotalInspecciones;
END;

-- Funcion que consulta: Establecimientos con licencias aprobadas:
--RETURNS TABLE FUE UNA QUERY DE CHATGPT: Chatgpt, dame un ejemplo de una funcion que devuelva una tabla.
CREATE FUNCTION dbo.FUNC_ObtenerEstablecimientosAprobados()
RETURNS TABLE
AS
RETURN
(
    SELECT e.estNumero, e.estNombre
    FROM Establecimientos e
    WHERE e.estNumero IN (
        SELECT l.estNumero 
        FROM Licencias l 
        WHERE l.licStatus = 'APR' and l.licFchVto > GETDATE()
    )
);

-- Funcion que consulta: Inspecciones de los establecimientos:
CREATE FUNCTION FUNC_ObtenerInspecciones(@estNumero numeric)
RETURNS TABLE
AS
RETURN
(
    SELECT i.inspId, i.inspResultado
    FROM Inspecciones i
    WHERE i.estNumero = @estNumero
);

-- Funcion que consulta: Cantidad de inspecciones aprobadas
CREATE FUNCTION FUNC_ContarInspeccionesAprobadas(@estNumero INT)
RETURNS INT
AS
BEGIN
    DECLARE @InspeccionesAprobadas INT;
    SELECT @InspeccionesAprobadas = COUNT(*)
    FROM Inspecciones
    WHERE @estNumero = estNumero AND inspResultado = 'Pasa';
    RETURN @InspeccionesAprobadas;
END;

-- Funcion que consulta: Cantidad de inspecciones rechazadas
CREATE FUNCTION FUNC_ContarInspeccionesRechazadas(@estNumero INT)
RETURNS INT
AS
BEGIN
    DECLARE @InspeccionesRechazadas INT;
    SELECT @InspeccionesRechazadas = COUNT(*)
    FROM Inspecciones
    WHERE @estNumero = estNumero AND inspResultado IN ('Falla', 'Pasa con condiciones');
    RETURN @InspeccionesRechazadas;
END;

-- Funcion que consulta: Porcentaje aprobadas
CREATE FUNCTION FUNC_CalcularPorcentajeAprobadas(@estNumero INT)
RETURNS DECIMAL(5, 2)
AS
BEGIN
    DECLARE @TotalInspecciones INT = dbo.FUNC_ContarInspeccionesTotales(@estNumero);
    DECLARE @InspeccionesAprobadas INT = dbo.FUNC_ContarInspeccionesAprobadas(@estNumero);
    IF @TotalInspecciones = 0
        RETURN 0;
	IF @InspeccionesAprobadas = 0
		return 0;
    RETURN CAST((@InspeccionesAprobadas * 100.0 / @TotalInspecciones) AS DECIMAL(5, 2));
END;

-- Funcion que consulta: Porcentaje rechazadas
CREATE FUNCTION FUNC_CalcularPorcentajeRechazadas(@estNumero INT)
RETURNS DECIMAL(5, 2)
AS
BEGIN
    DECLARE @TotalInspecciones INT = dbo.FUNC_ContarInspeccionesTotales(@estNumero);
    DECLARE @InspeccionesRechazadas INT = dbo.FUNC_ContarInspeccionesRechazadas(@estNumero);
    IF @TotalInspecciones = 0
        RETURN 0;
	IF @InspeccionesRechazadas = 0
		return 0;
    RETURN CAST((@InspeccionesRechazadas * 100.0 / @TotalInspecciones) AS DECIMAL(5, 2));
END;

-- Funcion principal que resuelve la consulta solicitada
CREATE FUNCTION dbo.ObtenerReporte()
RETURNS TABLE
AS
RETURN
(
    SELECT 
        e.estNumero AS NumeroEstablecimiento,
        e.estNombre AS NombreEstablecimiento,
		--Obtenemos todas las inspecciones de ese establecimiento
        (SELECT dbo.FUNC_ContarInspeccionesTotales(e.estNumero)) AS TotalInspecciones,
		--Contamos las aprobadas
        (SELECT dbo.FUNC_ContarInspeccionesAprobadas(e.estNumero)) AS InspeccionesAprobadas,
		--Obtenemos el porcentaje de aprobacion
        (SELECT dbo.FUNC_CalcularPorcentajeAprobadas(e.estNumero)) AS PorcentajeAprobadas,
		--Contamos las rechazadas
        (SELECT dbo.FUNC_ContarInspeccionesRechazadas(e.estNumero)) AS InspeccionesRechazadas,
		--Obtenemos el porcentaje de rechazo
        (SELECT dbo.FUNC_CalcularPorcentajeRechazadas(e.estNumero)) AS PorcentajeRechazadas
		--De los establecimientos con licencias vigentes (apr)
    FROM dbo.FUNC_ObtenerEstablecimientosAprobados() e
);

-- Llamar a la función principal
SELECT * FROM dbo.ObtenerReporte();