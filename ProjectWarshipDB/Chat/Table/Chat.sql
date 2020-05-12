CREATE TABLE [dbo].[Chat]
(
	[id] int primary key identity,
	[message] nvarchar(255) not null,
	[date] Date not null,
	[recipient] int,
	[sender] int not null

	CONSTRAINT FK_Sender FOREIGN KEY(sender) REFERENCES [User](id)
	CONSTRAINT FK_Recipient FOREIGN KEY(recipient) REFERENCES [User](id)
)
