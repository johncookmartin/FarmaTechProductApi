CREATE PROCEDURE [dbo].[stp_FormulationArray_SelectAll]
	@FormulationId INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Value]
	FROM dbo.[FormulationArrays]
	WHERE [FormulationId] = @FormulationId
	ORDER BY [SortOrder]

	RETURN 0
END
