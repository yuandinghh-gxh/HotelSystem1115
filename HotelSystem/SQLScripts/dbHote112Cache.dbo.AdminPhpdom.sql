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
ALTER TABLE [dbo].[AdminPhpdom] 
ADD [LastEditDate] DateTime NULL CONSTRAINT [DF_AdminPhpdom_LastEditDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[AdminPhpdom] 
ADD [CreationDate] DateTime NULL CONSTRAINT [DF_AdminPhpdom_CreationDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdminPhpdom_Tombstone]')) 
BEGIN 
CREATE TABLE [dbo].[AdminPhpdom_Tombstone]( 
    [AdminPhpdomId] Int NOT NULL,
    [DeletionDate] DateTime NULL
)END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[AdminPhpdom_Tombstone] ADD CONSTRAINT [PKDEL_AdminPhpdom_Tombstone_AdminPhpdomId]
   PRIMARY KEY CLUSTERED
    ([AdminPhpdomId])
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdminPhpdom_DeletionTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[AdminPhpdom_DeletionTrigger] 

GO
CREATE TRIGGER [dbo].[AdminPhpdom_DeletionTrigger] 
    ON [AdminPhpdom] 
    AFTER DELETE 
AS 
SET NOCOUNT ON 
UPDATE [dbo].[AdminPhpdom_Tombstone] 
    SET [DeletionDate] = GETUTCDATE() 
    FROM deleted 
    WHERE deleted.[AdminPhpdomId] = [AdminPhpdom_Tombstone].[AdminPhpdomId] 
IF @@ROWCOUNT = 0 
BEGIN 
    INSERT INTO [dbo].[AdminPhpdom_Tombstone] 
    ([AdminPhpdomId], DeletionDate)
    SELECT [AdminPhpdomId], GETUTCDATE()
    FROM deleted 
END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdminPhpdom_UpdateTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[AdminPhpdom_UpdateTrigger] 

GO
CREATE TRIGGER [dbo].[AdminPhpdom_UpdateTrigger] 
    ON [dbo].[AdminPhpdom] 
    AFTER UPDATE 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[AdminPhpdom] 
    SET [LastEditDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[AdminPhpdomId] = [AdminPhpdom].[AdminPhpdomId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdminPhpdom_InsertTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[AdminPhpdom_InsertTrigger] 

GO
CREATE TRIGGER [dbo].[AdminPhpdom_InsertTrigger] 
    ON [dbo].[AdminPhpdom] 
    AFTER INSERT 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[AdminPhpdom] 
    SET [CreationDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[AdminPhpdomId] = [AdminPhpdom].[AdminPhpdomId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;
COMMIT TRANSACTION;
