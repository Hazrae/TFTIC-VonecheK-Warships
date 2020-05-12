CREATE TRIGGER [OnDelete]
ON [User]
INSTEAD OF DELETE
AS
BEGIN
	UPDATE [User] 
	SET isDelete = 1 
	WHERE id in (Select id from inserted)
END
