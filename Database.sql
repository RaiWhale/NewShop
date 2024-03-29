USE [master]
GO
/****** Object:  Database [NewShop]    Script Date: 11/12/2018 05:00:02 PM ******/
CREATE DATABASE [NewShop]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NewShop', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\NewShop.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'NewShop_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\NewShop_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [NewShop] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NewShop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NewShop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NewShop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NewShop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NewShop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NewShop] SET ARITHABORT OFF 
GO
ALTER DATABASE [NewShop] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NewShop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NewShop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NewShop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NewShop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NewShop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NewShop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NewShop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NewShop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NewShop] SET  DISABLE_BROKER 
GO
ALTER DATABASE [NewShop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NewShop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NewShop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NewShop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NewShop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NewShop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NewShop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NewShop] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [NewShop] SET  MULTI_USER 
GO
ALTER DATABASE [NewShop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NewShop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NewShop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NewShop] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [NewShop] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [NewShop] SET QUERY_STORE = OFF
GO
USE [NewShop]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [NewShop]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 11/12/2018 05:00:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](100) NOT NULL,
	[ParentId] [int] NULL,
	[TopicId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 11/12/2018 05:00:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerName] [nvarchar](200) NOT NULL,
	[Gender] [tinyint] NOT NULL,
	[Birthday] [datetime] NULL,
	[Phone] [varchar](30) NULL,
	[Email] [nvarchar](150) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[Avatar] [varchar](max) NULL,
	[Address] [nvarchar](250) NULL,
	[CreatedDate] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InputDetails]    Script Date: 11/12/2018 05:00:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InputDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InputId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Unit] [nvarchar](20) NOT NULL,
	[Quantity] [decimal](18, 0) NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[Discount] [decimal](18, 0) NOT NULL,
	[Tax] [decimal](18, 0) NOT NULL,
	[Note] [nvarchar](150) NULL,
 CONSTRAINT [PK_InputDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inputs]    Script Date: 11/12/2018 05:00:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inputs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InputCode] [varchar](20) NULL,
	[InputDate] [datetime] NOT NULL,
	[UserId] [int] NOT NULL,
	[SupplierId] [int] NOT NULL,
	[Discount] [decimal](18, 0) NOT NULL,
	[Tax] [decimal](18, 0) NOT NULL,
	[Status] [tinyint] NOT NULL,
 CONSTRAINT [PK_Input] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 11/12/2018 05:00:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Unit] [nvarchar](20) NOT NULL,
	[Quantity] [decimal](18, 0) NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[Discount] [decimal](18, 0) NOT NULL,
	[Tax] [decimal](18, 0) NOT NULL,
	[Note] [nvarchar](150) NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 11/12/2018 05:00:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderCode] [varchar](20) NULL,
	[OrderDate] [datetime] NOT NULL,
	[UserId] [int] NULL,
	[CustomerId] [int] NOT NULL,
	[Discount] [decimal](18, 0) NOT NULL,
	[Tax] [decimal](18, 0) NOT NULL,
	[Status] [tinyint] NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pictures]    Script Date: 11/12/2018 05:00:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pictures](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[Url] [varchar](150) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 11/12/2018 05:00:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BarCode] [varchar](50) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[ProductName] [nvarchar](150) NOT NULL,
	[Unit] [nvarchar](20) NULL,
	[InputPrice] [decimal](18, 0) NOT NULL,
	[OutputPrice] [decimal](18, 0) NOT NULL,
	[PictureId] [int] NULL,
	[Discount] [decimal](5, 2) NOT NULL,
	[Description] [ntext] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Suppliers]    Script Date: 11/12/2018 05:00:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suppliers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SupplierName] [nvarchar](200) NOT NULL,
	[Phone] [varchar](30) NULL,
	[Email] [nvarchar](150) NULL,
	[Address] [nvarchar](250) NULL,
	[Description] [nvarchar](250) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Suppliers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Table]    Script Date: 11/12/2018 05:00:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Table](
	[Id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Topics]    Script Date: 11/12/2018 05:00:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Topics](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TopicName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Topics] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLevelPermissions]    Script Date: 11/12/2018 05:00:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLevelPermissions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserLevelId] [int] NOT NULL,
	[ControllerName] [varchar](100) NOT NULL,
	[PermissionCode] [tinyint] NOT NULL,
 CONSTRAINT [PK_LevelPermisions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLevels]    Script Date: 11/12/2018 05:00:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLevels](
	[Id] [int] NOT NULL,
	[UserLevelName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_UserLevels] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/12/2018 05:00:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LoginName] [nvarchar](50) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[FullName] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[Email] [nvarchar](150) NULL,
	[Address] [nvarchar](250) NULL,
	[Avatar] [nvarchar](250) NULL,
	[UserLevelId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[ResetPasswordToken] [varchar](100) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [CategoryName], [ParentId], [TopicId], [UserId]) VALUES (4, N'Smart phones', 0, 1, 5)
