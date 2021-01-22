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
ALTER TABLE [dbo].[Destine] 
ADD [LastEditDate] DateTime NULL CONSTRAINT [DF_Destine_LastEditDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[Destine] 
ADD [CreationDate] DateTime NULL CONSTRAINT [DF_Destine_CreationDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Destine_Tombstone]')) 
BEGIN 
CREATE TABLE [dbo].[Destine_Tombstone]( 
    [DestineId] Int NOT NULL,
    [DeletionDate] DateTime NULL
)END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[Destine_Tombstone] ADD CONSTRAINT [PKDEL_Destine_Tombstone_DestineId]
   PRIMARY KEY CLUSTERED
    ([DestineId])
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Destine_DeletionTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[Destine_DeletionTrigger] 

GO
CREATE TRIGGER [dbo].[Destine_DeletionTrigger] 
    ON [Destine] 
    AFTER DELETE 
AS 
SET NOCOUNT ON 
UPDATE [dbo].[Destine_Tombstone] 
    SET [DeletionDate] = GETUTCDATE() 
    FROM deleted 
    WHERE deleted.[DestineId] = [Destine_Tombstone].[DestineId] 
IF @@ROWCOUNT = 0 
BEGIN 
    INSERT INTO [dbo].[Destine_Tombstone] 
    ([DestineId], DeletionDate)
    SELECT [DestineId], GETUTCDATE()
    FROM deleted 
END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Destine_UpdateTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[Destine_UpdateTrigger] 

GO
CREATE TRIGGER [dbo].[Destine_UpdateTrigger] 
    ON [dbo].[Destine] 
    AFTER UPDATE 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[Destine] 
    SET [LastEditDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[DestineId] = [Destine].[DestineId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Destine_InsertTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[Destine_InsertTrigger] 

GO
CREATE TRIGGER [dbo].[Destine_InsertTrigger] 
    ON [dbo].[Destine] 
    AFTER INSERT 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[Destine] 
    SET [CreationDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[DestineId] = [Destine].[DestineId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;
COMMIT TRANSACTION;
