USE [DocsDB]
GO

/****** Object:  StoredProcedure [dbo].[stp_CheckEmailExist]    Script Date: 04/07/2015 10:21:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- ===================================================
-- Author:		Yule Souza	
-- Create date: 30/03/2015
-- Description:	Check If the e-mail exists 
-- ===================================================
CREATE PROCEDURE [dbo].[stp_CheckEmailExist]
	
	@p_Email VARCHAR(80)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT 
	U.Email
	FROM Person U
	JOIN PersonProfile P 
	ON U.EntityID = P.EntityID
	WHERE U.Email = @p_Email
		
END


GO


