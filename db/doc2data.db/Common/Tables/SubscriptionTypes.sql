CREATE TABLE [Common].[SubscriptionTypes] (
    [SubscriptionTypeId] INT            IDENTITY (1, 1) NOT NULL,
    [SubscriptionName]   NVARCHAR (50)  NOT NULL,
    [NumberOfDays]       INT            NOT NULL,
    [Description]        NVARCHAR (500) NULL,
    [IsActive]           BIT            CONSTRAINT [DF_SubscriptionTypes_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_SubscriptionTypes] PRIMARY KEY CLUSTERED ([SubscriptionTypeId] ASC)
);

