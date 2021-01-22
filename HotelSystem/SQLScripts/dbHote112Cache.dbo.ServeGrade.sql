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
ALTER TABLE [dbo].[ServeGrade] 
ADD [LastEditDate] DateTime NULL CONSTRAINT [DF_ServeGrade_LastEditDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[ServeGrade] 
ADD [CreationDate] DateTime NULL CONSTRAINT [DF_ServeGrade_CreationDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ServeGrade_Tombstone]')) 
BEGIN 
CREATE TABLE [dbo].[ServeGrade_Tombstone]( 
    [ServeGradeId] Int NOT NULL,
    [DeletionDate] DateTime NULL
)END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[ServeGrade_Tombstone] ADD CONSTRAINT [PKDEL_ServeGrade_Tombstone_ServeGradeId]
   PRIMARY KEY CLUSTERED
    ([ServeGradeId])
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ServeGrade_DeletionTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[ServeGrade_DeletionTrigger] 

GO
CREATE TRIGGER [dbo].[ServeGrade_DeletionTrigger] 
    ON [ServeGrade] 
    AFTER DELETE 
AS 
SET NOCOUNT ON 
UPDATE [dbo].[ServeGrade_Tombstone] 
    SET [DeletionDate] = GETUTCDATE() 
    FROM deleted 
    WHERE deleted.[ServeGradeId] = [ServeGrade_Tombstone].[ServeGradeId] 
IF @@ROWCOUNT = 0 
BEGIN 
    INSERT INTO [dbo].[ServeGrade_Tombstone] 
    ([ServeGradeId], DeletionDate)
    SELECT [ServeGradeId], GETUTCDATE()
    FROM deleted 
END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ServeGrade_UpdateTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[ServeGrade_UpdateTrigger] 

GO
CREATE TRIGGER [dbo].[ServeGrade_UpdateTrigger] 
    ON [dbo].[ServeGrade] 
    AFTER UPDATE 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[ServeGrade] 
    SET [LastEditDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[ServeGradeId] = [ServeGrade].[ServeGradeId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ServeGrade_InsertTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[ServeGrade_InsertTrigger] 

GO
CREATE TRIGGER [dbo].[ServeGrade_InsertTrigger] 
    ON [dbo].[ServeGrade] 
    AFTER INSERT 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[ServeGrade] 
    SET [CreationDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[ServeGradeId] = [ServeGrade].[ServeGradeId] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;
COMMIT TRANSACTION;
