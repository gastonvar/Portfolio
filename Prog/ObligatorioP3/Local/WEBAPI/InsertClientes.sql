USE ObligatorioP3DB;
GO
delete from [dbo].[Clientes];
INSERT INTO [dbo].[Clientes] (RazonSocial, RUT, Direccion_Calle, Direccion_Ciudad, Direccion_Distancia, Direccion_Numero) 
VALUES
('Supermercado El Ahorro', '112233455563', 'Av. Libertador 1234', 'Montevideo', 3.2, 101),
('Ferreter�a La Herramienta', '112232445567', 'Calle Rep�blica Argentina 432', 'Punta del Este', 8.5, 55),
('Tienda de Ropa Fashion', '334455667768', 'Av. Italia 987', 'Canelones', 15.3, 205),
('Panader�a El Buen Gusto', '445566778899', 'Calle Brasil 567', 'Salto', 5.7, 78),
('Restaurante La Parrilla', '556577889900', 'Av. 18 de Julio 876', 'Maldonado', 10.1, 309),
('Farmacia La Salud', '667788990011', 'Av. Artigas 321', 'Rivera', 12.6, 210),
('Librer�a El Saber', '878899001122', 'Calle San Mart�n 210', 'Tacuaremb�', 7.9, 95),
('Cafeter�a El Cafecito', '869900112233', 'Plaza Independencia 456', 'Colonia', 9.3, 40),
('Taller Mec�nico El R�pido', '590011223344', 'Av. Brasil 654', 'Paysand�', 4.8, 167),
('Gimnasio Fitness Plus', '112235445566', 'Av. Luis Alberto de Herrera 789', 'Durazno', 6.4, 77),
('Veterinaria Mascotas Felices', '334455667788', 'Av. Italia 432', 'Treinta y Tres', 11.2, 88),
('Droguer�a La Buena Vida', '556677889900', 'Calle Uruguay 987', 'Rocha', 14.7, 153),
('�ptica Visi�n Clara', '778899001122', 'Av. Joaqu�n Su�rez 210', 'San Jos�', 8.1, 64),
('Peluquer�a Estilo �nico', '990011223344', 'Calle Rivera 123', 'Flores', 3.6, 22),
('Florer�a Flores del Jard�n', '112233445563', 'Av. Gral. Flores 876', 'Lavalleja', 9.9, 115),
('Santeria Santito Feliz', '112233445562', 'Mateo Legnani 126', 'Santa Lucia', 101, 115);
