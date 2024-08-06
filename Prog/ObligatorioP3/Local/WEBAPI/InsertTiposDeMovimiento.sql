USE ObligatorioP3DB;
GO
delete from [dbo].[TiposDeMovimiento];
INSERT INTO [dbo].[TiposDeMovimiento] (Nombre, Aumento, Coeficiente) 
VALUES
('Compra de stock (I)', 1, 1),
('Venta de Productos (E)', 0, -1),
('Devolución de Venta (E)', 0, -1),
('Donación Recibida (I)', 1, 1),
('Donación Realizada (E)', 0, -1),
('Pérdida de stock (E)', 0, -1),
('Ganancia de stock (I)', 1, 1),
('Rotura (E)', 0, -1),
('Transferencia entrante entre Almacenes (I)', 1, 1),
('Transferencia saliente entre Almacenes (E)', 0, -1);