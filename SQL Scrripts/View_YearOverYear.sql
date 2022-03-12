CREATE VIEW V_YearOverYearReport As
select *
from (
SELECT Category,
	   COALESCE([Value],0) as [Value],	  
	   CONCAT('Q', [Quarter], '_Y', YEAR(GETDATE()) - [Year]) as [Column_Header]
  FROM [YearOverYear]
  UNION ALL
SELECT Category,
	   SUM(COALESCE([Value],0))  OVER (PARTITION BY [Category] ORDER BY [Category] ROWS 2 PRECEDING) as [Value],	  
	   'YOY' as [Column_Header]
  FROM [YearOverYear]
  GROUP BY Category,[Value]
  ) t
PIVOT(
	SUM([Value]) 
	FOR [Column_Header] IN (
		[YOY],
		[Q1_Y3],
		[Q1_Y2],
		[Q1_Y1],
		[Q1_Y0],
		[Q2_Y3],
		[Q2_Y2],
		[Q2_Y1],
		[Q2_Y0],
		[Q3_Y3],
		[Q3_Y2],
		[Q3_Y1],
		[Q3_Y0],
		[Q4_Y3],
		[Q4_Y2],
		[Q4_Y1],
		[Q4_Y0]
	)
) AS pivot_table


exec CreateYOYReport;