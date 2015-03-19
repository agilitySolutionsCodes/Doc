-- =====================================================
-- Author:		Yule Souza
-- Create date: 10/03/2015
-- Description:	Registra Novo Projeto no Sistema
-- =====================================================
CREATE PROCEDURE stp_RegistraNovoProjeto
	
	@P_GerenteID INT,
	@P_NomeProjeto VARCHAR(80),
	@P_DataInicio DATETIME,
	@P_DataTermino DATETIME,
	@P_HorasProjeto SMALLINT,
	@P_StatusProjeto BIT,
	@P_Classificacao VARCHAR(40),
	@P_IdProjeto INT OUTPUT
	
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO Projeto
	VALUES
	(
	  @P_GerenteID,
	  @P_NomeProjeto,
	  @P_DataInicio,
	  @P_DataTermino,
	  @P_StatusProjeto,
	  GETDATE()
	)
	
	SELECT @P_IdProjeto = @@IDENTITY
	
	INSERT INTO Prazo
	VALUES
	(
	 @P_IdProjeto,
	 @P_HorasProjeto,
	 @P_Classificacao,
	 GETDATE()
	)
END
GO
