-- "" unconventional variable name in quotes
-- '' strings in single quotes
-- GO documentation - https://docs.microsoft.com/en-us/sql/t-sql/language-elements/sql-server-utilities-statements-go?view=sql-server-ver15

-- ALTER DATABASE "rotoni_softdelete" SET SINGLE_USER WITH ROLLBACK IMMEDIATE;

IF db_id('rotoni_softdelete') IS NOT NULL BEGIN
    USE master
    DROP DATABASE "rotoni_softdelete"
END
GO

CREATE DATABASE "rotoni_softdelete"
GO

USE "rotoni_softdelete"
GO

-- Script for testing soft delete/update between 1:m tables
-- Create basic table structure for one to many relationship
CREATE TABLE Pizza_Type (
    Id      INT             NOT NULL    IDENTITY,
    Name    VARCHAR(128)    NOT NULL,
	Price	Decimal(5,2)	NOT NULL,
)

CREATE TABLE Default_Topping (
    Id					INT             NOT NULL    IDENTITY ,
    Topping				Varchar(128)	NOT NULL,
	Pizza_type_id		INT				NOT NULL,
	Pizza_type_deleted	DATETIME2		DEFAULT '9000-01-01' NOT NULL
)

-- Add metadata for soft delete
ALTER TABLE Pizza_Type		ADD DeletedAt DATETIME2 DEFAULT '9000-01-01' NOT NULL
ALTER TABLE Pizza_Type		ADD CreatedAt DATETIME2 NOT NULL
ALTER TABLE Default_Topping ADD DeletedAt DATETIME2 DEFAULT '9000-01-01' NOT NULL
ALTER TABLE Default_Topping ADD CreatedAt DATETIME2 NOT NULL

-- add index on metadata and ID
CREATE INDEX DeletedAt_idx ON Pizza_Type		( DeletedAt );
CREATE INDEX CreatedAt_idx ON Pizza_Type		( CreatedAt );
CREATE INDEX DeletedAt_idx ON Default_Topping	( DeletedAt );
CREATE INDEX CreatedAt_idx ON Default_Topping	( CreatedAt );
CREATE INDEX ID_idx ON Pizza_Type				( Id );
CREATE INDEX ID_idx ON Default_Topping			( Id );

--add COMPOSITE FKs and PKs
ALTER TABLE Pizza_Type		ADD CONSTRAINT PK_Pizza_Type		PRIMARY KEY (Id, DeletedAt)
ALTER TABLE Default_Topping ADD CONSTRAINT PK_Default_Topping	PRIMARY KEY (Id, DeletedAt) 
ALTER TABLE Default_Topping ADD CONSTRAINT FK_Pizza_Type		FOREIGN KEY (Pizza_type_id, Pizza_type_deleted)
	REFERENCES Pizza_Type (Id, DeletedAt) ON DELETE CASCADE ON UPDATE CASCADE

DECLARE @Time1 DATETIME2
SELECT @Time1 = '2020-01-01'

-- add data to the table

INSERT INTO Pizza_Type (Name, Price, CreatedAt) VALUES ('Hawaii Pizza', 8.50, @Time1)
INSERT INTO Pizza_Type (Name, Price, CreatedAt) VALUES ('Americana Pizza', 9.50, @Time1)
SELECT @Time1 = '2020-01-02'
INSERT INTO Pizza_Type (Name, Price, CreatedAt) VALUES ('Peperoni Pizza', 7.50, @Time1)

SELECT @Time1 = '2020-01-03'
INSERT INTO Default_Topping (Topping, CreatedAt, Pizza_type_id, Pizza_type_deleted) 
	VALUES ('Pineapple', @Time1, 1, '9000-01-01')
INSERT INTO Default_Topping (Topping, CreatedAt, Pizza_type_id, Pizza_type_deleted) 
	VALUES ('Peperoni', @Time1, 3, '9000-01-01')
SELECT @Time1 = '2020-01-05'
INSERT INTO Default_Topping (Topping, CreatedAt, Pizza_type_id, Pizza_type_deleted) 
	VALUES ('Salami', @Time1, 2, '9000-01-01')

SELECT 'Initial soft data'
-- Select all data before updating/deleting
SELECT * FROM Pizza_Type
SELECT * FROM Default_Topping


-- soft update
DECLARE @Time2 DATETIME2
DECLARE @id INT
SELECT @Time2 = '2020-01-11'
	--get the id for the record/s to upadte
