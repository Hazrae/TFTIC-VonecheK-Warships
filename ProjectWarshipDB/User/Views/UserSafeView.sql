CREATE VIEW [dbo].[UserSafeView]
	AS SELECT id, [login], mail, birthDate, isDelete, isActive, isAdmin 
	FROM [User]
