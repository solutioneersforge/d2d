CREATE TABLE [Common].[Companies] (
    [CompanyID]      UNIQUEIDENTIFIER CONSTRAINT [DF_Companies_CompanyID] DEFAULT (newid()) NOT NULL,
    [CompanyName]    NVARCHAR (255)   NOT NULL,
    [SubscriptionID] UNIQUEIDENTIFIER NOT NULL,
    [CreatedAt]      DATETIME         CONSTRAINT [DF__Companies__Creat__6ABAD62E] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Companies] PRIMARY KEY CLUSTERED ([CompanyID] ASC),
    CONSTRAINT [FK_Companies_Subscriptions] FOREIGN KEY ([SubscriptionID]) REFERENCES [Common].[Subscriptions] ([SubscriptionID]),
    CONSTRAINT [UQ__Companie__9BCE05DCC5D2C492] UNIQUE NONCLUSTERED ([CompanyName] ASC)
);

