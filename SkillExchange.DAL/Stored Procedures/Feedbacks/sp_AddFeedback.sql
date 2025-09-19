CREATE PROCEDURE sp_AddFeedback
    @UserId INT,
    @ContentId INT,
    @Rating INT,
    @Comment NVARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if user exists and is verified
    IF EXISTS (
        SELECT 1 
        FROM Users 
        WHERE Id = @UserId AND Status = 1 -- 1 = Verified
    )
    BEGIN
        INSERT INTO Feedbacks (UserId, ContentId, Rating, Comment, CreatedAt)
        VALUES (@UserId, @ContentId, @Rating, @Comment, GETUTCDATE());

        -- Return the created feedback (latest inserted row)
        SELECT TOP 1 *
        FROM Feedbacks
        WHERE UserId = @UserId AND ContentId = @ContentId
        ORDER BY CreatedAt DESC;
    END
    ELSE
    BEGIN
        -- Return an error if user not verified
        RAISERROR ('Only verified users can submit feedback.', 16, 1);
    END
END
GO
