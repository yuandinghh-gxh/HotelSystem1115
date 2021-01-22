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
ALTER TABLE [dbo].[RoomType] 
ADD [LastEditDate] DateTime NULL CONSTRAINT [DF_RoomType_LastEditDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[RoomType] 
ADD [CreationDate] DateTime NULL CONSTRAINT [DF_RoomType_CreationDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomType_Tombstone]')) 
BEGIN 
CREATE TABLE [dbo].[RoomType_Tombstone]( 
    [RoomTypeId] Int NOT NULL,
    [DeletionDate] DateTime NULL
)END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[RoomType_Tombstone] ADD CONSTRAINT [PKDEL_RoomType_Tombstone_RoomTypeId]
   PRIMARY KEY CLUSTERED
    ([RoomTypeId])
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomType_DeletionTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[RoomType_DeletionTrigger] 

GO
CREATE TRIGGER [dbo].[RoomType_DeletionTrigger] 
    ON [RoomType] 
    AFTER DELETE 
AS 
SET NOCOUNT ON 
UPDATE [dbo].[RoomType_Tombstone] 
    SET [DeletionDate] = GETUTCDATE() 
    FROM deleted 
    WHERE deleted.[RoomTypeId] = [RoomType_Tombstone].[RoomTypeId] 
IF @@ROWCOUNT = 0 
BEGIN 
    INSERT INTO [dbo].[RoomType_Tombstone] 
    ([RoomTypeId], DeletionDate)
    SELECT [RoomTypeId], GETUTCDATE()
    FROM deleted 
END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomType_UpdateTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[RoomType_UpdateTrigger] 

GO
CREATE TRIGGER [dbo].[RoomType_UpdateTrigger] 
    ON [dbo].[RoomType] 
    AFTER UPDATE 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[RoomType] 
    SET [LastEditDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[RoomTypeId] = [RoomType].[RoomTypeId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomType_InsertTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[RoomType_InsertTrigger] 

GO
CREATE TRIGGER [dbo].[RoomType_InsertTrigger] 
    ON [dbo].[RoomType] 
    AFTER INSERT 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[RoomType] 
    SET [CreationDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[RoomTypeId] = [RoomType].[RoomTypeId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;
COMMIT TRANSACTION;
