CREATE PROCEDURE sp_GetUserByEmail
    @Email NVARCHAR(256)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        u.Id,
        u.FullName,
        u.Email,
        u.Password,
        u.Status,
        u.CreatedAt
    FROM Users u
    WHERE u.Email = @Email;
END
GO
