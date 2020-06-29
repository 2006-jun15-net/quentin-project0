CREATE SCHEMA StoreDB
GO

CREATE TABLE [Location](
Id int PRIMARY KEY NOT NULL IDENTITY(1, 1),
Name nvarchar(100) NOT NULL

)

CREATE TABLE [CUSTOMER](
Id int PRIMARY KEY NOT NULL IDENTITY(1, 1),
FirstName nvarchar(50) NOT NULL,
LastName nvarchar(50),
DefaultLocationId int NOT NULL foreign key ([DefaultLocationId]) references [Location]([Id]),

)

CREATE TABLE [Product](
Id int PRIMARY KEY NOT NULL IDENTITY(1, 1),
Price money not null,
Name nvarchar(250)
)

CREATE TABLE [Inventory](
Qty int not null,
ProductId int not null foreign key ([ProductId]) references [Product]([Id]),
LocationId int not null foreign key ([LocationId]) references [Location]([Id]),
PRIMARY KEY(ProductId, LocationId)
)

CREATE TABLE [Order](
Id int PRIMARY KEY NOT NULL IDENTITY(1, 1),
CustomerId int not null foreign key ([CustomerId]) references [Customer]([Id]),
LocationId int not null,
Date datetime not null,
)

CREATE TABLE [OrderLineItems](
Id int PRIMARY KEY NOT NULL IDENTITY(1, 1),
Qty int not null, 
OrderId int not null foreign key ([OrderId]) references [Order]([Id]),
ProductId int not null foreign key ([ProductId]) references [Product]([Id])
)
