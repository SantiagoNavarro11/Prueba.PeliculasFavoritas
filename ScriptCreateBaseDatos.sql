-- Crear base de datos
CREATE DATABASE CarteleraPeliculas;
GO

-- Usar la base de datos
USE CarteleraPeliculas;
GO

-- Tabla de usuarios
CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Correo NVARCHAR(100) NOT NULL UNIQUE,
    Contrasena NVARCHAR(255) NOT NULL,
    FechaRegistro DATETIME DEFAULT GETDATE()
);
GO

-- Tabla de películas favoritas (relacionadas directamente al usuario)
CREATE TABLE PeliculasFavoritas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UsuarioId INT NOT NULL,
    ImdbID NVARCHAR(20) NOT NULL,
    Titulo NVARCHAR(200) NOT NULL,
    Anio INT,
    Director NVARCHAR(100),
    Genero NVARCHAR(100),
    PosterUrl NVARCHAR(500),
    Sinopsis NVARCHAR(MAX),
    CalificacionIMDB DECIMAL(3,1),
    Duracion NVARCHAR(50),
    Idioma NVARCHAR(100),
    Pais NVARCHAR(100),
    Actores NVARCHAR(300),
    Premios NVARCHAR(300),
    FechaAgregada DATETIME DEFAULT GETDATE(),

    CONSTRAINT FK_PeliculasFavoritas_Usuario FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id),
    CONSTRAINT UQ_Usuario_Pelicula UNIQUE (UsuarioId, ImdbID)
);
GO
