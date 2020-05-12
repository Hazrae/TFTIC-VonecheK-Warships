CREATE PROCEDURE [dbo].[LoginUser]
	@login nvarchar(50),
	@password nvarchar(25)
AS
	BEGIN
		exec dbo.GetOne(SELECT id FROM [User] WHERE [login] = @login AND [password] = HASHBYTES('SHA_512', dbo.Salt() + @password))
	END

