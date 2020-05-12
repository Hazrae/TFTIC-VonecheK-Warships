CREATE PROCEDURE [dbo].[DeleteUser]
	@mail varchar(50)
AS
BEGIN
		UPDATE [User] SET isDelete = 1 WHERE mail = @mail
END

