CREATE TABLE [dbo].[TicketTable]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL, 
    [Submitter] NVARCHAR(50) NOT NULL, 
    [DateTime] DATETIME NOT NULL, 
    [Priority] INT NOT NULL, 
    [Status] INT NOT NULL, 
    [Claimer] INT NULL
)
