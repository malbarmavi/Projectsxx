USE [master]
GO
/****** Object:  Database [projects]    Script Date: 02/10/2016 12:01:00 ******/
CREATE DATABASE [projects] ON  PRIMARY 
( NAME = N'projects', FILENAME = N'D:\projects\projects.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'projects_log', FILENAME = N'D:\projects\projects_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [projects] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [projects].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [projects] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [projects] SET ANSI_NULLS OFF
GO
ALTER DATABASE [projects] SET ANSI_PADDING OFF
GO
ALTER DATABASE [projects] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [projects] SET ARITHABORT OFF
GO
ALTER DATABASE [projects] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [projects] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [projects] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [projects] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [projects] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [projects] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [projects] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [projects] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [projects] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [projects] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [projects] SET  DISABLE_BROKER
GO
ALTER DATABASE [projects] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [projects] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [projects] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [projects] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [projects] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [projects] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [projects] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [projects] SET  READ_WRITE
GO
ALTER DATABASE [projects] SET RECOVERY FULL
GO
ALTER DATABASE [projects] SET  MULTI_USER
GO
ALTER DATABASE [projects] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [projects] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'projects', N'ON'
GO
USE [projects]
GO
/****** Object:  Table [dbo].[users]    Script Date: 02/10/2016 12:01:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_name] [nvarchar](50) NOT NULL,
	[password] [nvarchar](100) NOT NULL,
	[first_name] [nvarchar](100) NOT NULL,
	[last_name] [nvarchar](100) NOT NULL,
	[email] [nvarchar](100) NOT NULL,
	[role] [tinyint] NOT NULL,
	[phone_number] [nvarchar](50) NULL,
	[linkden_url] [nvarchar](100) NULL,
	[notes] [nvarchar](250) NOT NULL,
	[company_id] [int] NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[task_details]    Script Date: 02/10/2016 12:01:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[task_details](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[task_id] [int] NOT NULL,
	[project_id] [int] NOT NULL,
 CONSTRAINT [PK_task_details] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[task]    Script Date: 02/10/2016 12:01:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[task](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](75) NOT NULL,
	[description] [nvarchar](150) NOT NULL,
	[priority] [tinyint] NOT NULL,
	[state] [tinyint] NOT NULL,
	[notes] [nvarchar](250) NOT NULL,
	[project_id] [int] NOT NULL,
 CONSTRAINT [PK_task] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[project]    Script Date: 02/10/2016 12:01:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[project](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](75) NOT NULL,
	[create_date] [date] NOT NULL,
	[dead_line] [date] NOT NULL,
	[notes] [nvarchar](250) NOT NULL,
	[parent] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[company_id] [int] NOT NULL,
 CONSTRAINT [PK_project] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[notification]    Script Date: 02/10/2016 12:01:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[notification](
	[id] [int] NOT NULL,
	[message] [nvarchar](150) NOT NULL,
	[seen] [bit] NOT NULL,
	[user_id] [int] NOT NULL,
 CONSTRAINT [PK_notification] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[company]    Script Date: 02/10/2016 12:01:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[company](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](150) NOT NULL,
	[address] [nvarchar](150) NOT NULL,
	[type] [nvarchar](60) NOT NULL,
	[main_branch] [bit] NOT NULL,
	[facebook_url] [nvarchar](100) NULL,
	[twitter_url] [nvarchar](100) NULL,
	[website_url] [nvarchar](100) NULL,
	[phone_number] [nvarchar](50) NULL,
	[owner_id] [int] NOT NULL,
 CONSTRAINT [PK_company] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[comment]    Script Date: 02/10/2016 12:01:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[comment](
	[id] [int] NOT NULL,
	[comment] [nvarchar](150) NOT NULL,
	[user_name] [int] NOT NULL,
	[task_id] [int] NOT NULL,
 CONSTRAINT [PK_comment] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF_users_role]    Script Date: 02/10/2016 12:01:03 ******/
ALTER TABLE [dbo].[users] ADD  CONSTRAINT [DF_users_role]  DEFAULT ((0)) FOR [role]
GO
