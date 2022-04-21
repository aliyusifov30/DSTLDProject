IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220228191243_AppUserTableCreated')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220228191243_AppUserTableCreated')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        [Discriminator] nvarchar(max) NOT NULL,
        [FullName] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220228191243_AppUserTableCreated')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220228191243_AppUserTableCreated')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220228191243_AppUserTableCreated')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220228191243_AppUserTableCreated')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220228191243_AppUserTableCreated')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220228191243_AppUserTableCreated')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220228191243_AppUserTableCreated')
BEGIN
    CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220228191243_AppUserTableCreated')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220228191243_AppUserTableCreated')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220228191243_AppUserTableCreated')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220228191243_AppUserTableCreated')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220228191243_AppUserTableCreated')
BEGIN
    CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220228191243_AppUserTableCreated')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220228191243_AppUserTableCreated', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220228193300_IsAdminAddedToAppUser')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [IsAdmin] bit NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220228193300_IsAdminAddedToAppUser')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220228193300_IsAdminAddedToAppUser', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301231632_SettingTableCreated')
BEGIN
    CREATE TABLE [Settings] (
        [Id] int NOT NULL IDENTITY,
        [Key] nvarchar(100) NOT NULL,
        [Value] nvarchar(100) NOT NULL,
        CONSTRAINT [PK_Settings] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301231632_SettingTableCreated')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220301231632_SettingTableCreated', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220302162443_ShopOnInstagramTableCreated')
BEGIN
    CREATE TABLE [ShopOnInstagrams] (
        [Id] int NOT NULL IDENTITY,
        [Url] nvarchar(max) NULL,
        [Image] nvarchar(max) NULL,
        [CreateAt] datetime2 NOT NULL,
        [Desc] nvarchar(max) NULL,
        CONSTRAINT [PK_ShopOnInstagrams] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220302162443_ShopOnInstagramTableCreated')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220302162443_ShopOnInstagramTableCreated', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220302190231_SosialMediaTableCreated')
BEGIN
    CREATE TABLE [SosialMedias] (
        [Id] int NOT NULL IDENTITY,
        [Icon] nvarchar(100) NOT NULL,
        [Url] nvarchar(100) NOT NULL,
        CONSTRAINT [PK_SosialMedias] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220302190231_SosialMediaTableCreated')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220302190231_SosialMediaTableCreated', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220302230610_Category,Gender,GenderCategory-TableCreated')
BEGIN
    CREATE TABLE [Categories] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL,
        [ModifiedAt] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        [Name] nvarchar(100) NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220302230610_Category,Gender,GenderCategory-TableCreated')
BEGIN
    CREATE TABLE [Genders] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL,
        [ModifiedAt] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        [Name] nvarchar(50) NULL,
        CONSTRAINT [PK_Genders] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220302230610_Category,Gender,GenderCategory-TableCreated')
BEGIN
    CREATE TABLE [CategoryGenders] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL,
        [ModifiedAt] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        [GenderId] int NOT NULL,
        [CategoryId] int NOT NULL,
        CONSTRAINT [PK_CategoryGenders] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_CategoryGenders_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_CategoryGenders_Genders_GenderId] FOREIGN KEY ([GenderId]) REFERENCES [Genders] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220302230610_Category,Gender,GenderCategory-TableCreated')
BEGIN
    CREATE INDEX [IX_CategoryGenders_CategoryId] ON [CategoryGenders] ([CategoryId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220302230610_Category,Gender,GenderCategory-TableCreated')
BEGIN
    CREATE INDEX [IX_CategoryGenders_GenderId] ON [CategoryGenders] ([GenderId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220302230610_Category,Gender,GenderCategory-TableCreated')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220302230610_Category,Gender,GenderCategory-TableCreated', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220306213031_DetailTableDeletedAndAnotherTableAddedAgain')
BEGIN
    ALTER TABLE [SosialMedias] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220306213031_DetailTableDeletedAndAnotherTableAddedAgain')
BEGIN
    ALTER TABLE [SosialMedias] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220306213031_DetailTableDeletedAndAnotherTableAddedAgain')
BEGIN
    ALTER TABLE [SosialMedias] ADD [ModifiedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220306213031_DetailTableDeletedAndAnotherTableAddedAgain')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ShopOnInstagrams]') AND [c].[name] = N'Url');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [ShopOnInstagrams] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [ShopOnInstagrams] ALTER COLUMN [Url] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220306213031_DetailTableDeletedAndAnotherTableAddedAgain')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ShopOnInstagrams]') AND [c].[name] = N'Image');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [ShopOnInstagrams] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [ShopOnInstagrams] ALTER COLUMN [Image] nvarchar(100) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220306213031_DetailTableDeletedAndAnotherTableAddedAgain')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ShopOnInstagrams]') AND [c].[name] = N'Desc');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [ShopOnInstagrams] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [ShopOnInstagrams] ALTER COLUMN [Desc] nvarchar(500) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220306213031_DetailTableDeletedAndAnotherTableAddedAgain')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Genders]') AND [c].[name] = N'Name');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Genders] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Genders] ALTER COLUMN [Name] nvarchar(50) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220306213031_DetailTableDeletedAndAnotherTableAddedAgain')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Categories]') AND [c].[name] = N'Name');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Categories] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Categories] ALTER COLUMN [Name] nvarchar(100) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220306213031_DetailTableDeletedAndAnotherTableAddedAgain')
