CREATE PROCEDURE [dbo].[GetOne]
	@id int
AS
	SELECT * FROM UserSafeView where @id = id
RETURN 0
