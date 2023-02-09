use master

create database BookDB
go
use BookDB
go

create table Admins
(
Id int primary key not null identity(1,1),
Username nvarchar(50),
[Password] nvarchar(50),
[Name] nvarchar(50)
)
go

create table Customers
(
Id int primary key not null identity(1,1),
Username nvarchar(50),
[Password] nvarchar(50),
[Name] nvarchar(50),
Phone varchar(50),
Email varchar(50)
)
go

create table Categories
(
Id int primary key not null identity(1,1),
[name] nvarchar(50)
)

create table Books
(
Id int primary key not null identity(1,1),
Title nvarchar(50),
[Description] nvarchar(100),
Detail nvarchar(4000),
Price int,
[Image] nvarchar(500),
CategoryId int foreign key references Categories(Id)
)
go

create table Orders
(
Id int primary key not null identity(1,1),
CustomerId int foreign key references Customers(Id),
[Status] int,
Payment nvarchar(50),
Total int,
[Address] nvarchar(500)
)
go

create table OrdersItem
(
Id int primary key not null identity(1,1),
OrderId int foreign key references Orders(Id),
BookId int foreign key references Books(Id),
Quantity int,
Subtotal int
)
go

Insert into Admins Values ('admin','123','Admin')


