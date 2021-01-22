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
ALTER TABLE [dbo].[Sellperson] 
ADD [LastEditDate] DateTime NULL CONSTRAINT [DF_Sellperson_LastEditDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[Sellperson] 
ADD [CreationDate] DateTime NULL CONSTRAINT [DF_Sellperson_CreationDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sellperson_Tombstone]')) 
BEGIN 
CREATE TABLE [dbo].[Sellperson_Tombstone]( 
    [SellpersonId] Int NOT NULL,
    [DeletionDate] DateTime NULL
)END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[Sellperson_Tombstone] ADD CONSTRAINT [PKDEL_Sellperson_Tombstone_SellpersonId]
   PRIMARY KEY CLUSTERED
    ([SellpersonId])
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sellperson_DeletionTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[Sellperson_DeletionTrigger] 

GO
CREATE TRIGGER [dbo].[Sellperson_DeletionTrigger] 
    ON [Sellperson] 
    AFTER DELETE 
AS 
SET NOCOUNT ON 
UPDATE [dbo].[Sellperson_Tombstone] 
    SET [DeletionDate] = GETUTCDATE() 
    FROM deleted 
    WHERE deleted.[SellpersonId] = [Sellperson_Tombstone].[SellpersonId] 
IF @@ROWCOUNT = 0 
BEGIN 
    INSERT INTO [dbo].[Sellperson_Tombstone] 
    ([SellpersonId], DeletionDate)
    SELECT [SellpersonId], GETUTCDATE()
    FROM deleted 
END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sellperson_UpdateTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[Sellperson_UpdateTrigger] 

GO
CREATE TRIGGER [dbo].[Sellperson_UpdateTrigger] 
    ON [dbo].[Sellperson] 
    AFTER UPDATE 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[Sellperson] 
    SET [LastEditDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[SellpersonId] = [Sellperson].[SellpersonId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sellperson_InsertTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[Sellperson_InsertTrigger] 

GO
CREATE TRIGGER [dbo].[Sellperson_InsertTrigger] 
    ON [dbo].[Sellperson] 
    AFTER INSERT 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[Sellperson] 
    SET [CreationDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[SellpersonId] = [Sellperson].[SellpersonId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;
COMMIT TRANSACTION;
