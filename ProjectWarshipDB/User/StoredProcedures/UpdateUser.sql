CREATE PROCEDURE [dbo].[UpdateUser]
	@id int,
	@mail nvarchar(50),
	@login nvarchar(25),
	@password nvarchar(25),
	@birthDate Date,
	@country nvarchar(25)
AS
BEGIN
	UPDATE [User] 
	SET mail = @mail, [login] = @login, [password] = HASHBYTES('SHA2_512',dbo.Salt() +@password), birthDate = @birthDate, country = @country 
	WHERE id = @id
END