INSERT [dbo].[Categories] ([Id], [CategoryName], [ParentId], [TopicId], [UserId]) VALUES (5, N'Android Mobiles', 4, 1, 5)
INSERT [dbo].[Categories] ([Id], [CategoryName], [ParentId], [TopicId], [UserId]) VALUES (6, N'Windows Mobiles', 4, 1, 5)
INSERT [dbo].[Categories] ([Id], [CategoryName], [ParentId], [TopicId], [UserId]) VALUES (7, N'Refurbished Mobiles', 4, 1, 5)
INSERT [dbo].[Categories] ([Id], [CategoryName], [ParentId], [TopicId], [UserId]) VALUES (8, N'All Mobile Accessories', 4, 1, 5)
INSERT [dbo].[Categories] ([Id], [CategoryName], [ParentId], [TopicId], [UserId]) VALUES (10, N'Smart notebooks', 0, 2, 5)
INSERT [dbo].[Categories] ([Id], [CategoryName], [ParentId], [TopicId], [UserId]) VALUES (11, N'Android Note Book', 10, 2, 5)
INSERT [dbo].[Categories] ([Id], [CategoryName], [ParentId], [TopicId], [UserId]) VALUES (12, N'Windows Note Books', 10, 2, 5)
INSERT [dbo].[Categories] ([Id], [CategoryName], [ParentId], [TopicId], [UserId]) VALUES (13, N'Refurbished Note Books', 10, 2, 5)
INSERT [dbo].[Categories] ([Id], [CategoryName], [ParentId], [TopicId], [UserId]) VALUES (14, N'Note Books Accessories', 10, 2, 5)
INSERT [dbo].[Categories] ([Id], [CategoryName], [ParentId], [TopicId], [UserId]) VALUES (16, N'Smart tablets', 0, 3, 5)
INSERT [dbo].[Categories] ([Id], [CategoryName], [ParentId], [TopicId], [UserId]) VALUES (17, N'Android Tablets', 17, 3, 5)
INSERT [dbo].[Categories] ([Id], [CategoryName], [ParentId], [TopicId], [UserId]) VALUES (18, N'Windows Tablets', 17, 3, 5)
INSERT [dbo].[Categories] ([Id], [CategoryName], [ParentId], [TopicId], [UserId]) VALUES (19, N'Refurbished Tablets', 17, 3, 5)
INSERT [dbo].[Categories] ([Id], [CategoryName], [ParentId], [TopicId], [UserId]) VALUES (20, N'Tablets Accessories', 17, 3, 5)
INSERT [dbo].[Categories] ([Id], [CategoryName], [ParentId], [TopicId], [UserId]) VALUES (1003, N'HDD', 1002, 4, 5)
INSERT [dbo].[Categories] ([Id], [CategoryName], [ParentId], [TopicId], [UserId]) VALUES (1004, N'RAM', 1002, 4, 5)
INSERT [dbo].[Categories] ([Id], [CategoryName], [ParentId], [TopicId], [UserId]) VALUES (1005, N'MAINBOARDS', 1002, 4, 5)
INSERT [dbo].[Categories] ([Id], [CategoryName], [ParentId], [TopicId], [UserId]) VALUES (1006, N'VGA', 1002, 4, 5)
INSERT [dbo].[Categories] ([Id], [CategoryName], [ParentId], [TopicId], [UserId]) VALUES (1007, N'Case', 1002, 4, 5)
INSERT [dbo].[Categories] ([Id], [CategoryName], [ParentId], [TopicId], [UserId]) VALUES (1008, N'Monitor', 5, 4, 5)
INSERT [dbo].[Categories] ([Id], [CategoryName], [ParentId], [TopicId], [UserId]) VALUES (1010, N'SSD', NULL, 4, 5)
INSERT [dbo].[Categories] ([Id], [CategoryName], [ParentId], [TopicId], [UserId]) VALUES (1011, N'CHIP', NULL, 4, 5)
SET IDENTITY_INSERT [dbo].[Categories] OFF
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([Id], [CustomerName], [Gender], [Birthday], [Phone], [Email], [Password], [Avatar], [Address], [CreatedDate], [IsActive]) VALUES (2, N'Le Hoang Hai', 2, CAST(N'2018-09-14T00:00:00.000' AS DateTime), N'123456', N'lehoanghai22@gmail.com', N'ec278a38901287b2771a13739520384d43e4b078f78affe702def108774cce24', N'', N'ddsadsadsadsadsa', NULL, 1)
INSERT [dbo].[Customers] ([Id], [CustomerName], [Gender], [Birthday], [Phone], [Email], [Password], [Avatar], [Address], [CreatedDate], [IsActive]) VALUES (3, N'Nguyen Tien Hai', 1, CAST(N'2018-08-14T00:00:00.000' AS DateTime), N'123456', N'tienhai@gmail.com', N'ec278a38901287b2771a13739520384d43e4b078f78affe702def108774cce24', NULL, N'11/3 Dong Da', NULL, 1)
INSERT [dbo].[Customers] ([Id], [CustomerName], [Gender], [Birthday], [Phone], [Email], [Password], [Avatar], [Address], [CreatedDate], [IsActive]) VALUES (4, N'Le Thanh Binh', 1, CAST(N'1987-01-01T00:00:00.000' AS DateTime), N'123456', N'binhlt@gmail.com', N'ec278a38901287b2771a13739520384d43e4b078f78affe702def108774cce24', NULL, N'65 Yen Bai', NULL, 1)
INSERT [dbo].[Customers] ([Id], [CustomerName], [Gender], [Birthday], [Phone], [Email], [Password], [Avatar], [Address], [CreatedDate], [IsActive]) VALUES (1002, N'tomcruise', 1, CAST(N'1960-05-05T00:00:00.000' AS DateTime), N'090X.XYZ.XYZ', N'tomcruise@gmail.com', N'ec278a38901287b2771a13739520384d43e4b078f78affe702def108774cce24', NULL, N'Ha Noi', NULL, 1)
INSERT [dbo].[Customers] ([Id], [CustomerName], [Gender], [Birthday], [Phone], [Email], [Password], [Avatar], [Address], [CreatedDate], [IsActive]) VALUES (1003, N'tomhank', 1, CAST(N'1950-06-06T00:00:00.000' AS DateTime), N'090X.XYZ.XYZ', N'tomhank@gmail.com', N'ec278a38901287b2771a13739520384d43e4b078f78affe702def108774cce24', NULL, N'Da Nang', NULL, 1)
INSERT [dbo].[Customers] ([Id], [CustomerName], [Gender], [Birthday], [Phone], [Email], [Password], [Avatar], [Address], [CreatedDate], [IsActive]) VALUES (1004, N'peterjackson', 1, CAST(N'1950-06-06T00:00:00.000' AS DateTime), N'090X.XYZ.XYZ', N'peterjackson@gmail.com', N'ec278a38901287b2771a13739520384d43e4b078f78affe702def108774cce24', NULL, N'Hue', NULL, 1)
INSERT [dbo].[Customers] ([Id], [CustomerName], [Gender], [Birthday], [Phone], [Email], [Password], [Avatar], [Address], [CreatedDate], [IsActive]) VALUES (1018, N'123', 2, NULL, N'21321321321', N'asdas@gmail.com', N'', NULL, N'ádsa', NULL, 0)
SET IDENTITY_INSERT [dbo].[Customers] OFF
SET IDENTITY_INSERT [dbo].[OrderDetails] ON 

INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [Unit], [Quantity], [Price], [Discount], [Tax], [Note]) VALUES (1, 1, 10, N'Cai', CAST(10 AS Decimal(18, 0)), CAST(300 AS Decimal(18, 0)), CAST(5 AS Decimal(18, 0)), CAST(5 AS Decimal(18, 0)), NULL)
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [Unit], [Quantity], [Price], [Discount], [Tax], [Note]) VALUES (2, 1, 11, N'Cai', CAST(15 AS Decimal(18, 0)), CAST(300 AS Decimal(18, 0)), CAST(5 AS Decimal(18, 0)), CAST(5 AS Decimal(18, 0)), NULL)
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [Unit], [Quantity], [Price], [Discount], [Tax], [Note]) VALUES (3, 1, 12, N'Cai', CAST(20 AS Decimal(18, 0)), CAST(300 AS Decimal(18, 0)), CAST(5 AS Decimal(18, 0)), CAST(5 AS Decimal(18, 0)), NULL)
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [Unit], [Quantity], [Price], [Discount], [Tax], [Note]) VALUES (4, 2, 13, N'Cai', CAST(10 AS Decimal(18, 0)), CAST(100 AS Decimal(18, 0)), CAST(10 AS Decimal(18, 0)), CAST(10 AS Decimal(18, 0)), NULL)
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [Unit], [Quantity], [Price], [Discount], [Tax], [Note]) VALUES (5, 2, 14, N'Cai', CAST(15 AS Decimal(18, 0)), CAST(250 AS Decimal(18, 0)), CAST(10 AS Decimal(18, 0)), CAST(10 AS Decimal(18, 0)), NULL)
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [Unit], [Quantity], [Price], [Discount], [Tax], [Note]) VALUES (6, 2, 15, N'Cai', CAST(20 AS Decimal(18, 0)), CAST(400 AS Decimal(18, 0)), CAST(10 AS Decimal(18, 0)), CAST(10 AS Decimal(18, 0)), NULL)
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [Unit], [Quantity], [Price], [Discount], [Tax], [Note]) VALUES (7, 3, 16, N'Cai', CAST(10 AS Decimal(18, 0)), CAST(350 AS Decimal(18, 0)), CAST(5 AS Decimal(18, 0)), CAST(5 AS Decimal(18, 0)), NULL)
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [Unit], [Quantity], [Price], [Discount], [Tax], [Note]) VALUES (8, 3, 17, N'Cai', CAST(15 AS Decimal(18, 0)), CAST(250 AS Decimal(18, 0)), CAST(5 AS Decimal(18, 0)), CAST(5 AS Decimal(18, 0)), NULL)
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [Unit], [Quantity], [Price], [Discount], [Tax], [Note]) VALUES (9, 3, 18, N'Cai', CAST(20 AS Decimal(18, 0)), CAST(150 AS Decimal(18, 0)), CAST(5 AS Decimal(18, 0)), CAST(5 AS Decimal(18, 0)), NULL)
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [Unit], [Quantity], [Price], [Discount], [Tax], [Note]) VALUES (10, 1007, 32, N'cái', CAST(1 AS Decimal(18, 0)), CAST(43990000 AS Decimal(18, 0)), CAST(50 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), N'')
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [Unit], [Quantity], [Price], [Discount], [Tax], [Note]) VALUES (13, 1024, 32, N'cái', CAST(1 AS Decimal(18, 0)), CAST(43990000 AS Decimal(18, 0)), CAST(50 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), N'')
SET IDENTITY_INSERT [dbo].[OrderDetails] OFF
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([Id], [OrderCode], [OrderDate], [UserId], [CustomerId], [Discount], [Tax], [Status]) VALUES (1, N'123456', CAST(N'2018-09-12T00:00:01.000' AS DateTime), 1, 4, CAST(10 AS Decimal(18, 0)), CAST(10 AS Decimal(18, 0)), 0)
INSERT [dbo].[Orders] ([Id], [OrderCode], [OrderDate], [UserId], [CustomerId], [Discount], [Tax], [Status]) VALUES (2, N'914180', CAST(N'2018-09-12T10:30:54.000' AS DateTime), 1, 3, CAST(20 AS Decimal(18, 0)), CAST(30 AS Decimal(18, 0)), 0)
INSERT [dbo].[Orders] ([Id], [OrderCode], [OrderDate], [UserId], [CustomerId], [Discount], [Tax], [Status]) VALUES (3, N'534035', CAST(N'2018-09-09T00:00:00.000' AS DateTime), 1, 2, CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), 0)
INSERT [dbo].[Orders] ([Id], [OrderCode], [OrderDate], [UserId], [CustomerId], [Discount], [Tax], [Status]) VALUES (1003, N'456123', CAST(N'2018-09-04T00:00:00.000' AS DateTime), 1, 2, CAST(10 AS Decimal(18, 0)), CAST(10 AS Decimal(18, 0)), 1)
INSERT [dbo].[Orders] ([Id], [OrderCode], [OrderDate], [UserId], [CustomerId], [Discount], [Tax], [Status]) VALUES (1004, N'789123', CAST(N'2018-07-04T00:00:00.000' AS DateTime), 1, 3, CAST(10 AS Decimal(18, 0)), CAST(10 AS Decimal(18, 0)), 1)
INSERT [dbo].[Orders] ([Id], [OrderCode], [OrderDate], [UserId], [CustomerId], [Discount], [Tax], [Status]) VALUES (1005, N'123789', CAST(N'2018-07-04T00:00:00.000' AS DateTime), 1, 4, CAST(10 AS Decimal(18, 0)), CAST(10 AS Decimal(18, 0)), 1)
INSERT [dbo].[Orders] ([Id], [OrderCode], [OrderDate], [UserId], [CustomerId], [Discount], [Tax], [Status]) VALUES (1006, N'258369', CAST(N'2018-06-04T00:00:00.000' AS DateTime), 1, 2, CAST(10 AS Decimal(18, 0)), CAST(10 AS Decimal(18, 0)), 1)
INSERT [dbo].[Orders] ([Id], [OrderCode], [OrderDate], [UserId], [CustomerId], [Discount], [Tax], [Status]) VALUES (1007, N'718409', CAST(N'2018-11-12T16:06:44.890' AS DateTime), NULL, 1018, CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), 1)
INSERT [dbo].[Orders] ([Id], [OrderCode], [OrderDate], [UserId], [CustomerId], [Discount], [Tax], [Status]) VALUES (1024, N'228337', CAST(N'2018-11-12T16:55:24.397' AS DateTime), NULL, 2, CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), 1)
SET IDENTITY_INSERT [dbo].[Orders] OFF
SET IDENTITY_INSERT [dbo].[Pictures] ON 

