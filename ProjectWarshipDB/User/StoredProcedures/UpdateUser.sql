CREATE PROCEDURE [dbo].[UpdateUser]
	@id int,
	@mail nvarchar(50),
	@login nvarchar(25),
	@password nvarchar(25),
	@birthDate Date

AS
BEGIN
	UPDATE [User] 
	SET mail = @mail, [login] = @login, [password] = HASHBYTES('SHA2_512',dbo.Salt() +@password), birthDate = @birthDate
	WHERE id = @id
END

