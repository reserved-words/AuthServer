CREATE TABLE [dbo].[UserClaims]
(
	[SubjectId] VARCHAR(5) NOT NULL, 
    [Type] VARCHAR(20) NOT NULL, 
    [Value] VARCHAR(100) NULL, 
    PRIMARY KEY ([SubjectId], [Type]), 
    CONSTRAINT [FK_UserClaims_Users] FOREIGN KEY ([SubjectId]) REFERENCES [dbo].[Users]([SubjectId])
)
