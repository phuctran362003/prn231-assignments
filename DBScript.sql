-- Insert data into HealthGuideCategories table
INSERT INTO HealthGuideCategories (Name, Description, IsActive)
VALUES 
('Vaccination', 'Information about various vaccines and immunization schedules', 1),
('Child Health', 'Guidelines for maintaining child health and development', 1),
('Adult Health', 'Health recommendations for adults of different age groups', 1);

-- Insert data into Roles table (using square brackets because Role is a reserved keyword)
INSERT INTO Role (RoleName, IsDeleted, CreatedAt, CreatedBy, Id)
VALUES 
('Admin', 0, GETDATE(), NULL, 1),
('Doctor', 0, GETDATE(), NULL, 2),
('Patient', 0, GETDATE(), NULL, 3);

-- Insert data into Users table (using square brackets because User is a reserved keyword)
INSERT INTO [User](Id, FullName, Email, Gender, DateOfBirth, ImageUrl, PhoneNumber, PasswordHash, RoleName, Address, IsDeleted, CreatedAt, RoleId)
VALUES 
(NEWID(), 'John Doe', 'john.doe@example.com', 1, '1985-05-15', 'images/john.jpg', '1234567890', 'hashed_password_1', 'Admin', '123 Main St, City', 0, GETDATE(), 1),
(NEWID(), 'Jane Smith', 'jane.smith@example.com', 0, '1990-08-20', 'images/jane.jpg', '9876543210', 'hashed_password_2', 'Doctor', '456 Oak Ave, Town', 0, GETDATE(), 2),
(NEWID(), 'Alex Johnson', 'alex.johnson@example.com', 1, '1978-03-10', 'images/alex.jpg', '5555555555', 'hashed_password_3', 'Patient', '789 Pine Blvd, Village', 0, GETDATE(), 3);

-- Insert data into HealthGuides table
INSERT INTO HealthGuides (Title, Content, HealthGuideCategorieId, Author, CreatedAt, IsActive, Views, ImageUrl)
VALUES 
('COVID-19 Vaccination Guide', 'Comprehensive information about COVID-19 vaccines, their efficacy, and potential side effects.', 1, 'Dr. Jane Smith', GETDATE(), 1, 1250, 'images/covid-vaccine.jpg'),
('Childhood Immunization Schedule', 'Recommended vaccination schedule for children from birth to 18 years old.', 1, 'Dr. Robert Brown', DATEADD(DAY, -15, GETDATE()), 1, 980, 'images/child-vaccine.jpg'),
('Maintaining Heart Health', 'Guidelines for maintaining cardiovascular health through diet, exercise, and regular check-ups.', 3, 'Dr. Emily Wilson', DATEADD(DAY, -30, GETDATE()), 1, 750, 'images/heart-health.jpg');