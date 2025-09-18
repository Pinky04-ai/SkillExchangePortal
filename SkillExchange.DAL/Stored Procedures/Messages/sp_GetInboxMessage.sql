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
        fu.Id AS FromUser_Id,
        fu.FullName AS FromUser_FullName,
        fu.Email AS FromUser_Email,
        tu.Id AS ToUser_Id,
        tu.FullName AS ToUser_FullName,
        tu.Email AS ToUser_Email
    FROM Messages m
    INNER JOIN Users fu ON m.FromUserId = fu.Id
    INNER JOIN Users tu ON m.ToUserId = tu.Id
    WHERE m.ToUserId = @UserId
    ORDER BY m.SentAt DESC;
END

