CREATE TABLE [dbo].[UserExternalProviders]
(
	[SubjectId] VARCHAR(5) NOT NULL, 
    [ProviderId] VARCHAR(20) NOT NULL, 
    [ProviderUserId] VARCHAR(100) NOT NULL, 
    PRIMARY KEY ([SubjectId], [ProviderId]), 
    CONSTRAINT [FK_UserExternalProviders_Users] FOREIGN KEY ([SubjectId]) REFERENCES [dbo].[Users]([SubjectId]), 
    CONSTRAINT [FK_UserExternalProviders_Providers] FOREIGN KEY ([ProviderId]) REFERENCES [dbo].[Providers]([Id]), 
    CONSTRAINT [AK_UserExternalProviders_ProviderUserId] UNIQUE ([ProviderId], [ProviderUserId])
)
