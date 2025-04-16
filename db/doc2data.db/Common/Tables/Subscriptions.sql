CREATE TABLE [Common].[Subscriptions] (
    [SubscriptionID]   UNIQUEIDENTIFIER CONSTRAINT [DF_Subscriptions_SubscriptionID] DEFAULT (newid()) NOT NULL,
    [SubscriptionName] NVARCHAR (100)   NOT NULL,
    [MaxAccounts]      INT              CONSTRAINT [DF__Subscript__MaxAc__6D9742D9] DEFAULT ((5)) NOT NULL,
    CONSTRAINT [PK_Subscriptions] PRIMARY KEY CLUSTERED ([SubscriptionID] ASC)
);

