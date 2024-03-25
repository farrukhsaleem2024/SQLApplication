CREATE DATABASE appdb
use appdb
Create Table Products(ProductID int,ProductNamevarchar(1000),Quantity int);
insert into Products(ProductID,ProductName,Quantity)
Values(1,'Mobile',100),(2,'Laptop',200),(3,'Tabs',300);