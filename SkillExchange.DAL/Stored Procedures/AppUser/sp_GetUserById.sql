CREATE PROCEDURE sp_GetUserById
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. AppUser
    SELECT 
        u.Id,
        u.Email,
        u.Password,
        u.FullName,
        u.Status,
        u.CreatedAt
    FROM Users u
    WHERE u.Id = @UserId;

    -- 2. UserRoles + Roles
    SELECT 
        ur.Id,
        ur.UserId,
        ur.RoleId,
        r.RoleName
    FROM UserRoles ur
    INNER JOIN Roles r ON ur.RoleId = r.Id
    WHERE ur.UserId = @UserId;

    -- 3. ContentItems
    SELECT 
        ci.Id,
        ci.UserId,
        ci.Title,
        ci.Description,
        ci.Status,
        ci.CreatedAt
    FROM ContentItems ci
    WHERE ci.UserId = @UserId;

    -- 4. Feedbacks
    SELECT 
        f.Id,
        f.UserId,
        f.ContentId,
        f.Rating,
        f.Comment,
        f.CreatedAt
    FROM Feedbacks f
    WHERE f.UserId = @UserId;

    -- 5. Sent Messages
    SELECT 
        m.Id,
        m.FromUserId,
        m.ToUserId,
        m.Content,
        m.SentAt
    FROM Messages m
    WHERE m.FromUserId = @UserId;

    -- 6. Received Messages
    SELECT 
        m.Id,
        m.FromUserId,
        m.ToUserId,
        m.Content,
        m.SentAt
    FROM Messages m
    WHERE m.ToUserId = @UserId;
END
GO
