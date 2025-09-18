CREATE PROCEDURE sp_GetAllRole
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        r.Id AS RoleId,
        r.Name AS RoleName,
        r.Description,

        ur.Id AS UserRoleId,
        ur.UserId,
        ur.RoleId AS UserRole_RoleId
    FROM Role r
    LEFT JOIN UserRole ur ON r.Id = ur.RoleId
    ORDER BY r.Name;
END
GO
