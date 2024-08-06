USE FoodInspections;
/* 
=========================================================
   EJERCICIO 4: PROCEDIMIENTOS Y FUNCIONES
========================================================= 
*/

/* 
a)Escribir un procedimiento almacenado que dado un tipo de riesgo ('Bajo','Medio','Alto'),
muestre los datos de las violaciones (violCodigo, violDescrip) para dicho tipo, no devolver
datos repetidos.
*/
CREATE PROCEDURE SP_VIOLACIONES_POR_TIPORIESGO @tipoRiesgo VARCHAR(5)
AS
BEGIN
		IF @tipoRiesgo not in ('Bajo','Medio','Alto')
		BEGIN
		PRINT 'Error, tipo de riesgo invalido'
		END
		ELSE
		BEGIN
		SELECT DISTINCT v.violCodigo, v.violDescrip
		FROM Inspecciones i
		join TipoViolacion v ON i.violCodigo = v.violCodigo
		WHERE i.inspRiesgo = @tipoRiesgo
		ORDER BY v.violCodigo
		END
END

--Ejecutar el procedimiento creado
EXEC SP_VIOLACIONES_POR_TIPORIESGO 'TestError';
EXEC SP_VIOLACIONES_POR_TIPORIESGO 'Bajo';
EXEC SP_VIOLACIONES_POR_TIPORIESGO 'Medio';
EXEC SP_VIOLACIONES_POR_TIPORIESGO 'Alto';



/*
b) Mediante una función que reciba un código de violación, devolver cuantos establecimientos
con licencia vencida y nunca renovada tuvieron dicha violación.
*/
CREATE FUNCTION fn_EstablecimientosConLicenciaVencidaYNoRenovadaPorViolacion (@violCodigo int)
RETURNS int
AS
BEGIN
    DECLARE @CantidadEstablecimientos int;

    SELECT @CantidadEstablecimientos = COUNT(DISTINCT e.estNumero)
    FROM Establecimientos e
    JOIN Licencias l ON e.estNumero = l.estNumero
    LEFT JOIN Inspecciones i ON e.estNumero = i.estNumero AND l.licFchVto = (SELECT MAX(ul.licFchVto)
																FROM Licencias ul
																WHERE ul.estNumero = e.estNumero)
    WHERE l.licStatus = 'REV' AND i.violCodigo = @violCodigo AND GETDATE() > l.licFchVto;

    RETURN @CantidadEstablecimientos;
END;

-- Probar función con códigos 5 y 6
SELECT dbo.fn_EstablecimientosConLicenciaVencidaYNoRenovadaPorViolacion(6) AS Numero_De_Establecimientos;



/*
c) Escribir un procedimiento almacenado que dado un rango de fechas, retorne por parámetros
de salida la cantidad de inspecciones que tuvieron un resultado 'Oficina no encontrada' y la
cantidad de inspecciones que no tienen comentarios.
*/
CREATE PROCEDURE SP_CANTIDAD_INSPECCIONES_NOENCONTRADAS_SINCOMENTARIOS_SEGUNFECHAS 
    @fecha1 DATE, 
    @fecha2 DATE
AS
BEGIN
    IF @fecha2 < @fecha1
    BEGIN
        PRINT 'Error, fecha 2 no debe ser menor a fecha 1'
    END
    ELSE
    BEGIN
        SELECT 
            SUM(CASE WHEN i.inspResultado = 'Oficina no encontrada' THEN 1 ELSE 0 END) AS InspeccionesNoEncontradas,
            SUM(CASE WHEN i.inspComents IS NULL THEN 1 ELSE 0 END) AS InspeccionesSinComentarios
        FROM 
            Inspecciones i
        WHERE 
            i.inspFecha BETWEEN @fecha1 AND @fecha2
    END
END;

--Ejecutar el procedimiento creado
EXEC SP_CANTIDAD_INSPECCIONES_NOENCONTRADAS_SINCOMENTARIOS_SEGUNFECHAS '2020-01-01', '2023-01-25';
