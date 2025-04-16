CREATE TABLE [Receipt].[ReceiptItems] (
    [ReceiptItemID]   UNIQUEIDENTIFIER CONSTRAINT [DF_ReceiptItems_ReceiptItemID] DEFAULT (newid()) NOT NULL,
    [ReceiptId]       UNIQUEIDENTIFIER NOT NULL,
    [ItemDescription] NVARCHAR (250)   NOT NULL,
    [UnitOfMeasureId] INT              NULL,
    [SubCategoryId]   INT              NULL,
    [Quantity]        DECIMAL (18, 2)  NOT NULL,
    [UnitPrice]       DECIMAL (18, 2)  NOT NULL,
    [Discount]        DECIMAL (18, 2)  NOT NULL,
    [SubTotal]        DECIMAL (18, 2)  NOT NULL,
    CONSTRAINT [PK_ReceiptItems_1] PRIMARY KEY CLUSTERED ([ReceiptItemID] ASC),
    CONSTRAINT [FK_ReceiptItems_ExpenseSubCategories] FOREIGN KEY ([SubCategoryId]) REFERENCES [Common].[ExpenseSubCategories] ([SubCategoryId]),
    CONSTRAINT [FK_ReceiptItems_Receipts] FOREIGN KEY ([ReceiptId]) REFERENCES [Receipt].[Receipts] ([ReceiptId]),
    CONSTRAINT [FK_ReceiptItems_UnitOfMeasures] FOREIGN KEY ([UnitOfMeasureId]) REFERENCES [Common].[UnitOfMeasures] ([UnitOfMeasureId])
);

