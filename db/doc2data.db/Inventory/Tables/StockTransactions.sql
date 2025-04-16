CREATE TABLE [Inventory].[StockTransactions] (
    [TransactionId]          BIGINT           IDENTITY (1, 1) NOT NULL,
    [ItemId]                 INT              NOT NULL,
    [TransactionTypeId]      INT              NOT NULL,
    [TransactionDate]        DATETIME         NOT NULL,
    [Quantity]               INT              NOT NULL,
    [TransactionReferenceId] UNIQUEIDENTIFIER NOT NULL,
    [Notes]                  NVARCHAR (255)   NULL,
    CONSTRAINT [PK_StockTransactions] PRIMARY KEY CLUSTERED ([TransactionId] ASC),
    CONSTRAINT [FK_StockTransactions_Items] FOREIGN KEY ([ItemId]) REFERENCES [Inventory].[Items] ([ItemId]),
    CONSTRAINT [FK_StockTransactions_TransactionTypes] FOREIGN KEY ([TransactionTypeId]) REFERENCES [Inventory].[TransactionTypes] ([TransactionTypeId])
);

