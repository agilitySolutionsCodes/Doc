USE DocsDB

CREATE TABLE Person
(
	EntityID INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	Name VARCHAR(60) NOT NULL,
	LastName VARCHAR(60),
	Email VARCHAR(80) NOT NULL,
	BirthDate DATE,
	Avatar VARCHAR(50),
	ModifiedDate DATE NOT NULL
)

CREATE TABLE PersonPassword 
(
	EntityID INT NOT NULL PRIMARY KEY FOREIGN KEY REFERENCES Person(EntityID),
	PasswordHash VARCHAR(30) NOT NULL,
	ModifiedDate DATE NOT NULL
)

CREATE TABLE PersonProfile 
(
	EntityID INT NOT NULL PRIMARY KEY FOREIGN KEY REFERENCES Person(EntityID),
	Name VARCHAR(30) NOT NULL,
	ProfileCode CHAR(2) NOT NULL,
	ModifiedDate DATE NOT NULL
)

CREATE TABLE Project
(
	EntityID INT NOT NULL IDENTITY(1, 1) PRIMARY KEY, 
	ManagerID INT NOT NULL FOREIGN KEY REFERENCES Person(EntityID),
	ProjectName VARCHAR(80), 
	StartDate DATE NOT NULL,
	EndDate DATE NOT NULL,
	ProjectStatus BIT NOT NULL,
	ModifiedDate DATE NOT NULL
)

CREATE TABLE Term 
(
	EntityID INT NOT NULL PRIMARY KEY FOREIGN KEY REFERENCES Project(EntityID),
	TermHours SMALLINT NOT NULL,
	Classification VARCHAR(40),
	ModifiedDate DATE NOT NULL
)

CREATE TABLE Stage
(
	EntityID INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	StageName VARCHAR(70), 
	StartDate DATE NOT NULL,
	EndDate DATE NOT NULL,
	StageStatus BIT NOT NULL,
	ModifiedDate DATE NOT NULL
)

CREATE TABLE ProjectStage
(
	ProjectEntityID INT NOT NULL FOREIGN KEY REFERENCES Project(EntityID),
	StageEntityID INT NOT NULL FOREIGN KEY REFERENCES Stage(EntityID)
)

CREATE TABLE Document 
(
	DocumentID INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	AuthorID INT NOT NULL FOREIGN KEY REFERENCES Person(EntityID),
	Name VARCHAR(70) NOT NULL,
	Extension CHAR(10),
	InclusionDate DATE NOT NULL,
	ModifiedDate DATE NOT NULL
)

CREATE TABLE DocumentStage
(
	DocumentID INT NOT NULL FOREIGN KEY REFERENCES Document(DocumentID),
	StageEntityID INT NOT NULL FOREIGN KEY REFERENCES Stage(EntityID)
)