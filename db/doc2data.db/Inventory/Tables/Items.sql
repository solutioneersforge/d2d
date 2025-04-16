CREATE TABLE [Inventory].[Items] (
    [ItemId]      INT            IDENTITY (1, 1) NOT NULL,
    [ItemName]    NVARCHAR (255) NOT NULL,
    [Description] NVARCHAR (255) NULL,
    [IsActive]    BIT            NOT NULL,
    [CreatedOn]   DATETIME       NOT NULL,
    CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED ([ItemId] ASC)
);

