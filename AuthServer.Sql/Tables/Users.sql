﻿CREATE TABLE [dbo].[Users]
(
	[SubjectId] VARCHAR(5) NOT NULL PRIMARY KEY, 
    [Username] VARCHAR(15) NOT NULL, 
    [Password] VARCHAR(100) NOT NULL, 
    CONSTRAINT [AK_Users_Username] UNIQUE ([Username])
)
