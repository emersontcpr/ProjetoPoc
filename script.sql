USE [master]
GO
/****** Object:  Database [Cliente]    Script Date: 24/09/2024 23:00:58 ******/
CREATE DATABASE [Cliente]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Cliente', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Cliente.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Cliente_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Cliente_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Cliente] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Cliente].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Cliente] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Cliente] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Cliente] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Cliente] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Cliente] SET ARITHABORT OFF 
GO
ALTER DATABASE [Cliente] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Cliente] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Cliente] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Cliente] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Cliente] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Cliente] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Cliente] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Cliente] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Cliente] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Cliente] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Cliente] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Cliente] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Cliente] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Cliente] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Cliente] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Cliente] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Cliente] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Cliente] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Cliente] SET  MULTI_USER 
GO
ALTER DATABASE [Cliente] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Cliente] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Cliente] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Cliente] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Cliente] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Cliente] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Cliente] SET QUERY_STORE = ON
GO
ALTER DATABASE [Cliente] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Cliente]
GO
/****** Object:  User [Sistema1]    Script Date: 24/09/2024 23:00:58 ******/
CREATE USER [Sistema1] FOR LOGIN [Sistema1] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [Sistema]    Script Date: 24/09/2024 23:00:58 ******/
CREATE USER [Sistema] FOR LOGIN [Sistema] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [Sistema1]
GO
ALTER ROLE [db_owner] ADD MEMBER [Sistema]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [Sistema]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [Sistema]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [Sistema]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [Sistema]
GO
ALTER ROLE [db_datareader] ADD MEMBER [Sistema]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [Sistema]
GO
ALTER ROLE [db_denydatareader] ADD MEMBER [Sistema]
GO
ALTER ROLE [db_denydatawriter] ADD MEMBER [Sistema]
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 24/09/2024 23:00:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Logotipo] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Login]    Script Date: 24/09/2024 23:00:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Login](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Login] [varchar](50) NOT NULL,
	[Senha] [varchar](100) NOT NULL,
	[StatusUsuario] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Logradouro]    Script Date: 24/09/2024 23:00:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logradouro](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](255) NOT NULL,
	[Id_Cliente] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Token]    Script Date: 24/09/2024 23:00:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Token](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Token] [varchar](max) NOT NULL,
	[DataCadastro] [datetime] NOT NULL,
	[DataExpiracao] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Logradouro]  WITH CHECK ADD  CONSTRAINT [FK_Clientes_Logradouro] FOREIGN KEY([Id_Cliente])
REFERENCES [dbo].[Clientes] ([Id])
GO
ALTER TABLE [dbo].[Logradouro] CHECK CONSTRAINT [FK_Clientes_Logradouro]
GO
/****** Object:  StoredProcedure [dbo].[UpdateCamposCliente]    Script Date: 24/09/2024 23:00:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateCamposCliente]
    @Id INT,
    @Campo NVARCHAR(20),
    @Valor NVARCHAR(MAX)
AS
BEGIN
    -- Verifica se o campo informado é válido para evitar SQL Injection
    IF @Campo NOT IN ('Nome','Email')
    BEGIN
        RAISERROR('Campo inválido.', 16, 1);
        RETURN;
    END

    DECLARE @Sql NVARCHAR(MAX);

    -- Monta a instrução SQL dinâmica para atualizar o campo
    SET @Sql = 'UPDATE Clientes SET ' + QUOTENAME(@Campo) + ' = @Valor WHERE id = @id';

    -- Executa a instrução SQL dinâmica usando sp_executesql
    EXEC sp_executesql @Sql, N'@Valor NVARCHAR(100), @id INT', @Valor, @Id;
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateCamposLogradouro]    Script Date: 24/09/2024 23:00:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateCamposLogradouro]
    @Id INT,
    @Campo NVARCHAR(20),
    @Valor NVARCHAR(MAX)
AS
BEGIN
    -- Verifica se o campo informado é válido para evitar SQL Injection
    IF @Campo NOT IN ('Descricao')
    BEGIN
        RAISERROR('Campo inválido.', 16, 1);
        RETURN;
    END

    DECLARE @Sql NVARCHAR(MAX);

    -- Monta a instrução SQL dinâmica para atualizar o campo
    SET @Sql = 'UPDATE Logradouro SET ' + QUOTENAME(@Campo) + ' = @Valor WHERE id = @id';

    -- Executa a instrução SQL dinâmica usando sp_executesql
    EXEC sp_executesql @Sql, N'@Valor NVARCHAR(100), @id INT', @Valor, @Id;
END;
GO
USE [master]
GO
ALTER DATABASE [Cliente] SET  READ_WRITE 
GO
