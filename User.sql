USE [AutoSpy]
GO
/****** Object:  Table [dbo].[User]    Script Date: 6/14/2017 10:52:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] NOT NULL,
	[UserType] [int] NULL,
	[Email] [nvarchar](255) NULL,
	[Password] [varchar](1000) NULL,
	[FullName] [nvarchar](150) NULL,
	[PassPhrase] [varchar](20) NULL,
	[SaltValue] [varchar](20) NULL,
	[InitVector] [varchar](50) NULL,
	[Phone] [varchar](50) NULL,
	[Address] [nvarchar](255) NULL,
	[FacebookToken] [varchar](max) NULL,
	[Avatar] [varchar](500) NULL,
	[ClientKey] [varchar](50) NULL,
	[Locked] [bit] NULL,
	[LockTime] [datetime] NULL,
	[UnlockTime] [datetime] NULL,
	[RegisteredTime] [datetime] NULL,
	[ExpiredTime] [datetime] NULL,
	[PackageId] [int] NULL,
	[SpyPageCount] [int] NULL,
	[OwnerPageCount] [int] NULL,
	[Note] [nvarchar](255) NULL,
	[Active] [bit] NULL,
	[Visible] [bit] NULL,
	[CreaterId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[EditerId] [int] NULL,
	[EditTime] [datetime] NULL,
	[GetCommentByEmail] [bit] NULL,
	[GetCommentByPhone] [bit] NULL,
	[GetCommentByTag] [bit] NULL,
	[GetCommentByKeyword] [bit] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_Locked]  DEFAULT ((0)) FOR [Locked]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_SpyPageCount]  DEFAULT ((0)) FOR [SpyPageCount]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_OwnerPageCount]  DEFAULT ((0)) FOR [OwnerPageCount]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_Visible]  DEFAULT ((1)) FOR [Visible]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_GetCommentByEmail]  DEFAULT ((0)) FOR [GetCommentByEmail]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_GetCommentByPhone]  DEFAULT ((0)) FOR [GetCommentByPhone]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_GetCommentByTag]  DEFAULT ((0)) FOR [GetCommentByTag]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_GetCommentByKeyword]  DEFAULT ((0)) FOR [GetCommentByKeyword]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Package] FOREIGN KEY([PackageId])
REFERENCES [dbo].[Package] ([PackageId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Package]
GO
/****** Object:  StoredProcedure [dbo].[sp_User]    Script Date: 6/14/2017 10:52:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_User]
@Activity							NVARCHAR(50)			=		NULL,
@UserLoginId						INT						=		NULL,
@UserTypeLoginId					INT						=		NULL,
@Language							NVARCHAR(2)				=		NULL,
-------------------------------------------------------------------------
@Offset								INT						=		NULL,
@Limit								INT						=		NULL,
@OrderBy							NVARCHAR(100)			=		NULL,
@OrderType							INT						=		NULL,
-------------------------------------------------------------------------
@UserId								INT						=		NULL,
@UserType							INT						=		NULL,
@Email								NVARCHAR(255)			=		NULL,
@Password							VARCHAR(1000)			=		NULL,
@FullName							NVARCHAR(255)			=		NULL,
@PassPhrase							VARCHAR(20)				=		NULL,
@SaltValue							VARCHAR(20)				=		NULL,
@InitVector							VARCHAR(50)				=		NULL,
@Phone								VARCHAR(50)				=		NULL,
@Address							NVARCHAR(255)			=		NULL,
@FacebookToken						VARCHAR(MAX)			=		NULL,
@Avatar								VARCHAR(500)			=		NULL,
@ClientKey							VARCHAR(50)				=		NULL,
@Locked								BIT						=		NULL,
@LockTime							DATETIME				=		NULL,
@UnlockTime							DATETIME				=		NULL,
@RegisteredTime						DATETIME				=		NULL,
@ExpiredTime						DATETIME				=		NULL,
@PackageId							INT						=		NULL,
@SpyPageCount						INT						=		NULL,
@OwnerPageCount						INT						=		NULL,
@GetCommentByEmail					BIT						=		NULL,
@GetCommentByPhone					BIT						=		NULL,
@GetCommentByTag					BIT						=		NULL,
@GetCommentByKeyword				BIT						=		NULL,
@Note								NVARCHAR(255)			=		NULL,
@Active								BIT						=		NULL,
@UserIds							NVARCHAR(MAX)			=		NULL,
-------------------------------------------------------------------------
@ReturnMess							NVARCHAR(255)			=		NULL	OUTPUT,
@ReturnValue						NVARCHAR(255)			=		NULL	OUTPUT
-------------------------------------------------------------------------
AS
SET DATEFORMAT DMY
-------------------------------------------------------------------------
IF @Activity = 'Update' OR @Activity = 'Delete'
BEGIN
	IF NOT EXISTS (
		SELECT TOP 1 1
		FROM dbo.[User] (NOLOCK)
		WHERE 1 = 1
			AND Visible = 1
			AND UserId = @UserId
	)
	BEGIN
		SET @ReturnMess = [dbo].[fn_GetReplaceAllertMessageByLanguageId]('EXIST_001', @Language, '{{object}}', N'Người dùng', 'User')
		RETURN;
	END
END 
IF @Activity = 'Insert' OR @Activity = 'Update'
BEGIN
	IF @PackageId IS NOT NULL
		IF NOT EXISTS
		(
			SELECT TOP 1 1
			FROM dbo.Package (NOLOCK)
			WHERE Visible = 1
				AND PackageId = @PackageId
		)
		BEGIN
			SET @ReturnMess = [dbo].[fn_GetReplaceAllertMessageByLanguageId]('EXIST_001', @Language, '{{object}}', N'Gói', 'Package')
			RETURN;
		END 
END 
-------------------------------------------------------------------------
IF @Activity = 'Insert'
BEGIN
	IF EXISTS (
		SELECT TOP 1 1
		FROM dbo.[User] (NOLOCK)
		WHERE 1 = 1
			AND (Email = @Email)
			AND Visible = 1
	)
	BEGIN
		SET @ReturnMess = dbo.fn_GetAllertMessageByLanguageId('EXIST_000', @Language);
		RETURN;
	END 

	EXEC sys_GenerateSequence N'User', N'UserId', @UserId OUTPUT
	INSERT INTO dbo.[User]
	        ( UserId ,
	          UserType ,
	          Email ,
	          Password ,
	          FullName ,
	          PassPhrase ,
	          SaltValue ,
	          InitVector ,
	          Phone ,
	          Address ,
	          FacebookToken ,
	          Avatar ,
	          ClientKey ,
	          Locked ,
	          LockTime ,
	          UnlockTime ,
	          RegisteredTime ,
	          ExpiredTime ,
	          PackageId ,
	          SpyPageCount ,
	          OwnerPageCount ,
	          Note ,
	          Active ,
	          Visible ,
	          CreaterId ,
	          CreateTime,
			  GetCommentByEmail,
			  GetCommentByPhone,
			  GetCommentByTag,
			  GetCommentByKeyword
	        )
	VALUES  ( @UserId , -- UserId - int
	          @UserType , -- UserType - int
	          @Email , -- Email - nvarchar(255)
	          @Password , -- Password - varchar(1000)
	          @FullName , -- FullName - nvarchar(150)
	          @PassPhrase , -- PassPhrase - varchar(20)
	          @SaltValue , -- SaltValue - varchar(20)
	          @InitVector , -- InitVector - varchar(50)
	          @Phone , -- Phone - varchar(50)
	          @Address , -- Address - nvarchar(255)
	          @FacebookToken , -- FacebookToken - varchar(max)
	          @Avatar , -- Avatar - varchar(500)
	          @ClientKey , -- ClientKey - varchar(50)
	          0 , -- Locked - bit
	          NULL , -- LockTime - datetime
	          NULL , -- UnlockTime - datetime
	          @RegisteredTime , -- RegisteredTime - datetime
	          @ExpiredTime , -- ExpiredTime - datetime
	          @PackageId , -- PackageId - int
	          @SpyPageCount , -- SpyPageCount - int
	          @OwnerPageCount , -- OwnerPageCount - int
	          @Note , -- Note - nvarchar(255)
	          @Active , -- Active - bit
	          1 , -- Visible - bit
	          IIF(@UserLoginId IS NULL OR @UserLoginId = -1, @UserId, @UserLoginId) , -- CreaterId - int
	          GETUTCDATE(), -- CreateTime - datetime
			  @GetCommentByEmail , --BIT
			  @GetCommentByPhone , --BIT
			  @GetCommentByTag , --BIT
			  @GetCommentByKeyword  --BIT
	        )

	SET @ReturnValue = @UserId
END 
ELSE IF @Activity = 'Update'
BEGIN
	IF EXISTS (
		SELECT TOP 1 1
		FROM dbo.[User] (NOLOCK)
		WHERE 1 = 1
			AND (Email = @Email)
			AND UserId <> @UserId
			AND Visible = 1
	)
	BEGIN
		SET @ReturnMess = dbo.fn_GetAllertMessageByLanguageId('EXIST_000', @Language);
		RETURN;
	END 

	UPDATE dbo.[User]
	SET UserType = IIF(@UserType IS NULL, UserType, @UserType),
		Email = IIF(@Email IS NULL, Email, @Email),
		Password = IIF(@Password IS NULL, Password, @Password),
		FullName = IIF(@FullName IS NULL, FullName, @FullName),
		Phone = IIF(@Phone IS NULL, Phone, @Phone),
		Address = IIF(@Address IS NULL, Address, @Address),
		FacebookToken = IIF(@FacebookToken IS NULL, FacebookToken, @FacebookToken),
		Avatar = IIF(@Avatar IS NULL, Avatar, @Avatar),
		ClientKey = IIF(@ClientKey IS NULL, ClientKey, @ClientKey),
		Locked = IIF(@Locked IS NULL, Locked, @Locked),
		UnlockTime = IIF(@UnlockTime IS NULL, UnlockTime, @UnlockTime),
		RegisteredTime = IIF(@RegisteredTime IS NULL, RegisteredTime, @RegisteredTime),
		ExpiredTime = IIF(@ExpiredTime IS NULL, ExpiredTime, @ExpiredTime),
		PackageId = IIF(@PackageId IS NULL, PackageId, @PackageId),
		SpyPageCount = IIF(@SpyPageCount IS NULL, SpyPageCount, @SpyPageCount),
		OwnerPageCount = IIF(@OwnerPageCount IS NULL, OwnerPageCount, @OwnerPageCount),
		Note = IIF(@Note IS NULL, Note, @Note),
		Active = IIF(@Active IS NULL, Active, @Active),
		EditerId = @UserLoginId,
		EditTime = GETUTCDATE(),
		GetCommentByEmail = IIF(@GetCommentByEmail IS NULL, GetCommentByEmail, @GetCommentByEmail),
		GetCommentByPhone = IIF(@GetCommentByPhone IS NULL, GetCommentByPhone, @GetCommentByPhone),
		GetCommentByTag = IIF(@GetCommentByTag IS NULL, GetCommentByTag, @GetCommentByTag),
		GetCommentByKeyword = IIF(@GetCommentByKeyword IS NULL, GetCommentByKeyword, @GetCommentByKeyword)
	WHERE UserId = @UserId
END 
ELSE IF @Activity = 'Delete'
BEGIN
	UPDATE dbo.[User]
	SET Visible = 0,
		EditerId = @UserLoginId,
		EditTime = GETUTCDATE()
	WHERE UserId = @UserId

	UPDATE dbo.UserRole 
	SET Visible = 0
	WHERE UserId = @UserId

	UPDATE dbo.UserDomain 
	SET Visible = 0,
		Running = 0
	WHERE UserId = @UserId

	UPDATE dbo.SpyUserDomain
	SET Visible = 0,
		Running = 0
	FROM dbo.SpyUserDomain sud (NOLOCK)
		LEFT JOIN dbo.UserDomain (NOLOCK) ud ON ud.UserDomainId = sud.UserDomainId
	WHERE ud.UserId = @UserId

	UPDATE dbo.Spy
	SET Visible = 0,
		Running = 0,
		EditerId = @UserLoginId,
		EditTime = GETUTCDATE()
	WHERE CreaterId = @UserId
END 
ELSE IF @Activity = 'DeleteAll'
BEGIN
	DECLARE @CurrentPosition INT
	SET @CurrentPosition = 1

	WHILE (dbo.[fn_GetStringByToken](@UserIds + ',', ',', @CurrentPosition) <> '')
	BEGIN
		SET @UserId = CONVERT(INT, dbo.[fn_GetStringByToken](@UserIds + ',', ',', @CurrentPosition))
		EXEC dbo.sp_User @Activity = N'Delete', -- nvarchar(50)
		    @UserLoginId = @UserLoginId, -- int
		    @UserTypeLoginId = @UserTypeLoginId, -- int
		    @Language = @Language, -- nvarchar(2)
		    @UserId = @UserId,
		    @ReturnMess = @ReturnMess OUTPUT, -- nvarchar(255)
		    @ReturnValue = @ReturnValue OUTPUT -- nvarchar(255)
		
		IF ISNULL(@ReturnMess, '') <> ''
			RETURN;
		
		SET @CurrentPosition += 1;
	END 
END 
ELSE IF @Activity = 'GetAllItems'
BEGIN
	;WITH CTE_User AS (
		SELECT UserId
		FROM dbo.[User] (NOLOCK)
		WHERE Visible = 1
			AND (@Email IS NULL OR ISNULL(Email, '') = @Email)
	)

	SELECT u.UserId, u.UserType, u.Email, u.Password, u.FullName, u.PassPhrase, u.SaltValue, u.InitVector, u.Phone, u.Address, u.FacebookToken, u.Avatar, u.ClientKey, u.Locked, u.LockTime, u.UnlockTime, u.RegisteredTime, u.ExpiredTime, u.PackageId, u.SpyPageCount, u.OwnerPageCount, u.Note, u.Active, ct.TotalRow,u.GetCommentByEmail,u.GetCommentByPhone,u.GetCommentByTag,u.GetCommentByKeyword
	FROM CTE_User cte
		CROSS JOIN (
			SELECT COUNT(1) AS TotalRow
			FROM CTE_User
		) AS ct 
		INNER JOIN dbo.[User] (NOLOCK) u ON u.UserId = cte.UserId
	ORDER BY u.ExpiredTime ASC
	OFFSET @Offset ROWS
    FETCH NEXT @Limit ROWS ONLY
END 
ELSE IF @Activity = 'GetItemById'
BEGIN
	SELECT u.UserId, u.UserType, u.Email, u.Password, u.FullName, u.PassPhrase, u.SaltValue, u.InitVector, u.Phone, u.Address, u.FacebookToken, u.Avatar, u.ClientKey, u.Locked, u.LockTime, u.UnlockTime, u.RegisteredTime, u.ExpiredTime, u.PackageId, u.SpyPageCount, u.OwnerPageCount, u.Note, u.Active,u.GetCommentByEmail,u.GetCommentByPhone,u.GetCommentByTag,u.GetCommentByKeyword
	FROM dbo.[User] (NOLOCK) u
	WHERE u.Visible = 1
		AND (@UserId IS NULL OR u.UserId = @UserId)
		AND (@Email IS NULL OR u.Email = @Email)
END 
GO
