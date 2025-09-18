CREATE PROCEDURE sp_GetUnverifiedUsers
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        u.Id,
        u.UserName,
        u.Email,
        u.CreatedAt,
        u.Status
    FROM Users u
    WHERE u.Status = 0; -- Assuming UserStatus.UnderVerification = 1
END
GO
