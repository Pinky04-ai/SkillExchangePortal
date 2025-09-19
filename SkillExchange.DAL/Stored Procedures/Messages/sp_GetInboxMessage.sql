CREATE PROCEDURE sp_GetInboxMessage
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        m.Id,
        m.FromUserId,
        m.ToUserId,
        m.Content,
        m.SentAt,
        m.IsRead,
        fu.FullName AS FromUserName,
        fu.Email AS FromUserEmail,
        tu.FullName AS ToUserName,
        tu.Email AS ToUserEmail
    FROM Messages m
    INNER JOIN Users fu ON m.FromUserId = fu.Id
    INNER JOIN Users tu ON m.ToUserId = tu.Id
    WHERE m.ToUserId = @UserId
    ORDER BY m.SentAt DESC;
END