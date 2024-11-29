DELETE FROM dbo.Products;
DELETE FROM dbo.Users;
DELETE FROM dbo.Purchases;
DELETE FROM dbo.PurchaseItems;

DBCC CHECKIDENT ('dbo.PurchaseItems', RESEED, 0); 
DBCC CHECKIDENT ('dbo.Purchases', RESEED, 0);
DBCC CHECKIDENT ('dbo.Products', RESEED, 0);
DBCC CHECKIDENT ('dbo.Users', RESEED, 0);

INSERT INTO dbo.Users (FirstName, LastName, Email, Salt, HashedPassword, Address, City, State, PostalCode, Country, PhoneNumber)
VALUES
('John', 'Doe', 'johndoe@example.com', 
  0x0123456789ABCDEF0123456789ABCDEF, 
  0xFEDCBA9876543210FEDCBA9876543210FEDCBA9876543210FEDCBA9876543210FEDCBA9876543210FEDCBA9876543210FEDCBA9876543210FEDCBA9876543210, 
  '123 Main St', 'Anytown', 'Anystate', '12345', 'USA', '555-1234'),
('Jane', 'Smith', 'janesmith@example.com', 
  0x11111111111111112222222222222222, 
  0xAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA, 
  '456 Elm St', 'Othertown', 'Otherstate', '67890', 'USA', '555-5678'),
('Bob', 'Johnson', 'bobjohnson@example.com', 
  0x33333333333333334444444444444444, 
  0xBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB, 
  '789 Oak St', 'Somecity', 'Somestate', '11223', 'USA', '555-2468'),
('Alice', 'Williams', 'alicewilliams@example.com', 
  0x55555555555555556666666666666666, 
  0xCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC, 
  '101 Maple St', 'Anycity', 'Anystate', '33445', 'USA', '555-1357'),
('Charlie', 'Brown', 'charliebrown@example.com', 
  0x77777777777777778888888888888888, 
  0xDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD, 
  '202 Pine St', 'Smalltown', 'Smallstate', '55667', 'USA', '555-7890'),
('Иван', 'Иванов', 'abc@email.com', 
  0x77777777777777778888888888888888, 
  0xDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD, 
  '202 Pine St', 'Smalltown', 'Smallstate', '55667', 'USA', '555-7890'),
('Виктор', 'Петров', 'xyz@email.com', 
  0x77777777777777778888888888888888, 
  0xDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD, 
  '202 Pine St', 'Smalltown', 'Smallstate', '55667', 'USA', '555-7890');

INSERT INTO dbo.Products (ProductName, Description, Price, StockQuantity, Category)
VALUES
('Product A', 'Description for Product A', 10.99, 100, 'Category 1'),
('Product B', 'Description for Product B', 5.49, 200, 'Category 1'),
('Product C', 'Description for Product C', 20.00, 150, 'Category 2'),
('Product D', 'Description for Product D', 15.75, 80, 'Category 2'),
('Product E', 'Description for Product E', 30.00, 50, 'Category 3'),
('Product F', 'Description for Product F', 25.99, 60, 'Category 3'),
('Product G', 'Description for Product G', 40.00, 70, 'Category 4'),
('Product H', 'Description for Product H', 8.99, 120, 'Category 4'),
('Product I', 'Description for Product I', 12.50, 90, 'Category 5'),
('Product J', 'Description for Product J', 22.00, 110, 'Category 5'),
('LG 1755',   'Description for Product I', 12000.75, 100, 'Category 5'),
('Xiomi 12X', 'Description for Product I', 42000.75, 100, 'Category 5'),
('Noname 14232', 'Description for Product I', 1.70, 100, 'Category 5'),
('Noname 222',   'Description for Product I', 3.14, 100, 'Category 5');


INSERT INTO dbo.Purchases (UserID, PurchaseDate, TotalAmount, PaymentMethod, ShippingAddress, BillingAddress)
VALUES
(1,'2000-09-01',  27.47, 'Credit Card', '123 Main St, Anytown, Anystate, 12345, USA', '123 Main St, Anytown, Anystate, 12345, USA'),
(1,'2000-09-01', 51.50, 'PayPal', '123 Main St, Anytown, Anystate, 12345, USA', '123 Main St, Anytown, Anystate, 12345, USA'),
(2,'2000-09-01', 55.99, 'Credit Card', '456 Elm St, Othertown, Otherstate, 67890, USA', '456 Elm St, Othertown, Otherstate, 67890, USA'),
(2,'2000-09-01', 80.00, 'PayPal', '456 Elm St, Othertown, Otherstate, 67890, USA', '456 Elm St, Othertown, Otherstate, 67890, USA'),
(3,'2000-09-02',  44.95, 'Credit Card', '789 Oak St, Somecity, Somestate, 11223, USA', '789 Oak St, Somecity, Somestate, 11223, USA'),
(3,'2000-09-02',  59.50, 'PayPal', '789 Oak St, Somecity, Somestate, 11223, USA', '789 Oak St, Somecity, Somestate, 11223, USA'),
(4,'2000-09-03',  43.96, 'Credit Card', '101 Maple St, Anycity, Anystate, 33445, USA', '101 Maple St, Anycity, Anystate, 33445, USA'),
(4,'2000-09-03', 30.98, 'PayPal', '101 Maple St, Anycity, Anystate, 33445, USA', '101 Maple St, Anycity, Anystate, 33445, USA'),
(5,'2000-09-04',  45.75, 'Credit Card', '202 Pine St, Smalltown, Smallstate, 55667, USA', '202 Pine St, Smalltown, Smallstate, 55667, USA'),
(5,'2000-09-04', 91.98, 'PayPal', '202 Pine St, Smalltown, Smallstate, 55667, USA', '202 Pine St, Smalltown, Smallstate, 55667, USA');

INSERT INTO dbo.PurchaseItems (PurchaseID, ProductID, Quantity, UnitPrice)
VALUES
(1, 1, 2, 10.99),
(1, 2, 1, 5.49),

(2, 3, 1, 20.00),
(2, 4, 2, 15.75),

(3, 5, 1, 30.00),
(3, 6, 1, 25.99),

(4, 7, 2, 40.00),

(5, 8, 5, 8.99),

(6, 9, 3, 12.50),
(6, 10, 1, 22.00),

(7, 1, 4, 10.99),

(8, 2, 2, 5.49),
(8, 3, 1, 20.00),

(9, 4, 1, 15.75),
(9, 5, 1, 30.00),

(10, 6, 2, 25.99),
(10, 7, 1, 40.00);
