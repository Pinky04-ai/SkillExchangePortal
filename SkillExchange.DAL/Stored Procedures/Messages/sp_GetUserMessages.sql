CREATE PROCEDURE sp_GetUserMessages
@UserId INT 
AS 
BEGIN
SELECT m.Id , m.FromUserId,m.ToUserId,m.Content,m.SentAt,m.IsRead
FROM Messages m
WHERE m.ToUserId = @UserId
Order by m.SentAt;
END