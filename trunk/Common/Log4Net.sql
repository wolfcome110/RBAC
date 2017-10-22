CREATE TABLE [dbo].[UDT_SYS_Log](
	[LogID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Thread] [nvarchar](255) NULL,
	[Level] [nvarchar](50) NULL,
	[Logger] [nvarchar](255) NULL,
	[Message] [nvarchar](2000) NULL,
	[Exception] [nvarchar](4000) NULL,
 CONSTRAINT [PK_WebLog_Msg] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
))