set ansi_nulls on
go
set ansi_padding on
go
set quoted_identifier on
go

create database [GKP_DataBase]
go

use [GKP_Database]
go

create table [dbo].[Client]
(
	[Id_Client] [int] not null identity(1,1),
	[Second_Name_Cl] [varchar] (30) not null,
	[First_Name_Cl] [varchar] (20) not null,
	[Middle_Name_Cl] [varchar] (30) not null,
	[Passport_Used_By] [varchar] (70) not null,
	[Date_Of_Issue] [date] null default (format(Getdate(),'dd.MM.yyyy')),
	[Division_Code] [varchar] (7) not null,
	[Gender] [varchar] (3) not null,
	[Date_Of_Burth] [date] null default (format(Getdate(),'dd.MM.yyyy')),
	[Place_Of_Burth] [varchar] (50) not null,
	[Passport_Serries] [int] not null,
	[Id_Passport] [int] not null,
	[Details_Of_Snils] [varchar] (14) not null,
	[Details_Of_The_Mhif_Police] [varchar] (19) not null,
	[Contact_Number] [varchar] (11) not null,
	[Gmail_Cl] [varchar] (50) not null,
	[Login_Cl] [varchar] (32) not null,
	[Password_Cl] [varchar] (32) not null,
	constraint [PK_Client] primary key clustered
	([Id_Client] ASC) on [PRIMARY],
	constraint [CH_Date_Of_Issue] check ([Date_Of_Issue] <= Getdate()),
	constraint [CH_Date_Of_Burth] check ([Date_Of_Issue] <= Getdate()),
	constraint [UQ_Details_Of_Snils] unique ([Details_Of_Snils]),
	constraint [UQ_Details_Of_The_Mhif_Police] unique ([Details_Of_The_Mhif_Police]),
	constraint [UQ_Contact_Number] unique ([Contact_Number]),
	constraint [UQ_Gmail_Cl] unique ([Gmail_Cl]),
	constraint [UQ_Login_Cl] unique ([Login_Cl]),
	constraint [CH_Contact_Number_Count] check (len([Contact_Number])=11),
	constraint [CH_Login_Cl_Count] check (len([Login_Cl])>=5),
	constraint [CH_Password_Cl_Count] check (len([Password_Cl])>=8),
	constraint [CH_Gender] check ([Gender] in ('Жен', 'Муж')),
	constraint [CH_Contact_Number] check ([Contact_Number] like '8[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
	constraint [CH_Division_Code] check ([Division_Code] like '[0-9][0-9][0-9]-[0-9][0-9][0-9]'),
	constraint [CH_Details_Of_Snils] check ([Details_Of_Snils] like '[0-9][0-9][0-9]-[0-9][0-9][0-9]-[0-9][0-9][0-9]-[0-9][0-9]'),
	constraint [CH_Details_Of_The_Mhif_Police] check ([Details_Of_The_Mhif_Police] like '[0-9][0-9][0-9][0-9] [0-9][0-9][0-9][0-9] [0-9][0-9][0-9][0-9] [0-9][0-9][0-9][0-9]'),
	constraint [CH_Gmail_Cl] check ([Gmail_Cl] like '%@%' and [Gmail_Cl] like '%.%' and [Gmail_Cl] like '%[a-zA-Z0-9]%'),
	constraint [CH_Password_Cl] check ([Password_Cl] like '%[a-zA-Z0-9]%' and [Password_Cl] like '%[!@#$%^&*()]%'),
	constraint [CH_Login_Cl] check ([Login_Cl] like '%[a-zA-Z0-9]%')
)
go

insert into [dbo].[Client] ([Second_Name_Cl], [First_Name_Cl], [Middle_Name_Cl], [Passport_Used_By], [Date_Of_Issue], [Division_Code], [Gender], [Date_Of_Burth], [Place_Of_Burth], [Passport_Serries], [Id_Passport], [Details_Of_Snils], [Details_Of_The_Mhif_Police], [Contact_Number], [Gmail_Cl], [Login_Cl], [Password_Cl])
values ('Петров', 'Игорь', 'Васильевич', 'ГУ МВД России по г.Москве', '20.06.12', '770-121', 'Муж', '04.08.99', 'гор. Москва', '4601', '729721', '123-456-780-01', '7700 0000 0005 2111', '89852346541', 'petrov@gmail.com', 'Petrov1', 'Pa$$w0rd'),
('Арсеньтев', 'Петр', 'Игоревич', 'Отделом УФМС по гор. Новосибирск', '07.12.15 ', '540-006', 'Муж', '27.04.72', 'гор. Новосибирск', '4512', '729722', '123-456-780-02', '7700 0000 0005 2112', '89346782190', 'arsentev@gmail.com', 'Arsentev1', 'Pa$$w0rd'),
('Петров', 'Олег', 'Викторовчич', 'Отделом России по гор. Кемерово', '01.04.18', '420-018', 'Муж', '07.10.86', 'гор. Кемерово', '3407', '729723', '123-456-780-03', '7700 0000 0005 2113', '89841217839', 'petrov@yandex.ru', 'Petrov2', 'Pa$$w0rd'),
('Цапкина', 'Елена', 'Сергеевна', 'Отделом УФМС России по гор. Москве', '17.09.15 ', '770-060','Жен', '14.06.92', 'гор. Москва', '4102', '729724', '123-456-780-04', '7700 0000 0005 2114', '89335653488', 'capkina@gmail.com', 'Capkina1', 'Pa$$w0rd'),
('Малинкина', 'Мария', 'Олеговна', 'ГУ МВД России по г. Москве', '27.08.13', '770-121', 'Жен', '20.05.78', 'гор. Москва', '3814', '729725', '123-456-780-05', '7700 0000 0005 2115', '89830089137', 'malinkina@gmail.com', 'Malinkina1', 'Pa$$w0rd')
go

select [Second_Name_Cl]+' '+[First_Name_Cl]+' '+[Middle_Name_Cl] as "ФИО клиента", [Passport_Used_By] as "Кем выдан паспорт", [Date_Of_Issue] as "Дата выдачи", [Division_Code] as "Код подразделения", [Gender] as "Пол", [Date_Of_Burth] as "Дата рождения", [Place_Of_Burth] as "Место рождения",
[Passport_Serries] as "Серия паспорта", [Id_Passport] as "Номер паспорта", [Details_Of_Snils] as "Реквзиты СНИЛС", [Details_Of_The_Mhif_Police] as "Реквизиты полиса ФОМС", [Gmail_Cl] as "Адрес электронной почты", [Password_Cl] as "Пароль" from [dbo].[Client]
order by [Second_Name_Cl] ASC
go

create table [dbo].[Recommended_Treatment]
(
	[Id_Recommended_Treatment] [int] not null identity(1,1),
	[Exercise_Program] [varchar] (100) null default ('-'),
	constraint [PK_Recommended_Treatment] primary key clustered
	([Id_Recommended_Treatment] ASC) on [PRIMARY]
)
go

insert into [dbo].[Recommended_Treatment] ([Exercise_Program])
values ('Плавание'),
('-')
go

select [Exercise_Program] as "Программа упражнений" from [dbo].[Recommended_Treatment]
order by [Exercise_Program] ASC
go

create table [dbo].[Taking_Medications]
(
	[Id_Taking_Medications] [int] not null identity(1,1),
	[Name_Taking_Medications] [varchar] (50) not null,
	constraint [PK_Taking_Medications] primary key clustered
	([Id_Taking_Medications] ASC) on [PRIMARY]
)
go

insert into [dbo].[Taking_Medications] ([Name_Taking_Medications])
values ('Антровин'),
('Ектеосин'),
('Страксорам'),
('Глеоницал'),
('-')
go

select [Name_Taking_Medications] as "Название" from [dbo].[Taking_Medications]
order by [Name_Taking_Medications] ASC
go

create table [dbo].[Treatment_Employee]
(
	[Id_Treatment_Employee] [int] not null identity(1,1),
	[Second_Name_Empl] [varchar] (30) not null,
	[First_Name_Empl] [varchar] (20) not null,
	[Middle_Name_Empl] [varchar] (30) not null,
	[Login_Empl] [varchar] (32) not null,
	[Password_Empl] [varchar] (32) not null,
	constraint [CH_Login_Empl_Count] check (len([Login_Empl])>=5),
	constraint [CH_Password_Empl_Count] check (len([Password_Empl])>=8),
	constraint [PK_Treatment_Employee] primary key clustered
	([Id_Treatment_Employee] ASC) on [PRIMARY],
	constraint [UQ_Login_Empl] unique ([Login_Empl]),
	constraint [CH_Password_Empl] check ([Password_Empl] like '%[a-zA-Z0-9]%' and [Password_Empl] like '%[!@#$%^&*()]%'),
	constraint [CH_Login_Empl] check ([Login_Empl] like '%[a-zA-Z0-9]%')
)
go

insert into [dbo].[Treatment_Employee]
values ('Леонтьев', 'Евгений', ' Викторович', 'Leontev1', 'Pa$$w0rd'),
('Венечкин', 'Олег', 'Владимирович', 'Venechkin1', 'Pa$$w0rd'),
('Самсонов', 'Александр', ' Сергеевич', 'Samsonov1', 'Pa$$w0rd'),
('Смирнова', 'Вероника', 'Максимовна', 'Smirnova1', 'Pa$$w0rd'),
('Репина', 'Екатерина', 'Никитична', 'Repina1', 'Pa$$w0rd')
go

select [Second_Name_Empl]+' '+[First_Name_Empl]+' '+[Middle_Name_Empl] as "ФИО сотрудника", [Login_Empl] as "Логин", [Password_Empl] as "Пароль" from [dbo].[Treatment_Employee]
order by [Second_Name_Empl] ASC
go

create table [dbo].[Treatment_Profiles]
(
	[Id_Treatment_Profiles] [int] not null identity(1,1),
	[Name_Treatment_Profiles] [varchar] (30),
	constraint [PK_Treatment_Profiles] primary key clustered
	([Id_Treatment_Profiles] ASC) on [PRIMARY]
)
go

insert into [dbo].[Treatment_Profiles] ([Name_Treatment_profiles])
values ('Хирургия'),
('Кардиология'),
('Неврология'),
('Офтальмология'),
('Стоматология')
go

select 

select [Name_Treatment_profiles] as "Наименование" from [dbo].[Treatment_Profiles]
order by [Name_Treatment_profiles] ASC
go

create table [dbo].[Personal_Outpatient_Card]
(
	[Id_Personal_Outpatient_Card] [int] not null identity(1,1),
	[Personal_Outpatient_Card_Number] [varchar] (10) not null,
	[Client_Id] [int] not null,
	[Card_Registration_Date] [date] null default (format(Getdate(),'dd.MM.yyyy')),
	constraint [FK_Client] foreign key ([Client_Id])
	references [dbo].[Client] ([Id_Client]),
	constraint [CH_Card_Registration_Date] check ([Card_Registration_Date] <= Getdate()),
	constraint [CH_Personal_Outpatient_Card_Number] check ([Personal_Outpatient_Card_Number] like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
	constraint [UQ_Personal_Outpatient_Card] unique ([Personal_Outpatient_Card_Number]),
	constraint [PK_Personal_Outpatient_Card] primary key clustered
	([Id_Personal_Outpatient_Card] ASC) on [PRIMARY]
)
go

insert into [dbo].[Personal_Outpatient_Card] ([Client_Id],[Personal_Outpatient_Card_Number], [Card_Registration_Date])
values (1, '0000000001', '21.11.2014'),
(2, '0000000002', '12.04.2016'),
(3, '0000000003', '07.06.2010'),
(4, '0000000004', '15.06.2018'),
(5, '0000000005', '30.09.11')
go

select [Personal_Outpatient_Card_Number] as "Уникальный номер", [Card_Registration_Date] as "Дата регестрации" from [dbo].[Personal_Outpatient_Card]
order by [Personal_Outpatient_Card_Number] ASC
go

create table [dbo].[Ticket]
(
	[Id_Ticket] [int] not null identity(1,1),
	[Date_Of_Reception] [date] null default (format(Getdate(),'dd.MM.yyyy')),
	[Treatment_Employee_Id] [int] not null,
	[Personal_Outpatient_Card_Id] [int] not null,
	[Time_Of_Reception] [time] null default (format(Getdate(),'hh:mm:ss')),
	[Ticket_Number] [varchar] (10) not null,
	constraint [CH_Date_Of_Reception] check ([Date_Of_Reception] >= Getdate()),
	constraint [FK_Treatment_Employee] foreign key ([Treatment_Employee_Id])
	references [dbo].[Treatment_Employee] ([Id_Treatment_Employee]),
	constraint [UQ_Ticket_Number] unique ([Ticket_Number]),
	constraint [FK_Personal_Outpatient_Card] foreign key ([Personal_Outpatient_Card_Id])
	references [dbo].[Personal_Outpatient_Card] ([Id_Personal_Outpatient_Card]),
	constraint [CH_Ticket_Number] check ([Ticket_Number] like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
	constraint [PK_Ticket] primary key clustered
	([Id_Ticket] ASC) on [PRIMARY]
)
go

insert into [dbo].[Ticket] ([Date_Of_Reception], [Time_Of_Reception], [Ticket_Number], [Treatment_Employee_Id], [Personal_Outpatient_Card_Id])
values ('12.07.2022', '14:00:00', '3390000001',1,1),
('15.04.2022', '17:00:00','3390000002',2,2),
('10.06.2022', '12:00:00','3390000003',3,3),
('21.08.2022', '16:00:00','3390000004',4,4),
('12.05.2022', '10:00:00','3390000005',5,5)
go

select [Date_Of_Reception] as "Дата", [Time_Of_Reception] as "Время", [Ticket_Number] as "Уникальный номер" from [dbo].[Ticket]
order by [Date_Of_Reception] ASC
go

create table [dbo].[Diagnosis]
(
	[Id_Diagnosis] [int] not null identity(1,1),
	[Unique_Diagnosis_Number] [varchar] (5) not null,
	[Personal_Outpatient_Card_Id] [int] not null,
	[Name_Of_Diagnosis] [varchar] (50) not null,
	constraint [CH_Unique_Diagnosis_Number] check ([Unique_Diagnosis_Number] like '[А-Я][0-9][0-9].[0-9]'),
	constraint [FK_Personal_Outpatient_Card_Sec] foreign key ([Personal_Outpatient_Card_Id])
	references [dbo].[Personal_Outpatient_Card] ([Id_Personal_Outpatient_Card]),
	constraint [PK_Diagnosis] primary key clustered
	([Id_Diagnosis] ASC) on [PRIMARY]
)
go

insert into [dbo].[Diagnosis] ([Unique_Diagnosis_Number], [Name_Of_Diagnosis], [Personal_Outpatient_Card_Id])
values ('Я06.1', 'Прободная язва', 1),
('А03.0', 'Артимия сердца', 2),
('Ф05.0', 'Фактоматозы', 3),
('А01.0', 'Астенопия', 4),
('К01.0', 'Кариес', 5)
go

select [Unique_Diagnosis_Number] as "Уникальный код", [Name_Of_Diagnosis] as "Название" from [dbo].[Diagnosis]
order by [Unique_Diagnosis_Number] ASC
go

create table [dbo].[Sick_List]
(
	[Id_Sick_List] [int] not null identity(1,1),
	[Sick_List_Number] [varchar] (10) not null,
	[Ticket_Id] [int] not null,
	[Sick_List_Opening_Date] [date] null default (format(Getdate(),'dd.MM.yyyy')),
	[Sick_List_Closing_Date] [date] null default (format(Getdate(),'dd.MM.yyyy')),
	[Date_Of_Next_Visit] [date] null default (format(Getdate(),'dd.MM.yyyy')),
	[Name_Organisation] [varchar] (50) not null,
	constraint [CH_Sick_List_Opening_Date] check ([Sick_List_Opening_Date] <= Getdate()),
	constraint [CH_Sick_List_Closing_Date] check ([Sick_List_Closing_Date] >= Getdate()),
	constraint [CH_Date_Of_Next_Visit] check ([Date_Of_Next_Visit] >= Getdate()),
	constraint [FK_Ticket_Sec] foreign key ([Ticket_Id])
	references [dbo].[Ticket] ([Id_Ticket]),
	constraint [UQ_Sick_List_Number] unique ([Sick_List_Number]),
	constraint [PK_Sick_List] primary key clustered
	([Id_Sick_List] ASC) on [PRIMARY]
)
go

insert into [dbo].[Sick_List] ([Sick_List_Number], [Sick_List_Opening_Date], [Sick_List_Closing_Date], [Date_Of_Next_Visit], [Name_Organisation], [Ticket_Id])
values ('1234567891', '05.01.2022', '21.07.22', '12.06.22', 'ООО Молекула', 1),
('1234567892', '07.02.2022', '30.07.2022', '15.06.2022', 'ОАО Царевич', 2),
('1234567893', '04.03.2022', '17.06.2022', '10.06.2022', 'ООО Булочка', 3),
('1234567894', '15.02.2022', '29.08.2022', '21.06.2022', 'ЗАО ТЭЦС', 4),
('1234567895', '10.03.2022', '13.05.2022', '12.06.2022', 'ООО Маска', 5)
go

select [Sick_List_Number] as "Уникальный номер", [Sick_List_Opening_Date] as "Дата открытия", [Sick_List_Closing_Date] as "Дата закрытия", [Date_Of_Next_Visit] as "Дата следующего посещения", [Name_Organisation] as "Название организации" from [dbo].[Sick_List]
order by [Sick_List_Number] ASC
go

create table [dbo].[Employee_Profile]
(
	[Id_Employee_Profile] [int] not null identity(1,1),
	[Treatment_Employee_Id] [int] not null,
	[Treatment_Profiles_Id] [int] not null,
	constraint [FK_Treatment_Employee_Sec] foreign key ([Treatment_Employee_Id])
	references [dbo].[Treatment_Employee] ([Id_Treatment_Employee]),
	constraint [FK_Treatment_Profiles] foreign key ([Treatment_Profiles_Id])
	references [dbo].[Treatment_Profiles] ([Id_Treatment_Profiles]),
	constraint [PK_Employee_Profile] primary key clustered
	([Id_Employee_Profile] ASC) on [PRIMARY]
)
go

insert into [dbo].[Employee_Profile] ([Treatment_Employee_Id], [Treatment_Profiles_Id])
values (1,1),
(2,2),
(3,3),
(4,4),
(5,5)
go

select [Treatment_Employee_Id] as "Код сотрудника", [Treatment_Profiles_Id] as "Код профиля" from [dbo].[Employee_Profile]
order by [Treatment_Employee_Id] ASC
go

create table [dbo].[Service]
(
	[Id_Service] [int] not null identity(1,1),
	[Code_Of_Service] [varchar] (3) not null,
	[Ticket_Id] [int] not null,
	constraint [FK_Ticket] foreign key ([Ticket_Id])
	references [dbo].[Ticket] ([Id_Ticket]),
	constraint [PK_Service] primary key clustered
	([Id_Service] ASC) on [PRIMARY]
)
go

insert into [dbo].[Service] ([Code_Of_Service], [Ticket_Id])
values ('078', 1),
('053', 2),
('044', 3),
('025', 4),
('065', 5)
go

select [Code_Of_Service] as "Уникальный код" from [dbo].[Service]
order by [Code_Of_Service] ASC
go

create table [dbo].[Service_Diagnosis]
(
	[Id_Service_Diagnosis] [int] not null identity(1,1),
	[Service_Id] [int] not null,
	[Diagnosis_Id] [int] not null,
	constraint [FK_Service] foreign key ([Service_Id])
	references [dbo].[Service] ([Id_Service]),
	constraint [FK_Diagnosis] foreign key ([Diagnosis_Id])
	references [dbo].[Diagnosis] ([Id_Diagnosis]),
	constraint [PK_Service_Diagnosis] primary key clustered
	([Id_Service_Diagnosis] ASC) on [PRIMARY]
)
go

insert into [dbo].[Service_Diagnosis] ([Service_Id], [Diagnosis_Id])
values (1,1),
(2,2),
(3,3),
(4,4),
(5,5)
go

select [Service_Id] as "Код услуги", [Diagnosis_Id] as "Код диагноза" from [dbo].[Service_Diagnosis]
order by [Service_Id] ASC
go

create table [dbo].[Service_Treatment]
(
	[Id_Service_Treatment] [int] not null identity(1,1),
	[Service_Id] [int] not null,
	[Recommended_Treatment_Id] [int] not null,
	constraint [FK_Service_Sec] foreign key ([Service_Id])
	references [dbo].[Service] ([Id_Service]),
	constraint [FK_Recommended_Treatment_Sec] foreign key ([Recommended_Treatment_Id])
	references [dbo].[Recommended_Treatment] ([Id_Recommended_Treatment]),
	constraint [PK_Service_Treatment] primary key clustered
	([Id_Service_Treatment] ASC) on [PRIMARY]
)
go

insert into [dbo].[Service_Treatment] ([Service_Id], [Recommended_Treatment_Id])
values (1,2),
(2,1),
(3,2),
(4,2),
(5,2)
go

select [Service_Id] as "Код услуги", [Recommended_Treatment_Id] as "Код рекомендации" from [dbo].[Service_Treatment]
order by [Service_Id] ASC
go

create table [dbo].[Recommended_Taking_Medications]
(
	[Id_Recommended_Taking_Medications] [int] not null identity(1,1),
	[Recommended_Treatment_Id] [int] not null,
	[Taking_Medications_Id] [int] not null,
	constraint [FK_Recommended_Treatment_Thr] foreign key ([Recommended_Treatment_Id])
	references [dbo].[Recommended_Treatment] ([Id_Recommended_Treatment]),
	constraint [FK_Taking_Medications] foreign key ([Taking_Medications_Id])
	references [dbo].[Taking_Medications] ([Id_Taking_Medications]),
	constraint [PK_Recommended_Taking_Medications] primary key clustered
	([Id_Recommended_Taking_Medications] ASC) on [PRIMARY]
)
go

insert into [dbo].[Recommended_Taking_Medications] ([Recommended_Treatment_Id], [Taking_Medications_Id])
values (2, 1),
(1,2),
(2,3),
(2,4),
(2,5)
go

select [Recommended_Treatment_Id] as "Код рекомендации", [Taking_Medications_Id] as "Код препарата" from [dbo].[Recommended_Taking_Medications]
order by [Recommended_Treatment_Id] ASC
go