BEGIN
    CREATE TABLE [Colors] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL,
        [ModifiedAt] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        [Image] nvarchar(100) NULL,
        [Name] nvarchar(100) NOT NULL,
        CONSTRAINT [PK_Colors] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220306213031_DetailTableDeletedAndAnotherTableAddedAgain')
BEGIN
    CREATE TABLE [Products] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL,
        [ModifiedAt] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        [CategoryId] int NOT NULL,
        [Name] nvarchar(300) NOT NULL,
        [CostPrice] decimal(18,2) NOT NULL,
        [SalePrice] decimal(18,2) NOT NULL,
        [Discount] decimal(18,2) NOT NULL,
        [IsNew] bit NOT NULL,
        [Rating] int NOT NULL,
        [Desc] nvarchar(500) NULL,
        [Detail] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220306213031_DetailTableDeletedAndAnotherTableAddedAgain')
BEGIN
    CREATE TABLE [Sizes] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL,
        [ModifiedAt] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        [Name] nvarchar(25) NOT NULL,
        CONSTRAINT [PK_Sizes] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220306213031_DetailTableDeletedAndAnotherTableAddedAgain')
BEGIN
    CREATE TABLE [ProductColors] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL,
        [ModifiedAt] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        [ColorId] int NOT NULL,
        [ProductId] int NOT NULL,
        CONSTRAINT [PK_ProductColors] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProductColors_Colors_ColorId] FOREIGN KEY ([ColorId]) REFERENCES [Colors] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ProductColors_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220306213031_DetailTableDeletedAndAnotherTableAddedAgain')
BEGIN
    CREATE TABLE [ProductsImages] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL,
        [ModifiedAt] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        [Image] nvarchar(100) NULL,
        [ProductId] int NOT NULL,
        [PosterStatus] bit NULL,
        CONSTRAINT [PK_ProductsImages] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProductsImages_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220306213031_DetailTableDeletedAndAnotherTableAddedAgain')
BEGIN
    CREATE TABLE [ProductSizes] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL,
        [ModifiedAt] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        [SizeId] int NOT NULL,
        [ProductId] int NOT NULL,
        [SizeCount] int NOT NULL,
        CONSTRAINT [PK_ProductSizes] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProductSizes_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ProductSizes_Sizes_SizeId] FOREIGN KEY ([SizeId]) REFERENCES [Sizes] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220306213031_DetailTableDeletedAndAnotherTableAddedAgain')
