USE PrototypeMainDB
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 18/07/2021 12:18:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OccupationRating]    Script Date: 18/07/2021 12:18:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OccupationRating](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Occupation] [nvarchar](max) NULL,
	[Rating] [nvarchar](max) NULL,
 CONSTRAINT [PK_OccupationRating] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 18/07/2021 10:30:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NULL,
	[Age] [decimal](18, 2) NULL,
	[DateOfBirth] [datetime] NULL,
	[Occupation] [varchar](1000) NULL,
	[SumInsured] [decimal](18, 2) NULL,
	[MonthlyExpensesTotal] [decimal](18, 2) NULL,
	[State] [varchar](100) NULL,
	[Postcode] [int] NULL	
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RatingFactor]    Script Date: 18/07/2021 12:18:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RatingFactor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Rating] [varchar](500) NULL,
	[Factor] [decimal](18, 2) NULL
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE PrototypeMainDB SET  READ_WRITE 
GO
