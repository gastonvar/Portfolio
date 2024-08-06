/* 
=========================================================
   EJERCICIO 6: VISTA
========================================================= 
*/
/*
Escribir una vista que muestre todos los datos de las licencias vigentes y los días que faltan para
el vencimiento de cada una de ellas.
*/
USE FoodInspections;

CREATE VIEW VIEW_LicenciasVigentes AS
SELECT 
    licNumero,
    estNumero,
    licFchEmision,
    licFchVto,
    licStatus,
    DATEDIFF(day, GETDATE(), licFchVto) AS DiasParaVencimiento
FROM 
    Licencias
WHERE 
    licStatus = 'APR' AND
    licFchVto > GETDATE();  --(ignorar las vencidas)

-- Mostrar vista
SELECT * FROM VIEW_LicenciasVigentes;