INSERT [dbo].[Pictures] ([Id], [ProductId], [Url]) VALUES (52, 28, N'636765051871789515_4.jpg')
INSERT [dbo].[Pictures] ([Id], [ProductId], [Url]) VALUES (54, 29, N'636765055559810838_1.png')
INSERT [dbo].[Pictures] ([Id], [ProductId], [Url]) VALUES (55, 30, N'636765057334705783_2.png')
INSERT [dbo].[Pictures] ([Id], [ProductId], [Url]) VALUES (64, 31, N'636765061967693891_3.jpg')
INSERT [dbo].[Pictures] ([Id], [ProductId], [Url]) VALUES (72, 10, N'636765074049515347_i3321.png')
INSERT [dbo].[Pictures] ([Id], [ProductId], [Url]) VALUES (73, 14, N'636765076441201055_ssd.png')
INSERT [dbo].[Pictures] ([Id], [ProductId], [Url]) VALUES (75, 11, N'636765078931562955_i5.png')
INSERT [dbo].[Pictures] ([Id], [ProductId], [Url]) VALUES (77, 15, N'636765084877180655_ssd480.png')
INSERT [dbo].[Pictures] ([Id], [ProductId], [Url]) VALUES (78, 12, N'636765089304255468_xs.png')
INSERT [dbo].[Pictures] ([Id], [ProductId], [Url]) VALUES (79, 13, N'636765090053329204_xs.png')
INSERT [dbo].[Pictures] ([Id], [ProductId], [Url]) VALUES (80, 32, N'636765090599643023_xs.png')
SET IDENTITY_INSERT [dbo].[Pictures] OFF
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [BarCode], [CategoryId], [ProductName], [Unit], [InputPrice], [OutputPrice], [PictureId], [Discount], [Description], [IsActive]) VALUES (10, N'159753', 1011, N'CPU Intel Core i3-8350K', N'cái', CAST(4000000 AS Decimal(18, 0)), CAST(4489000 AS Decimal(18, 0)), NULL, CAST(50.00 AS Decimal(5, 2)), N'4Ghz / 8MB / 4 Cores, 4 Threads / Socket 1151 v2 (Coffee Lake )', 1)
INSERT [dbo].[Products] ([Id], [BarCode], [CategoryId], [ProductName], [Unit], [InputPrice], [OutputPrice], [PictureId], [Discount], [Description], [IsActive]) VALUES (11, N'753159', 1011, N'CPU Intel Core i5 8600K', N'cái', CAST(7099000 AS Decimal(18, 0)), CAST(7099000 AS Decimal(18, 0)), NULL, CAST(60.00 AS Decimal(5, 2)), N'3.6Ghz Turbo Up to 4.3Ghz / 9MB / 6 Cores, 6 Threads / Socket 1151 v2 (Coffee Lake )', 1)
INSERT [dbo].[Products] ([Id], [BarCode], [CategoryId], [ProductName], [Unit], [InputPrice], [OutputPrice], [PictureId], [Discount], [Description], [IsActive]) VALUES (12, N'789456', 4, N'Apple iPhone Xs Max 256GB', NULL, CAST(37000000 AS Decimal(18, 0)), CAST(37990000 AS Decimal(18, 0)), NULL, CAST(30.00 AS Decimal(5, 2)), N'Apple', 1)
INSERT [dbo].[Products] ([Id], [BarCode], [CategoryId], [ProductName], [Unit], [InputPrice], [OutputPrice], [PictureId], [Discount], [Description], [IsActive]) VALUES (13, N'456123', 4, N'Apple iPhone Xs Max 64GB', N'cái', CAST(33000000 AS Decimal(18, 0)), CAST(33990000 AS Decimal(18, 0)), NULL, CAST(20.00 AS Decimal(5, 2)), N'Apple', 1)
INSERT [dbo].[Products] ([Id], [BarCode], [CategoryId], [ProductName], [Unit], [InputPrice], [OutputPrice], [PictureId], [Discount], [Description], [IsActive]) VALUES (14, N'789123', 1010, N'SSD Samsung 860 EVO 250GB', N'cái', CAST(2000000 AS Decimal(18, 0)), CAST(2090000 AS Decimal(18, 0)), NULL, CAST(50.00 AS Decimal(5, 2)), N'SATA3 6Gb/s 2.5" ( Đọc 550MB/s, Ghi 520MB/s)', 1)
INSERT [dbo].[Products] ([Id], [BarCode], [CategoryId], [ProductName], [Unit], [InputPrice], [OutputPrice], [PictureId], [Discount], [Description], [IsActive]) VALUES (15, N'123789', 1010, N'SSD Corsair Force LE 480GB', N'cái', CAST(4000000 AS Decimal(18, 0)), CAST(4099000 AS Decimal(18, 0)), NULL, CAST(70.00 AS Decimal(5, 2)), N'SATA3 6Gb/s 2.5" (Doc 560MB/s, Ghi 530MB/s)', 1)
INSERT [dbo].[Products] ([Id], [BarCode], [CategoryId], [ProductName], [Unit], [InputPrice], [OutputPrice], [PictureId], [Discount], [Description], [IsActive]) VALUES (16, N'147258', 1004, N'16GB 1600MHz', NULL, CAST(300 AS Decimal(18, 0)), CAST(350 AS Decimal(18, 0)), NULL, CAST(80.00 AS Decimal(5, 2)), N'', 1)
INSERT [dbo].[Products] ([Id], [BarCode], [CategoryId], [ProductName], [Unit], [InputPrice], [OutputPrice], [PictureId], [Discount], [Description], [IsActive]) VALUES (17, N'258369', 1004, N'8GB 1600MHz', NULL, CAST(200 AS Decimal(18, 0)), CAST(250 AS Decimal(18, 0)), NULL, CAST(75.00 AS Decimal(5, 2)), N'', 1)
INSERT [dbo].[Products] ([Id], [BarCode], [CategoryId], [ProductName], [Unit], [InputPrice], [OutputPrice], [PictureId], [Discount], [Description], [IsActive]) VALUES (18, N'147369', 1004, N'4GB 1600MHz', NULL, CAST(100 AS Decimal(18, 0)), CAST(150 AS Decimal(18, 0)), NULL, CAST(35.00 AS Decimal(5, 2)), N'', 1)
INSERT [dbo].[Products] ([Id], [BarCode], [CategoryId], [ProductName], [Unit], [InputPrice], [OutputPrice], [PictureId], [Discount], [Description], [IsActive]) VALUES (28, N'716190', 12, N'ASUS FX504GD-E4177T ', N'cái', CAST(18990000 AS Decimal(18, 0)), CAST(18990000 AS Decimal(18, 0)), NULL, CAST(0.00 AS Decimal(5, 2)), N'i5-8300HQ 8GB 1TB GTX1050 2GB Win10 (VS)', 1)
INSERT [dbo].[Products] ([Id], [BarCode], [CategoryId], [ProductName], [Unit], [InputPrice], [OutputPrice], [PictureId], [Discount], [Description], [IsActive]) VALUES (29, N'681366', 12, N'LENOVO IdeaPad Y520-15IKBN 80WK01GEVN ', N'cái', CAST(20990000 AS Decimal(18, 0)), CAST(20990000 AS Decimal(18, 0)), NULL, CAST(10.00 AS Decimal(5, 2)), N'i7-7700HQ 8GB 1TB GTX1050 4GB Win10 (FPT)', 1)
INSERT [dbo].[Products] ([Id], [BarCode], [CategoryId], [ProductName], [Unit], [InputPrice], [OutputPrice], [PictureId], [Discount], [Description], [IsActive]) VALUES (30, N'171932', 12, N'DELL G7 N7588F ', N'1', CAST(27100000 AS Decimal(18, 0)), CAST(27100000 AS Decimal(18, 0)), NULL, CAST(0.00 AS Decimal(5, 2)), N'i7-8750H 8GB 1TB GTX1050Ti 4GB (VS)', 1)
INSERT [dbo].[Products] ([Id], [BarCode], [CategoryId], [ProductName], [Unit], [InputPrice], [OutputPrice], [PictureId], [Discount], [Description], [IsActive]) VALUES (31, N'611101', 12, N'ASUS UX391UA-EG030T', N'cái', CAST(35990000 AS Decimal(18, 0)), CAST(35990000 AS Decimal(18, 0)), NULL, CAST(0.00 AS Decimal(5, 2)), N'i7-8550U 8GB SSD512GB Win10 (FPT)', 1)
INSERT [dbo].[Products] ([Id], [BarCode], [CategoryId], [ProductName], [Unit], [InputPrice], [OutputPrice], [PictureId], [Discount], [Description], [IsActive]) VALUES (32, N'342262', 4, N'Apple iPhone Xs Max 512GB', N'cái', CAST(43000000 AS Decimal(18, 0)), CAST(43990000 AS Decimal(18, 0)), NULL, CAST(50.00 AS Decimal(5, 2)), N'Apple', 1)
SET IDENTITY_INSERT [dbo].[Products] OFF
SET IDENTITY_INSERT [dbo].[Suppliers] ON 

