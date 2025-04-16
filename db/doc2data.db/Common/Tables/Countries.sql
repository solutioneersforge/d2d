CREATE TABLE [Common].[Countries] (
    [CountryId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (150) NOT NULL,
    [Code]      NVARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED ([CountryId] ASC)
);

