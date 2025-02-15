USE [master]
GO
/****** Object:  Database [MyDB]    Script Date: 2023/3/16 下午 01:53:44 ******/
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
/****** Object:  Table [dbo].[Branch]    Script Date: 2023/3/16 下午 01:53:44 ******/
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
/****** Object:  Table [dbo].[Employees]    Script Date: 2023/3/16 下午 01:53:44 ******/
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
/****** Object:  Table [dbo].[HolidayDetails]    Script Date: 2023/3/16 下午 01:53:44 ******/
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
/****** Object:  Table [dbo].[Holidays]    Script Date: 2023/3/16 下午 01:53:44 ******/
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
/****** Object:  Table [dbo].[Plans]    Script Date: 2023/3/16 下午 01:53:44 ******/
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
/****** Object:  Table [dbo].[Schedules]    Script Date: 2023/3/16 下午 01:53:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedules](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EId] [nvarchar](50) NOT NULL,
	[WorkDay] [date] NOT NULL,
 CONSTRAINT [PK_Schedules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
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
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'admin', N'劉小德', N'admin.png', N'系統部', N'工程師', 0, N'男', N'ken@gmail.com', N'0926666777', N'Ken', N'admin', N'常日班', CAST(N'2019-07-07' AS Date), 1, 1)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E001', N'王曉薇', N'woman1.jpg', N'製造部', N'作業員', 1, N'女', N'aa.xx@mail.com', N'0912-345-678', N'Alice', N'F123456789', N'A班', CAST(N'2010-02-26' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E002', N'張裕翔', N'man.jpg', N'製造部', N'作業員', 1, N'男', N'cc.aa@mail.com', N'0925-112-637', N'John', N'F123456789', N'A班', CAST(N'2018-09-13' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E003', N'陳菲', N'woman2.jpg', N'製程部', N'製程主管', 2, N'女', N'zz.ss@mail.com', N'0913-975-666', N'Christine', N'F123456789', N'常日班', CAST(N'2002-05-05' AS Date), 1, 3)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E004', N'瑞淋', N'woman3.jpg', N'製造部', N'製造主管', 2, N'女', N'bb.oo@mail.com', N'0933-276-336', N'LinLin', N'F123456789', N'B班', CAST(N'2015-06-08' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E005', N'都瑞咪', N'woman4.jpg', N'客服部', N'客服人員', 1, N'女', N'cd.liu0607@gmail.com', N'0926050652', N'Doremi', N'F123456789', N'常日班', CAST(N'2023-02-15' AS Date), 1, 4)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E006', N'王大同', N'man1.jpg', N'製造部', N'作業員', 1, N'男', N'cd.liu@gmail.com', N'0933556788', N'Jacky', N'F123456789', N'B班', CAST(N'2000-06-30' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E007', N'吳香美', N'woman5.jpg', N'財務部', N'財務主管', 2, N'女', N'aa@gmail.com', N'0900876543', N'May', N'F123456789', N'常日班', CAST(N'2007-02-07' AS Date), 1, 5)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E008', N'劉瑞淋', N'woman6.jpg', N'會計部', N'會計主管', 2, N'女', N'cd.liu0607@gmail.com', N'0926050652', N'LinLin', N'F123456789', N'常日班', CAST(N'2023-07-07' AS Date), 1, 6)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E009', N'王凱欣', N'woman7.jpg', N'財務部', N'財務助理', 1, N'女', N'xx.gg@gmail.com', N'0900111222', N'caisin', N'F123456789', N'常日班', CAST(N'2023-03-01' AS Date), 1, 5)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E010', N'曾太好', N'man2.jpg', N'會計部', N'會計助理', 1, N'男', N'xx.gg@gmail.com', N'0900666888', N'Tai', N'F123456789', N'常日班', CAST(N'2000-06-06' AS Date), 1, 6)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E011', N'周蔚', N'woman8.jpg', N'客服部', N'客服人員', 1, N'女', N'ayw@gmail.com', N'0926050652', N'ayw', N'F123456789', N'常日班', CAST(N'2005-03-17' AS Date), 1, 4)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E012', N'黃萎萍', N'woman9.jpg', N'客服部', N'客服主管', 2, N'女', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'常日班', CAST(N'2009-11-20' AS Date), 1, 4)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E013', N'涂雅慧', N'woman1.jpg', N'製造部', N'作業員', 1, N'女', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'B班', CAST(N'2009-11-20' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E014', N'黃萎萍', N'woman2.jpg', N'製造部', N'作業員', 1, N'女', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'A班', CAST(N'2009-11-20' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E015', N'曲雅玲', N'woman3.jpg', N'製造部', N'作業員', 1, N'女', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'B班', CAST(N'2009-11-20' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E016', N'陶于婷', N'woman4.jpg', N'製造部', N'作業員', 1, N'女', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'A班', CAST(N'2009-11-20' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E017', N'鄭佳蓉', N'woman5.jpg', N'製造部', N'作業員', 1, N'女', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'B班', CAST(N'2009-11-20' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E018', N'黃淑芳', N'woman6.jpg', N'製造部', N'作業員', 1, N'女', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'A班', CAST(N'2009-11-20' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E019', N'王吟真', N'woman7.jpg', N'製造部', N'作業員', 1, N'女', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'B班', CAST(N'2009-11-20' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E020', N'謝宜紫', N'woman8.jpg', N'製造部', N'作業員', 1, N'女', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'A班', CAST(N'2009-11-20' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E021', N'吳宏偉', N'man3.jpg', N'製造部', N'作業員', 1, N'男', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'B班', CAST(N'2009-11-20' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E022', N'劉湖昆', N'man4.jpg', N'製造部', N'作業員', 1, N'男', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'A班', CAST(N'2009-11-20' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E023', N'陳俊嘉', N'man5.jpg', N'製造部', N'作業員', 1, N'男', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'B班', CAST(N'2009-11-20' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E024', N'張奕廷', N'man6.jpg', N'製造部', N'作業員', 1, N'男', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'A班', CAST(N'2009-11-20' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E025', N'吳秉雅', N'man7.jpg', N'製造部', N'作業員', 1, N'男', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'B班', CAST(N'2009-11-20' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E026', N'劉佑夫', N'man8.jpg', N'製造部', N'作業員', 1, N'男', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'A班', CAST(N'2009-11-20' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E027', N'羅士哲', N'man9.jpg', N'製造部', N'作業員', 1, N'男', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'B班', CAST(N'2009-11-20' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E028', N'林宗玫', N'man1.jpg', N'製造部', N'作業員', 1, N'男', N'pin@gmail.com', N'0926050652', N'pin', N'F123456789', N'A班', CAST(N'2009-11-20' AS Date), 1, 2)
INSERT [dbo].[Employees] ([EId], [Name], [Image], [Branch], [JobTitle], [JobRank], [Sex], [Email], [Phone], [EnglishName], [Password], [Shift], [StartDate], [Allow], [BranchId]) VALUES (N'E029', N'柏毅', N'woman9.jpg', N'客服部', N'客服人員', 1, N'女', N'ayw@gmail.com', N'0926050652', N'ayw', N'F123456789', N'常日班', CAST(N'2005-03-17' AS Date), 1, 4)
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
INSERT [dbo].[Plans] ([Id], [PlanTitle], [StartDate], [EndDate]) VALUES (N'1d3c8e6f-1d68-47e7-a6f4-02f5bab36f14', N'10', CAST(N'2023-03-18' AS Date), CAST(N'2023-03-20' AS Date))
INSERT [dbo].[Plans] ([Id], [PlanTitle], [StartDate], [EndDate]) VALUES (N'e61cff1c-da31-4cb5-9709-13bf80980010', N'20', CAST(N'2023-03-13' AS Date), CAST(N'2023-03-18' AS Date))
INSERT [dbo].[Plans] ([Id], [PlanTitle], [StartDate], [EndDate]) VALUES (N'80bc5028-6118-440a-acaf-1543365ab34d', N'10', CAST(N'2023-03-04' AS Date), CAST(N'2023-03-05' AS Date))
INSERT [dbo].[Plans] ([Id], [PlanTitle], [StartDate], [EndDate]) VALUES (N'50321728-8a97-40cd-8967-259dfa7d91b4', N'10', CAST(N'2023-03-05' AS Date), CAST(N'2023-03-06' AS Date))
INSERT [dbo].[Plans] ([Id], [PlanTitle], [StartDate], [EndDate]) VALUES (N'a484d637-a074-4456-823b-50bb37704c63', N'10', CAST(N'2023-04-01' AS Date), CAST(N'2023-04-06' AS Date))
INSERT [dbo].[Plans] ([Id], [PlanTitle], [StartDate], [EndDate]) VALUES (N'943b836a-3c7e-49db-b6ed-5b48dd4a1b22', N'20', CAST(N'2023-03-20' AS Date), CAST(N'2023-03-26' AS Date))
INSERT [dbo].[Plans] ([Id], [PlanTitle], [StartDate], [EndDate]) VALUES (N'0916174a-e6d3-4c68-91dd-7cee3efafdf9', N'20', CAST(N'2023-03-06' AS Date), CAST(N'2023-03-11' AS Date))
INSERT [dbo].[Plans] ([Id], [PlanTitle], [StartDate], [EndDate]) VALUES (N'3e241dec-7da5-40f6-8d32-81c790bcdd69', N'10', CAST(N'2023-03-26' AS Date), CAST(N'2023-03-27' AS Date))
INSERT [dbo].[Plans] ([Id], [PlanTitle], [StartDate], [EndDate]) VALUES (N'91206c58-cddf-4da1-8b61-abd058045e68', N'10', CAST(N'2023-03-11' AS Date), CAST(N'2023-03-13' AS Date))
INSERT [dbo].[Plans] ([Id], [PlanTitle], [StartDate], [EndDate]) VALUES (N'1a0a03d0-8ac0-4a59-9772-bd322988285e', N'20', CAST(N'2023-03-01' AS Date), CAST(N'2023-03-04' AS Date))
INSERT [dbo].[Plans] ([Id], [PlanTitle], [StartDate], [EndDate]) VALUES (N'6ee15b8a-0c3b-4c22-b8c2-e7b369b99a17', N'20', CAST(N'2023-03-27' AS Date), CAST(N'2023-04-01' AS Date))
INSERT [dbo].[Plans] ([Id], [PlanTitle], [StartDate], [EndDate]) VALUES (N'83d4a831-f64c-4747-ad79-ed53f5ad256b', N'20', CAST(N'2023-04-06' AS Date), CAST(N'2023-04-08' AS Date))
INSERT [dbo].[Plans] ([Id], [PlanTitle], [StartDate], [EndDate]) VALUES (N'f7932c0c-f200-40fa-84ee-ff173f8cbd40', N'10', CAST(N'2023-04-08' AS Date), CAST(N'2023-04-09' AS Date))
GO
SET IDENTITY_INSERT [dbo].[Schedules] ON 

INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (1, N'E001', CAST(N'2023-03-02' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (2, N'E001', CAST(N'2023-03-03' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (3, N'E001', CAST(N'2023-03-06' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (4, N'E001', CAST(N'2023-03-07' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (5, N'E001', CAST(N'2023-03-10' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (6, N'E001', CAST(N'2023-03-11' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (7, N'E001', CAST(N'2023-03-14' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (8, N'E001', CAST(N'2023-03-15' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (9, N'E001', CAST(N'2023-03-18' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (10, N'E001', CAST(N'2023-03-19' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (11, N'E001', CAST(N'2023-03-22' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (12, N'E001', CAST(N'2023-03-23' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (13, N'E001', CAST(N'2023-03-26' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (14, N'E001', CAST(N'2023-03-27' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (15, N'E001', CAST(N'2023-03-30' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (16, N'E001', CAST(N'2023-03-31' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (17, N'E002', CAST(N'2023-03-02' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (18, N'E002', CAST(N'2023-03-03' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (19, N'E002', CAST(N'2023-03-06' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (20, N'E002', CAST(N'2023-03-07' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (21, N'E002', CAST(N'2023-03-10' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (22, N'E002', CAST(N'2023-03-11' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (23, N'E002', CAST(N'2023-03-14' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (24, N'E002', CAST(N'2023-03-15' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (25, N'E002', CAST(N'2023-03-18' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (26, N'E002', CAST(N'2023-03-19' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (27, N'E002', CAST(N'2023-03-22' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (28, N'E002', CAST(N'2023-03-23' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (29, N'E002', CAST(N'2023-03-26' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (30, N'E002', CAST(N'2023-03-27' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (31, N'E002', CAST(N'2023-03-30' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (32, N'E002', CAST(N'2023-03-31' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (33, N'E004', CAST(N'2023-03-01' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (34, N'E004', CAST(N'2023-03-04' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (35, N'E004', CAST(N'2023-03-05' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (36, N'E004', CAST(N'2023-03-08' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (37, N'E004', CAST(N'2023-03-09' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (38, N'E004', CAST(N'2023-03-12' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (39, N'E004', CAST(N'2023-03-13' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (40, N'E004', CAST(N'2023-03-16' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (41, N'E004', CAST(N'2023-03-17' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (42, N'E004', CAST(N'2023-03-20' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (43, N'E004', CAST(N'2023-03-21' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (44, N'E004', CAST(N'2023-03-24' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (45, N'E004', CAST(N'2023-03-25' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (46, N'E004', CAST(N'2023-03-28' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (47, N'E004', CAST(N'2023-03-29' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (48, N'E006', CAST(N'2023-03-01' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (49, N'E006', CAST(N'2023-03-04' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (50, N'E006', CAST(N'2023-03-05' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (51, N'E006', CAST(N'2023-03-08' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (52, N'E006', CAST(N'2023-03-09' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (53, N'E006', CAST(N'2023-03-12' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (54, N'E006', CAST(N'2023-03-13' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (55, N'E006', CAST(N'2023-03-16' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (56, N'E006', CAST(N'2023-03-17' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (57, N'E006', CAST(N'2023-03-20' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (58, N'E006', CAST(N'2023-03-21' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (59, N'E006', CAST(N'2023-03-24' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (60, N'E006', CAST(N'2023-03-25' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (61, N'E006', CAST(N'2023-03-28' AS Date))
INSERT [dbo].[Schedules] ([Id], [EId], [WorkDay]) VALUES (62, N'E006', CAST(N'2023-03-29' AS Date))
SET IDENTITY_INSERT [dbo].[Schedules] OFF
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Branch1] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Branch1]
GO
ALTER TABLE [dbo].[HolidayDetails]  WITH CHECK ADD  CONSTRAINT [FK_HolidayDetails_Employees] FOREIGN KEY([EId])
REFERENCES [dbo].[Employees] ([EId])
GO
ALTER TABLE [dbo].[HolidayDetails] CHECK CONSTRAINT [FK_HolidayDetails_Employees]
GO
ALTER TABLE [dbo].[HolidayDetails]  WITH CHECK ADD  CONSTRAINT [FK_HolidayDetails_Holidays1] FOREIGN KEY([HId])
REFERENCES [dbo].[Holidays] ([HId])
GO
ALTER TABLE [dbo].[HolidayDetails] CHECK CONSTRAINT [FK_HolidayDetails_Holidays1]
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD  CONSTRAINT [FK_Schedules_Employees] FOREIGN KEY([EId])
REFERENCES [dbo].[Employees] ([EId])
GO
ALTER TABLE [dbo].[Schedules] CHECK CONSTRAINT [FK_Schedules_Employees]
GO
/****** Object:  StoredProcedure [dbo].[GetMemberById]    Script Date: 2023/3/16 下午 01:53:44 ******/
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
/****** Object:  StoredProcedure [dbo].[MemberLogin]    Script Date: 2023/3/16 下午 01:53:44 ******/
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
