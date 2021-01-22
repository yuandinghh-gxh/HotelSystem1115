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
ALTER TABLE [dbo].[GoodsClass] 
ADD [LastEditDate] DateTime NULL CONSTRAINT [DF_GoodsClass_LastEditDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[GoodsClass] 
ADD [CreationDate] DateTime NULL CONSTRAINT [DF_GoodsClass_CreationDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GoodsClass_Tombstone]')) 
BEGIN 
CREATE TABLE [dbo].[GoodsClass_Tombstone]( 
    [GoodsClassId] Int NOT NULL,
    [DeletionDate] DateTime NULL
)END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[GoodsClass_Tombstone] ADD CONSTRAINT [PKDEL_GoodsClass_Tombstone_GoodsClassId]
   PRIMARY KEY CLUSTERED
    ([GoodsClassId])
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GoodsClass_DeletionTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[GoodsClass_DeletionTrigger] 

GO
CREATE TRIGGER [dbo].[GoodsClass_DeletionTrigger] 
    ON [GoodsClass] 
    AFTER DELETE 
AS 
SET NOCOUNT ON 
UPDATE [dbo].[GoodsClass_Tombstone] 
    SET [DeletionDate] = GETUTCDATE() 
    FROM deleted 
    WHERE deleted.[GoodsClassId] = [GoodsClass_Tombstone].[GoodsClassId] 
IF @@ROWCOUNT = 0 
BEGIN 
    INSERT INTO [dbo].[GoodsClass_Tombstone] 
    ([GoodsClassId], DeletionDate)
    SELECT [GoodsClassId], GETUTCDATE()
    FROM deleted 
END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GoodsClass_UpdateTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[GoodsClass_UpdateTrigger] 

GO
CREATE TRIGGER [dbo].[GoodsClass_UpdateTrigger] 
    ON [dbo].[GoodsClass] 
    AFTER UPDATE 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[GoodsClass] 
    SET [LastEditDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[GoodsClassId] = [GoodsClass].[GoodsClassId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GoodsClass_InsertTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[GoodsClass_InsertTrigger] 

GO
CREATE TRIGGER [dbo].[GoodsClass_InsertTrigger] 
    ON [dbo].[GoodsClass] 
    AFTER INSERT 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[GoodsClass] 
    SET [CreationDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[GoodsClassId] = [GoodsClass].[GoodsClassId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;
COMMIT TRANSACTION;
