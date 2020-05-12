CREATE PROCEDURE [dbo].[UpdateAccountType]
	@isActive bit = 1,
	@isDelete bit = 0,
	@isAdmin bit = 0
AS
BEGIN
	UPDATE [User] SET isActive = @isActive, isDelete = @isDelete, isAdmin = @isAdmin
END