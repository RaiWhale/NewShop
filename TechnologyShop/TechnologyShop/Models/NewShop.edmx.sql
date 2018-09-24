
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/24/2018 15:53:39
-- Generated from EDMX file: E:\NewShop\TechnologyShop\TechnologyShop\Models\NewShop.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [NewShop];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Categories_ToTopics]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Categories] DROP CONSTRAINT [FK_Categories_ToTopics];
GO
IF OBJECT_ID(N'[dbo].[FK_Input_Suppliers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Inputs] DROP CONSTRAINT [FK_Input_Suppliers];
GO
IF OBJECT_ID(N'[dbo].[FK_Input_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Inputs] DROP CONSTRAINT [FK_Input_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_InputDetails_Inputs]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InputDetails] DROP CONSTRAINT [FK_InputDetails_Inputs];
GO
IF OBJECT_ID(N'[dbo].[FK_InputDetails_Products]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InputDetails] DROP CONSTRAINT [FK_InputDetails_Products];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderDetails_Orders]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderDetails] DROP CONSTRAINT [FK_OrderDetails_Orders];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderDetails_Products]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderDetails] DROP CONSTRAINT [FK_OrderDetails_Products];
GO
IF OBJECT_ID(N'[dbo].[FK_Orders_Customers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_Orders_Customers];
GO
IF OBJECT_ID(N'[dbo].[FK_Orders_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_Orders_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_Pictures_Products]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Pictures] DROP CONSTRAINT [FK_Pictures_Products];
GO
IF OBJECT_ID(N'[dbo].[FK_Products_Categories]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_Products_Categories];
GO
IF OBJECT_ID(N'[dbo].[FK_UserLevelPermissions_UserLevels]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserLevelPermissions] DROP CONSTRAINT [FK_UserLevelPermissions_UserLevels];
GO
IF OBJECT_ID(N'[dbo].[FK_UserLevels_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserLevels] DROP CONSTRAINT [FK_UserLevels_Users];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Categories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categories];
GO
IF OBJECT_ID(N'[dbo].[Customers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customers];
GO
IF OBJECT_ID(N'[dbo].[InputDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InputDetails];
GO
IF OBJECT_ID(N'[dbo].[Inputs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Inputs];
GO
IF OBJECT_ID(N'[dbo].[OrderDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderDetails];
GO
IF OBJECT_ID(N'[dbo].[Orders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Orders];
GO
IF OBJECT_ID(N'[dbo].[Pictures]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Pictures];
GO
IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO
IF OBJECT_ID(N'[dbo].[Suppliers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Suppliers];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[Topics]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Topics];
GO
IF OBJECT_ID(N'[dbo].[UserLevelPermissions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserLevelPermissions];
GO
IF OBJECT_ID(N'[dbo].[UserLevels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserLevels];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'InputDetails'
CREATE TABLE [dbo].[InputDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [InputId] int  NOT NULL,
    [ProductId] int  NOT NULL,
    [Unit] nvarchar(20)  NOT NULL,
    [Quantity] decimal(18,0)  NOT NULL,
    [Price] decimal(18,0)  NOT NULL,
    [Discount] decimal(18,0)  NOT NULL,
    [Tax] decimal(18,0)  NOT NULL,
    [Note] nvarchar(150)  NULL
);
GO

-- Creating table 'Inputs'
CREATE TABLE [dbo].[Inputs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [InputCode] varchar(20)  NULL,
    [InputDate] datetime  NOT NULL,
    [UserId] int  NOT NULL,
    [SupplierId] int  NOT NULL,
    [Discount] decimal(18,0)  NOT NULL,
    [Tax] decimal(18,0)  NOT NULL,
    [Status] tinyint  NOT NULL
);
GO

-- Creating table 'OrderDetails'
CREATE TABLE [dbo].[OrderDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrderId] int  NOT NULL,
    [ProductId] int  NOT NULL,
    [Unit] nvarchar(20)  NOT NULL,
    [Quantity] decimal(18,0)  NOT NULL,
    [Price] decimal(18,0)  NOT NULL,
    [Discount] decimal(18,0)  NOT NULL,
    [Tax] decimal(18,0)  NOT NULL,
    [Note] nvarchar(150)  NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrderCode] varchar(20)  NULL,
    [OrderDate] datetime  NOT NULL,
    [UserId] int  NOT NULL,
    [CustomerId] int  NOT NULL,
    [Discount] decimal(18,0)  NOT NULL,
    [Tax] decimal(18,0)  NOT NULL,
    [Status] tinyint  NOT NULL
);
GO

-- Creating table 'Pictures'
CREATE TABLE [dbo].[Pictures] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProductId] int  NULL,
    [Url] varchar(150)  NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BarCode] varchar(50)  NOT NULL,
    [CategoryId] int  NOT NULL,
    [ProductName] nvarchar(150)  NOT NULL,
    [Unit] nvarchar(20)  NULL,
    [InputPrice] decimal(18,0)  NOT NULL,
    [OutputPrice] decimal(18,0)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'Suppliers'
CREATE TABLE [dbo].[Suppliers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SupplierName] nvarchar(200)  NOT NULL,
    [Phone] varchar(30)  NULL,
    [Email] nvarchar(150)  NULL,
    [Address] nvarchar(250)  NULL,
    [Description] nvarchar(250)  NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'Topics'
CREATE TABLE [dbo].[Topics] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TopicName] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'UserLevelPermissions'
CREATE TABLE [dbo].[UserLevelPermissions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserLevelId] int  NOT NULL,
    [ControllerName] varchar(100)  NOT NULL,
    [PermissionCode] tinyint  NOT NULL
);
GO

-- Creating table 'UserLevels'
CREATE TABLE [dbo].[UserLevels] (
    [Id] int  NOT NULL,
    [UserLevelName] nvarchar(50)  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LoginName] nvarchar(50)  NOT NULL,
    [Password] varchar(100)  NOT NULL,
    [FullName] nvarchar(50)  NULL,
    [Phone] nvarchar(50)  NULL,
    [Email] nvarchar(150)  NULL,
    [Address] nvarchar(250)  NULL,
    [Avatar] nvarchar(250)  NULL,
    [UserLevelId] int  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL,
    [ResetPasswordToken] varchar(100)  NULL
);
GO

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CustomerName] nvarchar(200)  NOT NULL,
    [Gender] tinyint  NOT NULL,
    [Birthday] datetime  NULL,
    [Phone] varchar(30)  NULL,
    [Email] nvarchar(150)  NOT NULL,
    [Password] varchar(100)  NOT NULL,
    [Address] nvarchar(250)  NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CategoryName] nvarchar(100)  NOT NULL,
    [TopicId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'InputDetails'
ALTER TABLE [dbo].[InputDetails]
ADD CONSTRAINT [PK_InputDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Inputs'
ALTER TABLE [dbo].[Inputs]
ADD CONSTRAINT [PK_Inputs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderDetails'
ALTER TABLE [dbo].[OrderDetails]
ADD CONSTRAINT [PK_OrderDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Pictures'
ALTER TABLE [dbo].[Pictures]
ADD CONSTRAINT [PK_Pictures]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Suppliers'
ALTER TABLE [dbo].[Suppliers]
ADD CONSTRAINT [PK_Suppliers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [Id] in table 'Topics'
ALTER TABLE [dbo].[Topics]
ADD CONSTRAINT [PK_Topics]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserLevelPermissions'
ALTER TABLE [dbo].[UserLevelPermissions]
ADD CONSTRAINT [PK_UserLevelPermissions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserLevels'
ALTER TABLE [dbo].[UserLevels]
ADD CONSTRAINT [PK_UserLevels]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [InputId] in table 'InputDetails'
ALTER TABLE [dbo].[InputDetails]
ADD CONSTRAINT [FK_InputDetails_Inputs]
    FOREIGN KEY ([InputId])
    REFERENCES [dbo].[Inputs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InputDetails_Inputs'
CREATE INDEX [IX_FK_InputDetails_Inputs]
ON [dbo].[InputDetails]
    ([InputId]);
GO

-- Creating foreign key on [ProductId] in table 'InputDetails'
ALTER TABLE [dbo].[InputDetails]
ADD CONSTRAINT [FK_InputDetails_Products]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InputDetails_Products'
CREATE INDEX [IX_FK_InputDetails_Products]
ON [dbo].[InputDetails]
    ([ProductId]);
GO

-- Creating foreign key on [SupplierId] in table 'Inputs'
ALTER TABLE [dbo].[Inputs]
ADD CONSTRAINT [FK_Input_Suppliers]
    FOREIGN KEY ([SupplierId])
    REFERENCES [dbo].[Suppliers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Input_Suppliers'
CREATE INDEX [IX_FK_Input_Suppliers]
ON [dbo].[Inputs]
    ([SupplierId]);
GO

-- Creating foreign key on [UserId] in table 'Inputs'
ALTER TABLE [dbo].[Inputs]
ADD CONSTRAINT [FK_Input_Users]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Input_Users'
CREATE INDEX [IX_FK_Input_Users]
ON [dbo].[Inputs]
    ([UserId]);
GO

-- Creating foreign key on [OrderId] in table 'OrderDetails'
ALTER TABLE [dbo].[OrderDetails]
ADD CONSTRAINT [FK_OrderDetails_Orders]
    FOREIGN KEY ([OrderId])
    REFERENCES [dbo].[Orders]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderDetails_Orders'
CREATE INDEX [IX_FK_OrderDetails_Orders]
ON [dbo].[OrderDetails]
    ([OrderId]);
GO

-- Creating foreign key on [ProductId] in table 'OrderDetails'
ALTER TABLE [dbo].[OrderDetails]
ADD CONSTRAINT [FK_OrderDetails_Products]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderDetails_Products'
CREATE INDEX [IX_FK_OrderDetails_Products]
ON [dbo].[OrderDetails]
    ([ProductId]);
GO

-- Creating foreign key on [UserId] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Orders_Users]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Orders_Users'
CREATE INDEX [IX_FK_Orders_Users]
ON [dbo].[Orders]
    ([UserId]);
GO

-- Creating foreign key on [ProductId] in table 'Pictures'
ALTER TABLE [dbo].[Pictures]
ADD CONSTRAINT [FK_Pictures_Products]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Pictures_Products'
CREATE INDEX [IX_FK_Pictures_Products]
ON [dbo].[Pictures]
    ([ProductId]);
GO

-- Creating foreign key on [UserLevelId] in table 'UserLevelPermissions'
ALTER TABLE [dbo].[UserLevelPermissions]
ADD CONSTRAINT [FK_UserLevelPermissions_UserLevels]
    FOREIGN KEY ([UserLevelId])
    REFERENCES [dbo].[UserLevels]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserLevelPermissions_UserLevels'
CREATE INDEX [IX_FK_UserLevelPermissions_UserLevels]
ON [dbo].[UserLevelPermissions]
    ([UserLevelId]);
GO

-- Creating foreign key on [UserId] in table 'UserLevels'
ALTER TABLE [dbo].[UserLevels]
ADD CONSTRAINT [FK_UserLevels_Users]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserLevels_Users'
CREATE INDEX [IX_FK_UserLevels_Users]
ON [dbo].[UserLevels]
    ([UserId]);
GO

-- Creating foreign key on [CustomerId] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Orders_Customers]
    FOREIGN KEY ([CustomerId])
    REFERENCES [dbo].[Customers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Orders_Customers'
CREATE INDEX [IX_FK_Orders_Customers]
ON [dbo].[Orders]
    ([CustomerId]);
GO

-- Creating foreign key on [TopicId] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [FK_Categories_ToTopics]
    FOREIGN KEY ([TopicId])
    REFERENCES [dbo].[Topics]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Categories_ToTopics'
CREATE INDEX [IX_FK_Categories_ToTopics]
ON [dbo].[Categories]
    ([TopicId]);
GO

-- Creating foreign key on [CategoryId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_Products_Categories]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[Categories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Products_Categories'
CREATE INDEX [IX_FK_Products_Categories]
ON [dbo].[Products]
    ([CategoryId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------