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
ALTER TABLE [dbo].[VIPDiscount] 
ADD [LastEditDate] DateTime NULL CONSTRAINT [DF_VIPDiscount_LastEditDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[VIPDiscount] 
ADD [CreationDate] DateTime NULL CONSTRAINT [DF_VIPDiscount_CreationDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VIPDiscount_Tombstone]')) 
BEGIN 
CREATE TABLE [dbo].[VIPDiscount_Tombstone]( 
    [VIPDiscountId] Int NOT NULL,
    [DeletionDate] DateTime NULL
)END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[VIPDiscount_Tombstone] ADD CONSTRAINT [PKDEL_VIPDiscount_Tombstone_VIPDiscountId]
   PRIMARY KEY CLUSTERED
    ([VIPDiscountId])
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VIPDiscount_DeletionTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[VIPDiscount_DeletionTrigger] 

GO
CREATE TRIGGER [dbo].[VIPDiscount_DeletionTrigger] 
    ON [VIPDiscount] 
    AFTER DELETE 
AS 
SET NOCOUNT ON 
UPDATE [dbo].[VIPDiscount_Tombstone] 
    SET [DeletionDate] = GETUTCDATE() 
    FROM deleted 
    WHERE deleted.[VIPDiscountId] = [VIPDiscount_Tombstone].[VIPDiscountId] 
IF @@ROWCOUNT = 0 
BEGIN 
    INSERT INTO [dbo].[VIPDiscount_Tombstone] 
    ([VIPDiscountId], DeletionDate)
    SELECT [VIPDiscountId], GETUTCDATE()
    FROM deleted 
END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VIPDiscount_UpdateTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[VIPDiscount_UpdateTrigger] 

GO
CREATE TRIGGER [dbo].[VIPDiscount_UpdateTrigger] 
    ON [dbo].[VIPDiscount] 
    AFTER UPDATE 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[VIPDiscount] 
    SET [LastEditDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[VIPDiscountId] = [VIPDiscount].[VIPDiscountId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VIPDiscount_InsertTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[VIPDiscount_InsertTrigger] 

GO
CREATE TRIGGER [dbo].[VIPDiscount_InsertTrigger] 
    ON [dbo].[VIPDiscount] 
    AFTER INSERT 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[VIPDiscount] 
    SET [CreationDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[VIPDiscountId] = [VIPDiscount].[VIPDiscountId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;
COMMIT TRANSACTION;
