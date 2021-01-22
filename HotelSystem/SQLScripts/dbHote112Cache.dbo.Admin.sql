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
ALTER TABLE [dbo].[Admin] 
ADD [LastEditDate] DateTime NULL CONSTRAINT [DF_Admin_LastEditDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[Admin] 
ADD [CreationDate] DateTime NULL CONSTRAINT [DF_Admin_CreationDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_Tombstone]')) 
BEGIN 
CREATE TABLE [dbo].[Admin_Tombstone]( 
    [AdminId] Int NOT NULL,
    [DeletionDate] DateTime NULL
)END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[Admin_Tombstone] ADD CONSTRAINT [PKDEL_Admin_Tombstone_AdminId]
   PRIMARY KEY CLUSTERED
    ([AdminId])
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_DeletionTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[Admin_DeletionTrigger] 

GO
CREATE TRIGGER [dbo].[Admin_DeletionTrigger] 
    ON [Admin] 
    AFTER DELETE 
AS 
SET NOCOUNT ON 
UPDATE [dbo].[Admin_Tombstone] 
    SET [DeletionDate] = GETUTCDATE() 
    FROM deleted 
    WHERE deleted.[AdminId] = [Admin_Tombstone].[AdminId] 
IF @@ROWCOUNT = 0 
BEGIN 
    INSERT INTO [dbo].[Admin_Tombstone] 
    ([AdminId], DeletionDate)
    SELECT [AdminId], GETUTCDATE()
    FROM deleted 
END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_UpdateTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[Admin_UpdateTrigger] 

GO
CREATE TRIGGER [dbo].[Admin_UpdateTrigger] 
    ON [dbo].[Admin] 
    AFTER UPDATE 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[Admin] 
    SET [LastEditDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[AdminId] = [Admin].[AdminId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_InsertTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[Admin_InsertTrigger] 

GO
CREATE TRIGGER [dbo].[Admin_InsertTrigger] 
    ON [dbo].[Admin] 
    AFTER INSERT 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[Admin] 
    SET [CreationDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[AdminId] = [Admin].[AdminId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;
COMMIT TRANSACTION;