INSERT [dbo].[Suppliers] ([Id], [SupplierName], [Phone], [Email], [Address], [Description], [IsActive]) VALUES (1, N'Sony', N'090X.XYZ.XYZ', N'sony@gmail.com', N'Japan', NULL, 1)
INSERT [dbo].[Suppliers] ([Id], [SupplierName], [Phone], [Email], [Address], [Description], [IsActive]) VALUES (2, N'Asus', N'090X.XYZ.XYZ', N'asus@gmail.com', N'Taiwan', NULL, 1)
INSERT [dbo].[Suppliers] ([Id], [SupplierName], [Phone], [Email], [Address], [Description], [IsActive]) VALUES (3, N'Acer', N'090X.XYZ.XYZ', N'acer@gmail.com', N'Taiwan', NULL, 1)
INSERT [dbo].[Suppliers] ([Id], [SupplierName], [Phone], [Email], [Address], [Description], [IsActive]) VALUES (4, N'Samsung', N'090X.XYZ.XYZ', N'samsung@gmail.com', N'Korea', NULL, 1)
INSERT [dbo].[Suppliers] ([Id], [SupplierName], [Phone], [Email], [Address], [Description], [IsActive]) VALUES (5, N'LG', N'090X.XYZ.XYZ', N'lg@gmail.com', N'Korea', NULL, 1)
INSERT [dbo].[Suppliers] ([Id], [SupplierName], [Phone], [Email], [Address], [Description], [IsActive]) VALUES (6, N'Intel', N'090X.XYZ.XYZ', N'intel@gmail.com', N'USA', NULL, 1)
SET IDENTITY_INSERT [dbo].[Suppliers] OFF
SET IDENTITY_INSERT [dbo].[Topics] ON 

