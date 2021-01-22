/****
此 SQL 脚本由“配置数据同步”对话框生成。
此脚本包含在服务器数据库上创建更改跟踪列、已删除
项表和触发器的语句。这些数据库对象对于同步服务在
客户端和服务器数据库之间进行成功同步是必需的。
有关详细信息，请参阅帮助中的“如何: 配置数据库
服务器进行同步”主题。

****/


IF @@TRANCOUNT > 0
set ANSI_NULLS ON 
set QUOTED_IDENTIFIER ON 

GO
BEGIN TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[Client] 
ADD [LastEditDate] DateTime NULL CONSTRAINT [DF_Client_LastEditDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[Client] 
ADD [CreationDate] DateTime NULL CONSTRAINT [DF_Client_CreationDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Client_Tombstone]')) 
BEGIN 
CREATE TABLE [dbo].[Client_Tombstone]( 
    [clientId] Int NOT NULL,
    [DeletionDate] DateTime NULL
)END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[Client_Tombstone] ADD CONSTRAINT [PKDEL_Client_Tombstone_clientId]
   PRIMARY KEY CLUSTERED
    ([clientId])
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Client_DeletionTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[Client_DeletionTrigger] 

GO
CREATE TRIGGER [dbo].[Client_DeletionTrigger] 
    ON [Client] 
    AFTER DELETE 
AS 
SET NOCOUNT ON 
UPDATE [dbo].[Client_Tombstone] 
    SET [DeletionDate] = GETUTCDATE() 
    FROM deleted 
    WHERE deleted.[clientId] = [Client_Tombstone].[clientId] 
IF @@ROWCOUNT = 0 
BEGIN 
    INSERT INTO [dbo].[Client_Tombstone] 
    ([clientId], DeletionDate)
    SELECT [clientId], GETUTCDATE()
    FROM deleted 
END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Client_UpdateTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[Client_UpdateTrigger] 

GO
CREATE TRIGGER [dbo].[Client_UpdateTrigger] 
    ON [dbo].[Client] 
    AFTER UPDATE 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[Client] 
    SET [LastEditDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[clientId] = [Client].[clientId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Client_InsertTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[Client_InsertTrigger] 

GO
CREATE TRIGGER [dbo].[Client_InsertTrigger] 
    ON [dbo].[Client] 
    AFTER INSERT 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[Client] 
    SET [CreationDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[clientId] = [Client].[clientId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;
COMMIT TRANSACTION;