create database [Conditer_DataBase]
go

use [Conditer_DataBase]
go

create table [dbo].[Supplyer] 
(
	[Name_Supplyer] [varchar] (50) not null PRIMARY KEY,
	[Supplyer_Address] [varchar] (70) null,
	[Supply_Deadline] [date] not null default (format(Getdate(),'dd.MM.yyyy'))
)
go

create table [dbo].[Decoration] 
(
	[Dec_Vendor_Code] [varchar] (10) not null PRIMARY KEY,
	[Name_Dec] [varchar] (100) not null,
	[Dec_Unit] [varchar] (15) not null,
	[Dec_Quantity] [float] not null,
	[Main_Supplyer] [varchar] (50) null,
	[Dec_Picture] [varchar] (100) null,
	[Type_Dec] [varchar] (20) not null,
	[Dec_Purchase_Price] [float] null,
	[Dec_Weight] [varchar] (10) null,
	constraint [FK_Main_Supplyer] foreign key ([Main_Supplyer])
	references [dbo].[Supplyer] ([Name_Supplyer])
)
go

drop table [dbo].[Decoration]

select * from [dbo].[Decoration] 
 
create table [dbo].[Ingridient] 
	(
		[Ingr_Vendor_Code] [varchar] (10) not null PRIMARY KEY,
		[Name_Ingr] [varchar] (150) not null,
		[Ingr_Unit] [varchar] (15) null,
		[Ingr_Quantity] [float] null,
		[Main_Supplyer] [varchar] (50) null,
		[Ingr_Picture] [varchar] (100) null,
		[Type_Ingr] [varchar] (50) null,
		[Ingr_Purchase_Price] [float] null,
		[Gost] [varchar] (50) null,
		[Ingr_Packaging] [varchar] (50) null,
		[Ingr_Characteristic] [varchar] (150) null,
		constraint [FK_Main_Ingr_Supplyer] foreign key ([Main_Supplyer])
		references [dbo].[Supplyer] ([Name_Supplyer])
	)
	go

drop table [dbo].[Ingridient] 

	select * from [dbo].[Ingridient]


create table [dbo].[Product]
(
	[Name_Product] [varchar] (50) not null PRIMARY KEY,
	[Prodict_Size] [varchar] (50) not null,
	[Discriprion] [varchar] (250) NULL
)
go

select * from [dbo].[Product]

create table [dbo].[Cake_Decoration_Specification]
(
	[Name_Product_Dec] [varchar] (50) not null,
	[Code_Dec] [varchar] (10) not null,
	[Dec_Sp_Quantity] [float] not null,
	constraint [PK_Cake_Decoration_Specification_Specification] primary key ([Name_Product_Dec], [Code_Dec]),
	constraint [FK_Name_Product_Dec_Sp] foreign key ([Name_Product_Dec])
	references [dbo].[Product] ([Name_Product]),
	constraint [FK_Name_Dec_Sp] foreign key ([Code_Dec])
	references [dbo].[Decoration] ([Dec_Vendor_Code]),
)
go

create table [dbo].[Ingridients_Specification]
(
	[Ingr_Code] [varchar] (10) not null,
	[Name_Product_Sp] [varchar] (50) not null,
	[Ingr_Sp_Quantity] [float] not null,
	constraint [PK_Ingridients_Specification] primary key ([Ingr_Code], [Name_Product_Sp]),
	constraint [FK_Name_Product_Ingr_Sp] foreign key ([Name_Product_Sp])
	references [dbo].[Product] ([Name_Product]),
	constraint [FK_Name_Ingr_Sp] foreign key ([Ingr_Code])
	references [dbo].[Ingridient] ([Ingr_Vendor_Code])
)
go

create table [dbo].[Semimanufactures_Specification]
(
	[Name_Product_Ingr] [varchar] (50) not null,
	[Code_Ingr] [varchar] (10) not null,
	[Ingr_Sp_Quantity] [float] not null,
	constraint [PK_Semimanufactures_Specification] primary key ([Name_Product_Ingr], [Code_Ingr]),
	constraint [FK_Semimanufactures_Specification_Name_Product_Ingr] foreign key ([Name_Product_Ingr])
	references [dbo].[Product] ([Name_Product]),
	constraint [FK_Semimanufactures_Specification_Code_Ingr] foreign key ([Code_Ingr])
	references [dbo].[Ingridient] ([Ingr_Vendor_Code])
)
go

create table [dbo].[Type_Equipment]
(
	[Name_Type_Equipment] [varchar] (50) not null PRIMARY KEY
)
go

insert into [dbo].[Type_Equipment] ([Name_Type_Equipment]) values ('Печь')

