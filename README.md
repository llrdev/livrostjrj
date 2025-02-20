Frontend
=========
Angular CLI: 15.2.11
Node: 16.13.0
Package Manager: npm 8.1.0
OS: win32 x64

Angular: 15.2.10
... animations, common, compiler, compiler-cli, core, forms
... platform-browser, platform-browser-dynamic, router

Package                         Version
---------------------------------------------------------
@angular-devkit/architect       0.1502.11
@angular-devkit/build-angular   15.2.11
@angular-devkit/core            15.2.11
@angular-devkit/schematics      15.2.11
@angular/cli                    15.2.11
@schematics/angular             15.2.11
rxjs                            7.8.1
typescript                      4.9.5



Backend
========
.Net 8




Sscript criacao banco de dados
===============================
SQL SERVER

USE [Livros]
GO
/****** Object:  Table [dbo].[Autor]    Script Date: 20/02/2025 02:23:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Autor](
	[CodAu] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](40) NULL,
 CONSTRAINT [PK_Autor] PRIMARY KEY CLUSTERED 
(
	[CodAu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Assunto]    Script Date: 20/02/2025 02:23:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assunto](
	[CodAs] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](40) NULL,
 CONSTRAINT [PK_Assunto] PRIMARY KEY CLUSTERED 
(
	[CodAs] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Livro]    Script Date: 20/02/2025 02:23:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Livro](
	[Codi] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [varchar](40) NULL,
	[Editora] [varchar](40) NULL,
	[AnoPublicacao] [varchar](4) NULL,
	[Valor] [money] NULL,
 CONSTRAINT [PK_Livro] PRIMARY KEY CLUSTERED 
(
	[Codi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Livro_Autor]    Script Date: 20/02/2025 02:23:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Livro_Autor](
	[Livro_Codi_Au] [int] NOT NULL,
	[Autor_CodAu] [int] NOT NULL,
 CONSTRAINT [PK_Livro_Autor] PRIMARY KEY CLUSTERED 
(
	[Livro_Codi_Au] ASC,
	[Autor_CodAu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Livro_Assunto]    Script Date: 20/02/2025 02:23:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Livro_Assunto](
	[Livro_Codi_As] [int] NOT NULL,
	[Assunto_CodAs] [int] NOT NULL,
 CONSTRAINT [PK_Livro_Assunto] PRIMARY KEY CLUSTERED 
(
	[Livro_Codi_As] ASC,
	[Assunto_CodAs] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[RelatorioLivros]    Script Date: 20/02/2025 02:23:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[RelatorioLivros] AS
SELECT 
    l.Codi AS LivroID,
    l.Titulo AS Titulo,
    a.Nome AS Autor,
    ass.Descricao AS Assuntos
FROM 
    Livro l
JOIN 
    Livro_Autor la ON l.Codi = la.Livro_Codi_Au
JOIN 
    Autor a ON la.Autor_CodAu = a.CodAu
JOIN 
    Livro_Assunto las ON l.Codi = las.Livro_Codi_As
JOIN 
    Assunto ass ON las.Assunto_CodAs = ass.CodAs
GROUP BY 
    l.Codi, l.Titulo, a.Nome, ass.Descricao;
GO
ALTER TABLE [dbo].[Livro_Assunto]  WITH CHECK ADD  CONSTRAINT [FK_Livro_Assunto_Assunto] FOREIGN KEY([Assunto_CodAs])
REFERENCES [dbo].[Assunto] ([CodAs])
GO
ALTER TABLE [dbo].[Livro_Assunto] CHECK CONSTRAINT [FK_Livro_Assunto_Assunto]
GO
ALTER TABLE [dbo].[Livro_Assunto]  WITH CHECK ADD  CONSTRAINT [FK_Livro_Assunto_Livro] FOREIGN KEY([Livro_Codi_As])
REFERENCES [dbo].[Livro] ([Codi])
GO
ALTER TABLE [dbo].[Livro_Assunto] CHECK CONSTRAINT [FK_Livro_Assunto_Livro]
GO
ALTER TABLE [dbo].[Livro_Autor]  WITH CHECK ADD  CONSTRAINT [FK_Livro_Autor_Autor] FOREIGN KEY([Autor_CodAu])
REFERENCES [dbo].[Autor] ([CodAu])
GO
ALTER TABLE [dbo].[Livro_Autor] CHECK CONSTRAINT [FK_Livro_Autor_Autor]
GO
ALTER TABLE [dbo].[Livro_Autor]  WITH CHECK ADD  CONSTRAINT [FK_Livro_Autor_Livro] FOREIGN KEY([Livro_Codi_Au])
REFERENCES [dbo].[Livro] ([Codi])
GO
ALTER TABLE [dbo].[Livro_Autor] CHECK CONSTRAINT [FK_Livro_Autor_Livro]
GO




String conexao banco de dados
=============================
    "DefaultConnection": "Server=DESKTOP-2KLC0BD\\SQLEXPRESS;Database=livros;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;"
precisa ser alterada de acordo com o banco a ser utilizado.


