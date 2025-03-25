use TechShop;

--Tasks 2: Select, Where, Between, AND, LIKE:  

--1. Write an SQL query to retrieve the names and emails of all customers.  
select First_Name,Last_Name,Email_Id from Customers;

--2. Write an SQL query to list all orders with their order dates and corresponding customer names.
select OrderID,OrderDate, First_Name,Last_Name from Orders, Customers
where Orders.CID = Customers.CID;

--3. Write an SQL query to insert a new customer record into the "Customers" table. Include customer information such as name, email, and address.
Insert into Customers Values(111, 'Surya','Kumar', 987654322,'surya@email.com','4 MG Road,Old Street,Guindy',600015, 'Chennai');
select *from Customers;

--4. Write an SQL query to update the prices of all electronic gadgets in the "Products" table by increasing them by 10%.
update Products set Price = Price+(Price*0.10);

--5. Write an SQL query to delete a specific order and its associated order details from the "Orders" and "OrderDetails" tables. Allow users to input the order ID as a parameter.
Delete from Orderdetails where OrderId = 1003;
Delete from Orders where OrderId = 1003;
select *from OrderDetails;
select * from Orders;

--6. Write an SQL query to insert a new order into the "Orders" table. Include the customer ID, order date, and any other necessary information.
Insert into Customers values(112, 'John', 'Lubin',987654323, 'john@email.com', '4 MG Road,Old Street,Guindy',600015,'Chennai');
Insert into Orders values(11, 112, '2022-03-19', 1800);

--7. Write an SQL query to update the contact information (e.g., email and address) of a specific customer in the "Customers" table. Allow users to input the customer ID and new contact information.
Update Customers set Email_Id= 'kumar@email.com', PhoneNo =987654323 , Address= '4 MG Road,Old Street,Koymbedu'where CID = 111;

--8. Write an SQL query to recalculate and update the total cost of each order in the "Orders" table based on the prices and quantities in the "OrderDetails" table.


--9. Write an SQL query to delete all orders and their associated order details for a specific customer from the "Orders" and "OrderDetails" tables. Allow users to input the customer ID as a parameter.
delete from OrderDetails 
where OrderID IN(
select OrderID from Orders 
where CID=101 );
delete Orders where CID =101;


--10. Write an SQL query to insert a new electronic gadget product into the "Products" table, including product name, category, price, and any other relevant details.
 Insert into Products values(11, 'Laptop', '14.5-inch, 8GB RAM, 512GB SSD,Iris', 5999); 

--11. Write an SQL query to update the status of a specific order in the "Orders" table (e.g., from "Pending" to "Shipped"). Allow users to input the order ID and the new status.
alter table Orders add Status varchar(10);
Update Orders SET Status = 'COMPLETED' 
where OrderID= @CID;


--12. Write an SQL query to calculate and update the number of orders placed by each customer in the "Customers" table based on the data in the "Orders" table.
/*alter table customers add ordercount int;
Update Customers set ordercount = (
select COUNT(*)from Orders
where Orders.CID = Customers.CID);*/

alter table customers drop column ordercount;


