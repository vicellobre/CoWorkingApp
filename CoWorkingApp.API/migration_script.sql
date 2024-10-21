IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241020145811_InitialCreate'
)
BEGIN
    CREATE TABLE [Seats] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(50) NOT NULL,
        [IsBlocked] bit NOT NULL,
        [Description] nvarchar(255) NULL,
        CONSTRAINT [PK_Seats] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241020145811_InitialCreate'
)
BEGIN
    CREATE TABLE [Users] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(50) NOT NULL,
        [LastName] nvarchar(50) NOT NULL,
        [Email] nvarchar(100) NOT NULL,
        [Password] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241020145811_InitialCreate'
)
BEGIN
    CREATE TABLE [Reservations] (
        [Id] uniqueidentifier NOT NULL,
        [Date] datetime2 NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [SeatId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Reservations] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Reservations_Seats_SeatId] FOREIGN KEY ([SeatId]) REFERENCES [Seats] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Reservations_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241020145811_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Reservations_SeatId] ON [Reservations] ([SeatId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241020145811_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Reservations_UserId] ON [Reservations] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241020145811_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Seats_Name] ON [Seats] ([Name]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241020145811_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Users_Email] ON [Users] ([Email]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241020145811_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241020145811_InitialCreate', N'8.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241020162027_mssql.azure_migration_352'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241020162027_mssql.azure_migration_352', N'8.0.10');
END;
GO

COMMIT;
GO

