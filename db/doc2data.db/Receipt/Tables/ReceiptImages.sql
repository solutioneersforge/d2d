CREATE TABLE [Receipt].[ReceiptImages] (
    [ReceiptImageID]   UNIQUEIDENTIFIER CONSTRAINT [DF_ReceiptImages_ReceiptImageID] DEFAULT (newid()) NOT NULL,
    [ReceiptId]        UNIQUEIDENTIFIER NOT NULL,
    [OriginalFileName] NVARCHAR (150)   NOT NULL,
    [ImagePath]        NVARCHAR (MAX)   NOT NULL,
    [UploadedDateTime] DATETIME         NOT NULL,
    [IsDelete]         BIT              CONSTRAINT [DF_ReceiptImages_IsDelete] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ReceiptImages] PRIMARY KEY CLUSTERED ([ReceiptImageID] ASC),
    CONSTRAINT [FK_ReceiptImages_Receipts] FOREIGN KEY ([ReceiptId]) REFERENCES [Receipt].[Receipts] ([ReceiptId])
);