INSERT [dbo].[Topics] ([Id], [TopicName]) VALUES (1, N'Smart Phone')
INSERT [dbo].[Topics] ([Id], [TopicName]) VALUES (2, N'Note Book')
INSERT [dbo].[Topics] ([Id], [TopicName]) VALUES (3, N'Tablets')
INSERT [dbo].[Topics] ([Id], [TopicName]) VALUES (4, N'CPU')
SET IDENTITY_INSERT [dbo].[Topics] OFF
INSERT [dbo].[UserLevels] ([Id], [UserLevelName]) VALUES (1, N'Manager')
INSERT [dbo].[UserLevels] ([Id], [UserLevelName]) VALUES (2, N'Administrator')
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [LoginName], [Password], [FullName], [Phone], [Email], [Address], [Avatar], [UserLevelId], [CreatedDate], [IsActive], [ResetPasswordToken]) VALUES (1, N'manager', N'ec278a38901287b2771a13739520384d43e4b078f78affe702def108774cce24', N'Manager', N'132456789', N'manager@gmail.com', N'11/3 Dong Da', NULL, 1, CAST(N'2018-08-14T00:00:00.000' AS DateTime), 1, NULL)
INSERT [dbo].[Users] ([Id], [LoginName], [Password], [FullName], [Phone], [Email], [Address], [Avatar], [UserLevelId], [CreatedDate], [IsActive], [ResetPasswordToken]) VALUES (5, N'admin', N'ec278a38901287b2771a13739520384d43e4b078f78affe702def108774cce24', N'Administrator', N'123123123', N'admin@gmail.com', N'11/3 Dong Da', NULL, 2, CAST(N'2018-09-05T00:00:00.000' AS DateTime), 1, NULL)
SET IDENTITY_INSERT [dbo].[Users] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users]    Script Date: 11/12/2018 05:00:03 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users] ON [dbo].[Users]
(
	[LoginName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users_1]    Script Date: 11/12/2018 05:00:03 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_1] ON [dbo].[Users]
