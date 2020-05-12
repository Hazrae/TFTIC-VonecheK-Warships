CREATE PROCEDURE [dbo].[UpdateAccountSpec]
	@isActive bit = 1,
	@isDelete bit = 0,
	@isAdmin bit = 0,
	@id int
AS
BEGIN
	UPDATE [User] 
	SET isActive = @isActive, isDelete = @isDelete, isAdmin = @isAdmin 
	WHERE id = @id
END