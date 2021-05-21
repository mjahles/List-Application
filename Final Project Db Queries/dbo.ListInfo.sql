CREATE TABLE [dbo].[ListInfo] (
    [InfoId]     INT          IDENTITY (1, 1) NOT NULL,
    [ColumnData] VARCHAR (50) NULL,
    [RowNum]     INT          NOT NULL,
    [ColumnNum]  INT          NOT NULL,
    [ListId]     INT          NOT NULL,
    [IsChecked]  BIT          DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([InfoId] ASC),
    CONSTRAINT [FK_ListInfo_UserLists] FOREIGN KEY ([ListId]) REFERENCES [dbo].[UserLists] ([ListId])
);

