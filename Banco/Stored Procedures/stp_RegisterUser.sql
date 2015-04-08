USE [DocsDB]
GO

/****** Object:  StoredProcedure [dbo].[stp_RegisterUser]    Script Date: 04/08/2015 14:22:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- ================================================
-- Author:		Yule Souza
-- Create date: 10/03/2015
-- Description:	Register new user 
-- =============================================
ALTER PROCEDURE [dbo].[stp_RegisterUser]
	@p_FirstName VARCHAR(60),
	@p_LastName VARCHAR(60),
	@p_Email VARCHAR(80),
	@p_BirthDate DATE,
	@p_Avatar VARCHAR(50) = NULL,
	@p_UserProfile VARCHAR(30),
	@p_UserProfileCode VARCHAR(3),
	@p_PasswordHash VARCHAR(30),
	@p_UserID INT OUTPUT,
	@p_UserOnline BIT OUTPUT
AS
BEGIN	
	SET NOCOUNT ON;
IF(SELECT COUNT(*) FROM Person WHERE Email = @p_Email) = 0
 BEGIN
	INSERT INTO Person 
	VALUES
	(
	 @p_FirstName, 
	 @p_LastName,
	 @p_Email,
	 @p_BirthDate,
	 @p_Avatar,
	 GETDATE()
	)
	SELECT @p_UserID = @@IDENTITY
	
	INSERT INTO PersonPassword 
	VALUES
	(
	 @p_UserID,
	 @p_PasswordHash,
	 GETDATE()
	)
	
	INSERT INTO PersonProfile
	VALUES
	(
	 @p_UserID,
	 @p_UserProfile,
	 @p_UserProfileCode,
	 GETDATE()
	)
	
	SET @p_UserOnline = 'TRUE'
 END
END





GO


