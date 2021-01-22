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
ALTER TABLE [dbo].[VIPGuest] 
ADD [LastEditDate] DateTime NULL CONSTRAINT [DF_VIPGuest_LastEditDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[VIPGuest] 
ADD [CreationDate] DateTime NULL CONSTRAINT [DF_VIPGuest_CreationDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VIPGuest_Tombstone]')) 
BEGIN 
CREATE TABLE [dbo].[VIPGuest_Tombstone]( 
    [VIPGuestId] Int NOT NULL,
    [DeletionDate] DateTime NULL
)END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[VIPGuest_Tombstone] ADD CONSTRAINT [PKDEL_VIPGuest_Tombstone_VIPGuestId]
   PRIMARY KEY CLUSTERED
    ([VIPGuestId])
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VIPGuest_DeletionTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[VIPGuest_DeletionTrigger] 

GO
CREATE TRIGGER [dbo].[VIPGuest_DeletionTrigger] 
    ON [VIPGuest] 
    AFTER DELETE 
AS 
SET NOCOUNT ON 
UPDATE [dbo].[VIPGuest_Tombstone] 
    SET [DeletionDate] = GETUTCDATE() 
    FROM deleted 
    WHERE deleted.[VIPGuestId] = [VIPGuest_Tombstone].[VIPGuestId] 
IF @@ROWCOUNT = 0 
BEGIN 
    INSERT INTO [dbo].[VIPGuest_Tombstone] 
    ([VIPGuestId], DeletionDate)
    SELECT [VIPGuestId], GETUTCDATE()
    FROM deleted 
END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VIPGuest_UpdateTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[VIPGuest_UpdateTrigger] 

GO
CREATE TRIGGER [dbo].[VIPGuest_UpdateTrigger] 
    ON [dbo].[VIPGuest] 
    AFTER UPDATE 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[VIPGuest] 
    SET [LastEditDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[VIPGuestId] = [VIPGuest].[VIPGuestId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VIPGuest_InsertTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[VIPGuest_InsertTrigger] 

GO
CREATE TRIGGER [dbo].[VIPGuest_InsertTrigger] 
    ON [dbo].[VIPGuest] 
    AFTER INSERT 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[VIPGuest] 
    SET [CreationDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[VIPGuestId] = [VIPGuest].[VIPGuestId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;
COMMIT TRANSACTION;
