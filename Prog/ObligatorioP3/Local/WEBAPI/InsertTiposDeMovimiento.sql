USE ObligatorioP3DB;
GO
delete from [dbo].[TiposDeMovimiento];
INSERT INTO [dbo].[TiposDeMovimiento] (Nombre, Aumento, Coeficiente) 
VALUES
('Compra de stock (I)', 1, 1),
('Venta de Productos (E)', 0, -1),
('Devoluci�n de Venta (E)', 0, -1),
('Donaci�n Recibida (I)', 1, 1),
('Donaci�n Realizada (E)', 0, -1),
('P�rdida de stock (E)', 0, -1),
('Ganancia de stock (I)', 1, 1),
('Rotura (E)', 0, -1),
('Transferencia entrante entre Almacenes (I)', 1, 1),
('Transferencia saliente entre Almacenes (E)', 0, -1);