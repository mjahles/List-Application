CREATE TABLE [dbo].[ApprovedUsers] (
    [AppUserId] INT            IDENTITY (1, 1) NOT NULL,
    [UserId]    NVARCHAR (128) NOT NULL,
    [ListId]    INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([AppUserId] ASC),
    CONSTRAINT [FK_ApprovedUsers_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_ApprovedUsers_UserLists] FOREIGN KEY ([ListId]) REFERENCES [dbo].[UserLists] ([ListId])
);

