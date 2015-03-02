USE Docs

CREATE TABLE usuario
(
	entidadeID INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	nome VARCHAR(60) NOT NULL,
	sobrenome VARCHAR(60),
	email VARCHAR(80) NOT NULL,
	dataNascimento DATE,
	avatar VARCHAR(50),
	dataModificacao DATE NOT NULL
)

CREATE TABLE senha 
(
	entidadeID INT NOT NULL IDENTITY(1, 1) PRIMARY KEY FOREIGN KEY REFERENCES usuario(entidadeID),
	senhaHash VARCHAR(30) NOT NULL,
	dataModificacao DATE NOT NULL
)

CREATE TABLE perfil 
(
	entidadeID INT NOT NULL IDENTITY(1, 1) PRIMARY KEY FOREIGN KEY REFERENCES usuario(entidadeID),
	nome VARCHAR(30) NOT NULL,
	dataModificacao DATE NOT NULL
)

CREATE TABLE projeto
(
	entidadeID INT NOT NULL IDENTITY(1, 1) PRIMARY KEY, 
	gerenteID INT NOT NULL FOREIGN KEY REFERENCES usuario(entidadeID),
	nome VARCHAR(80), 
	dataInicio DATE NOT NULL,
	dataTermino DATE NOT NULL,
	statusProjeto BIT NOT NULL,
	dataModificacao DATE NOT NULL
)

CREATE TABLE prazo 
(
	entidadeID INT NOT NULL IDENTITY(1, 1) PRIMARY KEY FOREIGN KEY REFERENCES projeto(entidadeID),
	horas SMALLINT NOT NULL,
	classificacao VARCHAR(40),
	dataModificacao DATE NOT NULL
)

CREATE TABLE etapa
(
	etapaID INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	nome VARCHAR(70), 
	dataInicio DATE NOT NULL,
	dataTermino DATE NOT NULL,
	statusEtapa BIT NOT NULL,
	dataModificacao DATE NOT NULL
)

CREATE TABLE projeto_x_etapa
(
	projetoEntitdadeID INT NOT NULL FOREIGN KEY REFERENCES projeto(entidadeID),
	etapaID INT NOT NULL FOREIGN KEY REFERENCES etapa(etapaID)
)

CREATE TABLE documento 
(
	documentoID INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	autorID INT NOT NULL FOREIGN KEY REFERENCES usuario(entidadeID),
	nome VARCHAR(70) NOT NULL,
	extensao CHAR(10),
	dataInclusao DATE NOT NULL,
	dataModificacao DATE NOT NULL
)

CREATE TABLE documento_x_etapa
(
	documentoID INT NOT NULL FOREIGN KEY REFERENCES documento(documentoID),
	etapaID INT NOT NULL FOREIGN KEY REFERENCES etapa(etapaID)
)