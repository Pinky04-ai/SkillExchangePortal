CREATE PROCEDURE sp_GetContentByUser
    @UserId INT
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
        c.Id AS CategoryId,
        c.Name AS CategoryName
    FROM ContentItem ci
    INNER JOIN Category c ON ci.CategoryId = c.Id
    WHERE ci.UserId = @UserId
END
