CREATE TABLE [dbo].[UserLists] (
    [ListId]      INT            IDENTITY (1, 1) NOT NULL,
    [ListName]    VARCHAR (50)   NOT NULL,
    [RowCount]    INT            NOT NULL,
    [ColumnCount] INT            NOT NULL,
    [OwnerId]     NVARCHAR (128) NOT NULL,
    PRIMARY KEY CLUSTERED ([ListId] ASC),
    CONSTRAINT [FK_UserListsOwner_AspNetUsersId] FOREIGN KEY ([OwnerId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

