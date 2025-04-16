CREATE TABLE [Common].[Currencies] (
    [CurrencyId] INT           IDENTITY (1, 1) NOT NULL,
    [Name]       VARCHAR (50)  NOT NULL,
    [Code]       VARCHAR (50)  NOT NULL,
    [Symbol]     NVARCHAR (50) NULL,
    [IsActive]   BIT           CONSTRAINT [DF_Currencies_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Currencies] PRIMARY KEY CLUSTERED ([CurrencyId] ASC)
);

