Create database Baitap
Go
Use Baitap
Go

Create table Acount(
  id int identity primary key,
  username nvarchar(100),
  password nvarchar(20),
  role nvarchar(20) 
)
Go
INSERT dbo.Acount ([username], [password], [role]) VALUES (N'AdminRoot', N'123456', N'ADMIN')
INSERT dbo.Acount ([username], [password], [role]) VALUES (N'PhamDuyen', N'123456789', N'CASHIER')
Select *from Acount
Go
Create table Category
(
	CategoryId int primary key,
	CategoryName nvarchar(100),
	Discription nvarchar(100)
)
Go
Create table Product
(
	ProductId int identity primary key,
	ProductName nvarchar(100),
	Price float,
	CategoryId int foreign key references Category(CategoryId),
)
Go

Create table Department
(
	DepId int primary key identity, 
	DepName nvarchar(100) unique not null, 
	TotalEmployee int default 0 
)
go

Create table Employee(
	EmployeeId nvarchar(20) primary key,
	Name nvarchar(100),
	Email nvarchar(100),
	Phone nvarchar(100),
	Address nvarchar(100),
	DateStart datetime,
	EndStart datetime,
	DepId int foreign key references Department(DepId)
)
Go
Create table Orders(
	OrderId int identity primary key,
	Status int default 0 ,
	startDate date default GetDate() ,
	enddate date ,
	Price float default 0,
	Discount int default 0
)
Go

create table OrderDetail(
	STT int identity primary key,
	OrderId int foreign key references Orders(OrderId),
	ProductId int foreign key references Product(ProductId),
	Quantity int,
	Size nvarchar(5)
)
Go

select * from Orders

select * from OrderDetail
go
create proc USP_GetListOrder
	@idOrder int
as
begin
	select 
		pro.ProductName,
		pro.Price,
		od.Size,
		od.Quantity,
		pro.Price*od.Quantity as 'TotalPrice'
	from OrderDetail od
	join Product pro on od.ProductId = pro.ProductId
	join Orders odr on odr.OrderId = od.OrderId
	where odr.Status = 0 and od.OrderId = @idOrder
end

exec USP_GetListOrder 7

select MAX(OrderId) from Orders where Status = 0

select  * from OrderDetail