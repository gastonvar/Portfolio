USE ObligatorioP3DB;
GO
delete from [dbo].[Parametros];
INSERT INTO [dbo].[Parametros] (Nombre, Valor) 
VALUES
('iva', '1,22'),
('plazoExpress', '5'),
('recargoExpressA', '1,10'),
('recargoExpressB', '1,15'),
('recargoComun', '1,05'),
('topeMovimiento', '100'),
('cantidadRegistros', '20');

