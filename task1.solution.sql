SELECT
    p.ProductName,
    SUM(pi.Quantity) AS TotalQuantity,
    SUM(pi.Quantity * pi.UnitPrice) AS TotalAmount
FROM
    dbo.PurchaseItems pi
    INNER JOIN dbo.Purchases pu ON pi.PurchaseID = pu.PurchaseID
    INNER JOIN dbo.Products p ON pi.ProductID = p.ProductID
WHERE
    pu.PurchaseDate = '2000-09-01'
GROUP BY
    p.ProductName
ORDER BY
    p.ProductName;