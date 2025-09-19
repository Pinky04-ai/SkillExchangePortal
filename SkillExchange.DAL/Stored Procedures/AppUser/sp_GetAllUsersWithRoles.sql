CREATE PROCEDURE sp_GetAllUsersWithRoles
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        u.Id AS UserId,
        u.FullName,
        u.Email,
        u.Status,
        u.CreatedAt,
        ur.Id AS UserRoleId,
        ur.RoleId,
        r.RoleName,
        r.Description AS RoleDescription
    FROM Users u
    LEFT JOIN UserRole ur ON u.Id = ur.UserId
    LEFT JOIN Role r ON ur.RoleId = r.Id
    ORDER BY u.Id;
END
GO
