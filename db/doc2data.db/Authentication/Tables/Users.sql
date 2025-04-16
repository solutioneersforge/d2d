CREATE TABLE [Authentication].[Users] (
    [UserId]              UNIQUEIDENTIFIER CONSTRAINT [DF_Users_UserId] DEFAULT (newid()) NOT NULL,
    [Email]               NVARCHAR (255)   NOT NULL,
    [FirstName]           NVARCHAR (50)    NOT NULL,
    [LastName]            NVARCHAR (50)    NULL,
    [PasswordHash]        VARBINARY (MAX)  NOT NULL,
    [CreatedAt]           DATETIME         NOT NULL,
    [PasswordSalt]        VARBINARY (MAX)  NULL,
    [IsEmailConfirmed]    BIT              CONSTRAINT [DF_Users_IsEmailConfirmed] DEFAULT ((0)) NOT NULL,
    [IsTwoFactorEnabled]  BIT              CONSTRAINT [DF_Users_IsTwoFactorEnabled] DEFAULT ((0)) NOT NULL,
    [FailedLoginAttempts] INT              CONSTRAINT [DF_Users_FailedLoginAttempts] DEFAULT ((0)) NULL,
    [LockoutEnd]          DATETIME         NULL,
    [UpdatedAt]           DATETIME         NULL,
    [LastLoginAt]         DATETIME         NULL,
    [IsActive]            BIT              CONSTRAINT [DF_Users_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Users_1] PRIMARY KEY CLUSTERED ([UserId] ASC)
);

