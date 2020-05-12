CREATE PROCEDURE [dbo].[AddUser]
	@mail nvarchar(50),
	@login nvarchar(25),
	@password nvarchar(25),
	@birthDate Date,
	@country nvarchar(25)
AS
BEGIN
	INSERT INTO [User] (mail,[login],[password],birthDate,country)
	VALUES (@mail,@login,HASHBYTES('SHA2_512',dbo.Salt() +@password),@birthDate,@country)
END