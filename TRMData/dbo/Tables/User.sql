CREATE TABLE [dbo].[User]
(
    [Id] NCHAR(50) PRIMARY KEY, 
    [FirstName] NCHAR(50) NOT NULL, 
    [LastName] NCHAR(50) NOT NULL, 
    [EmailAddress] NCHAR(256) NOT NULL, 
    [CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate()
)
