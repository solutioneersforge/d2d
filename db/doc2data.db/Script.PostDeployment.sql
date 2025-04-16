-- Script.PostDeployment.sql
PRINT 'Starting post-deployment: Upserting data...'

---------------------------
-- 1. Authentication.Roles --
---------------------------
MERGE INTO [Authentication].Roles AS Target
USING (VALUES
    (N'ef7f58f4-1b74-4481-80fa-00bf07b6900d', N'Manager', 1),
    (N'49c9ebdf-a372-49c4-9005-4b0e1a424206', N'DataEntry', 1),
    (N'df1d3d29-fcd8-4396-8b91-6d7362a1b976', N'Approver', 1)
) AS Source (RoleID, RoleName, IsActive)
ON Target.RoleID = Source.RoleID
WHEN MATCHED THEN
    UPDATE SET 
        RoleName = Source.RoleName,
        IsActive = Source.IsActive
WHEN NOT MATCHED BY TARGET THEN
    INSERT (RoleID, RoleName, IsActive)
    VALUES (Source.RoleID, Source.RoleName, Source.IsActive);

-------------------------
-- 2. Common.Countries --
-------------------------
SET IDENTITY_INSERT Common.Countries ON; 

MERGE INTO Common.Countries AS Target
USING (VALUES
    (1, N'USA', N'USA')
) AS Source (CountryId, Name, Code)
ON Target.CountryId = Source.CountryId
WHEN MATCHED THEN
    UPDATE SET 
        Name = Source.Name,
        Code = Source.Code
WHEN NOT MATCHED BY TARGET THEN
    INSERT (CountryId, Name, Code)
    VALUES (Source.CountryId, Source.Name, Source.Code);

SET IDENTITY_INSERT Common.Countries OFF;

---------------------------
-- 3. Common.Currencies --
---------------------------
SET IDENTITY_INSERT Common.Currencies ON;

MERGE INTO Common.Currencies AS Target
USING (VALUES
    (1, N'USD', N'USD', N'$', 1)
) AS Source (CurrencyId, Name, Code, Symbol, IsActive)
ON Target.CurrencyId = Source.CurrencyId
WHEN MATCHED THEN
    UPDATE SET 
        Name = Source.Name,
        Code = Source.Code,
        Symbol = Source.Symbol,
        IsActive = Source.IsActive
WHEN NOT MATCHED BY TARGET THEN
    INSERT (CurrencyId, Name, Code, Symbol, IsActive)
    VALUES (Source.CurrencyId, Source.Name, Source.Code, Source.Symbol, Source.IsActive);

SET IDENTITY_INSERT Common.Currencies OFF;

----------------------------
-- 4. Common.PaymentTypes --
----------------------------
SET IDENTITY_INSERT Common.PaymentTypes ON; 

MERGE INTO Common.PaymentTypes AS Target
USING (VALUES
    (1, N'CASH'),
    (2, N'CARD'),
    (3, N'BANK TRANSFER'),
    (4, N'OTHER')
) AS Source (PaymentTypeId, PaymentType)
ON Target.PaymentTypeId = Source.PaymentTypeId
WHEN MATCHED THEN
    UPDATE SET PaymentType = Source.PaymentType
WHEN NOT MATCHED BY TARGET THEN
    INSERT (PaymentTypeId, PaymentType)
    VALUES (Source.PaymentTypeId, Source.PaymentType);

SET IDENTITY_INSERT Common.PaymentTypes OFF;

-----------------------
-- 5. Common.Status --
-----------------------
SET IDENTITY_INSERT Common.[Status] ON; 

MERGE INTO Common.[Status] AS Target
USING (VALUES
    (1, N'OPEN'),
    (2, N'APPROVED'),
    (3, N'REJECTED')
) AS Source (StatusId, Name)
ON Target.StatusId = Source.StatusId
WHEN MATCHED THEN
    UPDATE SET Name = Source.Name
WHEN NOT MATCHED BY TARGET THEN
    INSERT (StatusId, Name)
    VALUES (Source.StatusId, Source.Name);

SET IDENTITY_INSERT Common.[Status] OFF;
    
-----------------------------
-- 6. Common.Subscriptions --
-----------------------------
MERGE INTO Common.Subscriptions AS Target
USING (VALUES
    (N'05ce81e0-b393-472e-a03a-4a54f2750c03', N'Super', 1),
    (N'82e7294f-904a-4a81-ab4b-51ba0ecfb762', N'Premium', 4)
) AS Source (SubscriptionID, SubscriptionName, MaxAccounts)
ON Target.SubscriptionID = Source.SubscriptionID
WHEN MATCHED THEN
    UPDATE SET 
        SubscriptionName = Source.SubscriptionName,
        MaxAccounts = Source.MaxAccounts
WHEN NOT MATCHED BY TARGET THEN
    INSERT (SubscriptionID, SubscriptionName, MaxAccounts)
    VALUES (Source.SubscriptionID, Source.SubscriptionName, Source.MaxAccounts);

-------------------------------
-- 7. Common.UnitOfMeasures --
-------------------------------
SET IDENTITY_INSERT Common.UnitOfMeasures ON; 

MERGE INTO Common.UnitOfMeasures AS Target
USING (VALUES
    (1, N'Each', 1),
    (2, N'Dozen', 1),
    (3, N'Pair', 1),
    (4, N'Pack', 1),
    (5, N'Case', 1)
) AS Source (UnitOfMeasureId, Name, IsActive)
ON Target.UnitOfMeasureId = Source.UnitOfMeasureId
WHEN MATCHED THEN
    UPDATE SET 
        Name = Source.Name,
        IsActive = Source.IsActive
WHEN NOT MATCHED BY TARGET THEN
    INSERT (UnitOfMeasureId, Name, IsActive)
    VALUES (Source.UnitOfMeasureId, Source.Name, Source.IsActive);

SET IDENTITY_INSERT Common.UnitOfMeasures OFF; 

PRINT 'Post-deployment data upsert completed.';
GO