CREATE PROCEDURE sp_GetUserById
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        u.Id AS UserId,
        u.UserName,
        u.Email,
        u.CreatedAt,

        ur.Id AS UserRoleId,
        ur.RoleId,
        r.RoleName,
        r.Description AS RoleDescription,

        ci.Id AS ContentId,
        ci.Title AS ContentTitle,
        ci.Status AS ContentStatus,
        ci.CreatedAt AS ContentCreatedAt,

        f.Id AS FeedbackId,
        f.ContentId AS FeedbackContentId,
        f.Rating,
        f.Comment,
        f.CreatedAt AS FeedbackCreatedAt,

        sm.Id AS SentMessageId,
        sm.ToUserId AS SentToUserId,
        sm.Content AS SentMessageContent,
        sm.SentAt AS SentAt,

        rm.Id AS ReceivedMessageId,
        rm.FromUserId AS ReceivedFromUserId,
        rm.Content AS ReceivedMessageContent,
        rm.SentAt AS ReceivedAt

    FROM Users u
    LEFT JOIN UserRole ur ON u.Id = ur.UserId
    LEFT JOIN Role r ON ur.RoleId = r.Id
    LEFT JOIN ContentItem ci ON u.Id = ci.UserId
    LEFT JOIN Feedback f ON u.Id = f.UserId
    LEFT JOIN Message sm ON u.Id = sm.FromUserId
    LEFT JOIN Message rm ON u.Id = rm.ToUserId
    WHERE u.Id = @UserId;
END
GO
