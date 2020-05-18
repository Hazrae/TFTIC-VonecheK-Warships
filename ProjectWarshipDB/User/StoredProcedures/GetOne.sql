CREATE PROCEDURE [dbo].[GetOne]
	@id int
AS
BEGIN
	SELECT * FROM UserSafeView where id = @id
END
