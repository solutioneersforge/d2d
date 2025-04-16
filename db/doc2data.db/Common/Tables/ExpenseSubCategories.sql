CREATE TABLE [Common].[ExpenseSubCategories] (
    [SubCategoryId]   INT            IDENTITY (1, 1) NOT NULL,
    [CategoryId]      INT            NOT NULL,
    [SubCategoryName] NVARCHAR (100) NOT NULL,
    [IsActive]        BIT            CONSTRAINT [DF_ExpenseSubCategories_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_ExpenseSubCategories] PRIMARY KEY CLUSTERED ([SubCategoryId] ASC),
    CONSTRAINT [FK_ExpenseSubCategories_ExpenseCategories] FOREIGN KEY ([CategoryId]) REFERENCES [Common].[ExpenseCategories] ([CategoryId])
);