(
	[Phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users_2]    Script Date: 11/12/2018 05:00:03 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_2] ON [dbo].[Users]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Categories] ADD  CONSTRAINT [DF_Categories_ParentId]  DEFAULT ((0)) FOR [ParentId]
GO
ALTER TABLE [dbo].[Customers] ADD  CONSTRAINT [DF_Customers_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Inputs] ADD  CONSTRAINT [DF_Input_Discount]  DEFAULT ((0)) FOR [Discount]
GO
ALTER TABLE [dbo].[Inputs] ADD  CONSTRAINT [DF_Input_Tax]  DEFAULT ((0)) FOR [Tax]
GO
ALTER TABLE [dbo].[Inputs] ADD  CONSTRAINT [DF_Input_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[OrderDetails] ADD  CONSTRAINT [DF_OrderDetails_Quantity]  DEFAULT ((0)) FOR [Quantity]
GO
ALTER TABLE [dbo].[OrderDetails] ADD  CONSTRAINT [DF_OrderDetails_InputPrice]  DEFAULT ((0)) FOR [Price]
GO
ALTER TABLE [dbo].[OrderDetails] ADD  CONSTRAINT [DF_OrderDetails_Discount]  DEFAULT ((0)) FOR [Discount]
GO
ALTER TABLE [dbo].[OrderDetails] ADD  CONSTRAINT [DF_OrderDetails_Tax]  DEFAULT ((0)) FOR [Tax]
GO
ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [DF_Orders_OrderDate]  DEFAULT (getdate()) FOR [OrderDate]
GO
ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [DF_Orders_Discount]  DEFAULT ((0)) FOR [Discount]
GO
ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [DF_Orders_Tax]  DEFAULT ((0)) FOR [Tax]
GO
ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [DF_Orders_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_InputPrice]  DEFAULT ((0)) FOR [InputPrice]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_OutputPrice]  DEFAULT ((0)) FOR [OutputPrice]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT ((0)) FOR [Discount]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Suppliers] ADD  CONSTRAINT [DF_Suppliers_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[UserLevelPermissions] ADD  CONSTRAINT [DF_LevelPermisions_PermissionCode]  DEFAULT ((0)) FOR [PermissionCode]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Categories]  WITH CHECK ADD  CONSTRAINT [FK_Categories_ToTopics] FOREIGN KEY([TopicId])
REFERENCES [dbo].[Topics] ([Id])
GO
ALTER TABLE [dbo].[Categories] CHECK CONSTRAINT [FK_Categories_ToTopics]
GO
ALTER TABLE [dbo].[Categories]  WITH CHECK ADD  CONSTRAINT [FK_Categories_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Categories] CHECK CONSTRAINT [FK_Categories_Users]
GO
ALTER TABLE [dbo].[InputDetails]  WITH CHECK ADD  CONSTRAINT [FK_InputDetails_Inputs] FOREIGN KEY([InputId])
REFERENCES [dbo].[Inputs] ([Id])
GO
ALTER TABLE [dbo].[InputDetails] CHECK CONSTRAINT [FK_InputDetails_Inputs]
GO
ALTER TABLE [dbo].[InputDetails]  WITH CHECK ADD  CONSTRAINT [FK_InputDetails_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[InputDetails] CHECK CONSTRAINT [FK_InputDetails_Products]
GO
ALTER TABLE [dbo].[Inputs]  WITH CHECK ADD  CONSTRAINT [FK_Input_Suppliers] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Suppliers] ([Id])
GO
ALTER TABLE [dbo].[Inputs] CHECK CONSTRAINT [FK_Input_Suppliers]
GO
ALTER TABLE [dbo].[Inputs]  WITH CHECK ADD  CONSTRAINT [FK_Input_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Inputs] CHECK CONSTRAINT [FK_Input_Users]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Orders] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Orders]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Products]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Customers]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Users]
GO
ALTER TABLE [dbo].[Pictures]  WITH CHECK ADD  CONSTRAINT [FK_Pictures_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[Pictures] CHECK CONSTRAINT [FK_Pictures_Products]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Pictures] FOREIGN KEY([PictureId])
REFERENCES [dbo].[Pictures] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Pictures]
GO
ALTER TABLE [dbo].[UserLevelPermissions]  WITH CHECK ADD  CONSTRAINT [FK_UserLevelPermissions_UserLevels] FOREIGN KEY([UserLevelId])
REFERENCES [dbo].[UserLevels] ([Id])
GO
ALTER TABLE [dbo].[UserLevelPermissions] CHECK CONSTRAINT [FK_UserLevelPermissions_UserLevels]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserLevels] FOREIGN KEY([UserLevelId])
REFERENCES [dbo].[UserLevels] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_UserLevels]
GO
USE [master]
GO
ALTER DATABASE [NewShop] SET  READ_WRITE 
GO
