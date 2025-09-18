CREATE PROCEDURE sp_SearchContents
    @Title NVARCHAR(200) = NULL,
    @CategoryId INT = NULL,
    @MinStars INT = NULL,
    @OnlyApproved BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        c.Id,
        c.Title,
        c.Description,
        c.UserId,
        c.CategoryId,
        c.Stars,
        c.Status,
        c.CreatedAt,
        c.UpdatedAt
    FROM ContentItem c
    LEFT JOIN (
        SELECT ContentId, AVG(CAST(Rating AS FLOAT)) AS AvgRating
        FROM Feedback
        GROUP BY ContentId
    ) f ON c.Id = f.ContentId
    WHERE (@Title IS NULL OR c.Title LIKE '%' + @Title + '%')
      AND (@CategoryId IS NULL OR c.CategoryId = @CategoryId)
      AND (@OnlyApproved = 0 OR c.Status = 1)
      AND (@MinStars IS NULL OR f.AvgRating >= @MinStars)
    ORDER BY c.CreatedAt DESC
END
