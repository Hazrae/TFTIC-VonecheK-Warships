CREATE VIEW [dbo].[UserSafeView]
	AS SELECT [login], mail, birthDate, country, isDelete, isActive, isAdmin FROM [User]
