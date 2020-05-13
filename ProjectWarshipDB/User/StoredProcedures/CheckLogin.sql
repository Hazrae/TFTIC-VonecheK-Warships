CREATE PROCEDURE [dbo].[CheckLogin]
	@login varchar(50)
AS
	SELECT * FROM [User] WHERE [login] = @login;