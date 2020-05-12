CREATE TRIGGER [OnDelete]
ON [User]
INSTEAD OF DELETE
AS
BEGIN
	UPDATE [User] SET isDelete = 1 WHERE mail = deleted.mail
END
