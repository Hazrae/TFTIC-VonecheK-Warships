CREATE TABLE [dbo].[User]
(
	[id] int identity primary key,
	[mail] nvarchar(50) unique NOT NULL,
	[login] nvarchar(25) unique not null,
	[password] varbinary(64) not null,
	[birthDate] Date not null,
	country nvarchar(25) not null,
	isDelete bit not null default 0,
	isActive bit not null default 1,
	isAdmin bit not null default 0,
)
