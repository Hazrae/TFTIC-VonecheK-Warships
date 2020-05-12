CREATE PROCEDURE [dbo].[DeleteUser]
	@id varchar(50)
AS
BEGIN
		UPDATE [User] 
		SET isDelete = 1 
		WHERE id = @id
END

