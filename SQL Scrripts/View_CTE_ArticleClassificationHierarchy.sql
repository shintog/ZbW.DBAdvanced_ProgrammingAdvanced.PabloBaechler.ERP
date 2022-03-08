/****** Object:  View [dbo].[CTE_ArticleHierarchy]    Script Date: 06.03.2022 20:17:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[V_CTE_ArticleClassificationHierarchy] WITH SCHEMABINDING AS
With CTE_ArticleClassificationHierarchy (
	ClassificationNr, ParentProductID, ClassificationLevel )
AS (
	SELECT	ClassificationNr,
            Parent ,
            0 AS ClassificationLevel
    FROM dbo.ArticleClassification
    WHERE Parent IS NULL
    UNION ALL
    SELECT	ac.ClassificationNr ,
            ac.Parent ,
            ac1.ClassificationLevel + 1
    FROM dbo.ArticleClassification AS ac
    INNER JOIN CTE_ArticleClassificationHierarchy AS ac1
		ON ac1.ClassificationNr = ac.Parent
)
SELECT	ClassificationNr ,
        ParentProductID ,
        ClassificationLevel
FROM CTE_ArticleClassificationHierarchy;
GO


