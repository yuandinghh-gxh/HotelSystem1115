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
ALTER TABLE [dbo].[Goods] 
ADD [LastEditDate] DateTime NULL CONSTRAINT [DF_Goods_LastEditDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[Goods] 
ADD [CreationDate] DateTime NULL CONSTRAINT [DF_Goods_CreationDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Goods_Tombstone]')) 
BEGIN 
CREATE TABLE [dbo].[Goods_Tombstone]( 
    [GoodsId] Int NOT NULL,
    [DeletionDate] DateTime NULL
)END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[Goods_Tombstone] ADD CONSTRAINT [PKDEL_Goods_Tombstone_GoodsId]
   PRIMARY KEY CLUSTERED
    ([GoodsId])
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Goods_DeletionTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[Goods_DeletionTrigger] 

GO
CREATE TRIGGER [dbo].[Goods_DeletionTrigger] 
    ON [Goods] 
    AFTER DELETE 
AS 
SET NOCOUNT ON 
UPDATE [dbo].[Goods_Tombstone] 
    SET [DeletionDate] = GETUTCDATE() 
    FROM deleted 
    WHERE deleted.[GoodsId] = [Goods_Tombstone].[GoodsId] 
IF @@ROWCOUNT = 0 
BEGIN 
    INSERT INTO [dbo].[Goods_Tombstone] 
    ([GoodsId], DeletionDate)
    SELECT [GoodsId], GETUTCDATE()
    FROM deleted 
END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Goods_UpdateTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[Goods_UpdateTrigger] 

GO
CREATE TRIGGER [dbo].[Goods_UpdateTrigger] 
    ON [dbo].[Goods] 
    AFTER UPDATE 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[Goods] 
    SET [LastEditDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[GoodsId] = [Goods].[GoodsId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Goods_InsertTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[Goods_InsertTrigger] 

GO
CREATE TRIGGER [dbo].[Goods_InsertTrigger] 
    ON [dbo].[Goods] 
    AFTER INSERT 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[Goods] 
    SET [CreationDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[GoodsId] = [Goods].[GoodsId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;
COMMIT TRANSACTION;
