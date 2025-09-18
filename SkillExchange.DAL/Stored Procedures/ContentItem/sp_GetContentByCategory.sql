CREATE PROCEDURE sp_GetContentByCategory
@CategoryId INT
AS
BEGIN
SELECT c.Id, c.Title, c.Description,c.Stars,c.UserId
FROM ContentItem c
WHERE c.CategoryId = @CategoryId
ORDER BY c.Title;
END