CREATE DATABASE fit COLLATE Latin1_General_100_CI_AS_SC_UTF8;
GO

USE fit;
GO

CREATE LOGIN fit WITH PASSWORD = 'XXEr7Yf%<B0nRI';
GO

CREATE USER fit FOR LOGIN fit;
GO

ALTER ROLE db_owner ADD MEMBER fit;
GO