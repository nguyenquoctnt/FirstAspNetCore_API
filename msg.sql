USE [AutoSpy]
GO
ALTER TABLE [dbo].[AlertMessage] DROP CONSTRAINT [DF_AlertMessage_Visible]
GO
ALTER TABLE [dbo].[AlertMessage] DROP CONSTRAINT [DF_AlertMessage_Active]
GO
/****** Object:  Table [dbo].[AlertMessage]    Script Date: 6/14/2017 11:09:07 PM ******/
DROP TABLE [dbo].[AlertMessage]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetReplaceAllertMessageByLanguageId]    Script Date: 6/14/2017 11:09:07 PM ******/
DROP FUNCTION [dbo].[fn_GetReplaceAllertMessageByLanguageId]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetReplaceAllertMessageByLanguageId]    Script Date: 6/14/2017 11:09:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_GetReplaceAllertMessageByLanguageId]
(
	@Code				NVARCHAR(50)	=	NULL,
	@Language			VARCHAR(2)		=	'vi',
	@Parttern			VARCHAR(100)	=	NULL,
	@VNReplacement		NVARCHAR(255)	=	NULL,
	@ENReplacement		NVARCHAR(255)	=	NULL
)
RETURNS NVARCHAR(255)
AS
BEGIN
	DECLARE @Result NVARCHAR(255)
	SET @Result = ''

	SELECT @Result = CASE @Language WHEN 'vi' THEN VNMessage
									WHEN 'en' THEN ENMessage
					 END
	FROM dbo.AlertMessage (NOLOCK)
	WHERE Code = @Code

	RETURN REPLACE(@Result, @Parttern, CASE @Language WHEN 'vi' THEN @VNReplacement 
													  WHEN 'en' THEN @ENReplacement END)
END 


GO
/****** Object:  Table [dbo].[AlertMessage]    Script Date: 6/14/2017 11:09:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AlertMessage](
	[AlertMessageId] [int] NOT NULL,
	[Code] [nvarchar](20) NULL,
	[VNMessage] [nvarchar](255) NULL,
	[ENMessage] [nvarchar](255) NULL,
	[Active] [bit] NULL,
	[Visible] [bit] NULL,
 CONSTRAINT [PK_AlertMessage] PRIMARY KEY CLUSTERED 
(
	[AlertMessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[AlertMessage] ([AlertMessageId], [Code], [VNMessage], [ENMessage], [Active], [Visible]) VALUES (1, N'EXIST_000', N'Dữ liệu đã tồn tại. Vui lòng kiểm tra lại dữ liệu.', N'Data already exist. Please check the data again.', 1, 1)
INSERT [dbo].[AlertMessage] ([AlertMessageId], [Code], [VNMessage], [ENMessage], [Active], [Visible]) VALUES (2, N'EXIST_001', N'{{object}} không tồn tại. Vui lòng kiểm tra lại dữ liệu.', N'{{object}} does not exist. Please check the data again.', 1, 1)
INSERT [dbo].[AlertMessage] ([AlertMessageId], [Code], [VNMessage], [ENMessage], [Active], [Visible]) VALUES (3, N'EXIST_002', N'Lỗi xảy ra khi dữ liệu không tồn tại. Vui lòng kiểm tra lại dữ liệu.', N'The error occurred when the data does not exist. Please check the data again.', 1, 1)
INSERT [dbo].[AlertMessage] ([AlertMessageId], [Code], [VNMessage], [ENMessage], [Active], [Visible]) VALUES (4, N'WARN_000', N'Tồn tại dữ liệu không thể cập nhật hoặc xóa. Vui lòng kiểm tra lại dữ liệu.', N'Exists a data can not be update or delete. Please check the data again.', 1, 1)
INSERT [dbo].[AlertMessage] ([AlertMessageId], [Code], [VNMessage], [ENMessage], [Active], [Visible]) VALUES (5, N'WARN_001', N'Thời gian kết thúc không được nhỏ hơn thời gian bắt đầu.', N'The end time is not less than the start time.', 1, 1)
INSERT [dbo].[AlertMessage] ([AlertMessageId], [Code], [VNMessage], [ENMessage], [Active], [Visible]) VALUES (6, N'WARN_002', N'Đây là dữ liệu mặc định, không được phép chỉnh sửa.', N'This is the default data, are not allowed to edit.', 1, 1)
INSERT [dbo].[AlertMessage] ([AlertMessageId], [Code], [VNMessage], [ENMessage], [Active], [Visible]) VALUES (7, N'WARN_003', N'Số trang theo dõi của bạn đã vượt giới hạn!', N'The page number of the track you have exceeded the limit!', 1, 1)
INSERT [dbo].[AlertMessage] ([AlertMessageId], [Code], [VNMessage], [ENMessage], [Active], [Visible]) VALUES (8, N'WARN_004', N'Số trang bạn có thể quản lý đã vượt giới hạn!', N'Number of the page you can manage has exceeded the limit!', 1, 1)
ALTER TABLE [dbo].[AlertMessage] ADD  CONSTRAINT [DF_AlertMessage_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[AlertMessage] ADD  CONSTRAINT [DF_AlertMessage_Visible]  DEFAULT ((1)) FOR [Visible]
GO