BEGIN
    CREATE INDEX [IX_ProductColors_ColorId] ON [ProductColors] ([ColorId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220306213031_DetailTableDeletedAndAnotherTableAddedAgain')
BEGIN
    CREATE INDEX [IX_ProductColors_ProductId] ON [ProductColors] ([ProductId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220306213031_DetailTableDeletedAndAnotherTableAddedAgain')
BEGIN
    CREATE INDEX [IX_Products_CategoryId] ON [Products] ([CategoryId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220306213031_DetailTableDeletedAndAnotherTableAddedAgain')
BEGIN
    CREATE INDEX [IX_ProductsImages_ProductId] ON [ProductsImages] ([ProductId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220306213031_DetailTableDeletedAndAnotherTableAddedAgain')
BEGIN
    CREATE INDEX [IX_ProductSizes_ProductId] ON [ProductSizes] ([ProductId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220306213031_DetailTableDeletedAndAnotherTableAddedAgain')
BEGIN
    CREATE INDEX [IX_ProductSizes_SizeId] ON [ProductSizes] ([SizeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220306213031_DetailTableDeletedAndAnotherTableAddedAgain')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220306213031_DetailTableDeletedAndAnotherTableAddedAgain', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220307173423_GenderIdAddedToProduct')
BEGIN
    ALTER TABLE [Products] ADD [GenderId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220307173423_GenderIdAddedToProduct')
BEGIN
    CREATE INDEX [IX_Products_GenderId] ON [Products] ([GenderId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220307173423_GenderIdAddedToProduct')
BEGIN
    ALTER TABLE [Products] ADD CONSTRAINT [FK_Products_Genders_GenderId] FOREIGN KEY ([GenderId]) REFERENCES [Genders] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220307173423_GenderIdAddedToProduct')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220307173423_GenderIdAddedToProduct', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220307224356_BasketItemTableCreated')
BEGIN
    CREATE TABLE [BasketItems] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL,
        [ModifiedAt] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        [ProductId] int NOT NULL,
        [AppUserId] nvarchar(450) NULL,
        [Count] int NOT NULL,
        CONSTRAINT [PK_BasketItems] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_BasketItems_AspNetUsers_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220307224356_BasketItemTableCreated')
BEGIN
    CREATE INDEX [IX_BasketItems_AppUserId] ON [BasketItems] ([AppUserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220307224356_BasketItemTableCreated')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220307224356_BasketItemTableCreated', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220307224750_BasketItemUpdateFirstTime')
BEGIN
    CREATE INDEX [IX_BasketItems_ProductId] ON [BasketItems] ([ProductId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220307224750_BasketItemUpdateFirstTime')
BEGIN
    ALTER TABLE [BasketItems] ADD CONSTRAINT [FK_BasketItems_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220307224750_BasketItemUpdateFirstTime')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220307224750_BasketItemUpdateFirstTime', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220308205748_WishListItemsTableCreated')
BEGIN
    CREATE TABLE [WishListItems] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL,
        [ModifiedAt] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        [ProductId] int NOT NULL,
        [AppUserId] nvarchar(450) NULL,
        [Count] int NOT NULL,
        CONSTRAINT [PK_WishListItems] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_WishListItems_AspNetUsers_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_WishListItems_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220308205748_WishListItemsTableCreated')
BEGIN
    CREATE INDEX [IX_WishListItems_AppUserId] ON [WishListItems] ([AppUserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220308205748_WishListItemsTableCreated')
BEGIN
    CREATE INDEX [IX_WishListItems_ProductId] ON [WishListItems] ([ProductId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220308205748_WishListItemsTableCreated')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220308205748_WishListItemsTableCreated', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220309211712_OrderTableCreated')
BEGIN
    CREATE TABLE [Orders] (
        [Id] int NOT NULL IDENTITY,
        [AppUserId] nvarchar(450) NULL,
        [Email] nvarchar(100) NOT NULL,
        [FullName] nvarchar(50) NOT NULL,
        [Addresses] nvarchar(100) NOT NULL,
        [Aparment] nvarchar(100) NULL,
        [City] nvarchar(100) NOT NULL,
        [Country] nvarchar(100) NOT NULL,
        [State] nvarchar(100) NOT NULL,
        [ZipCode] int NOT NULL,
        [Phone] nvarchar(35) NOT NULL,
        [Status] int NOT NULL,
        [TotalAmount] decimal(18,2) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Orders_AspNetUsers_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220309211712_OrderTableCreated')
BEGIN
    CREATE TABLE [OrderItem] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL,
        [ModifiedAt] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        [OrderId] int NOT NULL,
        [ProductId] int NOT NULL,
        [Count] int NOT NULL,
        [ProdName] nvarchar(100) NULL,
        [SizeName] nvarchar(max) NULL,
        [CostPrice] decimal(18,2) NOT NULL,
        [SalePrice] decimal(18,2) NOT NULL,
        [Discount] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_OrderItem] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_OrderItem_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_OrderItem_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220309211712_OrderTableCreated')
BEGIN
    CREATE INDEX [IX_OrderItem_OrderId] ON [OrderItem] ([OrderId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220309211712_OrderTableCreated')
BEGIN
    CREATE INDEX [IX_OrderItem_ProductId] ON [OrderItem] ([ProductId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220309211712_OrderTableCreated')
BEGIN
    CREATE INDEX [IX_Orders_AppUserId] ON [Orders] ([AppUserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220309211712_OrderTableCreated')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220309211712_OrderTableCreated', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220309221129_ProductComemntTableCreaeted')
BEGIN
    CREATE TABLE [ProductComments] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL,
        [ProductId] int NOT NULL,
        [AppUserId] int NOT NULL,
        [Text] nvarchar(500) NOT NULL,
        [AppUserId1] nvarchar(450) NULL,
        CONSTRAINT [PK_ProductComments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProductComments_AspNetUsers_AppUserId1] FOREIGN KEY ([AppUserId1]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ProductComments_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220309221129_ProductComemntTableCreaeted')
BEGIN
    CREATE INDEX [IX_ProductComments_AppUserId1] ON [ProductComments] ([AppUserId1]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220309221129_ProductComemntTableCreaeted')
BEGIN
    CREATE INDEX [IX_ProductComments_ProductId] ON [ProductComments] ([ProductId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220309221129_ProductComemntTableCreaeted')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220309221129_ProductComemntTableCreaeted', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220309234852_EmailAndFullNameRemovedInOrderTable')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Orders]') AND [c].[name] = N'FullName');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Orders] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [Orders] ALTER COLUMN [FullName] nvarchar(50) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220309234852_EmailAndFullNameRemovedInOrderTable')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Orders]') AND [c].[name] = N'Email');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Orders] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [Orders] ALTER COLUMN [Email] nvarchar(100) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220309234852_EmailAndFullNameRemovedInOrderTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220309234852_EmailAndFullNameRemovedInOrderTable', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220310000559_StatusAddedToComment')
BEGIN
    ALTER TABLE [ProductComments] ADD [Status] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220310000559_StatusAddedToComment')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220310000559_StatusAddedToComment', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220310084152_ProductCommentUpdate')
BEGIN
    ALTER TABLE [ProductComments] DROP CONSTRAINT [FK_ProductComments_AspNetUsers_AppUserId1];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220310084152_ProductCommentUpdate')
BEGIN
    DROP INDEX [IX_ProductComments_AppUserId1] ON [ProductComments];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220310084152_ProductCommentUpdate')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductComments]') AND [c].[name] = N'AppUserId1');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [ProductComments] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [ProductComments] DROP COLUMN [AppUserId1];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220310084152_ProductCommentUpdate')
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductComments]') AND [c].[name] = N'AppUserId');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [ProductComments] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [ProductComments] ALTER COLUMN [AppUserId] nvarchar(450) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220310084152_ProductCommentUpdate')
BEGIN
    CREATE INDEX [IX_ProductComments_AppUserId] ON [ProductComments] ([AppUserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220310084152_ProductCommentUpdate')
BEGIN
    ALTER TABLE [ProductComments] ADD CONSTRAINT [FK_ProductComments_AspNetUsers_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220310084152_ProductCommentUpdate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220310084152_ProductCommentUpdate', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220310135619_CommentUpdateUserNameProp')
BEGIN
    ALTER TABLE [ProductComments] ADD [UserName] nvarchar(50) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220310135619_CommentUpdateUserNameProp')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220310135619_CommentUpdateUserNameProp', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220310195102_RejectTetxAddedToOrder')
BEGIN
    ALTER TABLE [Orders] ADD [RejectText] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220310195102_RejectTetxAddedToOrder')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220310195102_RejectTetxAddedToOrder', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220311081802_AddressTableCreated')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [AddressLimit] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220311081802_AddressTableCreated')
BEGIN
    CREATE TABLE [Addresses] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL,
        [ModifiedAt] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        [AppUserId] nvarchar(450) NULL,
        [Addresses] nvarchar(100) NULL,
        [Aparment] nvarchar(100) NULL,
        [City] nvarchar(100) NULL,
        [Country] nvarchar(100) NULL,
        [State] nvarchar(100) NULL,
        [ZipCode] int NOT NULL,
        [Phone] nvarchar(35) NULL,
        CONSTRAINT [PK_Addresses] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Addresses_AspNetUsers_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220311081802_AddressTableCreated')
BEGIN
    CREATE INDEX [IX_Addresses_AppUserId] ON [Addresses] ([AppUserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220311081802_AddressTableCreated')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220311081802_AddressTableCreated', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220311203718_IsMainAddedAddress')
BEGIN
    ALTER TABLE [Addresses] ADD [IsMain] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220311203718_IsMainAddedAddress')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220311203718_IsMainAddedAddress', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220312134214_GiftTableCreated')
BEGIN
    ALTER TABLE [Products] ADD [GiftId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220312134214_GiftTableCreated')
BEGIN
    CREATE TABLE [Gifts] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL,
        [ModifiedAt] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        [Code] nvarchar(10) NULL,
        [GiftDiscount] int NOT NULL,
        CONSTRAINT [PK_Gifts] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220312134214_GiftTableCreated')
BEGIN
    CREATE INDEX [IX_Products_GiftId] ON [Products] ([GiftId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220312134214_GiftTableCreated')
BEGIN
    ALTER TABLE [Products] ADD CONSTRAINT [FK_Products_Gifts_GiftId] FOREIGN KEY ([GiftId]) REFERENCES [Gifts] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220312134214_GiftTableCreated')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220312134214_GiftTableCreated', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220312145509_GiftLenghtUpdate')
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Gifts]') AND [c].[name] = N'Code');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Gifts] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [Gifts] ALTER COLUMN [Code] nvarchar(20) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220312145509_GiftLenghtUpdate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220312145509_GiftLenghtUpdate', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220312194859_SizeIdAddedToBasketITem')
BEGIN
    ALTER TABLE [BasketItems] ADD [SizeId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220312194859_SizeIdAddedToBasketITem')
BEGIN
    CREATE INDEX [IX_BasketItems_SizeId] ON [BasketItems] ([SizeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220312194859_SizeIdAddedToBasketITem')
BEGIN
    ALTER TABLE [BasketItems] ADD CONSTRAINT [FK_BasketItems_Sizes_SizeId] FOREIGN KEY ([SizeId]) REFERENCES [Sizes] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220312194859_SizeIdAddedToBasketITem')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220312194859_SizeIdAddedToBasketITem', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220313134053_HeaderTopTableCreated')
BEGIN
    CREATE TABLE [HeaderTops] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL,
        [ModifiedAt] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        [Text] nvarchar(max) NULL,
        CONSTRAINT [PK_HeaderTops] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220313134053_HeaderTopTableCreated')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220313134053_HeaderTopTableCreated', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315152253_LoveCount-addedTo-Product')
BEGIN
    ALTER TABLE [Products] ADD [LoveCount] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315152253_LoveCount-addedTo-Product')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220315152253_LoveCount-addedTo-Product', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315163700_SubscribeTableCreated')
BEGIN
    CREATE TABLE [Subscribes] (
        [Id] int NOT NULL IDENTITY,
        [SignIpTime] datetime2 NOT NULL,
        [Email] nvarchar(max) NULL,
        CONSTRAINT [PK_Subscribes] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315163700_SubscribeTableCreated')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220315163700_SubscribeTableCreated', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220316200726_PolicyTableCreated')
BEGIN
    CREATE TABLE [Policies] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL,
        [ModifiedAt] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        [Name] nvarchar(50) NULL,
        [Text] nvarchar(max) NULL,
        CONSTRAINT [PK_Policies] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220316200726_PolicyTableCreated')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220316200726_PolicyTableCreated', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220317101630_OrderCodeAdded')
BEGIN
    ALTER TABLE [Orders] ADD [OrderCode] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220317101630_OrderCodeAdded')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220317101630_OrderCodeAdded', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220317192146_ColorIdAdded-Image')
BEGIN
    ALTER TABLE [ProductsImages] ADD [ColorId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220317192146_ColorIdAdded-Image')
BEGIN
    CREATE INDEX [IX_ProductsImages_ColorId] ON [ProductsImages] ([ColorId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220317192146_ColorIdAdded-Image')
BEGIN
    ALTER TABLE [ProductsImages] ADD CONSTRAINT [FK_ProductsImages_Colors_ColorId] FOREIGN KEY ([ColorId]) REFERENCES [Colors] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220317192146_ColorIdAdded-Image')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220317192146_ColorIdAdded-Image', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220319064918_BestSaleCategoryCreated')
BEGIN
    CREATE TABLE [BestSaleCategories] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL,
        [ModifiedAt] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        [CategoryId] int NOT NULL,
        [Title] nvarchar(max) NULL,
        [Image] nvarchar(100) NULL,
        CONSTRAINT [PK_BestSaleCategories] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_BestSaleCategories_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220319064918_BestSaleCategoryCreated')
BEGIN
    CREATE INDEX [IX_BestSaleCategories_CategoryId] ON [BestSaleCategories] ([CategoryId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220319064918_BestSaleCategoryCreated')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220319064918_BestSaleCategoryCreated', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220319173517_ColorId-addedTo-BasketItem')
BEGIN
    ALTER TABLE [BasketItems] ADD [ColorId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220319173517_ColorId-addedTo-BasketItem')
BEGIN
    CREATE INDEX [IX_BasketItems_ColorId] ON [BasketItems] ([ColorId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220319173517_ColorId-addedTo-BasketItem')
BEGIN
    ALTER TABLE [BasketItems] ADD CONSTRAINT [FK_BasketItems_Colors_ColorId] FOREIGN KEY ([ColorId]) REFERENCES [Colors] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220319173517_ColorId-addedTo-BasketItem')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220319173517_ColorId-addedTo-BasketItem', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220319182743_ColorId-addedTo-OrderItem')
BEGIN
    ALTER TABLE [OrderItem] ADD [ColorId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220319182743_ColorId-addedTo-OrderItem')
BEGIN
    ALTER TABLE [OrderItem] ADD [ColorName] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220319182743_ColorId-addedTo-OrderItem')
BEGIN
    CREATE INDEX [IX_OrderItem_ColorId] ON [OrderItem] ([ColorId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220319182743_ColorId-addedTo-OrderItem')
BEGIN
    ALTER TABLE [OrderItem] ADD CONSTRAINT [FK_OrderItem_Colors_ColorId] FOREIGN KEY ([ColorId]) REFERENCES [Colors] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220319182743_ColorId-addedTo-OrderItem')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220319182743_ColorId-addedTo-OrderItem', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220319193301_GenderId-AddedTo-BestSaleCategory')
BEGIN
    ALTER TABLE [BestSaleCategories] ADD [GenderId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220319193301_GenderId-AddedTo-BestSaleCategory')
BEGIN
    CREATE INDEX [IX_BestSaleCategories_GenderId] ON [BestSaleCategories] ([GenderId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220319193301_GenderId-AddedTo-BestSaleCategory')
BEGIN
    ALTER TABLE [BestSaleCategories] ADD CONSTRAINT [FK_BestSaleCategories_Genders_GenderId] FOREIGN KEY ([GenderId]) REFERENCES [Genders] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220319193301_GenderId-AddedTo-BestSaleCategory')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220319193301_GenderId-AddedTo-BestSaleCategory', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220319202808_SettingValueLenghtRemove')
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Settings]') AND [c].[name] = N'Value');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Settings] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [Settings] ALTER COLUMN [Value] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220319202808_SettingValueLenghtRemove')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220319202808_SettingValueLenghtRemove', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220319223554_RemovedRequiredShopOnInstragramImage')
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ShopOnInstagrams]') AND [c].[name] = N'Image');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [ShopOnInstagrams] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [ShopOnInstagrams] ALTER COLUMN [Image] nvarchar(100) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220319223554_RemovedRequiredShopOnInstragramImage')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220319223554_RemovedRequiredShopOnInstragramImage', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220320072521_ProductRequiredAddded')
BEGIN
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'Desc');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var12 + '];');
    ALTER TABLE [Products] ALTER COLUMN [Desc] nvarchar(500) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220320072521_ProductRequiredAddded')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220320072521_ProductRequiredAddded', N'3.1.22');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220320141452_ColorId-AddedTo-WishList')
BEGIN
    ALTER TABLE [WishListItems] ADD [ColorId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220320141452_ColorId-AddedTo-WishList')
BEGIN
    CREATE INDEX [IX_WishListItems_ColorId] ON [WishListItems] ([ColorId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220320141452_ColorId-AddedTo-WishList')
BEGIN
    ALTER TABLE [WishListItems] ADD CONSTRAINT [FK_WishListItems_Colors_ColorId] FOREIGN KEY ([ColorId]) REFERENCES [Colors] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220320141452_ColorId-AddedTo-WishList')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220320141452_ColorId-AddedTo-WishList', N'3.1.22');
END;

GO

