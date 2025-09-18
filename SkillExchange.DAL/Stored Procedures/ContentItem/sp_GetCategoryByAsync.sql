CREATE PROCEDURE sp_GetContentItemsByCategory
    @CategoryId INT,
    @OnlyApproved BIT = 1
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
        c.Id AS CategoryId,
        c.Name AS CategoryName
    FROM ContentItems ci
    INNER JOIN AppUser u ON ci.UserId = u.Id
    INNER JOIN Category c ON ci.CategoryId = c.Id
    WHERE ci.CategoryId = @CategoryId
      AND (@OnlyApproved = 0 OR ci.Status = 'Approved')
END
