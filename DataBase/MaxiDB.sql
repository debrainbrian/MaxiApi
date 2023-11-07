
-- Create de database
CREATE DATABASE [MaxiDataBase];

GO

USE [MaxiDataBase]
GO

-- Create user
CREATE LOGIN MaxiUser 
		WITH PASSWORD = 'M4xiUser.',
		CHECK_POLICY     = OFF,
		CHECK_EXPIRATION = OFF;
GO

CREATE USER [MaxiUser] FOR LOGIN [MaxiUser]
GO

USE [MaxiDataBase]
GO

ALTER ROLE [db_owner] ADD MEMBER [MaxiUser]
GO

USE [MaxiDataBase];
GO

-- Create schemas
CREATE SCHEMA [Sec];
GO

CREATE SCHEMA [Bus];
GO

-- Create Tables

CREATE TABLE [Sec].[User]
(
	[UserId]		[INT] IDENTITY(1,1) NOT NULL,
	[Nickname]		[NVARCHAR](30) NOT NULL,
	[Password]		[NVARCHAR](100) NOT NULL,
	[Name]			[NVARCHAR](150) NOT NULL,
	[FirstName]		[NVARCHAR](150) NOT NULL,
	[LastName]		[NVARCHAR](150) NULL,
	[Email]			[NVARCHAR](150) NULL,
	[LastHash]		[NVARCHAR](400) NULL,
	[LastLoggedIn]	[DATETIME] NULL,
	[IsActive]		[BIT] NOT NULL,
	[InsertUserId]	[INT] NOT NULL,
	[InsertDate]	[SMALLDATETIME] NOT NULL,
	[UpdateUserId]	[INT] NOT NULL,
	[UpdateDate]	[SMALLDATETIME] NOT NULL,

	CONSTRAINT [PkcUser] PRIMARY KEY CLUSTERED 
	(
		[UserId] ASC
	) ON [PRIMARY]
) 
GO

CREATE TABLE [Sec].[LogError]
(
	[ErrorId]	[INT] IDENTITY(1,1) NOT NULL,
	[ErrorDate] [DATETIME] NOT NULL,
	[Class]		[NVARCHAR](100) NOT NULL,
	[Method]	[NVARCHAR](100) NOT NULL,
	[Trace]		[NVARCHAR](2500) NULL,
	[Message]	[NVARCHAR](2500) NULL,
	[Source]	[NVARCHAR](MAX) NULL,
	
	CONSTRAINT [PkcLogError] PRIMARY KEY CLUSTERED 
	(
		[ErrorId] ASC
	) ON [PRIMARY]
)
GO

CREATE TABLE [Bus].[Employee]
(
	[EmployeeId]	[INT] IDENTITY(1,1) NOT NULL,
	[Name]			[NVARCHAR](50) NOT NULL,
	[FirstName]		[NVARCHAR](50) NOT NULL,
	[LastName]		[NVARCHAR](50) NOT NULL,
	[BirthDate]		[DATE] NOT NULL,
	[EmployeeNo]	[NVARCHAR](5) NOT NULL,
	[Curp]			[NVARCHAR](20) NULL,
	[Ssn]			[NVARCHAR](10) NULL,
	[Telephone]		[NVARCHAR](10) NULL,
	[Nationality]	[NVARCHAR](50) NULL,
	
	CONSTRAINT [PkcEmployee] PRIMARY KEY CLUSTERED 
	(
		[EmployeeId] ASC
	) ON [PRIMARY]
)
GO

CREATE TABLE [Bus].[Beneficiary]
(
	[BeneficiaryId]	[INT] IDENTITY(1,1) NOT NULL,
	[EmployeeId]	[INT] NOT NULL,
	[Name]			[NVARCHAR](50) NOT NULL,
	[FirstName]		[NVARCHAR](50) NOT NULL,
	[LastName]		[NVARCHAR](50) NOT NULL,
	[BirthDate]		[DATE] NOT NULL,
	[Curp]			[NVARCHAR](20) NULL,
	[Ssn]			[NVARCHAR](10) NULL,
	[Telephone]		[NVARCHAR](10) NULL,
	[Nationality]	[NVARCHAR](50) NULL,
	[Percent]		[TINYINT] NULL,
	
	CONSTRAINT [PkcBeneficiary] PRIMARY KEY CLUSTERED 
	(
		[BeneficiaryId] ASC
	) ON [PRIMARY]
)
GO

ALTER TABLE [Bus].[Beneficiary] ADD CONSTRAINT [FkcBeneficiaryEmployee] FOREIGN KEY([EmployeeId])
REFERENCES [Bus].[Employee] ([EmployeeId])
GO

