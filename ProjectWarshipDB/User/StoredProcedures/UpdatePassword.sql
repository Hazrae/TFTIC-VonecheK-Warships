CREATE PROCEDURE [dbo].[UpdatePassword]
	@id int,
	@oldpassword nvarchar(25),
	@password nvarchar(25)
	

AS
BEGIN
	IF(SELECT COUNT(*) 
	   FROM [User]  
       WHERE password = HASHBYTES('SHA2_512',dbo.Salt() +@oldpassword)) = 0
		  return 1;
	ELSE
	BEGIN	
		UPDATE [User] 
		SET [password] = HASHBYTES('SHA2_512',dbo.Salt() +@password)
		WHERE id = @id
		return 0;
	END
END