SELECT @id = Id FROM Pizza_Type Where Name = 'Hawaii Pizza'

	--make a copy of the record as a deleted record
SET IDENTITY_INSERT Pizza_Type ON 
insert into Pizza_Type(id, DeletedAt, Name, Price, CreatedAt)
	select @id, @Time2, Name, Price, CreatedAt
	from Pizza_Type
	where Id = @id
SET IDENTITY_INSERT Pizza_Type OFF  

	--change the original record values
UPDATE Pizza_Type SET Name='TEST PIZZA' WHERE Id=@id AND DeletedAt > CURRENT_TIMESTAMP

SELECT 'Full Data after soft update'
-- All not updated Pizza_Type records
SELECT * FROM Pizza_Type WHERE DeletedAt > CURRENT_TIMESTAMP
-- All Pizza_Type records including pre-updated
SELECT * FROM Pizza_Type
-- All not updated Default_Topping records
SELECT * FROM Default_Topping WHERE DeletedAt > CURRENT_TIMESTAMP
-- Check if updated children point to correct parent
SELECT t.Pizza_type_id as 'Default_topping_Pizza_ID', p.id as 'Pizza_ID', Topping as 'Topping', p.Name as 'Pizza', p.Price as 'Price' FROM Default_Topping t
	JOIN Pizza_Type p ON t.Pizza_type_id = p.Id AND t.Pizza_type_deleted = p.DeletedAt 
	AND p.DeletedAt > CURRENT_TIMESTAMP AND t.DeletedAt > CURRENT_TIMESTAMP


-- soft delete
SELECT @Time1 = '2020-01-15'
SELECT @id = Id FROM Pizza_Type Where Name = 'Americana Pizza'
	--delete the record by setting value to DeletedAt
UPDATE Pizza_Type SET DeletedAt=@Time1 WHERE Id=@id AND DeletedAt > CURRENT_TIMESTAMP
	--also need to delete the record from child table by setting value to DeletedAt
UPDATE Default_Topping SET DeletedAt=@Time1 WHERE Pizza_type_id=@id AND DeletedAt > CURRENT_TIMESTAMP

SELECT 'Full Data after soft delete'
-- All not deleted Pizza_Type records
SELECT * FROM Pizza_Type WHERE DeletedAt > CURRENT_TIMESTAMP
-- All not deleted Default_Topping records
SELECT * FROM Default_Topping WHERE DeletedAt > CURRENT_TIMESTAMP
-- Check the remaining relationships between children and parents
SELECT t.Pizza_type_id as 'Default_topping_Pizza_ID', p.id as 'Pizza_ID', t.Topping as 'Topping', p.Name as 'Pizza', p.Price as 'Price' FROM Default_Topping t
	JOIN Pizza_Type p ON t.Pizza_type_id = p.Id AND t.Pizza_type_deleted = p.DeletedAt 
	AND p.DeletedAt > CURRENT_TIMESTAMP AND t.DeletedAt > CURRENT_TIMESTAMP






-- Script for testing soft delete/update between 1:0-1 tables
-- Create a table for 1 to 0-1 relationship
CREATE TABLE Pizza_Image (
    Id					INT									NOT NULL    IDENTITY,
    Image				VARCHAR(128)						NOT NULL,
	-- UNIQUE to make it 1:0-1
	Pizza_type_id		INT									NOT NULL	UNIQUE,
	Pizza_type_deleted	DATETIME2	DEFAULT '9000-01-01'	NOT NULL
)
-- Add metadata for soft delete
ALTER TABLE Pizza_Image		ADD DeletedAt DATETIME2 DEFAULT '9000-01-01' NOT NULL
ALTER TABLE Pizza_Image		ADD CreatedAt DATETIME2						 NOT NULL

-- add index on metadata and ID
CREATE INDEX DeletedAt_idx ON Pizza_Image		( DeletedAt );
CREATE INDEX CreatedAt_idx ON Pizza_Image		( CreatedAt );

--add COMPOSITE FK and PK
ALTER TABLE Pizza_Image		ADD CONSTRAINT PK_Pizza_Image	PRIMARY KEY (Id, DeletedAt)
ALTER TABLE Pizza_Image		ADD CONSTRAINT FK_IMAGE_Pizza_Type	FOREIGN KEY (Pizza_type_id, Pizza_type_deleted)
	REFERENCES Pizza_Type (Id, DeletedAt) ON DELETE CASCADE ON UPDATE CASCADE

-- add data to the table
SELECT @Time1 = '2020-02-02'

