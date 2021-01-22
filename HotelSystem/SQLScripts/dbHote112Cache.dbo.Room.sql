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
ALTER TABLE [dbo].[Room] 
ADD [LastEditDate] DateTime NULL CONSTRAINT [DF_Room_LastEditDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[Room] 
ADD [CreationDate] DateTime NULL CONSTRAINT [DF_Room_CreationDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Room_Tombstone]')) 
BEGIN 
CREATE TABLE [dbo].[Room_Tombstone]( 
    [RoomId] Int NOT NULL,
    [DeletionDate] DateTime NULL
)END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[Room_Tombstone] ADD CONSTRAINT [PKDEL_Room_Tombstone_RoomId]
   PRIMARY KEY CLUSTERED
    ([RoomId])
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Room_DeletionTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[Room_DeletionTrigger] 

GO
CREATE TRIGGER [dbo].[Room_DeletionTrigger] 
    ON [Room] 
    AFTER DELETE 
AS 
SET NOCOUNT ON 
UPDATE [dbo].[Room_Tombstone] 
    SET [DeletionDate] = GETUTCDATE() 
    FROM deleted 
    WHERE deleted.[RoomId] = [Room_Tombstone].[RoomId] 
IF @@ROWCOUNT = 0 
BEGIN 
    INSERT INTO [dbo].[Room_Tombstone] 
    ([RoomId], DeletionDate)
    SELECT [RoomId], GETUTCDATE()
    FROM deleted 
END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Room_UpdateTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[Room_UpdateTrigger] 

GO
CREATE TRIGGER [dbo].[Room_UpdateTrigger] 
    ON [dbo].[Room] 
    AFTER UPDATE 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[Room] 
    SET [LastEditDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[RoomId] = [Room].[RoomId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Room_InsertTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[Room_InsertTrigger] 

GO
CREATE TRIGGER [dbo].[Room_InsertTrigger] 
    ON [dbo].[Room] 
    AFTER INSERT 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[Room] 
    SET [CreationDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[RoomId] = [Room].[RoomId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;
COMMIT TRANSACTION;
