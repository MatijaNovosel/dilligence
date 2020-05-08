DROP DATABASE IF EXISTS [tvz2]
GO

CREATE DATABASE [tvz2]
GO

USE [tvz2]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[File]
(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[ContentType] [varchar](255) NULL,
	[Data] [varbinary](MAX) NULL,
	PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course]
(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[ISVU] [varchar](50) NULL,
	[ECTS] [int] NULL,
	[Password] [varchar](255) NULL,
	[MadeByID] [int] NULL,
	[SpecializationID] [int] NULL,
	PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User]
(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](255) NULL,
	[PasswordHash] [varbinary](255) NULL,
	[PasswordSalt] [varbinary](255) NULL,
	[Created] [datetime2](7) NULL DEFAULT CURRENT_TIMESTAMP,
	[Name] [varchar](255) NULL,
	[Surname] [varchar](255) NULL,
	[Email] [varchar](255) NULL,
	[ImageFileID] [int] NULL,
	PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subscription]
(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[JoinedAt] [datetime2](7) NULL DEFAULT CURRENT_TIMESTAMP,
	[UserID] [int] NULL,
	[CourseID] [int] NULL,
	PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SidebarContent]
(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](255) NULL,
	[CourseID] [int] NULL,
	PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SidebarContentFile]
(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SidebarContentID] [int] NULL,
	[FileID] [int] NULL,
	PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Specialization]
(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Shorthand] [varchar](20) NULL
		PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification]
(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](255) NULL,
	[SubmittedAt] [date] NULL,
	[ExpiresAt] [date] NULL,
	[Description] [varchar](max) NULL,
	[Color] [varchar](255) NULL,
	[CourseID] [int] NULL,
	[SubmittedByID] [int] NULL,
	PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationUserSeen]
(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NotificationID] [int] NULL,
	[UserID] [int] NULL,
	[Seen] [bit] DEFAULT 0,
	PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Exam]
(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[DueDate] [datetime2](7) NULL,
	[TimeNeeded] [int] NOT NULL,
	[Finalized] [bit] DEFAULT 0,
	[CourseID] [int] NULL,
	[CreatedByID] [int] NULL
		PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExamAttempt]
(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Terminated] [bit] NULL,
	[Started] [bit] NULL,
	[StartedAt] [datetime2](7) NULL,
	[UserID] [int] NULL,
	[ExamID] [int] NULL,
	PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAnswer]
(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AttemptID] [int] NULL,
	[QuestionID] [int] NULL,
	[AnswerID] [int] NULL,
	PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Question]
(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](255) NULL,
	[Content] [varchar](max) NULL,
	[ExamID] [int] NULL,
	[TypeID] [int] NULL
		PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionType]
(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Answer]
(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Content] [varchar](max) NULL,
	[Correct] [bit] NULL,
	[QuestionID] [int] NULL,
	PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chat]
(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstParticipantID] [int] NOT NULL,
	[SecondParticipantID] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Message]
(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Content] [varchar](max) NOT NULL,
	[SentAt] [datetime2](7) CONSTRAINT dfDate DEFAULT GETDATE(),
	[UserID] [int] NOT NULL,
	[ChatID] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Privileges]
(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPrivileges]
(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[PrivilegeID] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSettings]
(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[DarkMode] [bit] NOT NULL DEFAULT 0,
	[Locale] [varchar](max) NOT NULL DEFAULT 'en',
	PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserNotificationBlacklist]
