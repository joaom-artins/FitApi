CREATE DATABASE invite COLLATE Latin1_General_100_CI_AS_SC_UTF8;
GO

USE fit;
GO

CREATE LOGIN fit WITH PASSWORD = 'XXEr7Yf%<B0nRI';
GO

CREATE USER fit FOR LOGIN fit;
GO

EXEC sp_addrolemember 'db_owner', 'invite';
GO
