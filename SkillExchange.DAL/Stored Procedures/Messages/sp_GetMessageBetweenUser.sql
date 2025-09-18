CREATE PROCEDURE sp_GetMessageBetweenUser
    @FromUserId INT,
    @ToUserId INT
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
    WHERE 
        (m.FromUserId = @FromUserId AND m.ToUserId = @ToUserId)
        OR 
        (m.FromUserId = @ToUserId AND m.ToUserId = @FromUserId)
    ORDER BY m.SentAt ASC;
END
GO
