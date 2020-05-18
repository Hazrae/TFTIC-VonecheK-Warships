CREATE PROCEDURE [dbo].[UpdateUser]
	@id int,
	@mail nvarchar(50),
	@login nvarchar(25),	
	@birthDate Date

AS
BEGIN
	UPDATE [User] 
	SET mail = @mail, [login] = @login, birthDate = @birthDate
	WHERE id = @id
END

