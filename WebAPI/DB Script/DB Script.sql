Create Table Student
(StudentID INT PRIMARY KEY IDENTITY(1,1),
FirstName NVARCHAR(50),
LastName NVARCHAR(50),
PhoneNumber NVARCHAR(10),
Address NVARCHAR(300),
Gender NVARCHAR(10),
Email NVARCHAR(100),
CreateDate Datetime,
ModifyDate Datetime
)


Create Table StudentDetails
(
 StudentDetailID INT PRIMARY KEY IDENTITY(1,1),
 StudentID INT,    
 Class INT,
 StateID INT,
 DistrictID INT,
 CreateDate Datetime,
 ModifyDate Datetime
)



CREATE TABLE StateMaster
(
    StateID INT PRIMARY KEY IDENTITY(1,1),
    StateName NVARCHAR(100),
    IsActive BIT,
    CreateDate DATETIME
);

CREATE TABLE DistrictMaster
(
    DistrictID INT PRIMARY KEY IDENTITY(1,1),
    DistrictName NVARCHAR(100),
    StateID INT,
    CreateDate DATETIME,
    FOREIGN KEY (StateID) REFERENCES StateMaster(StateID)
);



INSERT INTO StateMaster (StateName, IsActive, CreateDate)
VALUES
('Andhra Pradesh', 1, GETDATE()),
('Bihar', 1, GETDATE()),
('Karnataka', 1, GETDATE()),
('Maharashtra', 1, GETDATE()),
('Uttar Pradesh', 1, GETDATE()),
('Tamil Nadu', 1, GETDATE()),
('West Bengal', 1, GETDATE()),
('Kerala', 1, GETDATE()),
('Rajasthan', 1, GETDATE()),
('Punjab', 1, GETDATE());

-- Insert data into DistrictMaster for Andhra Pradesh (StateID = 1)
INSERT INTO DistrictMaster (DistrictName, StateID, CreateDate)
VALUES
('Anantapur', 1, GETDATE()),
('Chittoor', 1, GETDATE()),
('East Godavari', 1, GETDATE()),
('Guntur', 1, GETDATE()),
('Krishna', 1, GETDATE());

-- Insert data into DistrictMaster for Bihar (StateID = 2)
INSERT INTO DistrictMaster (DistrictName, StateID, CreateDate)
VALUES
('Patna', 2, GETDATE()),
('Gaya', 2, GETDATE()),
('Bhagalpur', 2, GETDATE()),
('Muzaffarpur', 2, GETDATE()),
('Purnia', 2, GETDATE());

-- Insert data into DistrictMaster for Karnataka (StateID = 3)
INSERT INTO DistrictMaster (DistrictName, StateID, CreateDate)
VALUES
('Bengaluru', 3, GETDATE()),
('Mysuru', 3, GETDATE()),
('Hubli-Dharwad', 3, GETDATE()),
('Mangaluru', 3, GETDATE()),
('Kalaburagi', 3, GETDATE());

-- Insert data into DistrictMaster for Maharashtra (StateID = 4)
INSERT INTO DistrictMaster (DistrictName, StateID, CreateDate)
VALUES
('Mumbai', 4, GETDATE()),
('Pune', 4, GETDATE()),
('Nagpur', 4, GETDATE()),
('Nashik', 4, GETDATE()),
('Aurangabad', 4, GETDATE());

-- Insert data into DistrictMaster for Uttar Pradesh (StateID = 5)
INSERT INTO DistrictMaster (DistrictName, StateID, CreateDate)
VALUES
('Lucknow', 5, GETDATE()),
('Kanpur', 5, GETDATE()),
('Agra', 5, GETDATE()),
('Varanasi', 5, GETDATE()),
('Ghaziabad', 5, GETDATE());

-- Insert data into DistrictMaster for Tamil Nadu (StateID = 6)
INSERT INTO DistrictMaster (DistrictName, StateID, CreateDate)
VALUES
('Chennai', 6, GETDATE()),
('Coimbatore', 6, GETDATE()),
('Madurai', 6, GETDATE()),
('Tiruchirappalli', 6, GETDATE()),
('Salem', 6, GETDATE());

-- Insert data into DistrictMaster for West Bengal (StateID = 7)
INSERT INTO DistrictMaster (DistrictName, StateID, CreateDate)
VALUES
('Kolkata', 7, GETDATE()),
('Howrah', 7, GETDATE()),
('Siliguri', 7, GETDATE()),
('Asansol', 7, GETDATE()),
('Durgapur', 7, GETDATE());

-- Insert data into DistrictMaster for Kerala (StateID = 8)
INSERT INTO DistrictMaster (DistrictName, StateID, CreateDate)
VALUES
('Thiruvananthapuram', 8, GETDATE()),
('Kochi', 8, GETDATE()),
('Kozhikode', 8, GETDATE()),
('Malappuram', 8, GETDATE()),
('Thrissur', 8, GETDATE());

-- Insert data into DistrictMaster for Rajasthan (StateID = 9)
INSERT INTO DistrictMaster (DistrictName, StateID, CreateDate)
VALUES
('Jaipur', 9, GETDATE()),
('Udaipur', 9, GETDATE()),
('Jodhpur', 9, GETDATE()),
('Kota', 9, GETDATE()),
('Ajmer', 9, GETDATE());

-- Insert data into DistrictMaster for Punjab (StateID = 10)
INSERT INTO DistrictMaster (DistrictName, StateID, CreateDate)
VALUES
('Amritsar', 10, GETDATE()),
('Ludhiana', 10, GETDATE()),
('Jalandhar', 10, GETDATE()),
('Patiala', 10, GETDATE()),
('Mohali', 10, GETDATE());


CREATE TABLE ClassMaster (
    ClassMasterID int IDENTITY(1,1) NOT NULL,
    ClassName nvarchar(100) NULL,
    IsActive bit NULL,
    CreateDate datetime NULL,
    ModifyDate datetime NULL
);


INSERT INTO ClassMaster (ClassName, IsActive, CreateDate)
VALUES
    ('Mathematics', 1, GETDATE()),
    ('Physics', 1, GETDATE()),
    ('Chemistry', 1, GETDATE()),
    ('Biology', 0, GETDATE()),
    ('English', 1, GETDATE());

