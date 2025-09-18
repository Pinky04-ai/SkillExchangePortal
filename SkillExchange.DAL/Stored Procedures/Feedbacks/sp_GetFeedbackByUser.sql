CREATE PROCEDURE sp_GetFeedbackByUser
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT f.Id,
           f.ContentId,
           f.UserId,
           f.Rating,
           f.Comment,
           f.CreatedAt,
           c.Id AS ContentId,
           c.Title,
           c.Description
    FROM Feedbacks f
    INNER JOIN ContentItems c ON f.ContentId = c.Id
    WHERE f.UserId = @UserId;
END;
GO
