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
ALTER TABLE [dbo].[GoodsSell] 
ADD [LastEditDate] DateTime NULL CONSTRAINT [DF_GoodsSell_LastEditDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[GoodsSell] 
ADD [CreationDate] DateTime NULL CONSTRAINT [DF_GoodsSell_CreationDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GoodsSell_Tombstone]')) 
BEGIN 
CREATE TABLE [dbo].[GoodsSell_Tombstone]( 
    [GoodsSellId] Int NOT NULL,
    [DeletionDate] DateTime NULL
)END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[GoodsSell_Tombstone] ADD CONSTRAINT [PKDEL_GoodsSell_Tombstone_GoodsSellId]
   PRIMARY KEY CLUSTERED
    ([GoodsSellId])
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GoodsSell_DeletionTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[GoodsSell_DeletionTrigger] 

GO
CREATE TRIGGER [dbo].[GoodsSell_DeletionTrigger] 
    ON [GoodsSell] 
    AFTER DELETE 
AS 
SET NOCOUNT ON 
UPDATE [dbo].[GoodsSell_Tombstone] 
    SET [DeletionDate] = GETUTCDATE() 
    FROM deleted 
    WHERE deleted.[GoodsSellId] = [GoodsSell_Tombstone].[GoodsSellId] 
IF @@ROWCOUNT = 0 
BEGIN 
    INSERT INTO [dbo].[GoodsSell_Tombstone] 
    ([GoodsSellId], DeletionDate)
    SELECT [GoodsSellId], GETUTCDATE()
    FROM deleted 
END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GoodsSell_UpdateTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[GoodsSell_UpdateTrigger] 

GO
CREATE TRIGGER [dbo].[GoodsSell_UpdateTrigger] 
    ON [dbo].[GoodsSell] 
    AFTER UPDATE 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[GoodsSell] 
    SET [LastEditDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[GoodsSellId] = [GoodsSell].[GoodsSellId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GoodsSell_InsertTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[GoodsSell_InsertTrigger] 

GO
CREATE TRIGGER [dbo].[GoodsSell_InsertTrigger] 
    ON [dbo].[GoodsSell] 
    AFTER INSERT 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[GoodsSell] 
    SET [CreationDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[GoodsSellId] = [GoodsSell].[GoodsSellId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;
COMMIT TRANSACTION;
