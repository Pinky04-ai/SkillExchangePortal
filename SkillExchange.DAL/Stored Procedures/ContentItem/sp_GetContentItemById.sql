CREATE PROCEDURE sp_GetContentItemById
    @ContentItemId INT
AS
BEGIN
    SELECT 
        ci.Id,
        ci.Title,
        ci.Description,
        ci.ContentType,
        ci.UserId,
        ci.CategoryId,
        ci.Status,
        ci.CreatedAt,
        u.Id AS UserId,
        u.FullName AS UserFullName,
        u.Email AS UserEmail,
        c.Id AS CategoryId,
        c.Name AS CategoryName
    FROM ContentItems ci
    INNER JOIN Users u ON ci.UserId = u.Id
    INNER JOIN Categories c ON ci.CategoryId = c.Id
    WHERE ci.Id = @ContentItemId
END
