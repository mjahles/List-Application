CREATE TABLE [dbo].[ApprovedUsers]
(
	[AppUserId] INT IDENTITY PRIMARY KEY,
	[UserId] INT NOT NULL,
	[ListId] INT NOT NULL
);

CREATE TABLE [dbo].[UserLists]
(
	[ListId] INT IDENTITY PRIMARY KEY,
	[ListName] VARCHAR(50) NOT NULL,
	[OwnerId] INT NOT NULL
);

CREATE TABLE [dbo].[ListInfo]
(
	[InfoId] INT IDENTITY PRIMARY KEY,
	[ColumnName] VARCHAR(25) NOT NULL,
	[ColumnData] VARCHAR(50) NOT NULL,
    [RowId] INT NOT NULL,
    [ListId] INT NOT NULL
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