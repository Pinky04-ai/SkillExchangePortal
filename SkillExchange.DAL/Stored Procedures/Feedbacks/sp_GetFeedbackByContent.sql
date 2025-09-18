CREATE PROCEDURE sp_GetFeedbackByContent
    @ContentId INT
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
           u.Username,
           u.Email
    FROM Feedbacks f
    INNER JOIN Users u ON f.UserId = u.Id
    WHERE f.ContentId = @ContentId;
END;
GO
