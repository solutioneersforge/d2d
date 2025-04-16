CREATE TABLE [Authentication].[CompanyMembers] (
    [MemberID]  UNIQUEIDENTIFIER CONSTRAINT [DF_CompanyMembers_MemberID] DEFAULT (newid()) NOT NULL,
    [CompanyID] UNIQUEIDENTIFIER NOT NULL,
    [UserID]    UNIQUEIDENTIFIER NOT NULL,
    [RoleID]    UNIQUEIDENTIFIER NOT NULL,
    [CreatedAt] DATETIME         CONSTRAINT [DF__CompanyMe__Creat__7073AF84] DEFAULT (getdate()) NOT NULL,
    [IsActive]  BIT              CONSTRAINT [DF_CompanyMembers_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_CompanyMembers] PRIMARY KEY CLUSTERED ([MemberID] ASC),
    CONSTRAINT [FK_CompanyMembers_Companies] FOREIGN KEY ([CompanyID]) REFERENCES [Common].[Companies] ([CompanyID]),
    CONSTRAINT [FK_CompanyMembers_Roles] FOREIGN KEY ([RoleID]) REFERENCES [Authentication].[Roles] ([RoleID]),
    CONSTRAINT [FK_CompanyMembers_Users] FOREIGN KEY ([UserID]) REFERENCES [Authentication].[Users] ([UserId])
);

