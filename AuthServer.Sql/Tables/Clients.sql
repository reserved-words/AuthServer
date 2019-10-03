CREATE TABLE [dbo].[Clients]
(
	[ClientId] VARCHAR(20) NOT NULL PRIMARY KEY, 
    [ClientName] VARCHAR(30) NOT NULL, 
    [Enabled] BIT NOT NULL DEFAULT 1, 
    [RedirectUri] VARCHAR(250) NULL, 
    [AllowOfflineAccess] BIT NOT NULL DEFAULT 0, 
    [PostLogoutRedirectUri] VARCHAR(250) NULL, 
    [EnableLocalLogin] BIT NOT NULL DEFAULT 1, 
    [RequireConsent] BIT NOT NULL DEFAULT 1, 
    [AllowRememberConsent] BIT NOT NULL DEFAULT 1, 
    [ClientUri] VARCHAR(250) NULL, 
    [LogoUri] VARCHAR(250) NULL
)
