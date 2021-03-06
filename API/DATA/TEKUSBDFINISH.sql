USE [TekusDB]
GO
/****** Object:  Table [dbo].[Provider]    Script Date: 1/2/2022 4:17:38 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Provider](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NIT] [nvarchar](15) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Provider] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProviderDetail]    Script Date: 1/2/2022 4:17:38 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProviderDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IDProvider] [int] NOT NULL,
	[RowName] [nvarchar](50) NOT NULL,
	[RowValue] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ProviderDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProviderService]    Script Date: 1/2/2022 4:17:38 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProviderService](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IDProvider] [int] NOT NULL,
	[IDService] [int] NOT NULL,
	[PriceHour] [money] NOT NULL,
	[CountryISO] [nchar](3) NULL,
 CONSTRAINT [PK_ProviderServices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Service]    Script Date: 1/2/2022 4:17:38 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](300) NOT NULL,
 CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 1/2/2022 4:17:38 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](30) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Provider] ON 

INSERT [dbo].[Provider] ([ID], [NIT], [Name], [Email]) VALUES (18, N'1112223333', N'Importaciones Tekus S.A.', N'Jaime.marin@tekus.co')
INSERT [dbo].[Provider] ([ID], [NIT], [Name], [Email]) VALUES (19, N'222.111.333-1', N'VanguarSoft C.A', N'jleal@vanguarsoft.com.ve')
INSERT [dbo].[Provider] ([ID], [NIT], [Name], [Email]) VALUES (20, N'444.111.333-5', N'Microsoft', N'jleal@microsoft.com')
INSERT [dbo].[Provider] ([ID], [NIT], [Name], [Email]) VALUES (21, N'444.222.333-1', N'Meta', N'jleal@meta.com')
INSERT [dbo].[Provider] ([ID], [NIT], [Name], [Email]) VALUES (22, N'444.555.333-0', N'Traetelo', N'jleal@traetelo.com')
INSERT [dbo].[Provider] ([ID], [NIT], [Name], [Email]) VALUES (23, N'444.555.222-1', N'Quantic Vision S.A', N'jleal@quanticvision.com')
INSERT [dbo].[Provider] ([ID], [NIT], [Name], [Email]) VALUES (24, N'111.555.111-4', N'Student Center C.A', N'jleal@studentcenter.com')
INSERT [dbo].[Provider] ([ID], [NIT], [Name], [Email]) VALUES (25, N'666.555.111-8', N'Inversiones Aidix S.A', N'jleal@aidix.com')
INSERT [dbo].[Provider] ([ID], [NIT], [Name], [Email]) VALUES (26, N'666.555.777-2', N'Amazon', N'jleal@amazon.com')
INSERT [dbo].[Provider] ([ID], [NIT], [Name], [Email]) VALUES (27, N'888.555.777-8', N'Atlasian Jira', N'jleal@jira.com')
SET IDENTITY_INSERT [dbo].[Provider] OFF
GO
SET IDENTITY_INSERT [dbo].[ProviderDetail] ON 

INSERT [dbo].[ProviderDetail] ([Id], [IDProvider], [RowName], [RowValue]) VALUES (3, 19, N'Teléfono contacto', N'04122017600')
INSERT [dbo].[ProviderDetail] ([Id], [IDProvider], [RowName], [RowValue]) VALUES (4, 19, N'Nombre del presidente', N'Olgeddie Ferrer')
INSERT [dbo].[ProviderDetail] ([Id], [IDProvider], [RowName], [RowValue]) VALUES (8, 24, N'Nombre de la madre', N'Sonia')
INSERT [dbo].[ProviderDetail] ([Id], [IDProvider], [RowName], [RowValue]) VALUES (9, 18, N'Número de contacto en marte', N'4')
INSERT [dbo].[ProviderDetail] ([Id], [IDProvider], [RowName], [RowValue]) VALUES (10, 18, N'Cantidad de mascotas en la nómina', N'10')
SET IDENTITY_INSERT [dbo].[ProviderDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[ProviderService] ON 

INSERT [dbo].[ProviderService] ([Id], [IDProvider], [IDService], [PriceHour], [CountryISO]) VALUES (1, 18, 1, 2.0000, N'VEN')
INSERT [dbo].[ProviderService] ([Id], [IDProvider], [IDService], [PriceHour], [CountryISO]) VALUES (3, 18, 5, 1.0000, N'VEN')
INSERT [dbo].[ProviderService] ([Id], [IDProvider], [IDService], [PriceHour], [CountryISO]) VALUES (4, 19, 5, 2.0000, N'VEN')
INSERT [dbo].[ProviderService] ([Id], [IDProvider], [IDService], [PriceHour], [CountryISO]) VALUES (5, 19, 3, 100.0000, N'VEN')
INSERT [dbo].[ProviderService] ([Id], [IDProvider], [IDService], [PriceHour], [CountryISO]) VALUES (6, 24, 1, 2.0000, N'ABW')
SET IDENTITY_INSERT [dbo].[ProviderService] OFF
GO
SET IDENTITY_INSERT [dbo].[Service] ON 

INSERT [dbo].[Service] ([Id], [Name], [Description]) VALUES (1, N'Descarga espacial de contenidos', N'Descarga espacial de contenidos')
INSERT [dbo].[Service] ([Id], [Name], [Description]) VALUES (2, N'Desaparición  forzada de bytes', N'Desaparición  forzada de bytes')
INSERT [dbo].[Service] ([Id], [Name], [Description]) VALUES (3, N'Recuperación psicológica de Delete sin Where', N'Recuperación psicológica de Delete sin Where')
INSERT [dbo].[Service] ([Id], [Name], [Description]) VALUES (4, N'Convertidor de divisas', N'Convertidor de divisas')
INSERT [dbo].[Service] ([Id], [Name], [Description]) VALUES (5, N'Facturación electrónica', N'Facturación electrónica')
INSERT [dbo].[Service] ([Id], [Name], [Description]) VALUES (6, N'Componentes personalizados React O Vue', N'Componentes personalizados React O Vue 2')
INSERT [dbo].[Service] ([Id], [Name], [Description]) VALUES (7, N'Consultoría Backend', N'Consultoría Backend')
INSERT [dbo].[Service] ([Id], [Name], [Description]) VALUES (8, N'En mi local si funciona', N'En mi local si funciona')
INSERT [dbo].[Service] ([Id], [Name], [Description]) VALUES (9, N'Generador de certificado SSL Let''s Encrypt', N'Generador de certificado SSL Let''s Encrypt')
INSERT [dbo].[Service] ([Id], [Name], [Description]) VALUES (10, N'SprintBoot para microservicios en JAVA', N'SprintBoot para microservicios en JAVA')
SET IDENTITY_INSERT [dbo].[Service] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([ID], [UserName], [Password]) VALUES (1, N'JLEAL', N'12345678')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
