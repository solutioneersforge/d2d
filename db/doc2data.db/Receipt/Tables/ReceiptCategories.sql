CREATE TABLE [Receipt].[ReceiptCategories] (
    [ReceiptCategoryId] INT              IDENTITY (1, 1) NOT NULL,
    [ReceiptID]         UNIQUEIDENTIFIER NOT NULL,
    [CategoryId]        INT              NOT NULL,
    [Amount]            DECIMAL (18, 2)  NULL,
    CONSTRAINT [PK_ReceiptCategories] PRIMARY KEY CLUSTERED ([ReceiptCategoryId] ASC),
    CONSTRAINT [FK_ReceiptCategories_Receipts] FOREIGN KEY ([ReceiptID]) REFERENCES [Receipt].[Receipts] ([ReceiptId])
);

