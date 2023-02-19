


CREATE TABLE dbo.Bugs
	(
	BugID int NOT NULL IDENTITY (1, 1),
	BugTitle nvarchar(50) NOT NULL,
	BugDescription nvarchar(250) NOT NULL,
	BugOpened datetime NOT NULL,
	BugAssigned nvarchar(50) NULL,
	BugClosed bit NULL
	)  ON [PRIMARY]
GO

ALTER TABLE dbo.Bugs ADD CONSTRAINT
	PK_Bugs PRIMARY KEY CLUSTERED 
	(
	BugID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO


CREATE TABLE dbo.Users
	(
	UserID int NOT NULL IDENTITY (1, 1),
	Title nvarchar(5) NOT NULL,
	FirstName nvarchar(50) NOT NULL,
	LastName nvarchar(50) NOT NULL,
	DOB date NOT NULL
	)  ON [PRIMARY]
GO

ALTER TABLE dbo.Users ADD CONSTRAINT
	PK_Users PRIMARY KEY CLUSTERED 
	(
	UserID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

