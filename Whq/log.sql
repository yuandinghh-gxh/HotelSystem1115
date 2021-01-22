--
-- File generated with SQLiteStudio v3.1.1 on ÖÜËÄ 1ÔÂ 30 23:24:05 2020
--
-- Text encoding used: System
--
PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

-- Table: SystemLog
CREATE TABLE SystemLog (SystemLogId [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY, HandleTime [datetime] NOT NULL, operator [nvarchar] (20) NOT NULL, Abstract [nvarchar] (50) NOT NULL, opertorContent [nvarchar] (100) NOT NULL, CreationDate [datetime]);

COMMIT TRANSACTION;
PRAGMA foreign_keys = on;
