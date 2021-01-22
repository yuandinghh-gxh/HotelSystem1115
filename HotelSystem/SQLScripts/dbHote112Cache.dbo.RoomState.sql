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
ALTER TABLE [dbo].[RoomState] 
ADD [LastEditDate] DateTime NULL CONSTRAINT [DF_RoomState_LastEditDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[RoomState] 
ADD [CreationDate] DateTime NULL CONSTRAINT [DF_RoomState_CreationDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomState_Tombstone]')) 
BEGIN 
CREATE TABLE [dbo].[RoomState_Tombstone]( 
    [RoomStateId] Int NOT NULL,
    [DeletionDate] DateTime NULL
)END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[RoomState_Tombstone] ADD CONSTRAINT [PKDEL_RoomState_Tombstone_RoomStateId]
   PRIMARY KEY CLUSTERED
    ([RoomStateId])
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomState_DeletionTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[RoomState_DeletionTrigger] 

GO
CREATE TRIGGER [dbo].[RoomState_DeletionTrigger] 
    ON [RoomState] 
    AFTER DELETE 
AS 
SET NOCOUNT ON 
UPDATE [dbo].[RoomState_Tombstone] 
    SET [DeletionDate] = GETUTCDATE() 
    FROM deleted 
    WHERE deleted.[RoomStateId] = [RoomState_Tombstone].[RoomStateId] 
IF @@ROWCOUNT = 0 
BEGIN 
    INSERT INTO [dbo].[RoomState_Tombstone] 
    ([RoomStateId], DeletionDate)
    SELECT [RoomStateId], GETUTCDATE()
    FROM deleted 
END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomState_UpdateTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[RoomState_UpdateTrigger] 

GO
CREATE TRIGGER [dbo].[RoomState_UpdateTrigger] 
    ON [dbo].[RoomState] 
    AFTER UPDATE 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[RoomState] 
    SET [LastEditDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[RoomStateId] = [RoomState].[RoomStateId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomState_InsertTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[RoomState_InsertTrigger] 

GO
CREATE TRIGGER [dbo].[RoomState_InsertTrigger] 
    ON [dbo].[RoomState] 
    AFTER INSERT 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[RoomState] 
    SET [CreationDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[RoomStateId] = [RoomState].[RoomStateId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;
COMMIT TRANSACTION;
