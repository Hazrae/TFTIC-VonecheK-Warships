CREATE PROCEDURE [dbo].[UpdatePassword]
	@id int,
	@oldpassword nvarchar(25),
	@password nvarchar(25)	
AS
BEGIN
	IF(SELECT COUNT(*) 
	   FROM [User]  
       WHERE password = HASHBYTES('SHA2_512',dbo.Salt() +@oldpassword) AND id = @id) = 0
		Select 1
	ELSE
	BEGIN	
		UPDATE [User] 
		SET [password] = HASHBYTES('SHA2_512',dbo.Salt() +@password)
		WHERE id = @id
		Select 0
	END
END
