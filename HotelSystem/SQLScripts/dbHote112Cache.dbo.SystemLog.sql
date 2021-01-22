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
ALTER TABLE [dbo].[SystemLog] 
ADD [LastEditDate] DateTime NULL CONSTRAINT [DF_SystemLog_LastEditDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[SystemLog] 
ADD [CreationDate] DateTime NULL CONSTRAINT [DF_SystemLog_CreationDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SystemLog_Tombstone]')) 
BEGIN 
CREATE TABLE [dbo].[SystemLog_Tombstone]( 
    [SystemLogId] Int NOT NULL,
    [DeletionDate] DateTime NULL
)END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[SystemLog_Tombstone] ADD CONSTRAINT [PKDEL_SystemLog_Tombstone_SystemLogId]
   PRIMARY KEY CLUSTERED
    ([SystemLogId])
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SystemLog_DeletionTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[SystemLog_DeletionTrigger] 

GO
CREATE TRIGGER [dbo].[SystemLog_DeletionTrigger] 
    ON [SystemLog] 
    AFTER DELETE 
AS 
SET NOCOUNT ON 
UPDATE [dbo].[SystemLog_Tombstone] 
    SET [DeletionDate] = GETUTCDATE() 
    FROM deleted 
    WHERE deleted.[SystemLogId] = [SystemLog_Tombstone].[SystemLogId] 
IF @@ROWCOUNT = 0 
BEGIN 
    INSERT INTO [dbo].[SystemLog_Tombstone] 
    ([SystemLogId], DeletionDate)
    SELECT [SystemLogId], GETUTCDATE()
    FROM deleted 
END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SystemLog_UpdateTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[SystemLog_UpdateTrigger] 

GO
CREATE TRIGGER [dbo].[SystemLog_UpdateTrigger] 
    ON [dbo].[SystemLog] 
    AFTER UPDATE 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[SystemLog] 
    SET [LastEditDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[SystemLogId] = [SystemLog].[SystemLogId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SystemLog_InsertTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[SystemLog_InsertTrigger] 

GO
CREATE TRIGGER [dbo].[SystemLog_InsertTrigger] 
    ON [dbo].[SystemLog] 
    AFTER INSERT 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[SystemLog] 
    SET [CreationDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[SystemLogId] = [SystemLog].[SystemLogId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;
COMMIT TRANSACTION;
