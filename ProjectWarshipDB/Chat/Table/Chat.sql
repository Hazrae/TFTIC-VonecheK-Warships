CREATE TABLE [dbo].[Chat]
(
	[id] int primary key identity,
	[message] nvarchar(255) not null,
	[date] Date not null,
	[recipient] nvarchar(50),
	[sender] nvarchar(50) not null

	CONSTRAINT FK_Sender FOREIGN KEY(sender) REFERENCES [User](mail)
	CONSTRAINT FK_Recipient FOREIGN KEY(recipient) REFERENCES [User](mail)
)
