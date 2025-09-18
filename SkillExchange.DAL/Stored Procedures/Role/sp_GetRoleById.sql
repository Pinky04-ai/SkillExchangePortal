CREATE PROCEDURE sp_GetRoleById
    @RoleId INT
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
    WHERE r.Id = @RoleId;
END
GO
