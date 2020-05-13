CREATE PROCEDURE [dbo].[CheckMail]
	@mail varchar(50)
AS
	SELECT * FROM [User] WHERE mail = @mail;

