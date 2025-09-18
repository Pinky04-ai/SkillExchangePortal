CREATE PROCEDURE sp_GetRolesByUser
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ur.Id AS UserRoleId,
        ur.UserId,
        ur.RoleId,
        r.RoleName,
        r.Description
    FROM UserRole ur
    INNER JOIN Role r ON ur.RoleId = r.Id
    WHERE ur.UserId = @UserId;
END
GO
