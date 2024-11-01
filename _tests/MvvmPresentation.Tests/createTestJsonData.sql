DROP TABLE IF EXISTS #users
DROP TABLE IF EXISTS #orders
CREATE TABLE #users (
	Id INT IDENTITY(1,1) PRIMARY KEY, 
	DatabaseId INT NOT NULL,
	FullName NVARCHAR(255)
)

CREATE TABLE #orders (
	Id INT PRIMARY KEY, 
	Created DateTime NOT NULL,
	Sum smallmoney  NOT NULL,
	CustomerId INT NOT NULL
)

;WITH u_grouped
AS( 
	SELECT DISTINCT u.UserId, MAX(u.FullName) AS FullName
	FROM [ISOrders].[OrderContactInfo] u
	GROUP BY u.UserId
	HAVING COUNT(u.OrderId) > 7
)
,user_list
AS(
	SELECT TOP 10 *
	FROM u_grouped
	Order BY UserId DESC
)

INSERT INTO #users(DatabaseId, FullName)
SELECT ul.UserId, ul.FullName 
FROM user_list ul


INSERT INTO #orders(Id, Created, Sum, CustomerId)
SELECT TOP 100 z.zakaz_id AS Id, z.CreatedOnSite as Created, z.zakaz_summa AS Sum, u.Id AS CustomerId
FROM [ISOrders].[OrderContactInfo] ci
JOIN #users u ON u.DatabaseId = ci.UserId
JOIN B_Zakaz_CHG z ON z.zakaz_id = ci.OrderId

SELECT u.Id, u.FullName
FROM #users u
FOR JSON AUTO --, WITHOUT_ARRAY_WRAPPER

SELECT o.Id, o.Sum, o.Created, o.CustomerId
FROM #orders o
FOR JSON AUTO --, WITHOUT_ARRAY_WRAPPER