USE [DocsDB]
GO

/****** Object:  StoredProcedure [dbo].[stp_AuthenticateUser]    Script Date: 04/02/2015 11:24:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ===============================================================
-- Author:		Yule Souza
-- Create date: 09/03/2015
-- Description:	Authenticate User by e-mail and password
-- ===============================================================
CREATE PROCEDURE [dbo].[stp_AuthenticateUser]
	@p_Email VARCHAR(80),
	@p_Password VARCHAR(30)
AS
BEGIN
	SET NOCOUNT ON

	SELECT 
	U.EntityID, 
	U.Name, 
	U.LastName, 
	U.Email, 
	U.BirthDate, 
	U.Avatar, 
	S.PasswordHash, 
	P.Name AS PersonProfile, 
	U.ModifiedDate 
	FROM Person U 
	JOIN PersonPassword S ON U.EntityID = S.EntityID 
	JOIN PersonProfile P ON U.EntityID = P.EntityID  
	WHERE U.Email = @p_Email AND S.PasswordHash = @p_Password
END

GO


