CREATE TABLE [Common].[UnitOfMeasures] (
    [UnitOfMeasureId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]            NVARCHAR (100) NOT NULL,
    [IsActive]        BIT            CONSTRAINT [DF_UnitOfMeasures_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_UnitOfMeasures] PRIMARY KEY CLUSTERED ([UnitOfMeasureId] ASC)
);

