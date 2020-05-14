CREATE PROCEDURE [dbo].[LoginUser]
	@login nvarchar(50),
	@password nvarchar(25)
AS
	BEGIN
		SELECT id,mail,[login],birthDate,isActive,isDelete,isAdmin 
		FROM [User] 
		WHERE [login] = @login AND [password] = HASHBYTES('SHA2_512', dbo.Salt() + @password)
	END

