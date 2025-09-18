CREATE PROCEDURE sp_GetAllCategories
AS
BEGIN
	SELECT Id , Name 
	FROM Category
	ORDER BY Name;
END