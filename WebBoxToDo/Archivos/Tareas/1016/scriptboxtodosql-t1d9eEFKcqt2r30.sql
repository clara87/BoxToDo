USE [PW3TP_20181C_Tareas]
GO
/****** Object:  Table [dbo].[ArchivoTarea]    Script Date: 03-07-2018 22:40:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArchivoTarea](
	[IdArchivoTarea] [int] IDENTITY(1,1) NOT NULL,
	[RutaArchivo] [nvarchar](max) NOT NULL,
	[IdTarea] [int] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL CONSTRAINT [DF_ArchivoTarea_FechaCreacion]  DEFAULT (getdate()),
 CONSTRAINT [PK_ArchivoTarea] PRIMARY KEY CLUSTERED 
(
	[IdArchivoTarea] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Carpeta]    Script Date: 03-07-2018 22:40:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carpeta](
	[IdCarpeta] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Descripcion] [nvarchar](200) NULL,
	[FechaCreacion] [datetime] NOT NULL CONSTRAINT [DF_Carpeta_FechaCreacion]  DEFAULT (getdate()),
 CONSTRAINT [PK_Carpetas] PRIMARY KEY CLUSTERED 
(
	[IdCarpeta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ComentarioTarea]    Script Date: 03-07-2018 22:40:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComentarioTarea](
	[IdComentarioTarea] [int] IDENTITY(1,1) NOT NULL,
	[Texto] [nvarchar](max) NOT NULL,
	[IdTarea] [int] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL CONSTRAINT [DF_ComentarioTarea_FechaCreacion]  DEFAULT (getdate()),
 CONSTRAINT [PK_Comentario] PRIMARY KEY CLUSTERED 
(
	[IdComentarioTarea] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tarea]    Script Date: 03-07-2018 22:40:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tarea](
	[IdTarea] [int] IDENTITY(1,1) NOT NULL,
	[IdCarpeta] [int] NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Descripcion] [nvarchar](200) NULL,
	[EstimadoHoras] [decimal](18, 2) NULL,
	[FechaFin] [datetime] NULL,
	[Prioridad] [smallint] NOT NULL,
	[Completada] [smallint] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL CONSTRAINT [DF_Tarea_FechaCreacion]  DEFAULT (getdate()),
 CONSTRAINT [PK_Tareas] PRIMARY KEY CLUSTERED 
(
	[IdTarea] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 03-07-2018 22:40:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Apellido] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[Contrasenia] [nvarchar](50) NOT NULL,
	[Activo] [smallint] NOT NULL,
	[FechaRegistracion] [datetime] NOT NULL,
	[FechaActivacion] [datetime] NULL,
	[CodigoActivacion] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ArchivoTarea]  WITH CHECK ADD  CONSTRAINT [FK_ArchivoTarea_Tarea] FOREIGN KEY([IdTarea])
REFERENCES [dbo].[Tarea] ([IdTarea])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ArchivoTarea] CHECK CONSTRAINT [FK_ArchivoTarea_Tarea]
GO
ALTER TABLE [dbo].[Carpeta]  WITH CHECK ADD  CONSTRAINT [FK_Carpeta_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Carpeta] CHECK CONSTRAINT [FK_Carpeta_Usuario]
GO
ALTER TABLE [dbo].[ComentarioTarea]  WITH CHECK ADD  CONSTRAINT [FK_Comentario_Tarea] FOREIGN KEY([IdTarea])
REFERENCES [dbo].[Tarea] ([IdTarea])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ComentarioTarea] CHECK CONSTRAINT [FK_Comentario_Tarea]
GO
ALTER TABLE [dbo].[Tarea]  WITH CHECK ADD  CONSTRAINT [FK_Tareas_Usuarios] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tarea] CHECK CONSTRAINT [FK_Tareas_Usuarios]
GO
