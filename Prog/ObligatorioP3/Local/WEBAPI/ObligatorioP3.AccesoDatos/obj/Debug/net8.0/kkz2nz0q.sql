IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Articulos] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(max) NOT NULL,
    [Descripcion] nvarchar(max) NOT NULL,
    [Codigo] nvarchar(max) NOT NULL,
    [Precio] float NOT NULL,
    [Stock] int NOT NULL,
    CONSTRAINT [PK_Articulos] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Clientes] (
    [Id] int NOT NULL IDENTITY,
    [RazonSocial] nvarchar(max) NOT NULL,
    [RUT] nvarchar(max) NOT NULL,
    [Direccion_Calle] nvarchar(max) NOT NULL,
    [Direccion_Ciudad] nvarchar(max) NOT NULL,
    [Direccion_Distancia] float NOT NULL,
    [Direccion_Numero] int NOT NULL,
    CONSTRAINT [PK_Clientes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Pedidos] (
    [Id] int NOT NULL IDENTITY,
    [Fecha] datetime2 NOT NULL,
    [FechaEntrega] datetime2 NOT NULL,
    [ClienteId] int NOT NULL,
    [Discriminator] nvarchar(13) NOT NULL,
    CONSTRAINT [PK_Pedidos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Pedidos_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Pedidos_ClienteId] ON [Pedidos] ([ClienteId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240404213131_init', N'8.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Usuarios] (
    [Id] int NOT NULL IDENTITY,
    [Contrasena_ContrasenaEncriptada] nvarchar(max) NULL,
    [Contrasena_ContrasenaNoEncriptada] nvarchar(max) NOT NULL,
    [Email_ValorEmail] nvarchar(max) NOT NULL,
    [NombreCompleto_Apellido] nvarchar(max) NOT NULL,
    [NombreCompleto_Nombre] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Usuarios] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240407143936_AgregarTablaUsuarios', N'8.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Usuarios] ADD [Rol] nvarchar(max) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240407152231_try2CrearTablaUsuario', N'8.0.3');
GO

COMMIT;
GO

