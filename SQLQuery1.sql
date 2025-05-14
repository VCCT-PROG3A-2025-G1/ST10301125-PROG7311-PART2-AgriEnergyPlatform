-- Switch to the correct database
USE AgriEnergyConnect;
GO

-- Create Farmer table
CREATE TABLE Farmer (
    FarmerID INT IDENTITY(1,1) PRIMARY KEY,
    FarmerName VARCHAR(100) NOT NULL,
    FarmerPNumber VARCHAR(100) NOT NULL
);
GO

-- Create Products table
CREATE TABLE Products (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    FarmerID INT NOT NULL,
    ProductName VARCHAR(100) NOT NULL,
    ProductCategory VARCHAR(100) NOT NULL,
    ProductionDate DATE NOT NULL,
    FOREIGN KEY (FarmerID) REFERENCES Farmer(FarmerID)
);
GO

-- Create Employees table
CREATE TABLE Employees (
    EmployeeID INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeName VARCHAR(100) NOT NULL,
    EmployeePNumber VARCHAR(100) NOT NULL,
    EmployeeDepartment VARCHAR(100) NOT NULL
);
GO

-- Insert data into Farmer table
INSERT INTO Farmer (FarmerName, FarmerPNumber)
VALUES 
('James Johnson', '0726345567'),
('Charlie Cook', '0896378266'),
('Harry Holiday', '0762524398');
GO

-- View the data in the Farmer table
SELECT * FROM Farmer;

-- Delete all data from the Farmer table (be careful!)
DELETE FROM Farmer;
-- Reset the identity seed if you want FarmerID to start from 1 again
DBCC CHECKIDENT ('Farmer', RESEED, 0);
GO

-- Insert just the 3 intended rows
INSERT INTO Farmer (FarmerName, FarmerPNumber)
VALUES 
('James Johnson', '0726345567'),
('Charlie Cook', '0896378266'),
('Harry Holiday', '0762524398');
GO

-- Verify the table
SELECT * FROM Farmer;


-- Insert data into Products table
INSERT INTO Products (FarmerID, ProductName, ProductCategory, ProductionDate)
VALUES 
(1, 'Tomatoes', 'Vegetables', '2025-04-01'),
(2, 'Milk', 'Dairy', '2025-03-25'),
(3, 'Apples', 'Fruit', '2025-03-30');
GO

-- Verify the table
SELECT * FROM Products;

-- Insert data into Employees table
INSERT INTO Employees (EmployeeName, EmployeePNumber, EmployeeDepartment)
VALUES 
('Emma Edwards', '0976353267', 'Vegetables'),
('Larry Smith', '0845234990', 'Dairy'),
('Noah Adam', '0723489421', 'Fruit');
GO

-- Verify the table
SELECT * FROM Employees;