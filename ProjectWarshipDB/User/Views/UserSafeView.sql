CREATE VIEW [dbo].[UserSafeView]
	AS SELECT id, [login], mail, birthDate, country, isDelete, isActive, isAdmin 
	FROM [User]
