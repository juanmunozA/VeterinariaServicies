-- Crear base de datos
CREATE DATABASE VeterinariaDB;
GO

USE VeterinariaDB;
GO

-- Tabla de Clientes
CREATE TABLE Clientes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Cedula NVARCHAR(50) NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    Correo NVARCHAR(150) NOT NULL
);

-- Tabla de Razas
CREATE TABLE Razas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL
);

-- Tabla de Mascotas
CREATE TABLE Mascotas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Codigo NVARCHAR(50) NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    Edad INT NOT NULL,
    RazaId INT NOT NULL,
    ClienteId INT NOT NULL,
    FOREIGN KEY (RazaId) REFERENCES Razas(Id),
    FOREIGN KEY (ClienteId) REFERENCES Clientes(Id)
);

-- Tabla de Veterinarios
CREATE TABLE Veterinarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Documento NVARCHAR(50) NOT NULL,
    Nombre NVARCHAR(100) NOT NULL
);

-- Tabla de Medicamentos
CREATE TABLE Medicamentos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL
);

-- Tabla de Formulas
CREATE TABLE Formulas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Codigo NVARCHAR(50) NOT NULL,
    MascotaId INT NOT NULL,
    FOREIGN KEY (MascotaId) REFERENCES Mascotas(Id)
);

-- Tabla intermedia FormulaMedicamentos (N:N)
CREATE TABLE FormulaMedicamentos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FormulaId INT NOT NULL,
    MedicamentoId INT NOT NULL,
    Cantidad INT NOT NULL,
    FOREIGN KEY (FormulaId) REFERENCES Formulas(Id),
    FOREIGN KEY (MedicamentoId) REFERENCES Medicamentos(Id)
);

-- Tabla de Historiales clínicos
CREATE TABLE HistorialesClinicos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Codigo NVARCHAR(50) NOT NULL,
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    MascotaId INT NOT NULL,
    ClienteId INT NOT NULL,
    FormulaId INT NOT NULL,
    VeterinarioId INT NOT NULL,
    Observaciones NVARCHAR(MAX) NULL,
    FOREIGN KEY (MascotaId) REFERENCES Mascotas(Id),
    FOREIGN KEY (ClienteId) REFERENCES Clientes(Id),
    FOREIGN KEY (FormulaId) REFERENCES Formulas(Id),
    FOREIGN KEY (VeterinarioId) REFERENCES Veterinarios(Id)
);
