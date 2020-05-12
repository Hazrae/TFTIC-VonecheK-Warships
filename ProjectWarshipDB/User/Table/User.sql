CREATE TABLE [dbo].[User]
(
	[mail] nvarchar(50) NOT NULL PRIMARY KEY,
	[login] nvarchar(25) not null,
	[password] varbinary(64) not null,
	[birthDate] Date not null,
	country nvarchar(25) not null,
	isDelete bit not null default 0,
	isActive bit not null default 1,
	isAdmin bit not null default 0,
)
