-- Create Provinces table
CREATE TABLE Provinces (
    ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Name NVARCHAR(30) NOT NULL,
    Code NVARCHAR(2) NOT NULL UNIQUE
);

--drop table Cities
-- Create Cities table
CREATE TABLE Cities (
    ID INT IDENTITY(1,1) NOT NULL,
    Name NVARCHAR(30) NOT NULL  PRIMARY KEY,
    ProvCode NVARCHAR(2) NOT NULL,
    CONSTRAINT FK_Cities_Provinces FOREIGN KEY (ProvCode) REFERENCES Provinces(Code) 
        ON DELETE CASCADE -- Ensures that deleting a Province deletes its Cities
);

--drop table Users
-- Create Users table
CREATE TABLE Users (
    ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Address NVARCHAR(100) NOT NULL,
    CityName NVARCHAR(30) NOT NULL,
    PostalCode NVARCHAR(6) NOT NULL,
    CONSTRAINT FK_Users_Cities FOREIGN KEY (CityName) REFERENCES Cities(Name) 
        ON DELETE CASCADE -- Ensures that if a City is deleted, Users are set to NULL
);


INSERT INTO Provinces (Name, Code) VALUES 
('Alberta', 'AB'),
('British Columbia', 'BC'),
('Manitoba', 'MB'),
('New Brunswick', 'NB'),
('Newfoundland and Labrador', 'NL'),
('Northwest Territories', 'NT'),
('Nova Scotia', 'NS'),
('Nunavut', 'NU'),
('Ontario', 'ON'),
('Prince Edward Island', 'PE'),
('Quebec', 'QC'),
('Saskatchewan', 'SK'),
('Yukon', 'YT');


INSERT INTO Cities (Name, ProvCode) VALUES 
('Ardrossan', 'AB'),
('Edmonton', 'AB'),
('Vancouver', 'BC'),
('Victoria', 'BC'),
('Winnipeg', 'MB'),
('Woodstock', 'NB'),
('Whitbourne', 'NL'),
('Yellowknife', 'NT'),
('Halifax', 'NS'),
('Arviat', 'NU'),
('Brampton', 'ON'),
('Toronto', 'ON'),
('Kensington', 'PE'),
('Montreal', 'QC'),
('Wellington', 'QC'),
('Regina', 'SK'),
('Saskatoon', 'SK'),
('Mayo', 'YT');



-- Insert Users
INSERT INTO Users (Name, Address, CityName, PostalCode) VALUES 
('Bill Anderson', '220 Bay St.', 'Toronto', 'M6H2X4'),
('Jilal-ud-deen Akbar', '1007-33 Isabella St.', 'Toronto', 'M6H2Z9'),
('Karishma Patel', '2220 Seneca Blvd.', 'Brampton', 'M6H7Y8'),
('Mahinder Singh', '200-660 Bovaird Ave.', 'Brampton', 'M6H2U5');

--select * from Users

--select * from Users

--select * from UserClub

--select * from clubs

Scaffold-DbContext "Server=VOCBook15\SQLEXPRESS;Database=PracticeAsst5DB;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force
