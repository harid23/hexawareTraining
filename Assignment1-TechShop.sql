CREATE DATABASE TechShop ;

use TechShop;
--creating customer table
create table Customers(
CID int primary key,
First_Name varchar(20),
Last_Name varchar(20),
PhoneNo int,
Email_Id varchar(30),
Address varchar(50),
Pin_Code int,
City varchar(10)
);

--creating product table
create table Products(
PID int Primary key,
Product_Name varchar(20),
Description varchar(50),
Price int
);

--creating orders table
create table Orders(
OrderID int primary key,
CID int,
foreign key (CID) references Customers(CID),
OrderDate date,
TotalAmount int
);

--creating orderdetails table
create table OrderDetails(
OrderDetailsID int primary key,
OrderID int,
PID int,
foreign key (OrderID) references Orders(OrderID),
foreign key (PID) references Products(PID),
Quantity int
);

--creating Inventory table
--create table Inventory(
--InventoryID int primary key,
--foreign key (PID) references Products(PID),);

--inserting customers details
insert into Customers values(101, 'Alice','Smith', 987654321,'alicesmith@email.com','4 MG Road,Old Street,Guindy',600015, 'Chennai');
insert into Customers values(102, 'Rahul','Kumar', 987654322,'rahul@email.com','4 MG Road,Old Street,Koymbedu',600107 , 'Chennai');
insert into Customers values(103, 'Dilip','venkat', 987654323,'dilip@email.com','4 MG Road,Old Street,Chennai',600002 , 'Chennai');
insert into Customers values(104, 'charan','Srijith', 987654324,'charan@email.com','4 MG Road,Old Street,Electronic city', 560100, 'Bangalore');
insert into Customers values(105, 'Ashwin','Kumar', 987654325,'ashwin@email.com','4 MG Road,Old Street,Adyar',600020 , 'Chennai');
insert into Customers values(106, 'Venu','Aravind', 987654326,'aravind@email.com','4 MG Road,Old Street,Hyderbad',500034, 'Hyderbad');
insert into Customers values(107, 'Hari','Haran', 987654327,'hari@email.com','4 MG Road,Old Street,Madipakkam',600091, 'Chennai');
insert into Customers values(108, 'Madhan','Kumar', 987654328,'madhan@email.com','4 MG Road,Old Street,Bangalore', 560001, 'Bangalore');
insert into Customers values(109, 'Prasanna','Kumar', 987654329,'prasanna@email.com','4 MG Road,Old Street,Avadi',600071 , 'Chennai');
insert into Customers values(110, 'Prakash','Raj', 987654320,'prakash@email.com','4 MG Road,Old Street,Delhi', 110054, 'Delhi');

--inserting products details
Insert into Products Values (1, 'Laptop', '13-inch, 8GB RAM, 512GB SSD,i5-11th Gen', 1200);
Insert into Products Values (2, 'Laptop', '14-inch, 12GB RAM, 512GB SSD,Iris', 2500);
Insert into Products Values (3, 'Laptop', '15-inch, 12GB RAM, 512GB SSD,Iris Xe', 3000);
Insert into Products Values (4, 'Laptop', '14.5-inch,16GB RAM, 512GB SSD,RTX2050', 4500);
Insert into Products Values (5, 'Laptop', '15-inch, 12GB RAM, 512GB SSD,RTX3050,i5-11th Gen', 6200);
Insert into Products Values (6, 'Laptop', '16-inch, 16GB RAM, 512GB SSD,RTX3050,i5-12th Gen', 7500);
Insert into Products Values (7, 'Laptop', '17-inch, 16GB RAM, 512GB SSD,RTX4060,i5-13th Gen', 8200);
Insert into Products Values (8, 'Laptop', '15-inch, 8GB RAM, 512GB SSD,Ryzan5', 4000);
Insert into Products Values (9, 'Laptop', '14-inch, 12GB RAM, 512GB SSD,Ryzan7', 4500);
Insert into Products Values (10, 'Laptop', '16-inch, 16GB RAM, 512GB SSD,Iris Xe,Ryzan7', 4900);

--inserting orders table
Insert into Orders Values (1001, 101, '2025-01-27', 1200);
Insert into Orders Values (1002, 103, '2025-03-04', 6000);
Insert into Orders Values (1003, 105, '2025-02-23', 6200);
Insert into Orders Values (1004, 107, '2025-01-12', 8200);
Insert into Orders Values (1005, 109, '2025-02-19', 8000);
Insert into Orders Values (1006, 110, '2025-01-30', 7500);
Insert into Orders Values (1007, 102, '2025-01-30', 2500);
Insert into Orders Values (1008, 104, '2025-01-30', 4500);
Insert into Orders Values (1009, 106, '2025-01-30', 4900);
Insert into Orders Values (1010, 108, '2025-01-30', 4000);
--Insert into Orders Values (1007, 102, '2024-02-30', 8000);

--inserting orderdetails table
Insert into OrderDetails Values (10001, 1001, 1, 1);
Insert into OrderDetails Values (10002, 1002, 3, 2);
Insert into OrderDetails Values (10003, 1003, 5, 1);
Insert into OrderDetails Values (10004, 1004, 7, 1);
Insert into OrderDetails Values (10005, 1005, 8, 2);
Insert into OrderDetails Values (10006, 1006, 6, 1);
Insert into OrderDetails Values (10007, 1007, 2, 1);
Insert into OrderDetails Values (10008, 1008, 9, 1);
Insert into OrderDetails Values (10009, 1009, 10, 1);
Insert into OrderDetails Values (10010, 1010, 8, 1);
