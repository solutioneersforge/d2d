﻿CREATE TABLE [Inventory].[TransactionTypes] (
    [TransactionTypeId] INT          IDENTITY (1, 1) NOT NULL,
    [Name]              VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_TransactionTypes] PRIMARY KEY CLUSTERED ([TransactionTypeId] ASC)
);

