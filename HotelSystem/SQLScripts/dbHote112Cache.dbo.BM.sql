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
ALTER TABLE [dbo].[BM] 
ADD [LastEditDate] DateTime NULL CONSTRAINT [DF_BM_LastEditDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[BM] 
ADD [CreationDate] DateTime NULL CONSTRAINT [DF_BM_CreationDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BM_Tombstone]')) 
BEGIN 
CREATE TABLE [dbo].[BM_Tombstone]( 
    [BMId] Int NOT NULL,
    [DeletionDate] DateTime NULL
)END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[BM_Tombstone] ADD CONSTRAINT [PKDEL_BM_Tombstone_BMId]
   PRIMARY KEY CLUSTERED
    ([BMId])
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BM_DeletionTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[BM_DeletionTrigger] 

GO
CREATE TRIGGER [dbo].[BM_DeletionTrigger] 
    ON [BM] 
    AFTER DELETE 
AS 
SET NOCOUNT ON 
UPDATE [dbo].[BM_Tombstone] 
    SET [DeletionDate] = GETUTCDATE() 
    FROM deleted 
    WHERE deleted.[BMId] = [BM_Tombstone].[BMId] 
IF @@ROWCOUNT = 0 
BEGIN 
    INSERT INTO [dbo].[BM_Tombstone] 
    ([BMId], DeletionDate)
    SELECT [BMId], GETUTCDATE()
    FROM deleted 
END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BM_UpdateTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[BM_UpdateTrigger] 

GO
CREATE TRIGGER [dbo].[BM_UpdateTrigger] 
    ON [dbo].[BM] 
    AFTER UPDATE 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[BM] 
    SET [LastEditDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[BMId] = [BM].[BMId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BM_InsertTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[BM_InsertTrigger] 

GO
CREATE TRIGGER [dbo].[BM_InsertTrigger] 
    ON [dbo].[BM] 
    AFTER INSERT 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[BM] 
    SET [CreationDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[BMId] = [BM].[BMId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;
COMMIT TRANSACTION;
