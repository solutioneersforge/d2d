CREATE TABLE [Common].[ExpenseCategories] (
    [CategoryId]   INT              IDENTITY (1, 1) NOT NULL,
    [ClientId]     INT              NULL,
    [UserId]       UNIQUEIDENTIFIER NULL,
    [CategoryName] NVARCHAR (100)   NOT NULL,
    [IsActive]     BIT              CONSTRAINT [DF_ExpenseCategories_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_ExpenseCategories] PRIMARY KEY CLUSTERED ([CategoryId] ASC),
    CONSTRAINT [FK_ExpenseCategories_Users] FOREIGN KEY ([UserId]) REFERENCES [Authentication].[Users] ([UserId])
);

