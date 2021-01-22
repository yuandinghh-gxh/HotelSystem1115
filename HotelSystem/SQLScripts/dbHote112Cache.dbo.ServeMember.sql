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
ALTER TABLE [dbo].[ServeMember] 
ADD [LastEditDate] DateTime NULL CONSTRAINT [DF_ServeMember_LastEditDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[ServeMember] 
ADD [CreationDate] DateTime NULL CONSTRAINT [DF_ServeMember_CreationDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ServeMember_Tombstone]')) 
BEGIN 
CREATE TABLE [dbo].[ServeMember_Tombstone]( 
    [ServeMemberId] Int NOT NULL,
    [DeletionDate] DateTime NULL
)END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[ServeMember_Tombstone] ADD CONSTRAINT [PKDEL_ServeMember_Tombstone_ServeMemberId]
   PRIMARY KEY CLUSTERED
    ([ServeMemberId])
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ServeMember_DeletionTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[ServeMember_DeletionTrigger] 

GO
CREATE TRIGGER [dbo].[ServeMember_DeletionTrigger] 
    ON [ServeMember] 
    AFTER DELETE 
AS 
SET NOCOUNT ON 
UPDATE [dbo].[ServeMember_Tombstone] 
    SET [DeletionDate] = GETUTCDATE() 
    FROM deleted 
    WHERE deleted.[ServeMemberId] = [ServeMember_Tombstone].[ServeMemberId] 
IF @@ROWCOUNT = 0 
BEGIN 
    INSERT INTO [dbo].[ServeMember_Tombstone] 
    ([ServeMemberId], DeletionDate)
    SELECT [ServeMemberId], GETUTCDATE()
    FROM deleted 
END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ServeMember_UpdateTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[ServeMember_UpdateTrigger] 

GO
CREATE TRIGGER [dbo].[ServeMember_UpdateTrigger] 
    ON [dbo].[ServeMember] 
    AFTER UPDATE 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[ServeMember] 
    SET [LastEditDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[ServeMemberId] = [ServeMember].[ServeMemberId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ServeMember_InsertTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[ServeMember_InsertTrigger] 

GO
CREATE TRIGGER [dbo].[ServeMember_InsertTrigger] 
    ON [dbo].[ServeMember] 
    AFTER INSERT 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[ServeMember] 
    SET [CreationDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[ServeMemberId] = [ServeMember].[ServeMemberId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;
COMMIT TRANSACTION;
