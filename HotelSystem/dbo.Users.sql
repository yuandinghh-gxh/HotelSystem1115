CREATE TABLE [dbo].[Users] (
    [UserId]       INT           IDENTITY (1, 1) NOT NULL,
    [UserName]     NVARCHAR (20) NULL,
    [LoginName]    NVARCHAR (50) NULL,
    [PassWord]     NVARCHAR (32) NULL,
    [IfPwd]        NVARCHAR (2)  NULL,
    [AdminId]      INT           NULL,
    [State]        NVARCHAR (50) NULL,
    [XH]           NCHAR (2)     NULL,
    [LastEditDate] DATETIME      CONSTRAINT [DF_Users_LastEditDate] DEFAULT (getutcdate()) NULL,
    [CreationDate] DATETIME      CONSTRAINT [DF_Users_CreationDate] DEFAULT (getutcdate()) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserId] ASC)
);


GO
CREATE TRIGGER [dbo].[Users_UpdateTrigger] 
    ON [dbo].[Users] 
    AFTER UPDATE 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[Users] 
    SET [LastEditDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[UserId] = [Users].[UserId] 
END;
GO
CREATE TRIGGER [dbo].[Users_InsertTrigger] 
    ON [dbo].[Users] 
    AFTER INSERT 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[Users] 
    SET [CreationDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[UserId] = [Users].[UserId] 
END;