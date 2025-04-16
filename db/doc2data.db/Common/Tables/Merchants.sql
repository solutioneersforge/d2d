CREATE TABLE [Common].[Merchants] (
    [MerchantId]      INT            IDENTITY (1, 1) NOT NULL,
    [Name]            NVARCHAR (150) NOT NULL,
    [Address]         NVARCHAR (255) NULL,
    [Phone]           NVARCHAR (50)  NULL,
    [Email]           NVARCHAR (50)  NULL,
    [CountryId]       INT            NOT NULL,
    [CreatedOn]       DATETIME       NOT NULL,
    [IsActive]        BIT            CONSTRAINT [DF_Merchants_IsActive] DEFAULT ((1)) NOT NULL,
    [Website]         NVARCHAR (250) NULL,
    [CompanyRegNo]    NVARCHAR (250) NULL,
    [TaxCompanyRegNo] NVARCHAR (250) NULL,
    CONSTRAINT [PK_Merchants] PRIMARY KEY CLUSTERED ([MerchantId] ASC)
);

