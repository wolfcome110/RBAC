
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/03/2016 14:14:44
-- Generated from EDMX file: F:\wwwroot\RBAC\trunk\Model\RBAC_Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [RBAC];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Sys_LogDetails_Sys_Logs]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sys_LogDetails] DROP CONSTRAINT [FK_Sys_LogDetails_Sys_Logs];
GO
IF OBJECT_ID(N'[dbo].[FK_Sys_Logs_Sys_LogSettings]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sys_Logs] DROP CONSTRAINT [FK_Sys_Logs_Sys_LogSettings];
GO
IF OBJECT_ID(N'[dbo].[FK_Sys_Logs_Sys_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sys_Logs] DROP CONSTRAINT [FK_Sys_Logs_Sys_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_Sys_Users_Groups_Sys_Groups]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sys_Users_Groups] DROP CONSTRAINT [FK_Sys_Users_Groups_Sys_Groups];
GO
IF OBJECT_ID(N'[dbo].[FK_Sys_Users_Groups_Sys_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sys_Users_Groups] DROP CONSTRAINT [FK_Sys_Users_Groups_Sys_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_Sys_GroupNavBtns_Sys_Buttons]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sys_GroupNavBtns] DROP CONSTRAINT [FK_Sys_GroupNavBtns_Sys_Buttons];
GO
IF OBJECT_ID(N'[dbo].[FK_Sys_GroupNavBtns_Sys_Groups]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sys_GroupNavBtns] DROP CONSTRAINT [FK_Sys_GroupNavBtns_Sys_Groups];
GO
IF OBJECT_ID(N'[dbo].[FK_Sys_GroupNavBtns_Sys_Navigations]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sys_GroupNavBtns] DROP CONSTRAINT [FK_Sys_GroupNavBtns_Sys_Navigations];
GO
IF OBJECT_ID(N'[dbo].[FK_Sys_Users_Departments_Sys_Departments]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sys_Users_Departments] DROP CONSTRAINT [FK_Sys_Users_Departments_Sys_Departments];
GO
IF OBJECT_ID(N'[dbo].[FK_Sys_Users_Departments_Sys_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sys_Users_Departments] DROP CONSTRAINT [FK_Sys_Users_Departments_Sys_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_Sys_UserNavBtns_Sys_Buttons]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sys_UserNavBtns] DROP CONSTRAINT [FK_Sys_UserNavBtns_Sys_Buttons];
GO
IF OBJECT_ID(N'[dbo].[FK_Sys_UserNavBtns_Sys_Navigations]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sys_UserNavBtns] DROP CONSTRAINT [FK_Sys_UserNavBtns_Sys_Navigations];
GO
IF OBJECT_ID(N'[dbo].[FK_Sys_UserNavBtns_Sys_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sys_UserNavBtns] DROP CONSTRAINT [FK_Sys_UserNavBtns_Sys_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_Sys_Roles_Departments_Sys_Departments]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sys_Roles_Departments] DROP CONSTRAINT [FK_Sys_Roles_Departments_Sys_Departments];
GO
IF OBJECT_ID(N'[dbo].[FK_Sys_Roles_Departments_Sys_Roles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sys_Roles_Departments] DROP CONSTRAINT [FK_Sys_Roles_Departments_Sys_Roles];
GO
IF OBJECT_ID(N'[dbo].[FK_Sys_RoleNavBtns_Sys_Buttons]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sys_RoleNavBtns] DROP CONSTRAINT [FK_Sys_RoleNavBtns_Sys_Buttons];
GO
IF OBJECT_ID(N'[dbo].[FK_Sys_RoleNavBtns_Sys_Navigations]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sys_RoleNavBtns] DROP CONSTRAINT [FK_Sys_RoleNavBtns_Sys_Navigations];
GO
IF OBJECT_ID(N'[dbo].[FK_Sys_RoleNavBtns_Sys_Roles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sys_RoleNavBtns] DROP CONSTRAINT [FK_Sys_RoleNavBtns_Sys_Roles];
GO
IF OBJECT_ID(N'[dbo].[FK_Sys_NavButtons_Sys_Buttons]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sys_NavButtons] DROP CONSTRAINT [FK_Sys_NavButtons_Sys_Buttons];
GO
IF OBJECT_ID(N'[dbo].[FK_Sys_NavButtons_Sys_Navigations]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sys_NavButtons] DROP CONSTRAINT [FK_Sys_NavButtons_Sys_Navigations];
GO
IF OBJECT_ID(N'[dbo].[FK_Sys_Users_Roles_Sys_Roles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sys_Users_Roles] DROP CONSTRAINT [FK_Sys_Users_Roles_Sys_Roles];
GO
IF OBJECT_ID(N'[dbo].[FK_Sys_Users_Roles_Sys_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sys_Users_Roles] DROP CONSTRAINT [FK_Sys_Users_Roles_Sys_Users];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Sys_Buttons]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sys_Buttons];
GO
IF OBJECT_ID(N'[dbo].[Sys_Departments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sys_Departments];
GO
IF OBJECT_ID(N'[dbo].[Sys_Groups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sys_Groups];
GO
IF OBJECT_ID(N'[dbo].[Sys_LogDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sys_LogDetails];
GO
IF OBJECT_ID(N'[dbo].[Sys_Logs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sys_Logs];
GO
IF OBJECT_ID(N'[dbo].[Sys_LogSettings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sys_LogSettings];
GO
IF OBJECT_ID(N'[dbo].[Sys_Navigations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sys_Navigations];
GO
IF OBJECT_ID(N'[dbo].[Sys_Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sys_Roles];
GO
IF OBJECT_ID(N'[dbo].[Sys_Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sys_Users];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[Sys_Users_Groups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sys_Users_Groups];
GO
IF OBJECT_ID(N'[dbo].[Sys_GroupNavBtns]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sys_GroupNavBtns];
GO
IF OBJECT_ID(N'[dbo].[Sys_Users_Departments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sys_Users_Departments];
GO
IF OBJECT_ID(N'[dbo].[Sys_UserNavBtns]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sys_UserNavBtns];
GO
IF OBJECT_ID(N'[dbo].[Sys_Roles_Departments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sys_Roles_Departments];
GO
IF OBJECT_ID(N'[dbo].[Sys_RoleNavBtns]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sys_RoleNavBtns];
GO
IF OBJECT_ID(N'[dbo].[Sys_NavButtons]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sys_NavButtons];
GO
IF OBJECT_ID(N'[dbo].[Sys_Users_Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sys_Users_Roles];
GO
IF OBJECT_ID(N'[dbo].[Sys_Parameter]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sys_Parameter];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Sys_Buttons'
CREATE TABLE [dbo].[Sys_Buttons] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ButtonText] varchar(50)  NOT NULL,
    [SortNum] int  NOT NULL,
    [IconCls] varchar(50)  NULL,
    [IconUrl] nvarchar(500)  NULL,
    [ButtonTag] varchar(50)  NULL,
    [ButtonHtml] varchar(500)  NULL,
    [Remark] varchar(200)  NULL,
    [AddTime] datetime  NOT NULL
);
GO

-- Creating table 'Sys_Departments'
CREATE TABLE [dbo].[Sys_Departments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DepartmentName] varchar(50)  NOT NULL,
    [ParentId] int  NOT NULL,
    [SortNum] int  NOT NULL,
    [Remark] varchar(500)  NULL,
    [AddTime] datetime  NOT NULL
);
GO

-- Creating table 'Sys_Groups'
CREATE TABLE [dbo].[Sys_Groups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [GroupName] varchar(50)  NOT NULL,
    [AddTime] datetime  NOT NULL,
    [Remark] varchar(500)  NULL,
    [ParentId] int  NOT NULL
);
GO

-- Creating table 'Sys_LogDetails'
CREATE TABLE [dbo].[Sys_LogDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LogId] int  NOT NULL,
    [ColumnName] varchar(30)  NULL,
    [ColumnText] varchar(30)  NULL,
    [ColumnDataType] varchar(20)  NULL,
    [OldValue] varchar(max)  NULL,
    [NewValue] varchar(max)  NULL,
    [Remark] varchar(1000)  NULL,
    [AddTime] datetime  NOT NULL
);
GO

-- Creating table 'Sys_Logs'
CREATE TABLE [dbo].[Sys_Logs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LogSettingId] int  NOT NULL,
    [UserId] int  NOT NULL,
    [PrimaryKeyValue] varchar(50)  NOT NULL,
    [OperationIP] varchar(15)  NOT NULL,
    [SqlText] varchar(500)  NULL,
    [Url] varchar(300)  NOT NULL,
    [AddTime] datetime  NOT NULL,
    [Remark] varchar(200)  NULL
);
GO

-- Creating table 'Sys_LogSettings'
CREATE TABLE [dbo].[Sys_LogSettings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TableName] varchar(50)  NOT NULL,
    [BusinessName] varchar(50)  NOT NULL,
    [PrimaryKey] varchar(50)  NOT NULL,
    [AddTime] datetime  NOT NULL,
    [Remark] varchar(200)  NULL
);
GO

-- Creating table 'Sys_Navigations'
CREATE TABLE [dbo].[Sys_Navigations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NavTitle] varchar(50)  NOT NULL,
    [LinkUrl] varchar(1000)  NOT NULL,
    [IconCls] varchar(50)  NULL,
    [IconUrl] varchar(500)  NULL,
    [IsVisible] bit  NULL,
    [ParentId] int  NOT NULL,
    [NavTag] varchar(50)  NULL,
    [BigImageUrl] varchar(200)  NULL,
    [SortNum] int  NOT NULL,
    [Remark] varchar(200)  NULL,
    [AddTime] datetime  NOT NULL
);
GO

-- Creating table 'Sys_Roles'
CREATE TABLE [dbo].[Sys_Roles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RoleName] varchar(50)  NOT NULL,
    [IsDefault] bit  NULL,
    [SortNum] int  NOT NULL,
    [Remark] varchar(200)  NULL,
    [AddTime] datetime  NOT NULL,
    [ParentId] int  NULL
);
GO

-- Creating table 'Sys_Users'
CREATE TABLE [dbo].[Sys_Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] varchar(50)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL,
    [PassSalt] varchar(6)  NOT NULL,
    [Email] varchar(50)  NULL,
    [IsDisabled] bit  NULL,
    [TrueName] varchar(50)  NULL,
    [Mobile] varchar(20)  NULL,
    [QQ] varchar(20)  NULL,
    [MenusJson] varchar(max)  NULL,
    [ConfigJson] varchar(max)  NULL,
    [Remark] varchar(1000)  NULL,
    [AddTime] datetime  NOT NULL
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

-- Creating table 'Sys_Users_Groups'
CREATE TABLE [dbo].[Sys_Users_Groups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [GroupId] int  NOT NULL,
    [AddTime] datetime  NOT NULL
);
GO

-- Creating table 'Sys_GroupNavBtns'
CREATE TABLE [dbo].[Sys_GroupNavBtns] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [GroupId] int  NOT NULL,
    [NavId] int  NOT NULL,
    [BtnId] int  NOT NULL,
    [AddTime] datetime  NOT NULL
);
GO

-- Creating table 'Sys_Users_Departments'
CREATE TABLE [dbo].[Sys_Users_Departments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [DepId] int  NOT NULL,
    [AddTime] datetime  NOT NULL
);
GO

-- Creating table 'Sys_UserNavBtns'
CREATE TABLE [dbo].[Sys_UserNavBtns] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [NavId] int  NOT NULL,
    [BtnId] int  NOT NULL,
    [AddTime] datetime  NOT NULL
);
GO

-- Creating table 'Sys_Roles_Departments'
CREATE TABLE [dbo].[Sys_Roles_Departments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RoleId] int  NOT NULL,
    [DepId] int  NOT NULL,
    [Remark] varchar(200)  NOT NULL,
    [AddTime] datetime  NOT NULL
);
GO

-- Creating table 'Sys_RoleNavBtns'
CREATE TABLE [dbo].[Sys_RoleNavBtns] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RoleId] int  NOT NULL,
    [NavId] int  NOT NULL,
    [BtnId] int  NOT NULL,
    [AddTime] datetime  NOT NULL
);
GO

-- Creating table 'Sys_NavButtons'
CREATE TABLE [dbo].[Sys_NavButtons] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NavId] int  NOT NULL,
    [BtnId] int  NOT NULL,
    [SortNum] int  NOT NULL,
    [AddTime] datetime  NOT NULL
);
GO

-- Creating table 'Sys_Users_Roles'
CREATE TABLE [dbo].[Sys_Users_Roles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [RoleId] int  NOT NULL,
    [AddTime] datetime  NOT NULL
);
GO

-- Creating table 'Sys_Parameter'
CREATE TABLE [dbo].[Sys_Parameter] (
    [ParamCode] nvarchar(100)  NOT NULL,
    [ParamValue] nvarchar(100)  NOT NULL,
    [IsUserEditable] bit  NULL,
    [Description] nvarchar(1000)  NOT NULL,
    [AddTime] datetime  NOT NULL,
    [UpdateTime] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Sys_Buttons'
ALTER TABLE [dbo].[Sys_Buttons]
ADD CONSTRAINT [PK_Sys_Buttons]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sys_Departments'
ALTER TABLE [dbo].[Sys_Departments]
ADD CONSTRAINT [PK_Sys_Departments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sys_Groups'
ALTER TABLE [dbo].[Sys_Groups]
ADD CONSTRAINT [PK_Sys_Groups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sys_LogDetails'
ALTER TABLE [dbo].[Sys_LogDetails]
ADD CONSTRAINT [PK_Sys_LogDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sys_Logs'
ALTER TABLE [dbo].[Sys_Logs]
ADD CONSTRAINT [PK_Sys_Logs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sys_LogSettings'
ALTER TABLE [dbo].[Sys_LogSettings]
ADD CONSTRAINT [PK_Sys_LogSettings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sys_Navigations'
ALTER TABLE [dbo].[Sys_Navigations]
ADD CONSTRAINT [PK_Sys_Navigations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sys_Roles'
ALTER TABLE [dbo].[Sys_Roles]
ADD CONSTRAINT [PK_Sys_Roles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sys_Users'
ALTER TABLE [dbo].[Sys_Users]
ADD CONSTRAINT [PK_Sys_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [UserId], [GroupId] in table 'Sys_Users_Groups'
ALTER TABLE [dbo].[Sys_Users_Groups]
ADD CONSTRAINT [PK_Sys_Users_Groups]
    PRIMARY KEY CLUSTERED ([UserId], [GroupId] ASC);
GO

-- Creating primary key on [GroupId], [NavId], [BtnId] in table 'Sys_GroupNavBtns'
ALTER TABLE [dbo].[Sys_GroupNavBtns]
ADD CONSTRAINT [PK_Sys_GroupNavBtns]
    PRIMARY KEY CLUSTERED ([GroupId], [NavId], [BtnId] ASC);
GO

-- Creating primary key on [UserId], [DepId] in table 'Sys_Users_Departments'
ALTER TABLE [dbo].[Sys_Users_Departments]
ADD CONSTRAINT [PK_Sys_Users_Departments]
    PRIMARY KEY CLUSTERED ([UserId], [DepId] ASC);
GO

-- Creating primary key on [UserId], [NavId], [BtnId] in table 'Sys_UserNavBtns'
ALTER TABLE [dbo].[Sys_UserNavBtns]
ADD CONSTRAINT [PK_Sys_UserNavBtns]
    PRIMARY KEY CLUSTERED ([UserId], [NavId], [BtnId] ASC);
GO

-- Creating primary key on [RoleId], [DepId] in table 'Sys_Roles_Departments'
ALTER TABLE [dbo].[Sys_Roles_Departments]
ADD CONSTRAINT [PK_Sys_Roles_Departments]
    PRIMARY KEY CLUSTERED ([RoleId], [DepId] ASC);
GO

-- Creating primary key on [RoleId], [NavId], [BtnId] in table 'Sys_RoleNavBtns'
ALTER TABLE [dbo].[Sys_RoleNavBtns]
ADD CONSTRAINT [PK_Sys_RoleNavBtns]
    PRIMARY KEY CLUSTERED ([RoleId], [NavId], [BtnId] ASC);
GO

-- Creating primary key on [NavId], [BtnId] in table 'Sys_NavButtons'
ALTER TABLE [dbo].[Sys_NavButtons]
ADD CONSTRAINT [PK_Sys_NavButtons]
    PRIMARY KEY CLUSTERED ([NavId], [BtnId] ASC);
GO

-- Creating primary key on [UserId], [RoleId] in table 'Sys_Users_Roles'
ALTER TABLE [dbo].[Sys_Users_Roles]
ADD CONSTRAINT [PK_Sys_Users_Roles]
    PRIMARY KEY CLUSTERED ([UserId], [RoleId] ASC);
GO

-- Creating primary key on [ParamCode] in table 'Sys_Parameter'
ALTER TABLE [dbo].[Sys_Parameter]
ADD CONSTRAINT [PK_Sys_Parameter]
    PRIMARY KEY CLUSTERED ([ParamCode] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [LogId] in table 'Sys_LogDetails'
ALTER TABLE [dbo].[Sys_LogDetails]
ADD CONSTRAINT [FK_Sys_LogDetails_Sys_Logs]
    FOREIGN KEY ([LogId])
    REFERENCES [dbo].[Sys_Logs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Sys_LogDetails_Sys_Logs'
CREATE INDEX [IX_FK_Sys_LogDetails_Sys_Logs]
ON [dbo].[Sys_LogDetails]
    ([LogId]);
GO

-- Creating foreign key on [LogSettingId] in table 'Sys_Logs'
ALTER TABLE [dbo].[Sys_Logs]
ADD CONSTRAINT [FK_Sys_Logs_Sys_LogSettings]
    FOREIGN KEY ([LogSettingId])
    REFERENCES [dbo].[Sys_LogSettings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Sys_Logs_Sys_LogSettings'
CREATE INDEX [IX_FK_Sys_Logs_Sys_LogSettings]
ON [dbo].[Sys_Logs]
    ([LogSettingId]);
GO

-- Creating foreign key on [UserId] in table 'Sys_Logs'
ALTER TABLE [dbo].[Sys_Logs]
ADD CONSTRAINT [FK_Sys_Logs_Sys_Users]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Sys_Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Sys_Logs_Sys_Users'
CREATE INDEX [IX_FK_Sys_Logs_Sys_Users]
ON [dbo].[Sys_Logs]
    ([UserId]);
GO

-- Creating foreign key on [GroupId] in table 'Sys_Users_Groups'
ALTER TABLE [dbo].[Sys_Users_Groups]
ADD CONSTRAINT [FK_Sys_Users_Groups_Sys_Groups]
    FOREIGN KEY ([GroupId])
    REFERENCES [dbo].[Sys_Groups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Sys_Users_Groups_Sys_Groups'
CREATE INDEX [IX_FK_Sys_Users_Groups_Sys_Groups]
ON [dbo].[Sys_Users_Groups]
    ([GroupId]);
GO

-- Creating foreign key on [UserId] in table 'Sys_Users_Groups'
ALTER TABLE [dbo].[Sys_Users_Groups]
ADD CONSTRAINT [FK_Sys_Users_Groups_Sys_Users]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Sys_Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [BtnId] in table 'Sys_GroupNavBtns'
ALTER TABLE [dbo].[Sys_GroupNavBtns]
ADD CONSTRAINT [FK_Sys_GroupNavBtns_Sys_Buttons]
    FOREIGN KEY ([BtnId])
    REFERENCES [dbo].[Sys_Buttons]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Sys_GroupNavBtns_Sys_Buttons'
CREATE INDEX [IX_FK_Sys_GroupNavBtns_Sys_Buttons]
ON [dbo].[Sys_GroupNavBtns]
    ([BtnId]);
GO

-- Creating foreign key on [GroupId] in table 'Sys_GroupNavBtns'
ALTER TABLE [dbo].[Sys_GroupNavBtns]
ADD CONSTRAINT [FK_Sys_GroupNavBtns_Sys_Groups]
    FOREIGN KEY ([GroupId])
    REFERENCES [dbo].[Sys_Groups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [NavId] in table 'Sys_GroupNavBtns'
ALTER TABLE [dbo].[Sys_GroupNavBtns]
ADD CONSTRAINT [FK_Sys_GroupNavBtns_Sys_Navigations]
    FOREIGN KEY ([NavId])
    REFERENCES [dbo].[Sys_Navigations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Sys_GroupNavBtns_Sys_Navigations'
CREATE INDEX [IX_FK_Sys_GroupNavBtns_Sys_Navigations]
ON [dbo].[Sys_GroupNavBtns]
    ([NavId]);
GO

-- Creating foreign key on [DepId] in table 'Sys_Users_Departments'
ALTER TABLE [dbo].[Sys_Users_Departments]
ADD CONSTRAINT [FK_Sys_Users_Departments_Sys_Departments]
    FOREIGN KEY ([DepId])
    REFERENCES [dbo].[Sys_Departments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Sys_Users_Departments_Sys_Departments'
CREATE INDEX [IX_FK_Sys_Users_Departments_Sys_Departments]
ON [dbo].[Sys_Users_Departments]
    ([DepId]);
GO

-- Creating foreign key on [UserId] in table 'Sys_Users_Departments'
ALTER TABLE [dbo].[Sys_Users_Departments]
ADD CONSTRAINT [FK_Sys_Users_Departments_Sys_Users]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Sys_Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [BtnId] in table 'Sys_UserNavBtns'
ALTER TABLE [dbo].[Sys_UserNavBtns]
ADD CONSTRAINT [FK_Sys_UserNavBtns_Sys_Buttons]
    FOREIGN KEY ([BtnId])
    REFERENCES [dbo].[Sys_Buttons]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Sys_UserNavBtns_Sys_Buttons'
CREATE INDEX [IX_FK_Sys_UserNavBtns_Sys_Buttons]
ON [dbo].[Sys_UserNavBtns]
    ([BtnId]);
GO

-- Creating foreign key on [NavId] in table 'Sys_UserNavBtns'
ALTER TABLE [dbo].[Sys_UserNavBtns]
ADD CONSTRAINT [FK_Sys_UserNavBtns_Sys_Navigations]
    FOREIGN KEY ([NavId])
    REFERENCES [dbo].[Sys_Navigations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Sys_UserNavBtns_Sys_Navigations'
CREATE INDEX [IX_FK_Sys_UserNavBtns_Sys_Navigations]
ON [dbo].[Sys_UserNavBtns]
    ([NavId]);
GO

-- Creating foreign key on [UserId] in table 'Sys_UserNavBtns'
ALTER TABLE [dbo].[Sys_UserNavBtns]
ADD CONSTRAINT [FK_Sys_UserNavBtns_Sys_Users]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Sys_Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [DepId] in table 'Sys_Roles_Departments'
ALTER TABLE [dbo].[Sys_Roles_Departments]
ADD CONSTRAINT [FK_Sys_Roles_Departments_Sys_Departments]
    FOREIGN KEY ([DepId])
    REFERENCES [dbo].[Sys_Departments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Sys_Roles_Departments_Sys_Departments'
CREATE INDEX [IX_FK_Sys_Roles_Departments_Sys_Departments]
ON [dbo].[Sys_Roles_Departments]
    ([DepId]);
GO

-- Creating foreign key on [RoleId] in table 'Sys_Roles_Departments'
ALTER TABLE [dbo].[Sys_Roles_Departments]
ADD CONSTRAINT [FK_Sys_Roles_Departments_Sys_Roles]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[Sys_Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [BtnId] in table 'Sys_RoleNavBtns'
ALTER TABLE [dbo].[Sys_RoleNavBtns]
ADD CONSTRAINT [FK_Sys_RoleNavBtns_Sys_Buttons]
    FOREIGN KEY ([BtnId])
    REFERENCES [dbo].[Sys_Buttons]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Sys_RoleNavBtns_Sys_Buttons'
CREATE INDEX [IX_FK_Sys_RoleNavBtns_Sys_Buttons]
ON [dbo].[Sys_RoleNavBtns]
    ([BtnId]);
GO

-- Creating foreign key on [NavId] in table 'Sys_RoleNavBtns'
ALTER TABLE [dbo].[Sys_RoleNavBtns]
ADD CONSTRAINT [FK_Sys_RoleNavBtns_Sys_Navigations]
    FOREIGN KEY ([NavId])
    REFERENCES [dbo].[Sys_Navigations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Sys_RoleNavBtns_Sys_Navigations'
CREATE INDEX [IX_FK_Sys_RoleNavBtns_Sys_Navigations]
ON [dbo].[Sys_RoleNavBtns]
    ([NavId]);
GO

-- Creating foreign key on [RoleId] in table 'Sys_RoleNavBtns'
ALTER TABLE [dbo].[Sys_RoleNavBtns]
ADD CONSTRAINT [FK_Sys_RoleNavBtns_Sys_Roles]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[Sys_Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [BtnId] in table 'Sys_NavButtons'
ALTER TABLE [dbo].[Sys_NavButtons]
ADD CONSTRAINT [FK_Sys_NavButtons_Sys_Buttons]
    FOREIGN KEY ([BtnId])
    REFERENCES [dbo].[Sys_Buttons]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Sys_NavButtons_Sys_Buttons'
CREATE INDEX [IX_FK_Sys_NavButtons_Sys_Buttons]
ON [dbo].[Sys_NavButtons]
    ([BtnId]);
GO

-- Creating foreign key on [NavId] in table 'Sys_NavButtons'
ALTER TABLE [dbo].[Sys_NavButtons]
ADD CONSTRAINT [FK_Sys_NavButtons_Sys_Navigations]
    FOREIGN KEY ([NavId])
    REFERENCES [dbo].[Sys_Navigations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [RoleId] in table 'Sys_Users_Roles'
ALTER TABLE [dbo].[Sys_Users_Roles]
ADD CONSTRAINT [FK_Sys_Users_Roles_Sys_Roles]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[Sys_Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Sys_Users_Roles_Sys_Roles'
CREATE INDEX [IX_FK_Sys_Users_Roles_Sys_Roles]
ON [dbo].[Sys_Users_Roles]
    ([RoleId]);
GO

-- Creating foreign key on [UserId] in table 'Sys_Users_Roles'
ALTER TABLE [dbo].[Sys_Users_Roles]
ADD CONSTRAINT [FK_Sys_Users_Roles_Sys_Users]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Sys_Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------