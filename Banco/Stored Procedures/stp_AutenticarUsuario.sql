USE [Docs]
GO

/****** Object:  StoredProcedure [dbo].[stp_AutenticarUsuario]    Script Date: 03/19/2015 09:03:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ===============================================================
-- Author:		Yule Souza
-- Create date: 09/03/2015
-- Description:	Autentica Usuário através de e-mail e senha
-- ===============================================================
CREATE PROCEDURE [dbo].[stp_AutenticarUsuario]
	-- Add the parameters for the stored procedure here
	@P_Email VARCHAR(80),
	@P_Senha VARCHAR(30)
AS
BEGIN
	SET NOCOUNT ON

	SELECT 
	U.EntidadeID, 
	U.Nome, 
	U.Sobrenome, 
	U.Email, 
	U.DataNascimento, 
	U.Avatar, 
	S.SenhaHash, 
	P.Nome AS Perfil, 
	U.DataModificacao 
	FROM Usuario U 
	JOIN Senha S ON U.entidadeID = S.entidadeID 
	JOIN perfil P ON U.entidadeID = P.entidadeID  
	WHERE U.email = @P_Email AND S.SenhaHash = @P_Senha
END

GO


