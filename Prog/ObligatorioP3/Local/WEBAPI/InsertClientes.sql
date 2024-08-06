USE ObligatorioP3DB;
GO
delete from [dbo].[Clientes];
INSERT INTO [dbo].[Clientes] (RazonSocial, RUT, Direccion_Calle, Direccion_Ciudad, Direccion_Distancia, Direccion_Numero) 
VALUES
('Supermercado El Ahorro', '112233455563', 'Av. Libertador 1234', 'Montevideo', 3.2, 101),
('Ferretería La Herramienta', '112232445567', 'Calle República Argentina 432', 'Punta del Este', 8.5, 55),
('Tienda de Ropa Fashion', '334455667768', 'Av. Italia 987', 'Canelones', 15.3, 205),
('Panadería El Buen Gusto', '445566778899', 'Calle Brasil 567', 'Salto', 5.7, 78),
('Restaurante La Parrilla', '556577889900', 'Av. 18 de Julio 876', 'Maldonado', 10.1, 309),
('Farmacia La Salud', '667788990011', 'Av. Artigas 321', 'Rivera', 12.6, 210),
('Librería El Saber', '878899001122', 'Calle San Martín 210', 'Tacuarembó', 7.9, 95),
('Cafetería El Cafecito', '869900112233', 'Plaza Independencia 456', 'Colonia', 9.3, 40),
('Taller Mecánico El Rápido', '590011223344', 'Av. Brasil 654', 'Paysandú', 4.8, 167),
('Gimnasio Fitness Plus', '112235445566', 'Av. Luis Alberto de Herrera 789', 'Durazno', 6.4, 77),
('Veterinaria Mascotas Felices', '334455667788', 'Av. Italia 432', 'Treinta y Tres', 11.2, 88),
('Droguería La Buena Vida', '556677889900', 'Calle Uruguay 987', 'Rocha', 14.7, 153),
('Óptica Visión Clara', '778899001122', 'Av. Joaquín Suárez 210', 'San José', 8.1, 64),
('Peluquería Estilo Único', '990011223344', 'Calle Rivera 123', 'Flores', 3.6, 22),
('Florería Flores del Jardín', '112233445563', 'Av. Gral. Flores 876', 'Lavalleja', 9.9, 115),
('Santeria Santito Feliz', '112233445562', 'Mateo Legnani 126', 'Santa Lucia', 101, 115);
