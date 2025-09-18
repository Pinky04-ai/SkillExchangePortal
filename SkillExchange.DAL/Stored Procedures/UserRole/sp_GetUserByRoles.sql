CREATE PROCEDURE sp_GetUsersByRoles
    @RoleId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ur.Id AS UserRoleId,
        ur.UserId,
        ur.RoleId,
        u.FullName,
        u.Email,
        u.Status
    FROM UserRole ur
    INNER JOIN AppUser u ON ur.UserId = u.Id
    WHERE ur.RoleId = @RoleId;
END
GO
