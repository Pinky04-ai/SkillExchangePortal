CREATE PROCEDURE sp_GetUserRole
    @UserId INT,
    @RoleId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        ur.Id AS UserRoleId,
        ur.UserId,
        ur.RoleId,
        u.FullName AS UserFullName,
        u.Email AS UserEmail,
        u.Status AS UserStatus,
        r.RoleName,
        r.Description AS RoleDescription
    FROM UserRoles ur
    INNER JOIN AppUser u ON ur.UserId = u.Id
    INNER JOIN Role r ON ur.RoleId = r.Id
    WHERE ur.UserId = @UserId AND ur.RoleId = @RoleId;
END
GO
