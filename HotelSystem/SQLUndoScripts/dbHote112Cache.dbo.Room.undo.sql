/****
警告
  为了防止任何潜在的数据丢失问题，您应该在运行脚本之前
对其进行详细检查。

此 SQL 脚本是由“配置数据同步”对话框
生成的。此脚本补充了可用于创建跟踪更改所需的
必要数据库对象的脚本。此脚本
包含用于移除此类更改的语句。

有关更多信息，请参见帮助中的“如何: 配置数据库服务器进行同步”。
****/


IF @@TRANCOUNT > 0
set ANSI_NULLS ON 
set QUOTED_IDENTIFIER ON 

GO
BEGIN TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[Room] DROP CONSTRAINT [DF_Room_LastEditDate]
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[Room] DROP COLUMN [LastEditDate]
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[Room] DROP CONSTRAINT [DF_Room_CreationDate]
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[Room] DROP COLUMN [CreationDate]
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Room_Tombstone]') and TYPE = N'U') 
   DROP TABLE [dbo].[Room_Tombstone];


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Room_DeletionTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[Room_DeletionTrigger] 

GO


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Room_UpdateTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[Room_UpdateTrigger] 

GO


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Room_InsertTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[Room_InsertTrigger] 

GO
COMMIT TRANSACTION;