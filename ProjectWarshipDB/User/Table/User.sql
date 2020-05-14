CREATE TABLE [dbo].[User]
(
	[id] int identity primary key,
	[mail] nvarchar(50)  NOT NULL,
	[login] nvarchar(25) not null,
	[password] varbinary(64) not null,
	[birthDate] Date not null,	
	isDelete bit not null default 0,
	isActive bit not null default 1,
	isAdmin bit not null default 0,

	constraint Unique_mail unique (mail),
	constraint Unique_login unique ([login])
)
