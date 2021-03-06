USE [registrar]
GO
/****** Object:  Table [dbo].[courses]    Script Date: 12/13/2016 9:30:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[courses](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[number] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[courses_students]    Script Date: 12/13/2016 9:30:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[courses_students](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[course_id] [int] NULL,
	[student_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[students]    Script Date: 12/13/2016 9:30:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[students](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[enrollment] [datetime] NULL
) ON [PRIMARY]

GO
