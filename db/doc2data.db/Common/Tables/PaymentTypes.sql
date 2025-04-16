CREATE TABLE [Common].[PaymentTypes] (
    [PaymentTypeId] INT           IDENTITY (1, 1) NOT NULL,
    [PaymentType]   NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_PaymentTypes] PRIMARY KEY CLUSTERED ([PaymentTypeId] ASC)
);

