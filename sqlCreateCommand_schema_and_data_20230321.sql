USE [master]
GO
/****** Object:  Database [MyDB]    Script Date: 2023/3/21 下午 01:58:21 ******/
CREATE DATABASE [MyDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MyDB', FILENAME = N'D:\DataBase\MyDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MyDB_log', FILENAME = N'D:\DataBase\MyDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MyDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MyDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MyDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MyDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MyDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MyDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [MyDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MyDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MyDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MyDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MyDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MyDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MyDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MyDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MyDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MyDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MyDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MyDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MyDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MyDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MyDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MyDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MyDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MyDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MyDB] SET  MULTI_USER 
GO
ALTER DATABASE [MyDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MyDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MyDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MyDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MyDB] SET DELAYED_DURABILITY = DISABLED 
GO
USE [MyDB]
GO
/****** Object:  Table [dbo].[Attendance]    Script Date: 2023/3/21 下午 01:58:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attendance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WorkDate] [date] NOT NULL,
	[Shift] [nvarchar](50) NULL,
	[WeekWork] [bit] NOT NULL,
 CONSTRAINT [PK_Schedules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Branch]    Script Date: 2023/3/21 下午 01:58:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Branch](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Head] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Branch] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 2023/3/21 下午 01:58:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EId] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Image] [nvarchar](50) NOT NULL,
	[Branch] [nvarchar](50) NOT NULL,
	[JobTitle] [nvarchar](50) NOT NULL,
	[JobRank] [int] NOT NULL,
	[Sex] [nvarchar](10) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[EnglishName] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Shift] [nvarchar](50) NOT NULL,
	[StartDate] [date] NOT NULL,
	[Allow] [bit] NOT NULL,
	[BranchId] [int] NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[EId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HolidayDetails]    Script Date: 2023/3/21 下午 01:58:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HolidayDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EId] [nvarchar](50) NOT NULL,
	[HId] [int] NOT NULL,
	[BeginDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[State] [int] NOT NULL,
	[Remark] [nvarchar](max) NULL,
	[Prove] [nvarchar](max) NULL,
	[UsedDays] [int] NOT NULL,
	[BelongYear] [int] NOT NULL,
	[ApplyDate] [date] NOT NULL,
	[Reason] [nvarchar](max) NULL,
	[AllowManager] [nvarchar](50) NULL,
	[RangeDate] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_LeaveDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Holidays]    Script Date: 2023/3/21 下午 01:58:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Holidays](
	[HId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[TotalDays] [int] NOT NULL,
	[ProveType] [bit] NOT NULL,
	[AnyOne] [bit] NOT NULL,
 CONSTRAINT [PK_Leave] PRIMARY KEY CLUSTERED 
(
	[HId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OverTimeDetails]    Script Date: 2023/3/21 下午 01:58:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OverTimeDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EId] [nvarchar](50) NOT NULL,
	[OverTimeDate] [date] NOT NULL,
	[UserCheck] [bit] NOT NULL,
 CONSTRAINT [PK_OverTimeDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Plans]    Script Date: 2023/3/21 下午 01:58:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Plans](
	[Id] [uniqueidentifier] NOT NULL,
	[PlanTitle] [nvarchar](50) NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
 CONSTRAINT [PK_Plans_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Attendance] ON 

INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (1, CAST(N'2023-01-01' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (2, CAST(N'2023-01-02' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (3, CAST(N'2023-01-03' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (4, CAST(N'2023-01-04' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (5, CAST(N'2023-01-05' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (6, CAST(N'2023-01-06' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (7, CAST(N'2023-01-07' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (8, CAST(N'2023-01-08' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (9, CAST(N'2023-01-09' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (10, CAST(N'2023-01-10' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (11, CAST(N'2023-01-11' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (12, CAST(N'2023-01-12' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (13, CAST(N'2023-01-13' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (14, CAST(N'2023-01-14' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (15, CAST(N'2023-01-15' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (16, CAST(N'2023-01-16' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (17, CAST(N'2023-01-17' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (18, CAST(N'2023-01-18' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (19, CAST(N'2023-01-19' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (20, CAST(N'2023-01-20' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (21, CAST(N'2023-01-21' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (22, CAST(N'2023-01-22' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (23, CAST(N'2023-01-23' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (24, CAST(N'2023-01-24' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (25, CAST(N'2023-01-25' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (26, CAST(N'2023-01-26' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (27, CAST(N'2023-01-27' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (28, CAST(N'2023-01-28' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (29, CAST(N'2023-01-29' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (30, CAST(N'2023-01-30' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (31, CAST(N'2023-01-31' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (32, CAST(N'2023-02-01' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (33, CAST(N'2023-02-02' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (34, CAST(N'2023-02-03' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (35, CAST(N'2023-02-04' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (36, CAST(N'2023-02-05' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (37, CAST(N'2023-02-06' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (38, CAST(N'2023-02-07' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (39, CAST(N'2023-02-08' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (40, CAST(N'2023-02-09' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (41, CAST(N'2023-02-10' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (42, CAST(N'2023-02-11' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (43, CAST(N'2023-02-12' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (44, CAST(N'2023-02-13' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (45, CAST(N'2023-02-14' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (46, CAST(N'2023-02-15' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (47, CAST(N'2023-02-16' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (48, CAST(N'2023-02-17' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (49, CAST(N'2023-02-18' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (50, CAST(N'2023-02-19' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (51, CAST(N'2023-02-20' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (52, CAST(N'2023-02-21' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (53, CAST(N'2023-02-22' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (54, CAST(N'2023-02-23' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (55, CAST(N'2023-02-24' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (56, CAST(N'2023-02-25' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (57, CAST(N'2023-02-26' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (58, CAST(N'2023-02-27' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (59, CAST(N'2023-02-28' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (60, CAST(N'2023-03-01' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (61, CAST(N'2023-03-02' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (62, CAST(N'2023-03-03' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (63, CAST(N'2023-03-04' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (64, CAST(N'2023-03-05' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (65, CAST(N'2023-03-06' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (66, CAST(N'2023-03-07' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (67, CAST(N'2023-03-08' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (68, CAST(N'2023-03-09' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (69, CAST(N'2023-03-10' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (70, CAST(N'2023-03-11' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (71, CAST(N'2023-03-12' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (72, CAST(N'2023-03-13' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (73, CAST(N'2023-03-14' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (74, CAST(N'2023-03-15' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (75, CAST(N'2023-03-16' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (76, CAST(N'2023-03-17' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (77, CAST(N'2023-03-18' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (78, CAST(N'2023-03-19' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (79, CAST(N'2023-03-20' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (80, CAST(N'2023-03-21' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (81, CAST(N'2023-03-22' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (82, CAST(N'2023-03-23' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (83, CAST(N'2023-03-24' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (84, CAST(N'2023-03-25' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (85, CAST(N'2023-03-26' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (86, CAST(N'2023-03-27' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (87, CAST(N'2023-03-28' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (88, CAST(N'2023-03-29' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (89, CAST(N'2023-03-30' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (90, CAST(N'2023-03-31' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (91, CAST(N'2023-04-01' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (92, CAST(N'2023-04-02' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (93, CAST(N'2023-04-03' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (94, CAST(N'2023-04-04' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (95, CAST(N'2023-04-05' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (96, CAST(N'2023-04-06' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (97, CAST(N'2023-04-07' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (98, CAST(N'2023-04-08' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (99, CAST(N'2023-04-09' AS Date), N'B班', 0)
GO
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (100, CAST(N'2023-04-10' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (101, CAST(N'2023-04-11' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (102, CAST(N'2023-04-12' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (103, CAST(N'2023-04-13' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (104, CAST(N'2023-04-14' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (105, CAST(N'2023-04-15' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (106, CAST(N'2023-04-16' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (107, CAST(N'2023-04-17' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (108, CAST(N'2023-04-18' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (109, CAST(N'2023-04-19' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (110, CAST(N'2023-04-20' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (111, CAST(N'2023-04-21' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (112, CAST(N'2023-04-22' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (113, CAST(N'2023-04-23' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (114, CAST(N'2023-04-24' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (115, CAST(N'2023-04-25' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (116, CAST(N'2023-04-26' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (117, CAST(N'2023-04-27' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (118, CAST(N'2023-04-28' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (119, CAST(N'2023-04-29' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (120, CAST(N'2023-04-30' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (121, CAST(N'2023-05-01' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (122, CAST(N'2023-05-02' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (123, CAST(N'2023-05-03' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (124, CAST(N'2023-05-04' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (125, CAST(N'2023-05-05' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (126, CAST(N'2023-05-06' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (127, CAST(N'2023-05-07' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (128, CAST(N'2023-05-08' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (129, CAST(N'2023-05-09' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (130, CAST(N'2023-05-10' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (131, CAST(N'2023-05-11' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (132, CAST(N'2023-05-12' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (133, CAST(N'2023-05-13' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (134, CAST(N'2023-05-14' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (135, CAST(N'2023-05-15' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (136, CAST(N'2023-05-16' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (137, CAST(N'2023-05-17' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (138, CAST(N'2023-05-18' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (139, CAST(N'2023-05-19' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (140, CAST(N'2023-05-20' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (141, CAST(N'2023-05-21' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (142, CAST(N'2023-05-22' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (143, CAST(N'2023-05-23' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (144, CAST(N'2023-05-24' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (145, CAST(N'2023-05-25' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (146, CAST(N'2023-05-26' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (147, CAST(N'2023-05-27' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (148, CAST(N'2023-05-28' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (149, CAST(N'2023-05-29' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (150, CAST(N'2023-05-30' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (151, CAST(N'2023-05-31' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (152, CAST(N'2023-06-01' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (153, CAST(N'2023-06-02' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (154, CAST(N'2023-06-03' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (155, CAST(N'2023-06-04' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (156, CAST(N'2023-06-05' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (157, CAST(N'2023-06-06' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (158, CAST(N'2023-06-07' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (159, CAST(N'2023-06-08' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (160, CAST(N'2023-06-09' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (161, CAST(N'2023-06-10' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (162, CAST(N'2023-06-11' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (163, CAST(N'2023-06-12' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (164, CAST(N'2023-06-13' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (165, CAST(N'2023-06-14' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (166, CAST(N'2023-06-15' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (167, CAST(N'2023-06-16' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (168, CAST(N'2023-06-17' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (169, CAST(N'2023-06-18' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (170, CAST(N'2023-06-19' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (171, CAST(N'2023-06-20' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (172, CAST(N'2023-06-21' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (173, CAST(N'2023-06-22' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (174, CAST(N'2023-06-23' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (175, CAST(N'2023-06-24' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (176, CAST(N'2023-06-25' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (177, CAST(N'2023-06-26' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (178, CAST(N'2023-06-27' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (179, CAST(N'2023-06-28' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (180, CAST(N'2023-06-29' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (181, CAST(N'2023-06-30' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (182, CAST(N'2023-07-01' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (183, CAST(N'2023-07-02' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (184, CAST(N'2023-07-03' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (185, CAST(N'2023-07-04' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (186, CAST(N'2023-07-05' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (187, CAST(N'2023-07-06' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (188, CAST(N'2023-07-07' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (189, CAST(N'2023-07-08' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (190, CAST(N'2023-07-09' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (191, CAST(N'2023-07-10' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (192, CAST(N'2023-07-11' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (193, CAST(N'2023-07-12' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (194, CAST(N'2023-07-13' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (195, CAST(N'2023-07-14' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (196, CAST(N'2023-07-15' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (197, CAST(N'2023-07-16' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (198, CAST(N'2023-07-17' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (199, CAST(N'2023-07-18' AS Date), N'B班', 1)
GO
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (200, CAST(N'2023-07-19' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (201, CAST(N'2023-07-20' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (202, CAST(N'2023-07-21' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (203, CAST(N'2023-07-22' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (204, CAST(N'2023-07-23' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (205, CAST(N'2023-07-24' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (206, CAST(N'2023-07-25' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (207, CAST(N'2023-07-26' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (208, CAST(N'2023-07-27' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (209, CAST(N'2023-07-28' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (210, CAST(N'2023-07-29' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (211, CAST(N'2023-07-30' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (212, CAST(N'2023-07-31' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (213, CAST(N'2023-08-01' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (214, CAST(N'2023-08-02' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (215, CAST(N'2023-08-03' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (216, CAST(N'2023-08-04' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (217, CAST(N'2023-08-05' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (218, CAST(N'2023-08-06' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (219, CAST(N'2023-08-07' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (220, CAST(N'2023-08-08' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (221, CAST(N'2023-08-09' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (222, CAST(N'2023-08-10' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (223, CAST(N'2023-08-11' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (224, CAST(N'2023-08-12' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (225, CAST(N'2023-08-13' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (226, CAST(N'2023-08-14' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (227, CAST(N'2023-08-15' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (228, CAST(N'2023-08-16' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (229, CAST(N'2023-08-17' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (230, CAST(N'2023-08-18' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (231, CAST(N'2023-08-19' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (232, CAST(N'2023-08-20' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (233, CAST(N'2023-08-21' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (234, CAST(N'2023-08-22' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (235, CAST(N'2023-08-23' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (236, CAST(N'2023-08-24' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (237, CAST(N'2023-08-25' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (238, CAST(N'2023-08-26' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (239, CAST(N'2023-08-27' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (240, CAST(N'2023-08-28' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (241, CAST(N'2023-08-29' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (242, CAST(N'2023-08-30' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (243, CAST(N'2023-08-31' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (244, CAST(N'2023-09-01' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (245, CAST(N'2023-09-02' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (246, CAST(N'2023-09-03' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (247, CAST(N'2023-09-04' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (248, CAST(N'2023-09-05' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (249, CAST(N'2023-09-06' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (250, CAST(N'2023-09-07' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (251, CAST(N'2023-09-08' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (252, CAST(N'2023-09-09' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (253, CAST(N'2023-09-10' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (254, CAST(N'2023-09-11' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (255, CAST(N'2023-09-12' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (256, CAST(N'2023-09-13' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (257, CAST(N'2023-09-14' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (258, CAST(N'2023-09-15' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (259, CAST(N'2023-09-16' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (260, CAST(N'2023-09-17' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (261, CAST(N'2023-09-18' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (262, CAST(N'2023-09-19' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (263, CAST(N'2023-09-20' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (264, CAST(N'2023-09-21' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (265, CAST(N'2023-09-22' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (266, CAST(N'2023-09-23' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (267, CAST(N'2023-09-24' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (268, CAST(N'2023-09-25' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (269, CAST(N'2023-09-26' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (270, CAST(N'2023-09-27' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (271, CAST(N'2023-09-28' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (272, CAST(N'2023-09-29' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (273, CAST(N'2023-09-30' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (274, CAST(N'2023-10-01' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (275, CAST(N'2023-10-02' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (276, CAST(N'2023-10-03' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (277, CAST(N'2023-10-04' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (278, CAST(N'2023-10-05' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (279, CAST(N'2023-10-06' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (280, CAST(N'2023-10-07' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (281, CAST(N'2023-10-08' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (282, CAST(N'2023-10-09' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (283, CAST(N'2023-10-10' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (284, CAST(N'2023-10-11' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (285, CAST(N'2023-10-12' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (286, CAST(N'2023-10-13' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (287, CAST(N'2023-10-14' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (288, CAST(N'2023-10-15' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (289, CAST(N'2023-10-16' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (290, CAST(N'2023-10-17' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (291, CAST(N'2023-10-18' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (292, CAST(N'2023-10-19' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (293, CAST(N'2023-10-20' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (294, CAST(N'2023-10-21' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (295, CAST(N'2023-10-22' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (296, CAST(N'2023-10-23' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (297, CAST(N'2023-10-24' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (298, CAST(N'2023-10-25' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (299, CAST(N'2023-10-26' AS Date), N'B班', 1)
GO
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (300, CAST(N'2023-10-27' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (301, CAST(N'2023-10-28' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (302, CAST(N'2023-10-29' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (303, CAST(N'2023-10-30' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (304, CAST(N'2023-10-31' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (305, CAST(N'2023-11-01' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (306, CAST(N'2023-11-02' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (307, CAST(N'2023-11-03' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (308, CAST(N'2023-11-04' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (309, CAST(N'2023-11-05' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (310, CAST(N'2023-11-06' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (311, CAST(N'2023-11-07' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (312, CAST(N'2023-11-08' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (313, CAST(N'2023-11-09' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (314, CAST(N'2023-11-10' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (315, CAST(N'2023-11-11' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (316, CAST(N'2023-11-12' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (317, CAST(N'2023-11-13' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (318, CAST(N'2023-11-14' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (319, CAST(N'2023-11-15' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (320, CAST(N'2023-11-16' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (321, CAST(N'2023-11-17' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (322, CAST(N'2023-11-18' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (323, CAST(N'2023-11-19' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (324, CAST(N'2023-11-20' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (325, CAST(N'2023-11-21' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (326, CAST(N'2023-11-22' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (327, CAST(N'2023-11-23' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (328, CAST(N'2023-11-24' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (329, CAST(N'2023-11-25' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (330, CAST(N'2023-11-26' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (331, CAST(N'2023-11-27' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (332, CAST(N'2023-11-28' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (333, CAST(N'2023-11-29' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (334, CAST(N'2023-11-30' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (335, CAST(N'2023-12-01' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (336, CAST(N'2023-12-02' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (337, CAST(N'2023-12-03' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (338, CAST(N'2023-12-04' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (339, CAST(N'2023-12-05' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (340, CAST(N'2023-12-06' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (341, CAST(N'2023-12-07' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (342, CAST(N'2023-12-08' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (343, CAST(N'2023-12-09' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (344, CAST(N'2023-12-10' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (345, CAST(N'2023-12-11' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (346, CAST(N'2023-12-12' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (347, CAST(N'2023-12-13' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (348, CAST(N'2023-12-14' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (349, CAST(N'2023-12-15' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (350, CAST(N'2023-12-16' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (351, CAST(N'2023-12-17' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (352, CAST(N'2023-12-18' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (353, CAST(N'2023-12-19' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (354, CAST(N'2023-12-20' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (355, CAST(N'2023-12-21' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (356, CAST(N'2023-12-22' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (357, CAST(N'2023-12-23' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (358, CAST(N'2023-12-24' AS Date), N'A班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (359, CAST(N'2023-12-25' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (360, CAST(N'2023-12-26' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (361, CAST(N'2023-12-27' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (362, CAST(N'2023-12-28' AS Date), N'A班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (363, CAST(N'2023-12-29' AS Date), N'B班', 1)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (364, CAST(N'2023-12-30' AS Date), N'B班', 0)
INSERT [dbo].[Attendance] ([Id], [WorkDate], [Shift], [WeekWork]) VALUES (365, CAST(N'2023-12-31' AS Date), N'A班', 0)
SET IDENTITY_INSERT [dbo].[Attendance] OFF
GO
SET IDENTITY_INSERT [dbo].[Branch] ON 

INSERT [dbo].[Branch] ([Id], [Title], [Head]) VALUES (1, N'系統部', N'admin')
INSERT [dbo].[Branch] ([Id], [Title], [Head]) VALUES (2, N'製造部', N'E004')
INSERT [dbo].[Branch] ([Id], [Title], [Head]) VALUES (3, N'製程部', N'E003')
INSERT [dbo].[Branch] ([Id], [Title], [Head]) VALUES (4, N'客服部', N'E005')
INSERT [dbo].[Branch] ([Id], [Title], [Head]) VALUES (5, N'財務部', N'E007')
INSERT [dbo].[Branch] ([Id], [Title], [Head]) VALUES (6, N'會計部', N'E008')
SET IDENTITY_INSERT [dbo].[Branch] OFF
GO
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'admin', N'管理員', N'638146049357511157man.jpg', N'系統部', N'工程師', 0, N'男', N'ken@gmail.com', N'0926666777', N'Ken', N'admin', N'常日班', CAST(N'2019-07-07' AS Date), 1, 1)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E001', N'王曉薇', N'woman1.jpg', N'製造部', N'作業員', 1, N'女', N'aa.xx@mail.com', N'0912-345-678', N'Alice', N'F123456789', N'A班', CAST(N'2010-02-26' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E002', N'張裕翔', N'man.jpg', N'製造部', N'作業員', 1, N'男', N'cc.aa@mail.com', N'0925-112-637', N'John', N'F123456789', N'A班', CAST(N'2018-09-13' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E003', N'陳菲', N'638146791057722497woman4.jpg', N'製程部', N'製程主管', 2, N'女', N'zz.ss@mail.com', N'0913-975-666', N'Christine', N'F123456789', N'常日班', CAST(N'2002-05-05' AS Date), 1, 3)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E004', N'瑞淋', N'woman3.jpg', N'製造部', N'製造主管', 2, N'女', N'bb.oo@mail.com', N'0933-276-336', N'LinLin', N'F123456789', N'B班', CAST(N'2015-06-08' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E005', N'都瑞咪', N'woman4.jpg', N'客服部', N'客服人員', 1, N'女', N'cd.liu0607@gmail.com', N'0926050652', N'Doremi', N'F123456789', N'常日班', CAST(N'2023-02-15' AS Date), 1, 4)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E006', N'王大同', N'man1.jpg', N'製造部', N'作業員', 1, N'男', N'cd.liu@gmail.com', N'0933556788', N'Jacky', N'F123456789', N'B班', CAST(N'2000-06-30' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E007', N'吳香美', N'woman5.jpg', N'財務部', N'財務主管', 2, N'女', N'aa@gmail.com', N'0900876543', N'May', N'F123456789', N'常日班', CAST(N'2007-02-07' AS Date), 1, 5)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E008', N'劉瑞淋', N'woman6.jpg', N'會計部', N'會計主管', 2, N'女', N'cd.liu0607@gmail.com', N'0926050652', N'LinLin', N'F123456789', N'常日班', CAST(N'2023-02-02' AS Date), 1, 6)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E009', N'王凱欣', N'woman7.jpg', N'財務部', N'財務助理', 1, N'女', N'xx.gg@gmail.com', N'0900111222', N'caisin', N'F123456789', N'常日班', CAST(N'2023-03-01' AS Date), 1, 5)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E010', N'曾太好', N'man2.jpg', N'會計部', N'會計助理', 1, N'男', N'xx.gg@gmail.com', N'0900666888', N'Tai', N'F123456789', N'常日班', CAST(N'2000-06-06' AS Date), 1, 6)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E011', N'周蔚', N'woman8.jpg', N'客服部', N'客服人員', 1, N'女', N'ayw@gmail.com', N'0926050652', N'ayw', N'F123456789', N'常日班', CAST(N'2005-03-17' AS Date), 1, 4)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E012', N'黃萎萍', N'woman9.jpg', N'客服部', N'客服主管', 2, N'女', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'常日班', CAST(N'2009-11-20' AS Date), 1, 4)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E013', N'涂雅慧', N'woman1.jpg', N'製造部', N'作業員', 1, N'女', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'B班', CAST(N'2009-11-20' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E014', N'黃萎萍', N'638146794597692730woman9.jpg', N'製造部', N'作業員', 1, N'女', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'A班', CAST(N'2009-11-20' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E015', N'曲雅玲', N'woman3.jpg', N'製造部', N'作業員', 1, N'女', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'B班', CAST(N'2009-11-20' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E016', N'陶于婷', N'woman4.jpg', N'製造部', N'作業員', 1, N'女', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'A班', CAST(N'2009-11-20' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E017', N'鄭佳蓉', N'woman5.jpg', N'製造部', N'作業員', 1, N'女', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'B班', CAST(N'2009-11-20' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E018', N'黃淑芳', N'woman6.jpg', N'製造部', N'作業員', 1, N'女', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'A班', CAST(N'2009-11-20' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E019', N'王吟真', N'woman7.jpg', N'製造部', N'作業員', 1, N'女', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'B班', CAST(N'2009-11-20' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E020', N'謝宜紫', N'woman8.jpg', N'製造部', N'作業員', 1, N'女', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'A班', CAST(N'2009-11-20' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E021', N'吳宏偉', N'638146909007794020man11.jpg', N'製造部', N'作業員', 1, N'男', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'B班', CAST(N'2009-11-20' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E022', N'劉湖昆', N'man4.jpg', N'製造部', N'作業員', 1, N'男', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'A班', CAST(N'2009-11-20' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E023', N'陳俊嘉', N'man5.jpg', N'製造部', N'作業員', 1, N'男', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'B班', CAST(N'2009-11-20' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E024', N'張奕廷', N'man6.jpg', N'製造部', N'作業員', 1, N'男', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'A班', CAST(N'2009-11-20' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E025', N'吳秉雅', N'man7.jpg', N'製造部', N'作業員', 1, N'男', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'B班', CAST(N'2009-11-20' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E026', N'劉佑夫', N'man8.jpg', N'製造部', N'作業員', 1, N'男', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'A班', CAST(N'2009-11-20' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E027', N'羅士哲', N'638146826153061789638137009650984796man2.jpg', N'製造部', N'作業員', 1, N'男', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'B班', CAST(N'2009-11-20' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E028', N'林宗玫', N'man1.jpg', N'製造部', N'作業員', 1, N'男', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'A班', CAST(N'2009-11-20' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E029', N'柏毅', N'woman9.jpg', N'客服部', N'客服人員', 1, N'女', N'ayw@gmail.com', N'0926050652', N'ayw', N'F123456789', N'常日班', CAST(N'2005-03-17' AS Date), 1, 4)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E030', N'陳維', N'638149026112567767638146909007794020man11.jpg', N'製造部', N'作業員', 1, N'男', N'cd.liu0607@gmail.com', N'0912345678', N'Ken', N'F123456789', N'A班', CAST(N'2023-03-17' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E031', N' 王曉明', N'638149879357243239638146823832990553man4.jpg', N'製程部', N'工程師', 1, N'男', N'cd.liu0607@gmail.com', N'0912345678', N'MIN', N'F123456789', N'B班', CAST(N'2017-08-18' AS Date), 1, 3)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E032', N' 陳諄', N'638149883886254352man4.jpg', N'製程部', N'工程師', 1, N'男', N'cd.liu0607@gmail.com', N'0912345678', N'din', N'F123456789', N'A班', CAST(N'2006-01-01' AS Date), 1, 3)
GO
SET IDENTITY_INSERT [dbo].[HolidayDetails] ON 

INSERT [dbo].[HolidayDetails] ([Id], [EId], [HId], [BeginDate], [EndDate], [State], [Remark], [Prove], [UsedDays], [BelongYear], [ApplyDate], [Reason], [AllowManager], [RangeDate]) VALUES (1, N'E001', 1, CAST(N'2023-03-02' AS Date), CAST(N'2023-03-10' AS Date), 2, N'私事待辦', NULL, 5, 2023, CAST(N'2023-03-09' AS Date), NULL, N'admin', N'3/2/2023,3/10/2023,3/7/2023,3/6/2023,3/3/2023')
INSERT [dbo].[HolidayDetails] ([Id], [EId], [HId], [BeginDate], [EndDate], [State], [Remark], [Prove], [UsedDays], [BelongYear], [ApplyDate], [Reason], [AllowManager], [RangeDate]) VALUES (2, N'E011', 1, CAST(N'2023-03-03' AS Date), CAST(N'2023-03-24' AS Date), 2, N'出國', NULL, 16, 2023, CAST(N'2023-03-09' AS Date), NULL, N'admin', N'3/3/2023,3/24/2023,3/23/2023,3/22/2023,3/21/2023,3/20/2023,3/17/2023,3/16/2023,3/15/2023,3/14/2023,3/13/2023,3/10/2023,3/9/2023,3/8/2023,3/7/2023,3/6/2023')
INSERT [dbo].[HolidayDetails] ([Id], [EId], [HId], [BeginDate], [EndDate], [State], [Remark], [Prove], [UsedDays], [BelongYear], [ApplyDate], [Reason], [AllowManager], [RangeDate]) VALUES (3, N'E006', 1, CAST(N'2023-03-13' AS Date), CAST(N'2023-03-28' AS Date), 2, N'私事待辦', NULL, 8, 2023, CAST(N'2023-03-09' AS Date), NULL, N'admin', N'3/13/2023,3/28/2023,3/25/2023,3/24/2023,3/21/2023,3/20/2023,3/17/2023,3/16/2023')
INSERT [dbo].[HolidayDetails] ([Id], [EId], [HId], [BeginDate], [EndDate], [State], [Remark], [Prove], [UsedDays], [BelongYear], [ApplyDate], [Reason], [AllowManager], [RangeDate]) VALUES (4, N'E006', 3, CAST(N'2023-03-09' AS Date), CAST(N'2023-03-12' AS Date), 2, N'身體不適', N'6381397713402159081669933-XXL.jpg', 2, 2023, CAST(N'2023-03-09' AS Date), NULL, N'admin', N'3/9/2023,3/12/2023')
INSERT [dbo].[HolidayDetails] ([Id], [EId], [HId], [BeginDate], [EndDate], [State], [Remark], [Prove], [UsedDays], [BelongYear], [ApplyDate], [Reason], [AllowManager], [RangeDate]) VALUES (1001, N'E004', 2, CAST(N'2023-01-04' AS Date), CAST(N'2023-01-12' AS Date), 2, N'私事待辦', NULL, 5, 2023, CAST(N'2023-03-15' AS Date), NULL, N'admin', N'1/4/2023,1/12/2023,1/11/2023,1/8/2023,1/7/2023')
INSERT [dbo].[HolidayDetails] ([Id], [EId], [HId], [BeginDate], [EndDate], [State], [Remark], [Prove], [UsedDays], [BelongYear], [ApplyDate], [Reason], [AllowManager], [RangeDate]) VALUES (2001, N'E001', 3, CAST(N'2023-03-15' AS Date), CAST(N'2023-03-18' AS Date), 2, N'身體不適', N'638145145868625897w580.jpg', 2, 2023, CAST(N'2023-03-15' AS Date), NULL, N'admin', N'3/15/2023,3/18/2023')
INSERT [dbo].[HolidayDetails] ([Id], [EId], [HId], [BeginDate], [EndDate], [State], [Remark], [Prove], [UsedDays], [BelongYear], [ApplyDate], [Reason], [AllowManager], [RangeDate]) VALUES (2002, N'E001', 3, CAST(N'2023-01-01' AS Date), CAST(N'2023-01-06' AS Date), 2, N'身體不適', N'638145187218018682w580.jpg', 4, 2023, CAST(N'2023-03-15' AS Date), NULL, N'admin', N'1/1/2023,1/6/2023,1/5/2023,1/2/2023')
INSERT [dbo].[HolidayDetails] ([Id], [EId], [HId], [BeginDate], [EndDate], [State], [Remark], [Prove], [UsedDays], [BelongYear], [ApplyDate], [Reason], [AllowManager], [RangeDate]) VALUES (2003, N'E021', 9, CAST(N'2023-06-08' AS Date), CAST(N'2023-06-17' AS Date), 2, N'結婚', N'6381452090833771818652d44371c95267402134db58175c15.jpg', 6, 2023, CAST(N'2023-03-15' AS Date), NULL, N'admin', N'6/8/2023,6/17/2023,6/16/2023,6/13/2023,6/12/2023,6/9/2023')
INSERT [dbo].[HolidayDetails] ([Id], [EId], [HId], [BeginDate], [EndDate], [State], [Remark], [Prove], [UsedDays], [BelongYear], [ApplyDate], [Reason], [AllowManager], [RangeDate]) VALUES (2004, N'admin', 9, CAST(N'2023-07-17' AS Date), CAST(N'2023-07-26' AS Date), 2, N'婚假', N'6381452362663703958652d44371c95267402134db58175c15.jpg', 8, 2023, CAST(N'2023-03-16' AS Date), NULL, N'admin', N'7/17/2023,7/26/2023,7/25/2023,7/24/2023,7/21/2023,7/20/2023,7/19/2023,7/18/2023')
INSERT [dbo].[HolidayDetails] ([Id], [EId], [HId], [BeginDate], [EndDate], [State], [Remark], [Prove], [UsedDays], [BelongYear], [ApplyDate], [Reason], [AllowManager], [RangeDate]) VALUES (2005, N'admin', 1, CAST(N'2023-02-02' AS Date), CAST(N'2023-02-03' AS Date), 2, N' 私事待辦', NULL, 2, 2023, CAST(N'2023-03-16' AS Date), NULL, N'admin', N'2/2/2023,2/3/2023')
INSERT [dbo].[HolidayDetails] ([Id], [EId], [HId], [BeginDate], [EndDate], [State], [Remark], [Prove], [UsedDays], [BelongYear], [ApplyDate], [Reason], [AllowManager], [RangeDate]) VALUES (2006, N'admin', 1, CAST(N'2023-03-08' AS Date), CAST(N'2023-03-17' AS Date), 2, N'私事待辦', NULL, 8, 2023, CAST(N'2023-03-16' AS Date), NULL, N'admin', N'3/8/2023,3/17/2023,3/16/2023,3/15/2023,3/14/2023,3/13/2023,3/10/2023,3/9/2023')
INSERT [dbo].[HolidayDetails] ([Id], [EId], [HId], [BeginDate], [EndDate], [State], [Remark], [Prove], [UsedDays], [BelongYear], [ApplyDate], [Reason], [AllowManager], [RangeDate]) VALUES (2007, N'admin', 1, CAST(N'2023-05-03' AS Date), CAST(N'2023-05-04' AS Date), 2, N'私事待辦', NULL, 2, 2023, CAST(N'2023-03-16' AS Date), NULL, N'admin', N'5/3/2023,5/4/2023')
INSERT [dbo].[HolidayDetails] ([Id], [EId], [HId], [BeginDate], [EndDate], [State], [Remark], [Prove], [UsedDays], [BelongYear], [ApplyDate], [Reason], [AllowManager], [RangeDate]) VALUES (3001, N'admin', 3, CAST(N'2023-04-12' AS Date), CAST(N'2023-04-12' AS Date), 2, N'身體不適', N'638145580870245307w580.jpg', 1, 2023, CAST(N'2023-03-16' AS Date), NULL, N'admin', N'4/12/2023')
INSERT [dbo].[HolidayDetails] ([Id], [EId], [HId], [BeginDate], [EndDate], [State], [Remark], [Prove], [UsedDays], [BelongYear], [ApplyDate], [Reason], [AllowManager], [RangeDate]) VALUES (4001, N'E004', 1, CAST(N'2023-03-01' AS Date), CAST(N'2023-03-21' AS Date), 2, N'私事待辦', NULL, 11, 2023, CAST(N'2023-03-18' AS Date), NULL, N'E004', N'3/1/2023,3/21/2023,3/20/2023,3/17/2023,3/16/2023,3/13/2023,3/12/2023,3/9/2023,3/8/2023,3/5/2023,3/4/2023')
INSERT [dbo].[HolidayDetails] ([Id], [EId], [HId], [BeginDate], [EndDate], [State], [Remark], [Prove], [UsedDays], [BelongYear], [ApplyDate], [Reason], [AllowManager], [RangeDate]) VALUES (4002, N'E004', 1, CAST(N'2023-02-21' AS Date), CAST(N'2023-02-28' AS Date), 2, N'私事待辦', NULL, 4, 2023, CAST(N'2023-03-19' AS Date), NULL, N'E004', N'2/21/2023,2/28/2023,2/25/2023,2/24/2023')
INSERT [dbo].[HolidayDetails] ([Id], [EId], [HId], [BeginDate], [EndDate], [State], [Remark], [Prove], [UsedDays], [BelongYear], [ApplyDate], [Reason], [AllowManager], [RangeDate]) VALUES (5001, N'E008', 2, CAST(N'2023-03-20' AS Date), CAST(N'2023-03-22' AS Date), 2, N'私事待辦', NULL, 3, 2023, CAST(N'2023-03-19' AS Date), NULL, N'E008', N'3/20/2023,3/22/2023,3/21/2023')
INSERT [dbo].[HolidayDetails] ([Id], [EId], [HId], [BeginDate], [EndDate], [State], [Remark], [Prove], [UsedDays], [BelongYear], [ApplyDate], [Reason], [AllowManager], [RangeDate]) VALUES (5002, N'E030', 1, CAST(N'2023-03-22' AS Date), CAST(N'2023-03-22' AS Date), 2, N'私事待辦', NULL, 1, 2023, CAST(N'2023-03-20' AS Date), NULL, N'E004', N'3/22/2023')
INSERT [dbo].[HolidayDetails] ([Id], [EId], [HId], [BeginDate], [EndDate], [State], [Remark], [Prove], [UsedDays], [BelongYear], [ApplyDate], [Reason], [AllowManager], [RangeDate]) VALUES (5003, N'E030', 2, CAST(N'2023-03-23' AS Date), CAST(N'2023-03-23' AS Date), 2, N'私事待辦', NULL, 1, 2023, CAST(N'2023-03-20' AS Date), NULL, N'E004', N'3/23/2023')
INSERT [dbo].[HolidayDetails] ([Id], [EId], [HId], [BeginDate], [EndDate], [State], [Remark], [Prove], [UsedDays], [BelongYear], [ApplyDate], [Reason], [AllowManager], [RangeDate]) VALUES (6002, N'E030', 2, CAST(N'2023-03-26' AS Date), CAST(N'2023-03-26' AS Date), 2, N'私事待辦', NULL, 1, 2023, CAST(N'2023-03-20' AS Date), NULL, N'E004', N'3/26/2023')
INSERT [dbo].[HolidayDetails] ([Id], [EId], [HId], [BeginDate], [EndDate], [State], [Remark], [Prove], [UsedDays], [BelongYear], [ApplyDate], [Reason], [AllowManager], [RangeDate]) VALUES (7002, N'E031', 1, CAST(N'2023-03-09' AS Date), CAST(N'2023-03-16' AS Date), 2, N'私事待辦', NULL, 4, 2023, CAST(N'2023-03-21' AS Date), NULL, N'E003', N'3/9/2023,3/16/2023,3/13/2023,3/12/2023')
INSERT [dbo].[HolidayDetails] ([Id], [EId], [HId], [BeginDate], [EndDate], [State], [Remark], [Prove], [UsedDays], [BelongYear], [ApplyDate], [Reason], [AllowManager], [RangeDate]) VALUES (7003, N'E031', 2, CAST(N'2023-03-25' AS Date), CAST(N'2023-03-29' AS Date), 2, N'私事待辦', NULL, 3, 2023, CAST(N'2023-03-21' AS Date), NULL, N'E003', N'3/25/2023,3/29/2023,3/28/2023')
INSERT [dbo].[HolidayDetails] ([Id], [EId], [HId], [BeginDate], [EndDate], [State], [Remark], [Prove], [UsedDays], [BelongYear], [ApplyDate], [Reason], [AllowManager], [RangeDate]) VALUES (7004, N'E032', 1, CAST(N'2023-03-18' AS Date), CAST(N'2023-03-26' AS Date), 2, N'私事待辦', NULL, 5, 2023, CAST(N'2023-03-21' AS Date), NULL, N'E003', N'3/18/2023,3/26/2023,3/23/2023,3/22/2023,3/19/2023')
INSERT [dbo].[HolidayDetails] ([Id], [EId], [HId], [BeginDate], [EndDate], [State], [Remark], [Prove], [UsedDays], [BelongYear], [ApplyDate], [Reason], [AllowManager], [RangeDate]) VALUES (7005, N'E003', 1, CAST(N'2023-03-20' AS Date), CAST(N'2023-03-25' AS Date), 2, N'私事待辦', NULL, 6, 2023, CAST(N'2023-03-21' AS Date), NULL, N'E003', N'3/20/2023,3/25/2023,3/24/2023,3/23/2023,3/22/2023,3/21/2023')
INSERT [dbo].[HolidayDetails] ([Id], [EId], [HId], [BeginDate], [EndDate], [State], [Remark], [Prove], [UsedDays], [BelongYear], [ApplyDate], [Reason], [AllowManager], [RangeDate]) VALUES (7006, N'E006', 1, CAST(N'2023-06-08' AS Date), CAST(N'2023-06-13' AS Date), 1, N'私事待辦', NULL, 4, 2023, CAST(N'2023-03-21' AS Date), NULL, NULL, N'6/8/2023,6/13/2023,6/12/2023,6/9/2023')
SET IDENTITY_INSERT [dbo].[HolidayDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[Holidays] ON 

INSERT [dbo].[Holidays] ([HId], [Title], [TotalDays], [ProveType], [AnyOne]) VALUES (1, N'特休', 3, 0, 1)
INSERT [dbo].[Holidays] ([HId], [Title], [TotalDays], [ProveType], [AnyOne]) VALUES (2, N'事假', 14, 0, 1)
INSERT [dbo].[Holidays] ([HId], [Title], [TotalDays], [ProveType], [AnyOne]) VALUES (3, N'病假', 30, 1, 1)
INSERT [dbo].[Holidays] ([HId], [Title], [TotalDays], [ProveType], [AnyOne]) VALUES (4, N'生理假', 12, 0, 0)
INSERT [dbo].[Holidays] ([HId], [Title], [TotalDays], [ProveType], [AnyOne]) VALUES (5, N'家照假', 14, 1, 1)
INSERT [dbo].[Holidays] ([HId], [Title], [TotalDays], [ProveType], [AnyOne]) VALUES (6, N'產檢假', 7, 1, 0)
INSERT [dbo].[Holidays] ([HId], [Title], [TotalDays], [ProveType], [AnyOne]) VALUES (7, N'陪產假', 7, 1, 1)
INSERT [dbo].[Holidays] ([HId], [Title], [TotalDays], [ProveType], [AnyOne]) VALUES (8, N'安胎假', 30, 1, 0)
INSERT [dbo].[Holidays] ([HId], [Title], [TotalDays], [ProveType], [AnyOne]) VALUES (9, N'婚假', 8, 1, 1)
SET IDENTITY_INSERT [dbo].[Holidays] OFF
GO
SET IDENTITY_INSERT [dbo].[OverTimeDetails] ON 

INSERT [dbo].[OverTimeDetails] ([Id], [EId], [OverTimeDate], [UserCheck]) VALUES (1033, N'E002', CAST(N'2023-03-08' AS Date), 0)
INSERT [dbo].[OverTimeDetails] ([Id], [EId], [OverTimeDate], [UserCheck]) VALUES (1034, N'E014', CAST(N'2023-03-08' AS Date), 0)
INSERT [dbo].[OverTimeDetails] ([Id], [EId], [OverTimeDate], [UserCheck]) VALUES (1035, N'E016', CAST(N'2023-03-08' AS Date), 0)
INSERT [dbo].[OverTimeDetails] ([Id], [EId], [OverTimeDate], [UserCheck]) VALUES (1036, N'E030', CAST(N'2023-03-08' AS Date), 1)
INSERT [dbo].[OverTimeDetails] ([Id], [EId], [OverTimeDate], [UserCheck]) VALUES (1037, N'E014', CAST(N'2023-03-24' AS Date), 0)
INSERT [dbo].[OverTimeDetails] ([Id], [EId], [OverTimeDate], [UserCheck]) VALUES (1038, N'E016', CAST(N'2023-03-24' AS Date), 0)
INSERT [dbo].[OverTimeDetails] ([Id], [EId], [OverTimeDate], [UserCheck]) VALUES (1039, N'E018', CAST(N'2023-03-24' AS Date), 0)
INSERT [dbo].[OverTimeDetails] ([Id], [EId], [OverTimeDate], [UserCheck]) VALUES (1040, N'E030', CAST(N'2023-03-24' AS Date), 1)
SET IDENTITY_INSERT [dbo].[OverTimeDetails] OFF
GO
INSERT [dbo].[Plans] ([Id], [PlanTitle], [StartDate], [EndDate]) VALUES (N'58004e47-b13d-4a7e-8c6b-a8bcfba6f85a', N'11', CAST(N'2023-03-18' AS Date), CAST(N'2023-03-19' AS Date))
GO
ALTER TABLE [dbo].[Branch]  WITH CHECK ADD  CONSTRAINT [FK_Branch_Employees] FOREIGN KEY([Head])
REFERENCES [dbo].[Employees] ([EId])
GO
ALTER TABLE [dbo].[Branch] CHECK CONSTRAINT [FK_Branch_Employees]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Branch] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Branch]
GO
ALTER TABLE [dbo].[HolidayDetails]  WITH CHECK ADD  CONSTRAINT [FK_HolidayDetails_Employees] FOREIGN KEY([EId])
REFERENCES [dbo].[Employees] ([EId])
GO
ALTER TABLE [dbo].[HolidayDetails] CHECK CONSTRAINT [FK_HolidayDetails_Employees]
GO
ALTER TABLE [dbo].[HolidayDetails]  WITH CHECK ADD  CONSTRAINT [FK_HolidayDetails_Holidays] FOREIGN KEY([HId])
REFERENCES [dbo].[Holidays] ([HId])
GO
ALTER TABLE [dbo].[HolidayDetails] CHECK CONSTRAINT [FK_HolidayDetails_Holidays]
GO
ALTER TABLE [dbo].[OverTimeDetails]  WITH CHECK ADD  CONSTRAINT [FK_OverTimeDetails_Employees] FOREIGN KEY([EId])
REFERENCES [dbo].[Employees] ([EId])
GO
ALTER TABLE [dbo].[OverTimeDetails] CHECK CONSTRAINT [FK_OverTimeDetails_Employees]
GO
/****** Object:  StoredProcedure [dbo].[GetMemberById]    Script Date: 2023/3/21 下午 01:58:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetMemberById]
		(@id int)
AS
BEGIN
	SELECT * FROM Members WHERE Id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[MemberLogin]    Script Date: 2023/3/21 下午 01:58:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MemberLogin]
		(@account nvarchar(50) , @pwd nvarchar(50))
AS
BEGIN
	SELECT * FROM Members WHERE Account = @account AND Password = @pwd
END
GO
USE [master]
GO
ALTER DATABASE [MyDB] SET  READ_WRITE 
GO
