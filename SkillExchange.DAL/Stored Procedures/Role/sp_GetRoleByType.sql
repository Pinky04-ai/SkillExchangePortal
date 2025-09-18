CREATE PROCEDURE sp_GetRoleByType
    @RoleName NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        r.Id AS RoleId,
        r.RoleName,
        r.Description
    FROM Roles r
    WHERE r.RoleName = @RoleName;
END
GO
