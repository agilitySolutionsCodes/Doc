USE [DocsDB]
GO

/****** Object:  StoredProcedure [dbo].[stp_RegisterProject]    Script Date: 04/02/2015 14:35:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =====================================================
-- Author:		Yule Souza
-- Create date: 10/03/2015
-- Description:	Register new project 
-- =====================================================
CREATE PROCEDURE [dbo].[stp_RegisterProject]
	
	@p_ManagerID INT,
	@p_ProjectName VARCHAR(80),
	@p_StartDate DATETIME,
	@p_EndDate DATETIME,
	@p_ProjectStatus BIT,
	@p_TermHours SMALLINT,
	@p_ProjectClassification VARCHAR(40),
	@p_ProjectID INT OUTPUT
	
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO Project
	VALUES
	(
	  @p_ManagerID,
	  @p_ProjectName,
	  @p_StartDate,
	  @p_EndDate,
	  @p_ProjectStatus,
	  GETDATE()
	)
	
	SET @p_ProjectID = @@IDENTITY
	
	INSERT INTO Term
	VALUES
	(
	 @p_ProjectID,
	 @p_TermHours,
	 @p_ProjectClassification,
	 GETDATE()
	)
END


GO


