CREATE DATABASE RestaurantProjectMVC01
GO 
Use RestaurantProjectMVC01
GO

--Restaurang tabellen

CREATE TABLE Restaurant (
UserId int not null References [dbo].[UserProfile](UserId),
Id uniqueidentifier primary key not null,
Name nvarchar(100) not null,
[Description] nvarchar(500),
[Address] nvarchar(30) not null,
Zipcode int not null,
Phone nvarchar(100),
City nvarchar (30),
TotalSeats int,
Capacity int,
MaxSeatPerBooking int,
Rating nvarchar(10),
Photo nvarchar(max),
Email nvarchar(50),
Activated bit not null
)

--Tables Tabellen

CREATE TABLE [Table](
RestaurantId uniqueidentifier not null references Restaurant(Id),
Id int not null primary key identity(1,1),
TableName nvarchar(50) not null,
Seats int not null,
)

--Avaliable reservation Tabellen
Create TABLE OpenForBooking(
RestaurantId uniqueidentifier not null references Restaurant(Id),
Id int identity(1,1) primary key not null,
[Day] nvarchar(50) NOT NULL,
[StartTime] TIME not null,
[EndTime] TIME not null,
)

--Closed for reservation Tabellen

CREATE TABLE ClosedForBooking(
RestaurantId uniqueidentifier not null references Restaurant(id),
Id int not null primary key identity(1,1),
[Day] nvarchar(50) not null,
[StartTime] TIME not null,
[EndTime] TIME not null,
)

--Bokade bord Tabellen
CREATE TABLE BookedTable(
TableId int NOT NULL references [Table](Id),
Id int identity(1,1) not null primary key,
[Date] DATETIME NOT NULL,
)


--Reservation Tabellen

CREATE TABLE Reservation(
RestaurantId uniqueidentifier not null references Restaurant(Id),
Id int identity(1,1) primary key not null,
TableId int not null references [Table](Id),
[Day] nvarchar(50) not null,
[Date] DATETIME not null,
CustomerName nvarchar(50) not null,
CustomerPhoneNumber nvarchar(50) not null,
ContactEmail nvarchar(50) not null,
TotalGuests int not null,
ConfirmedBooking bit not null Default(0)
)