INSERT INTO Pizza_Type (Name, Price, CreatedAt) VALUES ('My Pizza', 10.50, @Time1)
INSERT INTO Pizza_Type (Name, Price, CreatedAt) VALUES ('Your Pizza', 9.50, @Time1)
SELECT @Time1 = '2020-02-03'
INSERT INTO Pizza_Type (Name, Price, CreatedAt) VALUES ('Their Pizza', 8.50, @Time1)

SELECT @Time1 = '2020-02-04'
INSERT INTO Pizza_Image (Image, CreatedAt, Pizza_type_id, Pizza_type_deleted) 
	VALUES ('MyPizza.png', @Time1, 4, '9000-01-01')
INSERT INTO Pizza_Image (Image, CreatedAt, Pizza_type_id, Pizza_type_deleted) 
	VALUES ('YourPizza.png', @Time1, 5, '9000-01-01')
SELECT @Time1 = '2020-02-05'
INSERT INTO Pizza_Image (Image, CreatedAt, Pizza_type_id, Pizza_type_deleted) 
	VALUES ('TheirPizza.png', @Time1, 6, '9000-01-01')


SELECT 'Data'
-- Select all data before updating/deleting
SELECT * FROM Pizza_Type WHERE DeletedAt > CURRENT_TIMESTAMP
SELECT * FROM Pizza_Image WHERE DeletedAt > CURRENT_TIMESTAMP

-- soft update
SELECT @Time2 = '2020-02-11'
	--get the id for the record/s to upadte
SELECT @id = Id FROM Pizza_Type Where Name = 'Their Pizza'

	--make a copy of the record as a deleted record
SET IDENTITY_INSERT Pizza_Type ON 
insert into Pizza_Type(id, DeletedAt, Name, Price, CreatedAt)
	select @id, @Time2, Name, Price, CreatedAt
	from Pizza_Type
	where Id = @id
SET IDENTITY_INSERT Pizza_Type OFF  

	--change the original record values
UPDATE Pizza_Type SET Name='Their PizzaXXXXXXX' WHERE Id=@id AND DeletedAt > CURRENT_TIMESTAMP

SELECT 'Full Data after soft update'
-- All not updated Pizza_Type records
SELECT * FROM Pizza_Type WHERE DeletedAt > CURRENT_TIMESTAMP
-- All Pizza_Type records including pre-update records
SELECT * FROM Pizza_Type
-- All not updated Pizza_Image records
SELECT * FROM Pizza_Image WHERE DeletedAt > CURRENT_TIMESTAMP
-- Check if updated children point to correct parent
SELECT t.Pizza_type_id as 'IMAGE_PIZZA_ID', p.id as 'Pizza_ID', Image as 'Image', p.Name as 'Pizza', p.Price as 'Price' FROM Pizza_Image t
	JOIN Pizza_Type p ON t.Pizza_type_id = p.Id AND t.Pizza_type_deleted = p.DeletedAt 
	AND p.DeletedAt > CURRENT_TIMESTAMP AND t.DeletedAt > CURRENT_TIMESTAMP

-- soft delete
SELECT @Time1 = '2020-02-15'
SELECT @id = Id FROM Pizza_Type Where Name = 'Your Pizza'
	--delete the record by setting value to DeletedAt
UPDATE Pizza_Type SET DeletedAt=@Time1 WHERE Id=@id AND DeletedAt > CURRENT_TIMESTAMP
	--also need to delete the record from child table by setting value to DeletedAt
UPDATE Pizza_Image SET DeletedAt=@Time1 WHERE Pizza_type_id=@id AND DeletedAt > CURRENT_TIMESTAMP

SELECT 'Full Data after soft delete'
-- All not deleted Pizza_Type records
SELECT * FROM Pizza_Type WHERE DeletedAt > CURRENT_TIMESTAMP
-- All not deleted Pizza_Image records
SELECT * FROM Pizza_Image WHERE DeletedAt > CURRENT_TIMESTAMP
-- Check the remaining relationships between children and parents
SELECT t.Pizza_type_id as 'IMAGE_Pizza_ID', p.id as 'Pizza_ID', Image as 'Image', p.Name as 'Pizza', p.Price as 'Price' FROM Pizza_Image t
	JOIN Pizza_Type p ON t.Pizza_type_id = p.Id AND t.Pizza_type_deleted = p.DeletedAt 
	AND p.DeletedAt > CURRENT_TIMESTAMP AND t.DeletedAt > CURRENT_TIMESTAMP