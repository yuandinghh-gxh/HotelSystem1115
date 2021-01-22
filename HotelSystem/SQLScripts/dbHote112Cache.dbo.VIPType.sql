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
ALTER TABLE [dbo].[VIPType] 
ADD [LastEditDate] DateTime NULL CONSTRAINT [DF_VIPType_LastEditDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[VIPType] 
ADD [CreationDate] DateTime NULL CONSTRAINT [DF_VIPType_CreationDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VIPType_Tombstone]')) 
BEGIN 
CREATE TABLE [dbo].[VIPType_Tombstone]( 
    [VIPTypeId] Int NOT NULL,
    [DeletionDate] DateTime NULL
)END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[VIPType_Tombstone] ADD CONSTRAINT [PKDEL_VIPType_Tombstone_VIPTypeId]
   PRIMARY KEY CLUSTERED
    ([VIPTypeId])
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VIPType_DeletionTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[VIPType_DeletionTrigger] 

GO
CREATE TRIGGER [dbo].[VIPType_DeletionTrigger] 
    ON [VIPType] 
    AFTER DELETE 
AS 
SET NOCOUNT ON 
UPDATE [dbo].[VIPType_Tombstone] 
    SET [DeletionDate] = GETUTCDATE() 
    FROM deleted 
    WHERE deleted.[VIPTypeId] = [VIPType_Tombstone].[VIPTypeId] 
IF @@ROWCOUNT = 0 
BEGIN 
    INSERT INTO [dbo].[VIPType_Tombstone] 
    ([VIPTypeId], DeletionDate)
    SELECT [VIPTypeId], GETUTCDATE()
    FROM deleted 
END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VIPType_UpdateTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[VIPType_UpdateTrigger] 

GO
CREATE TRIGGER [dbo].[VIPType_UpdateTrigger] 
    ON [dbo].[VIPType] 
    AFTER UPDATE 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[VIPType] 
    SET [LastEditDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[VIPTypeId] = [VIPType].[VIPTypeId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VIPType_InsertTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[VIPType_InsertTrigger] 

GO
CREATE TRIGGER [dbo].[VIPType_InsertTrigger] 
    ON [dbo].[VIPType] 
    AFTER INSERT 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[VIPType] 
    SET [CreationDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[VIPTypeId] = [VIPType].[VIPTypeId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;
COMMIT TRANSACTION;
