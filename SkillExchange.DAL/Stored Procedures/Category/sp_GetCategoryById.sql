CREATE PROCEDURE sp_GetCategoryById
@CategoryId INT
AS 
BEGIN
SELECT Id , Name
FROM Category
WHERE Id = @CategoryId;
END