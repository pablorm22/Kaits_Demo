-- Crear la base de datos
CREATE DATABASE Kaits;
GO
-- Usar la base de datos
USE Kaits;
GO
-- Crear tabla Clientes
CREATE TABLE Clientes (
    CodigoCliente INT PRIMARY KEY IDENTITY(1,1),
    NombreApellido NVARCHAR(100) NOT NULL,
    DNI CHAR(8) NOT NULL
);

-- Crear tabla Productos
CREATE TABLE Productos (
    CodigoProducto INT PRIMARY KEY IDENTITY(1,1),
    Descripcion NVARCHAR(100) NOT NULL
);

-- Crear tabla Pedido
CREATE TABLE Pedido (
    IdOrden INT PRIMARY KEY IDENTITY(1,1),
    FechaOrden DATE NOT NULL,
    CodigoCliente INT NOT NULL,
    PrecioTotal DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (CodigoCliente) REFERENCES Clientes(CodigoCliente)
);

-- Crear tabla DetallePedido
CREATE TABLE DetallePedido (
    IdDetalle INT PRIMARY KEY IDENTITY(1,1),
    IdOrden INT NOT NULL,
    CodigoProducto INT NOT NULL,
    DescripcionProducto NVARCHAR(100),
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10,2) NOT NULL,
    Subtotal DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (IdOrden) REFERENCES Pedido(IdOrden),
    FOREIGN KEY (CodigoProducto) REFERENCES Productos(CodigoProducto)
);