-- Create Procedures

CREATE PROCEDURE [Sec].[SetLoginData]
(
	@UserId INT ,
	@Token NVARCHAR(400) ,
	@LastLogin DATETIME 
)
AS
BEGIN

	UPDATE	[Sec].[User]

	SET		[LastLoggedIn] = @LastLogin ,
			[LastHash] = @Token

	WHERE	[UserId] = @UserId

END
GO

CREATE PROCEDURE [Sec].[GetUser]
(
	@UserId		[INT],
	@Nickname	[NVARCHAR](30),
	@IsActive	[BIT]
)
AS
BEGIN
	
	SELECT	[Usr].[UserId] ,
			1 AS [RoleId] ,
			[Usr].[Nickname] , 
			[Usr].[Password] , 
			[Usr].[Name] , 	
			[Usr].[FirstName]	, 
			[Usr].[LastName] , 
			[Usr].[Email] , 	
			[Usr].[LastHash] , 
			[Usr].[LastLoggedIn] , 
			[Usr].[IsActive]
			
	FROM		[Sec].[User] AS [Usr]

	WHERE	[Usr].[UserId] = COALESCE(@UserId, [Usr].[UserId])
	AND		[Usr].[Nickname] = COALESCE(@Nickname, [Usr].[Nickname]) 
	AND		[Usr].[IsActive] = COALESCE(@IsActive, [Usr].[IsActive])

END
GO

CREATE PROCEDURE [Sec].[SaveError]
(
	@ErrorDate	AS DATETIME , 
	@Class		AS NVARCHAR(100) , 
	@Method		AS NVARCHAR(100) ,
	@Trace		AS NVARCHAR(2500) ,
	@Message	AS NVARCHAR(2500) ,
	@Source		AS NVARCHAR(MAX)
)
AS
BEGIN

	SET NOCOUNT ON

	INSERT INTO [Sec].[LogError] ( [ErrorDate]	, [Class] , [Method] , [Trace] , [Message] , [Source])
	VALUES ( @ErrorDate	, @Class ,  @Method , @Trace , @Message , @Source )
	
	SELECT @@IDENTITY	 		
END	
GO

CREATE PROCEDURE [Bus].[GetEmployee]
(
	@EmployeeId	AS [INT] = NULL ,
	@Name		AS NVARCHAR(50) = NULL , 
	@FirstName	AS NVARCHAR(100) = NULL , 
	@EmployeeNo	AS NVARCHAR(5) = NULL 
)
AS
BEGIN

	SET NOCOUNT ON

	SELECT	[EmployeeId] , [Name] , [FirstName] , [LastName] , [BirthDate] , [EmployeeNo] , [Curp] , [Ssn] , [Telephone] , [Nationality]
	FROM	[Bus].[Employee]
	WHERE	[EmployeeId] = COALESCE(@EmployeeId, [EmployeeId])
	AND		[Name] = COALESCE(@Name, [Name])
	AND		[FirstName] = COALESCE(@FirstName, [FirstName])
	AND		[EmployeeNo] = COALESCE(@EmployeeNo, [EmployeeNo])
		 		
END	
GO

CREATE PROCEDURE [Bus].[SetEmployee]
(
	@EmployeeId		AS [INT] ,
	@Name			AS NVARCHAR(50) , 
	@FirstName		AS NVARCHAR(50) , 
	@LastName		AS NVARCHAR(50) , 
	@BirthDate		AS DATE ,
	@EmployeeNo		AS NVARCHAR(5)  ,
	@Curp			AS NVARCHAR(20) ,
	@Ssn			AS NVARCHAR(10) ,
	@Telephone		AS NVARCHAR(10) ,
	@Nationality	AS NVARCHAR(50)  
)
AS
BEGIN

	SET NOCOUNT ON

	IF(@EmployeeId = -1)
	BEGIN
	
		INSERT INTO [Bus].[Employee] ( [Name] , [FirstName] , [LastName] , [BirthDate] , [EmployeeNo] , [Curp] , [Ssn] , [Telephone] , [Nationality] )
		VALUES ( @Name , @FirstName , @LastName , @BirthDate , @EmployeeNo , @Curp , @Ssn , @Telephone , @Nationality )

		SET @EmployeeId = SCOPE_IDENTITY()

	END
	ELSE
	BEGIN

		UPDATE [Bus].[Employee]
		SET		[Name] = @Name , 
				[FirstName] = @FirstName , 
				[LastName] = @LastName , 
				[BirthDate] = @BirthDate , 
				[EmployeeNo] = @EmployeeNo , 
				[Curp] = @Curp , 
				[Ssn] = @Ssn , 
				[Telephone] = @Telephone , 
				[Nationality] = @Nationality
		WHERE	[EmployeeId] = @EmployeeId

	END

	SELECT @EmployeeId
		 		
