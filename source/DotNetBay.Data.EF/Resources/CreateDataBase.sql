USE DotNetBay;
----------------------------
-- Datenbank zurücksetzen --
----------------------------
-- 1. Alle Constraints
DECLARE @sql NVARCHAR(MAX) = N'';

SELECT @sql += N'
ALTER TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(parent_object_id))
    + '.' + QUOTENAME(OBJECT_NAME(parent_object_id)) + 
    ' DROP CONSTRAINT ' + QUOTENAME(name) + ';'
FROM sys.foreign_keys;

EXEC sp_executesql @sql;
GO

-- 2. Alle Tables
DECLARE @sql NVARCHAR(MAX) = N'';
SELECT @sql += N'DROP TABLE ' + name + ';' FROM sys.objects WHERE type = 'U';
EXEC sp_executesql @sql;
GO

------------------------
-- Tabellen erstellen --
------------------------
CREATE TABLE Member (
	Id INTEGER IDENTITY(1,1) PRIMARY KEY,
	UniqueId VARCHAR(255) NOT NULL,
	DisplayName VARCHAR(255),
	Location VARCHAR(255),
	EMail VARCHAR(255)
);
GO

-- CREATE UNIQUE INDEX IdxMemberUniqueId ON Member(UniqueId);
GO
CREATE TABLE Auction (
	Id INTEGER IDENTITY(1,1) PRIMARY KEY,
	StartPrice DECIMAL(8, 2),
	Title VARCHAR(255),
	Description VARCHAR(MAX),
	CurrentPrice DECIMAL(8, 2),
	StartDateTimeUtc DATETIME,
	EndDateTimeUtc DATETIME,
	CloseDateTimeUtc DATETIME,
	IsClosed BIT NOT NULL default(0),
	IsRunning BIT NOT NULL default(0),
	SellerId INTEGER,
	WinnerId INTEGER,
	ActiveBidId INTEGER,
	Image IMAGE
	Constraint FK_Seller Foreign Key (SellerId) REFERENCES Member(Id),
	Constraint FK_Winner Foreign Key (WinnerId) REFERENCES Member(Id)
);
GO

CREATE TABLE Bid (
	Id INTEGER IDENTITY(1,1) PRIMARY KEY,
	ReceivedOnUtc DATETIME,
	TransactionId VARCHAR(255),
	Amount DECIMAL (8, 2),
	Accepted BIT,
	AuctionId Integer,
	BidderId Integer,
	Constraint FK_Bidder Foreign Key (BidderId) REFERENCES Member(Id),
	Constraint FK_Auction Foreign Key (AuctionId) REFERENCES Auction(Id)
);
GO

ALTER TABLE Auction ADD
	CONSTRAINT FK_ActiveBid Foreign Key (ActiveBidId) REFERENCES Bid(Id);
GO
