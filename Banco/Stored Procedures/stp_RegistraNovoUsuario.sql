USE [Docs]
GO

/****** Object:  StoredProcedure [dbo].[stp_RegistraNovoUsuario]    Script Date: 03/19/2015 09:04:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




-- ================================================
-- Author:		Yule Souza
-- Create date: 10/03/2015
-- Description:	Registra novo usuário
-- =============================================
CREATE PROCEDURE [dbo].[stp_RegistraNovoUsuario]
	@P_Nome VARCHAR(60),
	@P_Sobrenome VARCHAR(60),
	@P_Email VARCHAR(80),
	@P_DataNascimento DATE,
	@P_Avatar VARCHAR(50) = NULL,
	@P_PerfilUsuario VARCHAR(30),
	@P_PerfilCodigoUsuario VARCHAR(3),
	@P_SenhaHash VARCHAR(30),
	@P_IdUsuario INT OUTPUT,
	@P_UsuarioOnline BIT OUTPUT
AS
BEGIN	
	SET NOCOUNT ON;
IF(SELECT COUNT(*) FROM Usuario WHERE Email = @P_Email) = 0
 BEGIN
	INSERT INTO Usuario 
	VALUES
	(
	 @P_Nome, 
	 @P_Sobrenome,
	 @P_Email,
	 @P_DataNascimento,
	 @P_Avatar,
	 GETDATE()
	)
	SELECT @P_IdUsuario = @@IDENTITY
	
	INSERT INTO Senha 
	VALUES
	(
	 @P_IdUsuario,
	 @P_SenhaHash,
	 GETDATE()
	)
	
	INSERT INTO Perfil
	VALUES
	(
	 @P_IdUsuario,
	 @P_PerfilUsuario,
	 @P_PerfilCodigoUsuario,
	 GETDATE()
	)
	
	SET @P_UsuarioOnline = 'TRUE'
 END
END




GO


