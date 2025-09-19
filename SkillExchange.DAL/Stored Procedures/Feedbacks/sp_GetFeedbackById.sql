CREATE PROCEDURE sp_GetFeedbackById
    @FeedbackId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT f.Id,
           f.ContentId,       
           f.UserId,          
           f.Rating,
           f.Comment,
           f.CreatedAt,
           c.Description AS Content_Description
    FROM Feedbacks f
    INNER JOIN Users u ON f.UserId = u.Id
    INNER JOIN ContentItems c ON f.ContentId = c.Id
    WHERE f.Id = @FeedbackId;
END;
GO