(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[CourseID] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[Course] ON
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(1, N'Analogni sklopovi E', N'24078/22300', 6, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(2, N'Automatizacija postrojenja', N'24586/155818', 6, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(3, N'Automatsko upravljanje E', N'24069/22268', 6, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(4, N'Automatsko upravljanje E', N'24297/93349', 6, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(5, N'Digitalna obradba signala', N'24085/22316', 5, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(6, N'Digitalni signal procesori', N'24288/85715', 5, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(7, N'Digitalni sklopovi E', N'24217/63208', 5, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(8, N'Digitalno upravljanje', N'24783/158578', 5, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(9, N'Elektricitet i magnetizam', N'25058/184788', 8, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(10, N'Električni strojevi I', N'24180/26091', 5, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(11, N'Električni strojevi II', N'24587/155820', 6, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(12, N'Elektroenergetika', N'25139/189957', 6, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(13, N'Elektroenergetska postrojenja', N'25135/189953', 7, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(14, N'Elektroenergetske mreže E', N'24076/22294', 5, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(15, N'Elektromotorni pogoni', N'24631/155988', 6, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(16, N'Elektromotorni pogoni EE', N'25137/189955', 6, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(17, N'Elektronička računala i računalna oprema', N'24632/155989', 4, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(18, N'Elektroničke komponente', N'24064/22244', 6, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(19, N'Elektronički sklopovi', N'25244/22287', 5, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(20, N'Elementi automatizacije', N'24066/22264', 5, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(21, N'Energetska elektronika E', N'24071/22280', 5, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(22, N'Engleski u elektrotehnici 1', N'24565/155632', 2, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(23, N'Engleski u elektrotehnici 2', N'24566/155633', 2, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(24, N'Engleski u elektrotehnici 3', N'24567/155634', 2, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(25, N'Fizika', N'25060/184793', 6, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(26, N'Informacije i kodiranje', N'24332/128258', 5, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(27, N'Instalacije i rasvjeta E', N'24077/22296', 4, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(28, N'Izvođenje elektrotehničkih postrojenja E', N'24073/22284', 6, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(29, N'Kineziološka kultura I', N'24462/143308', 1, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(30, N'Kineziološka kultura II', N'24463/143309', 1, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(31, N'Kineziološka kultura III', N'24464/143310', 1, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(32, N'Kineziološka kultura IV', N'24465/143311', 1, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(33, N'LabView grafičko programiranje', N'24633/155990', 4, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(34, N'Linearne i nelinearne mreže', N'24079/22301', 4, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(35, N'Matematički alati u elektrotehnici', N'24634/155991', 2, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(36, N'Matematika I', N'24635/155992', 7, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(37, N'Matematika II', N'25056/184786', 8, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(38, N'Materijali u elektrotehnici', N'25207/85611', 4, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(39, N'Mjerenja u elektrotehnici', N'25097/185689', 6, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(40, N'Mobilne mreže viših generacija', N'24797/169758', 5, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(41, N'Mobilne radiokomunikacije', N'24796/169757', 5, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(42, N'Numerička matematika E', N'24072/22282', 5, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(43, N'Objektno orijentirano programiranje', N'24302/104555', 5, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(44, N'Obnovljivi izvori energije', N'24301/104554', 6, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(45, N'Održavanje elektrotehničke opreme', N'24287/85714', 5, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(46, N'Optičke komunikacije', N'24637/155996', 5, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(47, N'Osnove elektroakustike i audiotehnike', N'25241/200537', 5, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(48, N'Osnove elektrotehnike', N'25062/184795', 9, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(49, N'Primjena osobnih računala u elektrotehnici', N'25064/184797', 4, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(50, N'Procesna mjerenja', N'24070/22270', 5, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(51, N'Procesna računala', N'24065/22256', 5, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(52, N'Programiranje', N'24080/22303', 5, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(53, N'Programirljivi logički kontroleri', N'24638/155998', 5, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(54, N'Projektiranje i primjena ugradbenih računalnih sustava', N'24242/75867', 5, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(55, N'Radarski sklopovi E', N'24083/22314', 5, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(56, N'Radiokomunikacijski uređaji i sustavi E', N'24082/22311', 4, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(57, N'Signali i procesi', N'24285/85647', 6, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(58, N'Sklopni aparati', N'24075/22288', 4, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(59, N'Socijalna filozofija', N'24067/22265', 2, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(60, N'Stručna praksa', N'24266/83431', 6, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(61, N'Sustavi automatizacije', N'24639/156000', 6, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(62, N'Tehnička mehanika', N'24068/22266', 4, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(63, N'Tehničko dokumentiranje', N'25209/184784', 4, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(64, N'Tehnološko poduzetništvo', N'24330/128246', 2, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(65, N'Telekomunikacijske mreže E', N'24081/22309', 5, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(66, N'Transformatori', N'25133/189951', 5, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(67, N'Transformatori i el. rotacijski strojevi', N'24640/156005', 5, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(68, N'Uređaji i sustavi upravljanja E', N'24084/22315', 5, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(69, N'Uvod u mrežne tehnologije', N'24641/156006', 5, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(70, N'Virtualna instrumentacija', N'24787/160839', 5, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(71, N'Visokofrekvencijska i mikrovalna elektronika', N'24286/85705', 5, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(72, N'Vjerojatnost i statistika', N'24284/85624', 3, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(73, N'Vodovi i antene', N'24642/156008', 5, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(74, N'Zaštita i mjerenja u el. postrojenjima', N'24184/32767', 5, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(75, N'Završni rad', N'24643/156009', 8, NULL, 3)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(76, N'Betonske konstrukcije I', N'24600/155926', 5, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(77, N'Betonske konstrukcije II', N'24601/155927', 5, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(78, N'Ceste I', N'25029/184720', 5, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(79, N'Ceste II', N'24602/155929', 5, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(80, N'Drvene konstrukcije', N'25022/184649', 5, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(81, N'Elementi zgrada I', N'24603/155931', 7, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(82, N'Elementi zgrada II', N'24604/155932', 5, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(83, N'Engleski jezik u graditeljstvu I', N'24531/147431', 4, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(84, N'Engleski jezik u graditeljstvu II', N'24532/147432', 2, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(85, N'Fizika', N'24089/22336', 5, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(86, N'Geodezija', N'24605/155933', 2, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(87, N'Geotehnika', N'24606/155934', 5, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(88, N'Gospodarenje otpadom', N'24607/155935', 4, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(89, N'Građevinski materijali', N'25016/184642', 5, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(90, N'Građevinski strojevi', N'24090/22358', 4, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(91, N'Hidrologija i hidraulika', N'24608/155936', 5, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(92, N'Instalacije zgrada I', N'25043/184755', 4, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(93, N'Instalacije zgrada II', N'25047/184759', 4, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(94, N'Kakvoća voda', N'24535/147436', 4, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(95, N'Kineziološka kultura I', N'24466/143316', 1, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(96, N'Kineziološka kultura II', N'24467/143317', 1, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(97, N'Kineziološka kultura III', N'24468/143318', 1, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(98, N'Kineziološka kultura IV', N'24469/143319', 1, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(99, N'Korištenje voda', N'25032/184725', 5, NULL, 5)
GO
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(100, N'Matematika I', N'24086/22319', 7, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(101, N'Matematika II', N'24529/147426', 6, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(102, N'Mehanika tla', N'25027/184718', 5, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(103, N'Metalne konstrukcije', N'25024/184651', 5, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(104, N'Metode planiranja', N'24609/155943', 6, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(105, N'Metodologija i menadžment u graditeljstvu', N'25051/184763', 2, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(106, N'Montažne građevine', N'25026/184653', 5, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(107, N'Nacrtna geometrija u graditeljstvu I', N'24610/155949', 3, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(108, N'Nacrtna geometrija u graditeljstvu II', N'25038/184741', 3, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(109, N'Njemački jezik u graditeljstvu I', N'24533/147433', 4, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(110, N'Njemački jezik u graditeljstvu II', N'24530/147429', 2, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(111, N'Opskrba vodom i odvodnja I', N'24611/155955', 4, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(112, N'Opskrba vodom i odvodnja II', N'24612/155957', 5, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(113, N'Organizacija gradilišta', N'24613/155960', 6, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(114, N'Organizacija građenja I', N'24614/155962', 5, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(115, N'Organizacija građenja II', N'24615/155963', 5, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(116, N'Osnove geologije', N'24088/22335', 2, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(117, N'Osnove hidrologije i hidraulike', N'24616/155964', 5, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(118, N'Osnove željeznica', N'24087/22330', 2, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(119, N'Otpornost materijala', N'24617/155965', 3, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(120, N'Poslovanje tvrtke', N'24618/155966', 5, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(121, N'Povijesni razvoj graditeljstva', N'24536/147442', 2, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(122, N'Promet i okoliš', N'25036/184732', 5, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(123, N'Proračun konstrukcija', N'25020/184646', 6, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(124, N'Računarstvo u graditeljstvu', N'24528/147425', 2, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(125, N'Regulacije i melioracije', N'24619/155969', 5, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(126, N'Regulativa i vođenje projekata', N'25053/184765', 5, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(127, N'Riječno inženjerstvo', N'24620/155971', 6, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(128, N'Sociologija rada', N'25040/184743', 2, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(129, N'Tehnička mehanika', N'25018/184644', 6, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(130, N'Tehnologija građenja', N'24621/155972', 5, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(131, N'Terenska nastava', N'24091/22359', 2, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(132, N'Tržište i poslovno okruženje', N'25041/184753', 5, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(133, N'Uvod u urbanizam i prostorno planiranje', N'24534/147434', 2, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(134, N'Vodnogospodarske građevine', N'24622/155974', 4, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(135, N'Vodogradnje', N'24195/39869', 2, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(136, N'Zaštita na radu', N'24623/155975', 5, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(137, N'Zaštita okoliša', N'24624/155976', 2, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(138, N'Zaštita voda', N'25034/184729', 5, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(139, N'Završni rad sa stručnom praksom-GP', N'24625/155979', 12, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(140, N'Završni rad sa stručnom praksom-NIS', N'24626/155980', 12, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(141, N'Završni rad sa stručnom praksom-OUG', N'24627/155981', 12, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(142, N'Završni rad sa stručnom praksom-VIS', N'24628/155982', 12, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(143, N'Završni radovi', N'24629/155983', 5, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(144, N'Zgradarstvo I', N'25045/184757', 4, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(145, N'Zgradarstvo II', N'25049/184761', 5, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(146, N'Željeznice', N'24630/155986', 6, NULL, 5)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(147, N'3D modeliranje', N'25066/185288', 5, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(148, N'Administriranje i poslovanje mrežama računala', N'24554/155616', 4, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(149, N'Administriranje UNIX sustava', N'24555/155617', 5, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(150, N'Analiza tabličnih podataka', N'25183/200096', 3, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(151, N'Baze podataka', N'24279/85254', 5, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(152, N'Digitalna animacija', N'24664/156268', 5, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(153, N'Digitalna antropologija', N'25008/180914', 5, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(154, N'Digitalna fotografija', N'24541/152729', 4, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(155, N'Digitalna televizija', N'24540/148961', 5, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(156, N'Dizajn i primjena vektorske grafike', N'24420/142127', 5, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(157, N'Dizajn i vizualno značenje', N'24419/142125', 6, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(158, N'Dizajn proizvoda', N'24782/158290', 3, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(159, N'Dizajn vizualnih komunikacija', N'24214/63173', 6, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(160, N'Društvene mreže', N'24417/142118', 5, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(161, N'Elektroničko poslovanje u ekonomiji', N'25201/200114', 6, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(162, N'Elektroničko poslovanje u informatici', N'25199/200112', 6, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(163, N'Engleski jezik za IT', N'24274/85213', 3, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(164, N'Fizika', N'25095/185591', 6, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(165, N'Građa računala', N'24415/142116', 5, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(166, N'Grafičke tehnike', N'24177/22755', 4, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(167, N'Grafički dizajn', N'24213/63168', 6, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(168, N'Grafički programski jezici', N'24176/22753', 5, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(169, N'Informacijska pismenost i kritičko razmišljanje', N'24667/156271', 6, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(170, N'Informatičke tehnologije', N'25179/200092', 3, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(171, N'Inovacije u informatici', N'24306/111756', 5, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(172, N'Integracija medija', N'24174/22750', 6, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(173, N'Interaktivno programiranje na Web-u', N'24103/22428', 4, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(174, N'Interaktivno programiranje na Web-u', N'24283/85392', 4, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(175, N'Kineziološka kultura I', N'24458/143284', 1, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(176, N'Kineziološka kultura II', N'24459/143286', 1, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(177, N'Kineziološka kultura III', N'24460/143288', 1, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(178, N'Kineziološka kultura IV', N'24461/143289', 1, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(179, N'Matematički alati u informatici', N'25181/200094', 4, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(180, N'Matematika I', N'24588/155821', 6, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(181, N'Matematika II', N'24589/155822', 6, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(182, N'Mobilne komunikacije', N'24099/22418', 3, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(183, N'Multimedijski marketing', N'24175/22752', 5, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(184, N'Napredne baze podataka', N'24281/85268', 5, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(185, N'Napredne tehnologije interneta', N'24556/155619', 3, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(186, N'Napredno elektroničko poslovanje u ekonomiji', N'25205/200118', 4, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(187, N'Napredno elektroničko poslovanje u informatici', N'25203/200116', 4, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(188, N'Objektno orijentirano programiranje I', N'24558/155621', 5, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(189, N'Objektno orijentirano programiranje II', N'24211/63137', 5, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(190, N'Oblikovanje e literature', N'24296/91954', 5, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(191, N'Oblikovanje Web stranica', N'24104/22429', 6, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(192, N'Obrada slike, zvuka i videa', N'24282/85390', 4, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(193, N'Obrada teksta', N'24097/22415', 3, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(194, N'Operacijski sustavi', N'24559/155622', 5, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(195, N'Organizacija i informatizacija ureda', N'24096/22414', 3, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(196, N'Osnove kibernetičke sigurnosti', N'25185/200098', 5, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(197, N'Osnove programiranja', N'25100/185969', 6, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(198, N'Poslovni engleski jezik za IT', N'24275/85215', 3, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(199, N'Praktikum iz dizajna', N'24179/22766', 5, NULL, 1)
GO
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(200, N'Pretražnici i navigacija na Web-u', N'24102/22427', 3, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(201, N'Primjena HTML i CSS tehnologija u razvoju mrežnih stranica', N'25195/200108', 4, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(202, N'Procesi video produkcije', N'24410/133389', 4, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(203, N'Produkcija digitalnih medija', N'25197/200110', 4, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(204, N'Produkcija zvuka', N'24665/156269', 4, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(205, N'Programiranje', N'25102/185971', 7, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(206, N'Programiranje web aplikacija', N'24244/75875', 5, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(207, N'Progresivne web aplikacije', N'25191/200104', 5, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(208, N'Projektno programiranje', N'24560/155627', 3, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(209, N'Računalna grafika', N'24178/22759', 5, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(210, N'Računalna tipografija', N'24093/22405', 6, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(211, N'Razvoj računalnih igara', N'24538/148931', 5, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(212, N'Sigurnost i zaštita informacijskih sustava', N'25271/22423', 5, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(213, N'Socio tehnološki aspekti Informacijskih sustava', N'24680/156295', 4, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(214, N'Stručna praksa', N'24094/22408', 3, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(215, N'Sustavi elektroničkog poslovanja', N'24194/39179', 5, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(216, N'Tehnološko poduzetništvo', N'24561/155628', 6, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(217, N'Tehnološko poduzetništvo', N'25014/181288', 6, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(218, N'Teorija i razvoj dizajna', N'24411/133727', 6, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(219, N'Tržišne komunikacije', N'24562/155629', 5, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(220, N'TV i Video snimanje', N'24666/156270', 5, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(221, N'Upravljanje bojama', N'25189/200102', 5, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(222, N'Uredsko poslovanje', N'24414/142115', 6, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(223, N'Uvod u mreže računala', N'24563/155630', 4, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(224, N'Uvod u UNIX sustave', N'24564/155631', 5, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(225, N'Vjerojatnost i statistika', N'24592/155825', 4, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(226, N'Vještine komuniciranja', N'24243/75874', 4, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(227, N'XML programiranje', N'24418/142120', 6, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(228, N'Završni rad', N'24095/22409', 12, NULL, 1)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(229, N'Elektromotorni pogoni', N'24412/134343', 5, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(230, N'Elektronički elementi i sklopovi', N'24256/83379', 5, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(231, N'Elementi automatizacije', N'24258/83382', 5, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(232, N'Energetska elektrotehnika', N'24255/83378', 6, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(233, N'Engleski jezik u mehatronici', N'24322/128191', 3, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(234, N'Fizika', N'24571/155770', 6, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(235, N'Gospodarenje energijom', N'24773/156947', 4, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(236, N'Kineziološka kultura I', N'24932/172322', 1, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(237, N'Kineziološka kultura II', N'24916/171218', 1, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(238, N'Kineziološka kultura III', N'24917/171219', 1, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(239, N'Kineziološka kultura IV', N'24918/171220', 1, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(240, N'Komunikacijske tehnike u mehatronici', N'25091/185487', 4, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(241, N'Kostruiranje primjenom računala', N'24321/128190', 5, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(242, N'Manipulatori i roboti', N'24327/128197', 4, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(243, N'Matematika', N'24250/83165', 7, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(244, N'Materijali i proizvodni postupci', N'24251/83167', 5, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(245, N'Matlab', N'24320/128189', 2, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(246, N'Mehanika i čvrstoća', N'24572/155771', 6, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(247, N'Mehatronički strojni elementi', N'24573/155772', 5, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(248, N'Metodologija stručnog i istraživačkog rada', N'24928/172304', 2, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(249, N'Mjeriteljstvo i upravljanje kvalitetom', N'24264/83402', 4, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(250, N'Modeliranje i simuliranje sustava', N'24574/155773', 5, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(251, N'Njemački jezik u mehatronici', N'24323/128192', 3, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(252, N'Numerički upravljivi alatni strojevi', N'24326/128196', 4, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(253, N'Održavanje tehničkih sustava u mehatronici', N'24263/83399', 4, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(254, N'Osnove elektrotehnike', N'24253/83372', 6, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(255, N'Osnove mehanizama', N'24257/83381', 5, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(256, N'Osnove programiranja', N'24926/172302', 5, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(257, N'Pneumatika i hidraulika', N'24260/83387', 6, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(258, N'Poslovni engleski jezik u mehatronici', N'24324/128194', 3, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(259, N'Poslovni njemački jezik u mehatronici', N'24325/128195', 3, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(260, N'Primijenjena matematika', N'24252/83370', 5, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(261, N'Procesna računala', N'24259/83386', 6, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(262, N'Projektiranje tiskanih pločica', N'25068/185365', 4, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(263, N'Projektiranje ugrađenih računalnih sustava', N'24262/83397', 5, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(264, N'Semestralni rad', N'24575/155774', 5, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(265, N'Senzori', N'24254/83374', 5, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(266, N'Stručna praksa', N'24576/155775', 7, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(267, N'Tehničko dokumentiranje', N'24319/128188', 4, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(268, N'Tehnologije i postrojenja za obradu i recikliranje otpada', N'25155/196103', 6, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(269, N'Tehnološko poduzetništvo', N'24329/128200', 6, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(270, N'Transportna sredstva', N'24328/128198', 4, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(271, N'Upravljanje i regulacija', N'24261/83388', 5, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(272, N'Završni rad', N'24265/83407', 12, NULL, 4)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(273, N'Administracija računalnih mreža', N'25188/200101', 5, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(274, N'Administriranje UNIX sustava', N'24521/147088', 5, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(275, N'Algoritmi i strukture podataka', N'24546/154952', 7, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(276, N'Arhitektura računala', N'24547/154954', 7, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(277, N'Baze podataka', N'24278/85250', 5, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(278, N'Engleski jezik za računarstvo', N'24270/84849', 3, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(279, N'Fizika', N'24548/154955', 7, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(280, N'Kineziološka kultura I', N'24422/143073', 1, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(281, N'Kineziološka kultura II', N'24423/143075', 1, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(282, N'Kineziološka kultura III', N'24424/143076', 1, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(283, N'Kineziološka kultura IV', N'24425/143077', 1, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(284, N'Matematika I', N'24172/22742', 7, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(285, N'Matematika II', N'24170/22730', 7, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(286, N'Metodologija poslovnih procesa', N'25173/200086', 4, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(287, N'Metodologija stručnog i istraživačkog rada', N'25169/200082', 6, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(288, N'Mrežne usluge', N'24168/22720', 5, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(289, N'Napredne baze podataka', N'24280/85264', 5, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(290, N'Napredne teme računalnih mreža', N'25217/152537', 6, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(291, N'Napredno JavaScript programiranje', N'24644/156035', 5, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(292, N'Napredno programiranje u jeziku Python', N'24421/142130', 5, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(293, N'Nekonvencionalni računalni postupci', N'24169/22722', 6, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(294, N'Objektno orijentirano programiranje', N'24249/81892', 7, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(295, N'Oblikovanje e literature', N'25154/195334', 5, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(296, N'Oblikovanje web stranica', N'24269/83677', 6, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(297, N'Operacijski sustavi', N'24171/22735', 6, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(298, N'Osnove elektrotehnike i elektronike', N'24549/154957', 7, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(299, N'Otvorene platforme za razvoj ugrađenih sustava', N'24305/111724', 5, NULL, 2)
GO
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(300, N'Poslovni engleski jezik za računarstvo', N'24272/85204', 3, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(301, N'Primjena računala', N'24550/154959', 5, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(302, N'Programiranje', N'24551/154960', 7, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(303, N'Programiranje u jeziku Java', N'24552/154961', 5, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(304, N'Programiranje web aplikacija', N'24240/75220', 5, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(305, N'Računala za nadzor i upravljanje tehnickim procesima', N'24167/22698', 5, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(306, N'Računalne mreže', N'24553/154963', 6, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(307, N'Razvoj aplikacija na Android platformi', N'24304/111519', 6, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(308, N'Razvoj računalnih igara', N'24539/148934', 5, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(309, N'Razvoj web aplikacija u ASP.NET MVC tehnologiji', N'24303/111517', 6, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(310, N'Stručna praksa', N'25177/200090', 3, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(311, N'Uvod u umjetnu inteligenciju', N'24776/156980', 4, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(312, N'Uvod u umjetnu inteligenciju', N'25167/200080', 5, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(313, N'Uvod u UNIX sustave', N'24216/63200', 4, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(314, N'Uvod u web tehnologije', N'24293/91686', 5, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(315, N'Vjerojatnost i statistika', N'24593/155826', 6, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(316, N'Web aplikacije u Javi', N'24241/75223', 6, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(317, N'Završni rad', N'25175/200088', 12, NULL, 2)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(318, N'Čvrstoća', N'25088/185461', 4, NULL, 6)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(319, N'Dizajn proizvoda', N'25162/200060', 4, NULL, 6)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(320, N'Elementi strojeva', N'25084/185456', 5, NULL, 6)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(321, N'Engleski jezik u strojarstvu', N'25211/200051', 3, NULL, 6)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(322, N'Fizika', N'25077/185448', 6, NULL, 6)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(323, N'Kineziološka kultura I', N'25078/185449', 1, NULL, 6)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(324, N'Kineziološka kultura II', N'25085/185457', 1, NULL, 6)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(325, N'Kineziološka kultura III', N'25157/200046', 1, NULL, 6)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(326, N'Kineziološka kultura IV', N'25163/200061', 1, NULL, 6)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(327, N'Konstruiranje pomoću računala 1', N'25158/200047', 6, NULL, 6)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(328, N'Manipulatori i roboti', N'25215/200067', 6, NULL, 6)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(329, N'Matematika', N'25079/185450', 7, NULL, 6)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(330, N'Materijali', N'25080/185452', 5, NULL, 6)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(331, N'Matlab', N'25086/185459', 2, NULL, 6)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(332, N'Mehanika', N'25087/185460', 7, NULL, 6)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(333, N'Mehanika fluida', N'25159/200048', 7, NULL, 6)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(334, N'Mehanizmi', N'25160/200049', 6, NULL, 6)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(335, N'Metodologija stručnog i istraživačkog rada', N'25082/185454', 2, NULL, 6)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(336, N'Motori i vozila', N'25164/200062', 5, NULL, 6)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(337, N'Njemački jezik u strojarstvu', N'25212/200052', 3, NULL, 6)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(338, N'Numerički upravljivi alatni strojevi', N'25165/200063', 5, NULL, 6)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(339, N'Osnove elektrotehnike', N'25089/185462', 6, NULL, 6)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(340, N'Pneumatika i hidraulika', N'25166/200064', 6, NULL, 6)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(341, N'Poslovni engleski jezik u strojarstvu', N'25213/200065', 3, NULL, 6)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(342, N'Poslovni njemački jezik u strojarstvu', N'25214/200066', 3, NULL, 6)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(343, N'Primijenjena matematika', N'25090/185463', 5, NULL, 6)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(344, N'Proizvodni postupci', N'25081/185453', 5, NULL, 6)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(345, N'Tehničko dokumentiranje', N'25083/185455', 4, NULL, 6)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(346, N'Tehnologije i postrojenja za obradu i recikliranje otpada', N'25216/200068', 6, NULL, 6)
INSERT [dbo].[Course]
	([ID], [Name], [ISVU], [ECTS], [MadeByID], [SpecializationID])
VALUES
	(347, N'Termodinamika', N'25161/200050', 7, NULL, 6)
SET IDENTITY_INSERT [dbo].[Course] OFF

SET IDENTITY_INSERT [dbo].[Specialization] ON
INSERT [dbo].[Specialization]
	([ID], [Name], [Shorthand])
VALUES
	(1, N'Informatika', N'inf')
INSERT [dbo].[Specialization]
	([ID], [Name], [Shorthand])
VALUES
	(2, N'Računarstvo', N'rac')
INSERT [dbo].[Specialization]
	([ID], [Name], [Shorthand])
VALUES
	(3, N'Elektrotehnika', N'elo')
INSERT [dbo].[Specialization]
	([ID], [Name], [Shorthand])
VALUES
	(4, N'Mehatronika', N'meh')
INSERT [dbo].[Specialization]
	([ID], [Name], [Shorthand])
VALUES
	(5, N'Graditeljstvo', N'gra')
INSERT [dbo].[Specialization]
	([ID], [Name], [Shorthand])
VALUES
	(6, N'Strojarstvo', N'stro')
SET IDENTITY_INSERT [dbo].[Specialization] OFF

SET IDENTITY_INSERT [dbo].[QuestionType] ON
INSERT INTO [dbo].[QuestionType]
	([ID], [Name])
VALUES
	(1, 'Radio')
INSERT INTO [dbo].[QuestionType]
	([ID], [Name])
VALUES
	(2, 'Checkbox')
GO
SET IDENTITY_INSERT [dbo].[QuestionType] OFF

SET IDENTITY_INSERT [dbo].[Privileges] ON
INSERT INTO [dbo].[Privileges]
	([ID], [Name])
VALUES
	(1, 'CanViewSubjects')
INSERT INTO [dbo].[Privileges]
	([ID], [Name])
VALUES
	(2, 'CanViewEmployees')
INSERT INTO [dbo].[Privileges]
	([ID], [Name])
VALUES
	(3, 'CanCreateCourse')
INSERT INTO [dbo].[Privileges]
	([ID], [Name])
VALUES
	(4, 'CanUploadFilesToCourse')
INSERT INTO [dbo].[Privileges]
	([ID], [Name])
VALUES
	(5, 'CanSendNotifications')
INSERT INTO [dbo].[Privileges]
	([ID], [Name])
VALUES
	(6, 'CanCreateLabSessions')
INSERT INTO [dbo].[Privileges]
	([ID], [Name])
VALUES
	(7, 'CanInviteUsersToCourse')
INSERT INTO [dbo].[Privileges]
	([ID], [Name])
VALUES
	(8, 'CanViewCourseUsers')
INSERT INTO [dbo].[Privileges]
	([ID], [Name])
VALUES
	(9, 'CanEditCourseInfo')
INSERT INTO [dbo].[Privileges]
	([ID], [Name])
VALUES
	(10, 'CanGradeTest')
INSERT INTO [dbo].[Privileges]
	([ID], [Name])
VALUES
	(11, 'CanCreateTest')
INSERT INTO [dbo].[Privileges]
	([ID], [Name])
VALUES
	(12, 'CanSetCoursePassword')
INSERT INTO [dbo].[Privileges]
	([ID], [Name])
VALUES
	(13, 'CanCreateCourseDiscussionBoard')
INSERT INTO [dbo].[Privileges]
	([ID], [Name])
VALUES
	(14, 'CanViewUsers')
GO
SET IDENTITY_INSERT [dbo].[QuestionType] OFF

ALTER TABLE [dbo].[Exam] WITH CHECK ADD FOREIGN KEY([CourseID])
REFERENCES [dbo].[Course] ([ID])
GO

ALTER TABLE [dbo].[Exam] WITH CHECK ADD FOREIGN KEY([CreatedByID])
REFERENCES [dbo].[User] ([ID])
GO

ALTER TABLE [dbo].[ExamAttempt] WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO

ALTER TABLE [dbo].[ExamAttempt] WITH CHECK ADD FOREIGN KEY([ExamID])
REFERENCES [dbo].[Exam] ([ID])
GO

ALTER TABLE [dbo].[UserAnswer] WITH CHECK ADD FOREIGN KEY([AttemptID])
REFERENCES [dbo].[ExamAttempt] ([ID])
GO

ALTER TABLE [dbo].[UserAnswer] WITH CHECK ADD FOREIGN KEY([QuestionID])
REFERENCES [dbo].[Question] ([ID])
GO

ALTER TABLE [dbo].[UserAnswer] WITH CHECK ADD FOREIGN KEY([AnswerID])
REFERENCES [dbo].[Answer] ([ID])
GO

ALTER TABLE [dbo].[Question] WITH CHECK ADD FOREIGN KEY([ExamID])
REFERENCES [dbo].[Exam] ([ID])
GO

ALTER TABLE [dbo].[Question] WITH CHECK ADD FOREIGN KEY([TypeID])
REFERENCES [dbo].[QuestionType] ([ID])
GO

ALTER TABLE [dbo].[Answer] WITH CHECK ADD FOREIGN KEY([QuestionID])
REFERENCES [dbo].[Question] ([ID])
GO

ALTER TABLE [dbo].[Course] WITH CHECK ADD FOREIGN KEY([SpecializationID])
REFERENCES [dbo].[Specialization] ([ID])
GO

ALTER TABLE [dbo].[Subscription] WITH CHECK ADD FOREIGN KEY([CourseID])
REFERENCES [dbo].[Course] ([ID])
GO

ALTER TABLE [dbo].[Subscription] WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO

ALTER TABLE [dbo].[SidebarContent] WITH CHECK ADD FOREIGN KEY([CourseID])
REFERENCES [dbo].[Course] ([ID])
GO

ALTER TABLE [dbo].[Notification] WITH CHECK ADD FOREIGN KEY([CourseID])
REFERENCES [dbo].[Course] ([ID])
GO

ALTER TABLE [dbo].[Notification] WITH CHECK ADD FOREIGN KEY([SubmittedByID])
REFERENCES [dbo].[User] ([ID])
GO

ALTER TABLE [dbo].[SidebarContentFile] WITH CHECK ADD FOREIGN KEY([SidebarContentID])
REFERENCES [dbo].[SidebarContent] ([ID]) ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SidebarContentFile] WITH CHECK ADD FOREIGN KEY([FileID])
REFERENCES [dbo].[File] ([ID])
GO

ALTER TABLE [dbo].[Chat] WITH CHECK ADD FOREIGN KEY([FirstParticipantID])
REFERENCES [dbo].[User] ([ID])
GO

ALTER TABLE [dbo].[Chat] WITH CHECK ADD FOREIGN KEY([SecondParticipantID])
REFERENCES [dbo].[User] ([ID]) 
GO

ALTER TABLE [dbo].[User] WITH CHECK ADD FOREIGN KEY([ImageFileID])
REFERENCES [dbo].[File] ([ID]) 
GO

ALTER TABLE [dbo].[Message] WITH CHECK ADD FOREIGN KEY([ChatID])
REFERENCES [dbo].[Chat] ([ID])
GO

ALTER TABLE [dbo].[Message] WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO

ALTER TABLE [dbo].[UserPrivileges] WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO

ALTER TABLE [dbo].[UserPrivileges] WITH CHECK ADD FOREIGN KEY([PrivilegeID])
REFERENCES [dbo].[Privileges] ([ID])
GO

ALTER TABLE [dbo].[UserSettings] WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO

ALTER TABLE [dbo].[UserNotificationBlacklist] WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO

ALTER TABLE [dbo].[UserNotificationBlacklist] WITH CHECK ADD FOREIGN KEY([CourseID])
REFERENCES [dbo].[Course] ([ID])
GO

ALTER TABLE [dbo].[NotificationUserSeen] WITH CHECK ADD FOREIGN KEY([NotificationID])
REFERENCES [dbo].[Notification] ([ID])
GO

ALTER TABLE [dbo].[NotificationUserSeen] WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO

UPDATE [dbo].[Course] SET [Password] = 'testing'
GO

DROP TRIGGER IF EXISTS NewUserSettingsTrigger
GO

CREATE TRIGGER NewUserSettingsTrigger on [dbo].[User]
FOR INSERT 
AS 
INSERT INTO [UserSettings]
	([UserID])
SELECT ID
FROM INSERTED;
GO