create table [dbo].[Equipment]
(
	[Marking] [varchar] (14) not null PRIMARY KEY,
	[Type_Equip] [varchar] (50) not null,
	[Equip_Characteristic] [varchar] (150) null,
	constraint [FK_Type_Equipment] foreign key ([Type_Equip])
	references [dbo].[Type_Equipment] ([Name_Type_Equipment])
)
go

create table [dbo].[Operation_Specification]
(
	[Sp_Op_Product] [varchar] (50) not null,
	[Name_Operation] [varchar] (30) not null,
	[Serial_Number] [varchar] (10) not null,
	[Type_Equip] [varchar] (50) null,
	[Time_For_Operation] [datetime] not null,
	constraint [PK_Operation_Specification] primary key ([Sp_Op_Product], [Name_Operation], [Serial_Number]),
	CONSTRAINT [CH_Serial_Op_Number] CHECK ([Serial_Number] LIKE '%[0-9]%'),
	constraint [FK_Op_Product] foreign key ([Sp_Op_Product])
	references [dbo].[Product] ([Name_Product]),
	constraint [FK_Type_Equipment_Op] foreign key ([Type_Equip])
	references [dbo].[Type_Equipment] ([Name_Type_Equipment])
)
go

create table [dbo].[User]
(
	[Login] [varchar] (32) not null unique,
	[Password] [varchar] (32) not null,
	[Role] [varchar] (50) not null,
	[FIO] [varchar] (100) null,
	[Photo] [varchar] (200) null,
	constraint [PK_User] primary key ([Login], [Password])
)
go

UPDATE [dbo].[User]
SET [FIO] = REPLACE([FIO], '  ', ' ')
WHERE [FIO] LIKE '%  %';

ALTER TABLE [dbo].[User]
ADD CONSTRAINT [UQ_User_Login] UNIQUE ([Login]);

select * from [dbo].[User]

ALTER TABLE [dbo].[User] ALTER COLUMN [Photo] VARCHAR(500)

ALTER TABLE [dbo].[Order]
ADD CONSTRAINT PK_Order PRIMARY KEY ([Ord_Numb]);
GO


create table [dbo].[Order]
(
	[Ord_Numb] [varchar] (12) not null,
	[Ord_Date] [date] not null default Getdate(),
	[Name_Ord] [varchar] (50) not null,
	[Customer] [varchar] (32) not null,
	[Manager] [varchar] (32) null,
	[Price] [float] null,
	[Finish_Date] [date] null default format(Getdate(),'dd.MM.yyyy'),
	[Examples] [varchar] (200) null,
	[Status] [varchar] (50) not null,
	[Ord_Product] [varchar] (50) not null,
	constraint [PK_Order] primary key ([Ord_Numb], [Ord_Date]),
	constraint [FK_Customer] foreign key ([Customer])
	references [dbo].[User] ([Login]),
	constraint [FK_Manager] foreign key ([Manager])
	references [dbo].[User] ([Login]),
	constraint [FK_Product] foreign key ([Ord_Product])
	references [dbo].[Product] ([Name_Product])
)
go

create table [dbo].[Equipment_Failures]
(
	[Start_Date][date] not null default format(Getdate(),'dd.MM.yyyy'),
	[End_Date][date] null default format(Getdate(),'dd.MM.yyyy'),
	[Start_Time][time] not null,
	[End_Time][time] null,
	[Reason][varchar](150) not null,
	[Equip] [varchar] (50) not null,
	constraint [FK_Equip] foreign key ([Equip])
	references [dbo].[Equipment] ([Marking])
)
go

-- Добавление нового столбца Id_Equip_Fail с автоинкрементом и ограничением
ALTER TABLE [dbo].[Equipment_Failures]
ADD [Id_Equip_Fail] INT NOT NULL IDENTITY(1,1);

-- Создание ограничения первичного ключа
ALTER TABLE [dbo].[Equipment_Failures]
ADD CONSTRAINT [PK_Equip_Fail] PRIMARY KEY CLUSTERED ([Id_Equip_Fail] ASC);


ALTER TABLE [dbo].[Order]
ADD CONSTRAINT UK_Order_Ord_Numb UNIQUE ([Ord_Numb]);
GO
drop column [Ord_Name_Product]

create table [dbo].[Order_Specification]
(
	[Id_Order_Specification] int IDENTITY(1,1)  primary key,
	[Indridients] [varchar](max) not null,
	[Decorations] [varchar](max) not null,
	[Operations] [varchar](max) not null,
	[Ord_Numb] [varchar] (12) not null,
	constraint [FK_Ord_Spec] foreign key ([Ord_Numb])
	references [dbo].[Order] ([Ord_Numb])
)
go