END	
GO

CREATE PROCEDURE [Bus].[DelEmployee]
(
	@EmployeeId	AS [INT] = NULL 
)
AS
BEGIN

	SET NOCOUNT ON

	BEGIN TRANSACTION [Delete]

	BEGIN TRY

		DELETE	[Bus].[Beneficiary]
		WHERE	[EmployeeId] = @EmployeeId

		DELETE	[Bus].[Employee]
		WHERE	[EmployeeId] = @EmployeeId

	END TRY
	BEGIN CATCH

		ROLLBACK TRANSACTION

	END CATCH

	IF @@TRANCOUNT > 0
	BEGIN

		COMMIT TRANSACTION [Delete];

	END
END	
GO

CREATE PROCEDURE [Bus].[GetBeneficiary]
(
	@EmployeeId	AS [INT] = NULL 
)
AS
BEGIN

	SET NOCOUNT ON

	SELECT	[BeneficiaryId] , [EmployeeId] , [Name] , [FirstName] , [LastName] , [BirthDate] , [Curp] , [Ssn] , [Telephone] , [Nationality] , [Percent]
	FROM	[Bus].[Beneficiary]
	WHERE	[EmployeeId] = COALESCE(@EmployeeId, [EmployeeId])
		 		
END	
GO

CREATE PROCEDURE [Bus].[DelBeneficiary]
(
	@BeneficiaryId	AS [INT] = NULL 
)
AS
BEGIN

	SET NOCOUNT ON

	DELETE	[Bus].[Beneficiary]
	WHERE	[BeneficiaryId] = @BeneficiaryId

END	
GO

CREATE PROCEDURE [Bus].[SetBeneficiary]
(
	@BeneficiaryId	AS [INT] ,
	@EmployeeId		AS [INT] ,
	@Name			AS NVARCHAR(50) , 
	@FirstName		AS NVARCHAR(50) , 
	@LastName		AS NVARCHAR(50) , 
	@BirthDate		AS DATE ,
	@Curp			AS NVARCHAR(20) ,
	@Ssn			AS NVARCHAR(10) ,
	@Telephone		AS NVARCHAR(10) ,
	@Nationality	AS NVARCHAR(50) ,
	@Percent		AS TINYINT
)
AS
BEGIN

	SET NOCOUNT ON

	IF(@BeneficiaryId = -1)
	BEGIN
	
		INSERT INTO [Bus].[Beneficiary] ( [EmployeeId] , [Name] , [FirstName] , [LastName] , [BirthDate] , [Curp] , [Ssn] , [Telephone] , [Nationality] , [Percent] )
		VALUES ( @EmployeeId , @Name , @FirstName , @LastName , @BirthDate , @Curp , @Ssn , @Telephone , @Nationality , @Percent )

		SET @BeneficiaryId = SCOPE_IDENTITY()

	END
	ELSE
	BEGIN

		UPDATE [Bus].[Beneficiary]
		SET		[EmployeeId] = @EmployeeId,
				[Name] = @Name , 
				[FirstName] = @FirstName , 
				[LastName] = @LastName , 
				[BirthDate] = @BirthDate ,  
				[Curp] = @Curp , 
				[Ssn] = @Ssn , 
				[Telephone] = @Telephone , 
				[Nationality] = @Nationality ,
				[Percent] = @Percent
		WHERE	[EmployeeId] = @EmployeeId

	END

	SELECT @BeneficiaryId
		 		
END	
GO

INSERT INTO [Sec].[User]
           ([Nickname]
           ,[Password]
           ,[Name]
           ,[FirstName]
           ,[LastName]
           ,[Email]
           ,[LastHash]
           ,[LastLoggedIn]
           ,[IsActive]
           ,[InsertUserId]
           ,[InsertDate]
           ,[UpdateUserId]
           ,[UpdateDate])
VALUES
        (
		   'bmartinez' , 
           'bgSA4c0kEXQQPB9t6E2jtg27MAs489XEnl3l4vVrJzK/uMVJycJrFmQH6s/aJnRd' , 
           'Brian' , 
           'Mártinez' , 
           'Narváez' , 
           'de_brainbrianhotmail.com' , 
           '' , 
           '20231101' , 
           1 , 
           1 , 
           '20231101' , 
           1 , 
           '20231101'  
		)
GO