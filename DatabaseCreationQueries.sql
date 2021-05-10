CREATE TABLE [dbo].[ApprovedUsers]
(
	[AppUserId] INT IDENTITY PRIMARY KEY,
	[UserId] NVARCHAR(128) NOT NULL,
	[ListId] INT NOT NULL
);

CREATE TABLE [dbo].[UserLists]
(
	[ListId] INT IDENTITY PRIMARY KEY,
	[ListName] VARCHAR(50) NOT NULL,
    [RowCount] INT NOT NULL,
    [ColumnCount] INT NOT NULL,
	[OwnerId] NVARCHAR(128) NOT NULL
);

CREATE TABLE [dbo].[ListInfo]
(
	[InfoId] INT IDENTITY PRIMARY KEY,
	[ColumnName] VARCHAR(25),
	[ColumnData] VARCHAR(50),
    [RowNum] INT NOT NULL,
    [ColumnNum] INT NOT NULL,
    [ListId] INT NOT NULL,
    [IsChecked]  BIT DEFAULT ((0)) NOT NULL,
    CONSTRAINT [FK_ListInfo_UserLists] FOREIGN KEY ([ListId]) REFERENCES [UserLists]([ListId])
);

ALTER TABLE ApprovedUsers
ADD CONSTRAINT FK_AppUserToAspNetUsersId
FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id);

ALTER TABLE ApprovedUsers
ADD CONSTRAINT FK_AppListToUserListsList
FOREIGN KEY (ListId) REFERENCES UserLists(ListId);

ALTER TABLE UserLists
ADD CONSTRAINT FK_UserListsOwnerToAspNetUsersId
FOREIGN KEY (OwnerId) REFERENCES AspNetUsers(Id);

ALTER TABLE ListInfo
ADD CONSTRAINT FK_ListInfoListToUserListsList
FOREIGN KEY (ListId) REFERENCES UserLists(OwnerId);