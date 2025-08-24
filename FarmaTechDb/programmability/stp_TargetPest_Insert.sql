CREATE PROCEDURE [dbo].[stp_TargetPest_Insert]
	@TargetPest VARCHAR(255),
	@User VARCHAR(100)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @TargetPestId INT;

	IF EXISTS(SELECT 1 FROM [dbo].[TargetPests] WHERE [TargetPest] = @TargetPest AND [DeletedAt] IS NULL)
		BEGIN
			SET @TargetPestId = -1;
		END
	ELSE
		BEGIN
			INSERT INTO [dbo].[TargetPests]
			(
				[TargetPest],
				[CreatedBy]
			)
			VALUES
			(
				@TargetPest,
				@User
			);

			SET @TargetPestId = SCOPE_IDENTITY();			
		END

	SELECT @TargetPestId AS TargetPestId;

	RETURN 0;
END
