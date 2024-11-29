USE master;
GO

ALTER DATABASE InternetStore
SET SINGLE_USER
WITH ROLLBACK IMMEDIATE;
GO

DROP DATABASE InternetStore;
GO

CREATE DATABASE InternetStore;
GO

USE InternetStore;

CREATE TABLE dbo.Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Salt VARBINARY(16) NOT NULL,
    HashedPassword VARBINARY(64) NOT NULL,
    Address NVARCHAR(200) NULL,
    City NVARCHAR(100) NULL,
    State NVARCHAR(100) NULL,
    PostalCode NVARCHAR(20) NULL,
    Country NVARCHAR(100) NULL,
    PhoneNumber NVARCHAR(20) NULL,
    DateRegistered DATETIME2 NOT NULL DEFAULT GETDATE()
);

CREATE TABLE dbo.Products (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    Price DECIMAL(10, 2) NOT NULL CHECK (Price >= 0),
    StockQuantity INT NOT NULL CHECK (StockQuantity >= 0),
    Category NVARCHAR(100) NULL,
    DateAdded DATETIME2 NOT NULL DEFAULT GETDATE()
);

CREATE TABLE dbo.Purchases (
    PurchaseID INT IDENTITY(1,1) PRIMARY KEY,
	NumberOrder INT,
    UserID INT NOT NULL,
    PurchaseDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    TotalAmount DECIMAL(10, 2) NOT NULL CHECK (TotalAmount >= 0),
    PaymentMethod NVARCHAR(50) NOT NULL,
    ShippingAddress NVARCHAR(200) NOT NULL,
    BillingAddress NVARCHAR(200) NOT NULL,
    CONSTRAINT FK_Purchases_Users FOREIGN KEY (UserID) 
        REFERENCES dbo.Users(UserID)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);


CREATE TABLE dbo.PurchaseItems (
    PurchaseItemID INT IDENTITY(1,1) PRIMARY KEY,
    PurchaseID INT NOT NULL,
	NumberOrder INT,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    UnitPrice DECIMAL(10, 2) NOT NULL CHECK (UnitPrice >= 0),
    CONSTRAINT FK_PurchaseItems_Purchases FOREIGN KEY (PurchaseID) 
        REFERENCES dbo.Purchases(PurchaseID)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
    CONSTRAINT FK_PurchaseItems_Products FOREIGN KEY (ProductID) 
        REFERENCES dbo.Products(ProductID)
);