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
           u.Id AS UserId,
           u.FullName,
           u.Email,
           c.Id AS ContentId,
           c.Title,
           c.Description
    FROM Feedbacks f
    INNER JOIN Users u ON f.UserId = u.Id
    INNER JOIN ContentItems c ON f.ContentId = c.Id
    WHERE f.Id = @FeedbackId;
END;
GO
