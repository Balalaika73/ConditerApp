use [GKP_DataBase]
go

create or alter procedure [dbo].[Client_Insert]
@Second_Client [varchar] (30), @First_Client [varchar] (20), @Middle_Name_Cl [varchar] (30), 
@Passport_Used_By [varchar] (70), @Date_Of_Issue [date], @Division_Code [varchar] (7), 
@Gender [varchar] (3), @Date_Of_Burth [date], @Place_Of_Burth [varchar] (50), 
@Passport_Serries [int], @Id_Passport [int], @Details_Of_Snils [varchar] (14), 
@Details_Of_The_Mhif_Police [varchar] (19), @Contact_Number [varchar] (17), 
@Gmail_Cl [varchar] (50), @Login_Cl [varchar] (32), @Password_Cl [varchar] (32)
as
	begin try
	declare @exsit_employee [int] = (select count(*) from [dbo].[Treatment_Employee] 
	where [Login_Empl] = @Login_Cl)
	if (@exsit_employee > 0)
		print('Пользователь с указанным логином, уже есть в системе!')
	else
		begin
			declare @exist_client [int] = (select count(*) from [dbo].[Client] 
			where [First_Name_Cl] = @First_Client and
			[Second_Name_Cl] = @Second_Client and [Middle_Name_Cl] = @Middle_Name_Cl
			and [Login_Cl] = @Login_Cl and [Passport_Serries] = @Passport_Serries 
			and [Id_Passport] = @Id_Passport and [Details_Of_Snils] = @Details_Of_Snils 
			and [Details_Of_The_Mhif_Police] = @Details_Of_The_Mhif_Police 
			and [Contact_Number] = @Contact_Number and [Gmail_Cl] = @Gmail_Cl and [Password_Cl] = @Password_Cl)
			if (@exist_client > 0)
			print('Пользователь с указанными данными уже есть в системе!')
			else
			begin
			if (@Middle_Name_Cl = null)
			insert into [dbo].[Client] ([Second_Name_Cl], [First_Name_Cl],
			[Passport_Used_By],[Date_Of_Issue],[Division_Code],[Gender],[Date_Of_Burth],
			[Place_Of_Burth],[Passport_Serries],[Id_Passport],[Details_Of_Snils],
			[Details_Of_The_Mhif_Police],[Contact_Number],[Gmail_Cl],[Login_Cl], [Password_Cl])
			values (@Second_Client, @First_Client, @Passport_Used_By, @Date_Of_Issue, 
			@Division_Code, @Gender, @Date_Of_Burth, @Place_Of_Burth, @Passport_Serries,
			@Id_Passport, @Details_Of_Snils, @Details_Of_The_Mhif_Police, @Contact_Number, 
			@Gmail_Cl, @Login_Cl, @Password_Cl)
			else
			insert into [dbo].[Client] ([Second_Name_Cl], [First_Name_Cl],[Middle_Name_Cl],
			[Passport_Used_By],[Date_Of_Issue],[Division_Code],[Gender],
			[Date_Of_Burth],[Place_Of_Burth],[Passport_Serries],[Id_Passport],[Details_Of_Snils],
			[Details_Of_The_Mhif_Police],[Contact_Number],[Gmail_Cl],[Login_Cl], [Password_Cl])
			values (@Second_Client, @First_Client, @Middle_Name_Cl, @Passport_Used_By,
			@Date_Of_Issue, @Division_Code, @Gender, @Date_Of_Burth, @Place_Of_Burth, 
			@Passport_Serries,@Id_Passport, @Details_Of_Snils, @Details_Of_The_Mhif_Police, 
			@Contact_Number, @Gmail_Cl, @Login_Cl, @Password_Cl)
			end
		end
	end try
begin catch
print('Пользователь с указанным логином, уже есть в системе!')
end catch
go

create or alter procedure [dbo].[Client_update]
@Id_Client [int], @Second_Client [varchar] (30), @First_Client [varchar] (20), @Middle_Name_Cl [varchar] (30), 
@Passport_Used_By [varchar] (70), @Date_Of_Issue [date], @Division_Code [varchar] (7), 
@Gender [varchar] (3), @Date_Of_Burth [date], @Place_Of_Burth [varchar] (50), 
@Passport_Serries [int], @Id_Passport [int], @Details_Of_Snils [varchar] (14), 
@Details_Of_The_Mhif_Police [varchar] (19), @Contact_Number [varchar] (17), 
@Gmail_Cl [varchar] (50), @Login_Cl [varchar] (32), @Password_Cl [varchar] (32)
as
begin try
	declare @excist_employee [int] = (select count(*) from [dbo].[Treatment_Employee] 
	where [Login_Empl] = @Login_Cl) 
	if (@excist_employee > 0)
		print('Пользователь с указанным логином уже есть в системе!')
	else
		begin
			declare @exist_client [int] = (select count(*) from [dbo].[Client]
			where [First_Name_Cl] = @First_Client and
			[Second_Name_Cl] = @Second_Client and [Middle_Name_Cl] = @Middle_Name_Cl 
			and [Login_Cl] = @Login_Cl and [Passport_Serries] = @Passport_Serries 
			and [Id_Passport] = @Id_Passport and [Details_Of_Snils] = @Details_Of_Snils 
			and [Details_Of_The_Mhif_Police] = @Details_Of_The_Mhif_Police 
			and [Contact_Number] = @Contact_Number and [Gmail_Cl] = @Gmail_Cl and [Password_Cl] = @Password_Cl)
			if (@exist_client > 0)
				print('Пользователь с такими данными уже есть в системе!')
			else
				if (@Middle_Name_Cl = null) update [dbo].[Client] set
					[Second_Name_Cl] = @Second_Client, [First_Name_Cl] = @First_Client,
					[Login_Cl] = @Login_Cl, [Passport_Used_By] = @Passport_Used_By, 
					[Date_Of_Issue] = @Date_Of_Issue, [Division_Code] = @Division_Code,
					[Passport_Serries] = @Passport_Serries, [Gender] = @Gender,
					[Date_Of_Burth] = @Date_Of_Burth, [Place_Of_Burth] = @Place_Of_Burth,
					[Id_Passport] = @Id_Passport, [Details_Of_Snils] = @Details_Of_Snils,
					[Details_Of_The_Mhif_Police] = @Details_Of_The_Mhif_Police,
					[Contact_Number] = @Contact_Number, [Gmail_Cl] = @Gmail_Cl,
					[Password_Cl] = @Password_Cl
					where [Id_Client] = @Id_Client
				else
					 update [dbo].[Client] set
					[Second_Name_Cl] = @Second_Client, [First_Name_Cl] = @First_Client,
					[Middle_Name_Cl] = @Middle_Name_Cl, [Login_Cl] = @Login_Cl, 
					[Passport_Used_By] = @Passport_Used_By, 
					[Date_Of_Issue] = @Date_Of_Issue, [Division_Code] = @Division_Code,
					[Passport_Serries] = @Passport_Serries, [Gender] = @Gender,
					[Date_Of_Burth] = @Date_Of_Burth, [Place_Of_Burth] = @Place_Of_Burth,
					[Id_Passport] = @Id_Passport, [Details_Of_Snils] = @Details_Of_Snils,
					[Details_Of_The_Mhif_Police] = @Details_Of_The_Mhif_Police,
					[Contact_Number] = @Contact_Number, [Gmail_Cl] = @Gmail_Cl,
					[Password_Cl] = @Password_Cl
					where [Id_Client] = @Id_Client
		end
end try
begin catch
	print('Пользователь с указанным логином, уже есть в системе!')
end catch
go

create or alter procedure [dbo].[Client_delete]
@Id_Client [int]
as
	declare @any_child_record [int] = (select count(*) from [dbo].[Personal_Outpatient_Card]
	where [Client_Id] = @Id_Client)
	if (@any_child_record > 0 )
		print('Клиент не может быть удален, так как в таблице "Персональная амбулаторная 
		карта", есть связанные данные!')
	else
		delete from [dbo].[Client] where [Id_Client] = @Id_Client
go

create or alter procedure [dbo].[Medical_Degree_Insert]
@Name_Medical_Degree [varchar] (50)
as
	begin try
		insert into [dbo].[Medical_Degree] ([Name_Medical_Degree])
		values (@Name_Medical_Degree)
	end try
	begin catch
		throw 50001, 'Данная степень уже есть в таблице!', 1
	end catch
go

create or alter procedure [dbo].[Medical_Degree_Update]
@Id_Medical_Degree [int], @Name_Medical_Degree [varchar] (50)
as
	begin try
	update [dbo].[Medical_Degree] set
	[Name_Medical_Degree] = @Name_Medical_Degree
	where [Id_Medical_Degree] = @Id_Medical_Degree
	end try
	begin catch
		print('Данная степень уже есть в таблице!')
	end catch
go

create or alter procedure [dbo].[Medical_Degree_Delete]
@Id_Medical_Degree [int]
as
	declare @any_child_record [int] = (select count(*) from [dbo].[Treatment_Employee] where [Medical_Degree_Id] = @Id_Medical_Degree)
	if (@any_child_record > 0)
		print('Данная степень не может быть удалена, сначала измените или удалите сотрудников с этой степенью')
	else
		delete from [dbo].[Medical_Degree] where [Id_Medical_Degree] = @Id_Medical_Degree
go

create or alter procedure [dbo].[Recommended_Treatment_Insert]
@Exercise_Program [varchar] (max)
as
	begin try
		insert into [dbo].[Recommended_Treatment] ([Exercise_Program])
		values (@Exercise_Program)
	end try
	begin catch
		print('Данное упражнение уже есть в таблице!')
	end catch
go

create or alter procedure [dbo].[Recommended_Treatment_Update]
@Id_Recommended_Treatment [int], @Exercise_Program [varchar] (max)
as
	begin try
	update [dbo].[Recommended_Treatment] set
	[Exercise_Program] = @Exercise_Program
	where [Id_Recommended_Treatment] = @Id_Recommended_Treatment
	end try
	begin catch
		throw 50002, 'Данное упражнение уже есть в таблице!', 1
	end catch
go

create or alter procedure [dbo].[Recommended_Treatment_Delete]
@Id_Recommended_Treatment [int]
as
	declare @any_child_record [int] = (select count(*) from [dbo].[Recommended_Taking_Medications] where [Recommended_Treatment_Id] = @Id_Recommended_Treatment)
	if (@any_child_record > 0)
		print('Данная степень не может быть удалена, так как в таблице "Recommended_Taking_Medications" есть связанные данные!')
	else
		declare @any_child_record1 [int] = (select count(*) from [dbo].[Service_Treatment] where [Recommended_Treatment_Id] = @Id_Recommended_Treatment)
		if (@any_child_record1 > 0)
			print('Данная степень не может быть удалена, так как в таблице "Service_Treatment" есть связанные данные!')
		else
			delete from [dbo].[Recommended_Treatment] where [Id_Recommended_Treatment] = @Id_Recommended_Treatment
go

create or alter procedure [dbo].[Taking_Medications_Insert]
@Name_Taking_Medications [varchar] (50)
as
	begin try
		insert into [dbo].[Taking_Medications] ([Name_Taking_Medications])
		values (@Name_Taking_Medications)
	end try
	begin catch
		print('Данное лекартство уже есть в таблице!')
	end catch
go

create or alter procedure [dbo].[Taking_Medications_Update]
@Id_Taking_Medications [int], @Name_Taking_Medications [varchar] (50)
as
	begin try
	update [dbo].[Taking_Medications] set
	[Name_Taking_Medications] = @Name_Taking_Medications
	where [Id_Taking_Medications] = @Id_Taking_Medications
	end try
	begin catch
		throw 5004, 'Данное лекарство уже есть в таблице!',1
	end catch
go

create or alter procedure [dbo].[Taking_Medications_Delete]
@Id_Taking_Medications [int]
as
	declare @any_child_record [int] = (select count(*) from [dbo].[Recommended_Taking_Medications] where [Taking_Medications_Id] = @Id_Taking_Medications)
	if (@any_child_record > 0)
		print('Данная степень не может быть удалена,  так как в таблице "Service_Treatment" есть связанные данные!')
	else
		delete from [dbo].[Taking_Medications] where [Id_Taking_Medications] = @Id_Taking_Medications
go

create or alter procedure [dbo].[Position_Insert]
@Name_Position [varchar] (50)
as
	begin try
		insert into [dbo].[Position] ([Name_Position])
		values (@Name_Position)
	end try
	begin catch
		print('Данная должность уже есть в таблице!')
	end catch
go

create or alter procedure [dbo].[Position_Update]
@Id_Position [int], @Name_Position [varchar] (50)
as
	begin try
	update [dbo].[Position] set
	[Name_Position] = @Name_Position
	where [Id_Position] = @Id_Position
	end try
	begin catch
		print('Данная должность уже есть в таблице!')
	end catch
go

create or alter procedure [dbo].[Position_Delete]
@Id_Position [int]
as
	declare @any_child_record [int] = (select count(*) from [dbo].[Employee_Position] where [Position_Id] = @Id_Position)
	if (@any_child_record > 0)
		throw 50003,'Данная степень не может быть удалена,  так как в таблице "Employee_Position" есть связанные данные!',1
	else
		delete from [dbo].[Position] where [Id_Position] = @Id_Position
go

create or alter procedure [dbo].[Institution_Insert]
@Legal_address [varchar] (50), @Physical_adress [varchar] (50), @Name_Institution [varchar] (50)
as
	begin try
		insert into [dbo].[Institution] ([Legal_address], [Physical_adress], [Name_Institution])
		values (@Legal_address, @Physical_adress, @Name_Institution)
	end try
	begin catch
		print('Данный корпус уже есть в таблице!')
	end catch
go

create or alter procedure [dbo].[Institution_Update]
@Id_Institution [int], @Legal_address [varchar] (50), @Physical_adress [varchar] (50), @Name_Institution [varchar] (50)
as
	begin try
	update [dbo].[Institution] set
	[Legal_address] = @Legal_address,
	[Physical_adress] = @Physical_adress,
	[Name_Institution] = @Name_Institution
	where [Id_Institution] = @Id_Institution
	end try
	begin catch
		print('Данный корпус уже есть в таблице!')
	end catch
go

create or alter procedure [dbo].[Institution_Delete]
@Id_Institution [int]
as
	declare @any_child_record [int] = (select count(*) from [dbo].[Medical_Departments] where [Institution_Id] = @Id_Institution)
	if (@any_child_record > 0)
		print('Данная степень не может быть удалена,  так как в таблице "Medical_Departments" есть связанные данные!')
	else
		delete from [dbo].[Institution] where [Id_Institution] = @Id_Institution
go

create or alter procedure [dbo].[Medical_Departments_Insert]
@Medical_Profile [varchar] (50), @Name_Medical_Departments [varchar] (max), @Institution_Id [int]
as
	begin try
		insert into [dbo].[Medical_Departments] ([Medical_Profile], [Name_Medical_Departments], [Institution_Id])
		values (@Medical_Profile, @Name_Medical_Departments, @Institution_Id)
	end try
	begin catch
		print('Данные уже есть в таблице!')
	end catch
go

create or alter procedure [dbo].[Medical_Departments_Update]
@Id_Medical_Departments [int], @Medical_Profile [varchar] (50), @Name_Medical_Departments [varchar] (max), @Institution_Id [int]
as
	begin try
	update [dbo].[Medical_Departments] set
	[Medical_Profile] = @Medical_Profile,
	[Name_Medical_Departments] = @Name_Medical_Departments,
	[Institution_Id] = @Institution_Id
	where [Id_Medical_Departments] = @Id_Medical_Departments
	end try
	begin catch
		print('Данное учреждение уже есть в таблице!')
	end catch
go

create or alter procedure [dbo].[Medical_Departments_Delete]
@Id_Medical_Departments [int]
as
	declare @any_child_record [int] = (select count(*) from [dbo].[Treatment_Employee] where [Medical_Departments_Id] = @Id_Medical_Departments)
	if (@any_child_record > 0)
		print('Данная степень не может быть удалена,  так как в таблице "Treatment_Employee" есть связанные данные!')
	else
		delete from [dbo].[Medical_Departments] where [Id_Medical_Departments] = @Id_Medical_Departments
go

create or alter procedure [dbo].[Treatment_Employee_Insert]
@Second_Name_Empl [varchar] (30), @First_Name_Empl [varchar] (20), 
@Middle_Name_Empl [varchar] (30), @Login_Empl [varchar] (32), 
@Password_Empl [varchar] (32), @Medical_Degree_Id [int], 
@Medical_Departments_Id [int]
as
	begin try
		insert into [dbo].[Treatment_Employee] ([Second_Name_Empl], [First_Name_Empl], [Middle_Name_Empl], [Login_Empl], [Password_Empl], [Medical_Degree_Id], [Medical_Departments_Id])
		values (@Second_Name_Empl, @First_Name_Empl, @Middle_Name_Empl, @Login_Empl, @Password_Empl, @Medical_Degree_Id, @Medical_Departments_Id)
	end try
	begin catch
		print('Данные уже есть в таблице!')
	end catch
go

create or alter procedure [dbo].[Treatment_Employee_Update]
@Id_Treatment_Employee [int], @Second_Name_Empl [varchar] (30), @First_Name_Empl [varchar] (20), 
@Middle_Name_Empl [varchar] (30), @Login_Empl [varchar] (32), 
@Password_Empl [varchar] (32), @Medical_Degree_Id [int], 
@Medical_Departments_Id [int]
as
	begin try
	update [dbo].[Treatment_Employee] set
	[Second_Name_Empl] = @Second_Name_Empl,
	[First_Name_Empl] = @First_Name_Empl,
	[Middle_Name_Empl] = @Middle_Name_Empl,
	[Login_Empl] = @Login_Empl,
	[Password_Empl] = @Password_Empl,
	[Medical_Degree_Id] = @Medical_Degree_Id,
	[Medical_Departments_Id] = @Medical_Departments_Id
	where [Id_Treatment_Employee] = @Id_Treatment_Employee
	end try
	begin catch
		print('Данный сотрудник уже есть в таблице!')
	end catch
go

create or alter procedure [dbo].[Treatment_Employee_Delete]
@Id_Treatment_Employee [int]
as
	declare @any_child_record [int] = (select count(*) from [dbo].[Employee_Profile] where [Treatment_Employee_Id] = @Id_Treatment_Employee)
	if (@any_child_record > 0)
		print('Данная степень не может быть удалена,  так как в таблице "Employee_Profile" есть связанные данные!')
	else
		declare @any_child_record1 [int] = (select count(*) from [dbo].[Outpatient_Card_Employee] where [Treatment_Employee_Id] = @Id_Treatment_Employee)
		if (@any_child_record1 > 0)
				print('Запись не может быть удалена,  так как в таблице "Outpatient_Card_Employee" есть связанные данные!')
		else
			declare @any_child_record2 [int] = (select count(*) from [dbo].[Ticket] where [Treatment_Employee_Id] = @Id_Treatment_Employee)
			if (@any_child_record2 > 0)
				print('Запись не может быть удалена,  так как в таблице "Outpatient_Card_Employee" есть связанные данные!')
			else
				delete from [dbo].[Treatment_Employee] where [Id_Treatment_Employee] = @Id_Treatment_Employee
go

create or alter procedure [dbo].[Treatment_Profiles_Insert]
@Name_Treatment_Profiles [varchar] (30)
as
	begin try
		insert into [dbo].[Treatment_Profiles] ([Name_Treatment_Profiles])
		values (@Name_Treatment_Profiles)
	end try
	begin catch
		print('Данные уже есть в таблице!')
	end catch
go

create or alter procedure [dbo].[Treatment_Profiles_Update]
@Id_Treatment_Profiles [int], @Name_Treatment_Profiles [varchar] (30)
as
	begin try
	update [dbo].[Treatment_Profiles] set
	[Name_Treatment_Profiles] = @Name_Treatment_Profiles
	where [Id_Treatment_Profiles] = @Id_Treatment_Profiles
	end try
	begin catch
		print('Данный профиль уже есть в таблице!')
	end catch
go

create or alter procedure [dbo].[Treatment_Profiles_Delete]
@Id_Treatment_Profiles [int]
as
	declare @any_child_record [int] = (select count(*) from [dbo].[Employee_Profile] where [Treatment_Profiles_Id] = @Id_Treatment_Profiles)
	if (@any_child_record > 0)
		print('Данная степень не может быть удалена,  так как в таблице "Employee_Profile" есть связанные данные!')
	else
		delete from [dbo].[Treatment_Profiles] where [Id_Treatment_Profiles] = @Id_Treatment_Profiles
go

create or alter procedure [dbo].[Personal_Outpatient_Card_Insert]
@Check_Number [varchar] (10), @Client_Id [int], 
@Card_Registration_Date [date]
as
begin try
	if (@Check_Number = '_____________') or 
		(@Check_Number = null) or (@Check_Number = ' ')
		begin
		declare @new_Number [varchar] (10) = ' '
		declare @Part_Number [int] = (SELECT TOP 1 
		[Personal_Outpatient_Card_Number] FROM 
		[dbo].[Personal_Outpatient_Card] ORDER BY 
		[Id_Personal_Outpatient_Card] DESC)
		declare @New [int] = @Part_Number + 1
		declare @New_Count [int] = (select len(@New))
		declare @step [int] = 10 - @New_Count
		while @step > 0
			begin
			set @new_Number = @new_Number + '0'
			set @step = @step - 1
			end
			declare @NewV [varchar] = CONVERT(varchar, @New)
			insert into [dbo].[Personal_Outpatient_Card] 
			([Personal_Outpatient_Card_Number], [Client_Id], 
			[Card_Registration_Date])
			values (@new_Number+@NewV, @Client_Id, 
			@Card_Registration_Date)
			end
		else
		insert into [dbo].[Personal_Outpatient_Card] 
			([Personal_Outpatient_Card_Number], [Client_Id], 
			[Card_Registration_Date])
			values (@Check_Number, @Client_Id, 
			@Card_Registration_Date)
	end try
	begin catch
		print('Данные уже есть в таблице!')
	end catch
go

create or alter procedure [dbo].[Personal_Outpatient_Card_Update]
@Id_Personal_Outpatient_Card [int], @Personal_Outpatient_Card_Number [varchar] (10), @Client_Id [int], @Card_Registration_Date [date]
as
	begin try
	update [dbo].[Personal_Outpatient_Card] set
	[Personal_Outpatient_Card_Number] = @Personal_Outpatient_Card_Number,
	[Client_Id] = @Client_Id,
	[Card_Registration_Date] = @Card_Registration_Date
	where [Id_Personal_Outpatient_Card] = @Id_Personal_Outpatient_Card
	end try
	begin catch
		print('Данная карта уже есть в таблице!')
	end catch
go

create or alter procedure [dbo].[Personal_Outpatient_Card_Delete]
@Id_Personal_Outpatient_Card [int]
as
	declare @any_child_record [int] = (select count(*) from [dbo].[Diagnosis] where [Personal_Outpatient_Card_Id] = @Id_Personal_Outpatient_Card)
	if (@any_child_record > 0)
		print('Данная степень не может быть удалена,  так как в таблице "Diagnosis" есть связанные данные!')
	else
		declare @any_child_record1 [int] = (select count(*) from [dbo].[Outpatient_Card_Employee] where [Personal_Outpatient_Card_Id] = @Id_Personal_Outpatient_Card)
		if (@any_child_record1 > 0)
				print('Запись не может быть удалена,  так как в таблице "Outpatient_Card_Employee" есть связанные данные!')
		else
			declare @any_child_record2 [int] = (select count(*) from [dbo].[Ticket] where [Personal_Outpatient_Card_Id] = @Id_Personal_Outpatient_Card)
			if (@any_child_record2 > 0)
				print('Запись не может быть удалена,  так как в таблице "Ticket" есть связанные данные!')
			else
				delete from [dbo].[Personal_Outpatient_Card] where [Id_Personal_Outpatient_Card] = @Id_Personal_Outpatient_Card
go

create or alter procedure [dbo].[Ticket_Insert]
@Date_Of_Reception [date], @Treatment_Employee_Id [int], 
@Personal_Outpatient_Card_Id [int], 
@Time_Of_Reception [time], @Ticket_Number [varchar] (10) 
as
	begin try
		if (@Ticket_Number = '__________') or 
		(@Ticket_Number = null) or (@Ticket_Number = ' ')
		begin
		declare @last_numb [varchar] (10) = (SELECT max([Ticket_Number]) FROM [dbo].[Ticket])
		declare @numb [varchar] (7) = (select substring(@last_numb,4,8))
		declare @new_part [int] = (select (convert([int], @numb)) + 1)
		declare @count [int] = (select len(@new_part))
		declare @i [int] = 0
		declare @o [varchar] (7) = ' '
		while @i<7-@count
		begin
			set @o = @o+'0'
			set @i = @i+1
		end
		declare @o1 [varchar] (7) = (select trim(@o))
		declare @new_numb [varchar] (10) = (select ('339'+@o1+convert([varchar](7),@new_part)))
		insert into [dbo].[Ticket] ([Date_Of_Reception], 
		[Treatment_Employee_Id], [Personal_Outpatient_Card_Id], 
		[Time_Of_Reception], [Ticket_Number])
		values (@Date_Of_Reception, @Treatment_Employee_Id, 
		@Personal_Outpatient_Card_Id, @Time_Of_Reception,
		@new_numb)
		end
		else
		insert into [dbo].[Ticket] ([Date_Of_Reception], 
		[Treatment_Employee_Id], [Personal_Outpatient_Card_Id], 
		[Time_Of_Reception], [Ticket_Number])
		values (@Date_Of_Reception, @Treatment_Employee_Id, 
		@Personal_Outpatient_Card_Id, @Time_Of_Reception,@Ticket_Number)
	end try
	begin catch
		print('Данные уже есть в таблице!')
	end catch
go

create or alter procedure [dbo].[Ticket_Update]
@Id_Ticket [int], @Date_Of_Reception [date], @Treatment_Employee_Id [int], @Personal_Outpatient_Card_Id [int], @Time_Of_Reception [time], @Ticket_Number [varchar] (10) 
as
	begin try
	update [dbo].[Ticket] set
	[Date_Of_Reception] = @Date_Of_Reception,
	[Treatment_Employee_Id] = @Treatment_Employee_Id,
	[Personal_Outpatient_Card_Id] = @Personal_Outpatient_Card_Id,
	[Time_Of_Reception] = @Time_Of_Reception,
	[Ticket_Number] = @Ticket_Number
	where [Id_Ticket] = @Id_Ticket
	end try
	begin catch
		print('Данная карта уже есть в таблице!')
	end catch
go

create or alter procedure [dbo].[Ticket_Delete]
@Id_Ticket [int]
as
	declare @any_child_record [int] = (select count(*) from [dbo].[Sick_List] where [Ticket_Id] = @Id_Ticket)
	if (@any_child_record > 0)
		print('Данная степень не может быть удалена,  так как в таблице "Ticket" есть связанные данные!')
	else
		declare @any_child_record1 [int] = (select count(*) from [dbo].[Service] where [Ticket_Id] = @Id_Ticket)
		if (@any_child_record1 > 0)
				print('Запись не может быть удалена,  так как в таблице "Service" есть связанные данные!')
		else
			delete from [dbo].[Ticket] where [Id_Ticket] = @Id_Ticket
go

create or alter procedure [dbo].[Diagnosis_Insert]
@Unique_Diagnosis_Number [varchar] (5), @Name_Of_Diagnosis [varchar] (50),@Personal_Outpatient_Card_Id [int]
as
	begin try
		insert into [dbo].[Diagnosis] ([Unique_Diagnosis_Number], [Name_Of_Diagnosis], [Personal_Outpatient_Card_Id])
		values (@Unique_Diagnosis_Number, @Name_Of_Diagnosis, @Personal_Outpatient_Card_Id)
	end try
	begin catch
		print('Данные уже есть в таблице!')
	end catch
go

create or alter procedure [dbo].[Diagnosis_Update]
@Id_Diagnosis [int], @Unique_Diagnosis_Number [varchar] (5), @Name_Of_Diagnosis [varchar] (50),@Personal_Outpatient_Card_Id [int]
as
	begin try
	update [dbo].[Diagnosis] set
	[Unique_Diagnosis_Number] = @Unique_Diagnosis_Number,
	[Name_Of_Diagnosis] = @Name_Of_Diagnosis,
	[Personal_Outpatient_Card_Id] = @Personal_Outpatient_Card_Id
	where [Id_Diagnosis] = @Id_Diagnosis
	end try
	begin catch
		print('Данный диагноз уже есть в таблице!')
	end catch
go

create or alter procedure [dbo].[Diagnosis_Delete]
@Id_Diagnosis [int]
as
	declare @any_child_record [int] = (select count(*) from [dbo].[Service_Diagnosis] where [Diagnosis_Id] = @Id_Diagnosis)
	if (@any_child_record > 0)
		print('Данный диагноз не может быть удален,  так как в таблице "Service_Diagnosis" есть связанные данные!')
	else
		delete from [dbo].[Diagnosis] where [Id_Diagnosis] = @Id_Diagnosis
go

create or alter procedure [dbo].[Sick_List_Insert]
@Sick_List_Number [varchar] (10), @Sick_List_Opening_Date [date], @Sick_List_Closing_Date [date], @Date_Of_Next_Visit [date], @Name_Organisation [varchar] (50), @Ticket_Id [int]
as
	begin try
		if (@Sick_List_Number = '__________') or (@Sick_List_Number = null) or (@Sick_List_Number = '')
		begin
		declare @new_Number [int] = ''
		declare @Part_Number [int] = (select convert(int, [Sick_List_Number]) from [dbo].[Sick_List])
		set @new_Number = @Part_Number + 1
		insert into [dbo].[Sick_List] ([Sick_List_Number], [Sick_List_Opening_Date], [Sick_List_Closing_Date], [Date_Of_Next_Visit], [Name_Organisation], [Ticket_Id])
		values (@new_Number, @Sick_List_Opening_Date, @Sick_List_Closing_Date, @Date_Of_Next_Visit, @Name_Organisation, @Ticket_Id)
		end
		else
		insert into [dbo].[Sick_List] ([Sick_List_Number], [Sick_List_Opening_Date], [Sick_List_Closing_Date], [Date_Of_Next_Visit], [Name_Organisation], [Ticket_Id])
		values (@Sick_List_Number, @Sick_List_Opening_Date, @Sick_List_Closing_Date, @Date_Of_Next_Visit, @Name_Organisation, @Ticket_Id)
	end try
	begin catch
		print('Данные уже есть в таблице!')
	end catch
go

create or alter procedure [dbo].[Sick_List_Update]
@Id_Sick_List [int], @Sick_List_Number [varchar] (10), @Sick_List_Opening_Date [date], @Sick_List_Closing_Date [date], @Date_Of_Next_Visit [date], @Name_Organisation [varchar] (50), @Ticket_Id [int]
as
	begin try
	update [dbo].[Sick_List] set
	[Sick_List_Number] = @Sick_List_Number,
	[Sick_List_Opening_Date] = @Sick_List_Opening_Date,
	[Sick_List_Closing_Date] = @Sick_List_Closing_Date,
	[Date_Of_Next_Visit] = @Date_Of_Next_Visit,
	[Name_Organisation] = @Name_Organisation,
	[Ticket_Id] = @Ticket_Id
	where [Id_Sick_List] = @Id_Sick_List
	end try
	begin catch
		print('Данный лист уже есть в таблице!')
	end catch
go

create or alter procedure [dbo].[Sick_List_Delete]
@Id_Sick_List [int]
as
	delete from [dbo].[Sick_List] where [Id_Sick_List] = @Id_Sick_List
go

create or alter procedure [dbo].[Employee_Profile_Insert]
@Treatment_Employee_Id [int], @Treatment_Profiles_Id [int]
as
	declare @exist_rectord [int] = (select count(*) from [dbo].[Employee_Profile]
	where [Treatment_Employee_Id] = @Treatment_Employee_Id and [Treatment_Profiles_Id] = @Treatment_Profiles_Id)
	if (@exist_rectord > 0 )
		print('Указанный профиль уже имеется у сотрудника!')
	else
	insert into [dbo].[Employee_Profile] ([Treatment_Employee_Id], [Treatment_Profiles_Id])
	values (@Treatment_Employee_Id, @Treatment_Profiles_Id)
go

create or alter procedure [dbo].[Employee_Profile_Update]
@Id_Employee_Profile [int], @Treatment_Employee_Id [int], @Treatment_Profiles_Id [int]
as
	declare @exist_rectord [int] = (select count(*) from [dbo].[Employee_Profile]
	where [Treatment_Employee_Id] = @Treatment_Employee_Id and [Treatment_Profiles_Id] = @Treatment_Profiles_Id)
	if (@exist_rectord > 0 )
		print('Указанный профиль уже имеется у сотрудника!')
	else
		update [dbo].[Employee_Profile] set
		[Treatment_Employee_Id] = @Treatment_Employee_Id,
		[Treatment_Profiles_Id] = @Treatment_Profiles_Id
		where
		[Id_Employee_Profile] = @Id_Employee_Profile

go

create or alter procedure [dbo].[Employee_Profile_Delete]
@Id_Employee_Profile [int]
as
	delete from [dbo].[Employee_Profile]
	where [Id_Employee_Profile] = @Id_Employee_Profile
go

create or alter procedure [dbo].[Service_Insert]
@Code_Of_Service [varchar] (3), @Ticket_Id [int]
as
	begin try
		declare @exist_record [int] = (select count(*) from [dbo].[Service]
		where [Code_Of_Service] = @Code_Of_Service and
			[Ticket_Id] = @Ticket_Id)
		if (@exist_record > 0)
			print('Указанный код услуги уже есть в талоне!')
		else
			insert into [dbo].[Service] ([Code_Of_Service], [Ticket_Id])
			values (@Code_Of_Service, @Ticket_Id)
	end try
	begin catch
		print('Указанный код уже есть в таблице!')
	end catch
go

create or alter procedure [dbo].[Service_Update]
@Id_Service [int], @Code_Of_Service [varchar] (3), @Ticket_Id [int]
as
	begin try
		declare @exist_record [int] = (select count(*) from [dbo].[Service]
		where [Code_Of_Service] = @Code_Of_Service and
			[Ticket_Id] = @Ticket_Id)
		if (@exist_record > 0)
		print('Указанный производитель уже есть у данной страны!')
		else
			update [dbo].[Service] set
			[Code_Of_Service] = @Code_Of_Service,
			[Ticket_Id] = @Ticket_Id
			where [Id_Service] = @Id_Service
	end try
	begin catch
		print('Указанный производитель уже есть в таблице!')
	end catch
go

create or alter procedure [dbo].[Service_Delete]
@Id_Service [int]
as
	declare @any_child_record [int] = (select count(*) from [dbo].[Service_Diagnosis]
	where [Service_Id] = @Id_Service)
	if (@any_child_record > 0 )
		print('Услуга не может быть удалена, так как в таблице "Service_Diagnosis", есть связанные данные!')
	else
		delete from [dbo].[Service]
		where
		[Id_Service] = @Id_Service
go

create or alter procedure [dbo].[Service_Diagnosis_Insert]
@Service_Id [int], @Diagnosis_Id [int]
as
	declare @exist_rectord [int] = (select count(*) from [dbo].[Service_Diagnosis]
	where [Service_Id] = @Service_Id and [Diagnosis_Id] = @Diagnosis_Id)
	if (@exist_rectord > 0 )
		print('Указанный диагноз уже имеется у услуги!')
	else
	insert into [dbo].[Service_Diagnosis] ([Service_Id], [Diagnosis_Id])
	values (@Service_Id, @Diagnosis_Id)
go

create or alter procedure [dbo].[Service_Diagnosis_Update]
@Id_Service_Diagnosis [int], @Service_Id [int], @Diagnosis_Id [int]
as
	declare @exist_rectord [int] = (select count(*) from [dbo].[Service_Diagnosis]
	where [Service_Id] = @Service_Id and [Diagnosis_Id] = @Diagnosis_Id)
	if (@exist_rectord > 0 )
		print('Указанный диагноз уже имеется у услуги!')
	else
		update [dbo].[Service_Diagnosis] set
		[Service_Id] = @Service_Id,
		[Diagnosis_Id] = @Diagnosis_Id
		where
		[Id_Service_Diagnosis] = @Id_Service_Diagnosis
go

create or alter procedure [dbo].[Service_Diagnosis_Delete]
@Id_Service_Diagnosis [int]
as
	delete from [dbo].[Service_Diagnosis]
	where [Id_Service_Diagnosis] = @Id_Service_Diagnosis
go

create or alter procedure [dbo].[Service_Treatment_Insert]
@Service_Id [int], @Recommended_Treatment_Id [int]
as
	declare @exist_rectord [int] = (select count(*) from [dbo].[Service_Treatment]
	where [Service_Id] = @Service_Id and [Recommended_Treatment_Id] = @Recommended_Treatment_Id)
	if (@exist_rectord > 0 )
		print('Указанное рекомендуемое лечение уже имеется у услуги!')
	else
	insert into [dbo].[Service_Treatment] ([Service_Id], [Recommended_Treatment_Id])
	values (@Service_Id, @Recommended_Treatment_Id)
go

create or alter procedure [dbo].[Service_Treatment_Update]
@Id_Service_Treatment [int], @Service_Id [int], @Recommended_Treatment_Id [int]
as
	declare @exist_rectord [int] = (select count(*) from [dbo].[Service_Treatment]
	where [Service_Id] = @Service_Id and [Recommended_Treatment_Id] = @Recommended_Treatment_Id)
	if (@exist_rectord > 0 )
		print('Указанное рекомендуемое лечение уже имеется у услуги!')
	else
		update [dbo].[Service_Treatment] set
		[Service_Id] = @Service_Id,
		[Recommended_Treatment_Id] = @Recommended_Treatment_Id
		where
		@Id_Service_Treatment = [Id_Service_Treatment]
go

create or alter procedure [dbo].[Service_Treatment_Delete]
@Id_Service_Treatment [int]
as
	delete from [dbo].[Service_Treatment]
	where [Id_Service_Treatment] = @Id_Service_Treatment
go

create or alter procedure [dbo].[Recommended_Taking_Medications_Insert]
@Recommended_Treatment_Id [int], @Taking_Medications_Id [int]
as
	declare @exist_rectord [int] = (select count(*) from [dbo].[Recommended_Taking_Medications]
	where [Recommended_Treatment_Id] = @Recommended_Treatment_Id and [Taking_Medications_Id] = @Taking_Medications_Id)
	if (@exist_rectord > 0 )
		print('Указанные лекарство уже имеется у лечения!')
	else
	insert into [dbo].[Recommended_Taking_Medications] ([Recommended_Treatment_Id], [Taking_Medications_Id])
	values (@Recommended_Treatment_Id, @Taking_Medications_Id)
go

create or alter procedure [dbo].[Recommended_Taking_Medications_Update]
@Id_Recommended_Taking_Medications [int], @Recommended_Treatment_Id [int], @Taking_Medications_Id [int]
as
	declare @exist_rectord [int] = (select count(*) from [dbo].[Recommended_Taking_Medications]
	where [Recommended_Treatment_Id] = @Recommended_Treatment_Id and [Taking_Medications_Id] = @Taking_Medications_Id)
	if (@exist_rectord > 0 )
		print('Указанное лекарство уже имеется у лечения!')
	else
		update [dbo].[Recommended_Taking_Medications] set
		[Recommended_Treatment_Id] = @Recommended_Treatment_Id,
		[Taking_Medications_Id] = @Taking_Medications_Id
		where
		[Id_Recommended_Taking_Medications] = @Id_Recommended_Taking_Medications
go

create or alter procedure [dbo].[Recommended_Taking_Medications_Delete]
@Id_Recommended_Taking_Medications [int]
as
	delete from [dbo].[Recommended_Taking_Medications]
	where [Id_Recommended_Taking_Medications] = @Id_Recommended_Taking_Medications
go

create or alter procedure [dbo].[Employee_Position_Insert]
@Treatment_Employee_Id [int], @Position_Id [int]
as
	declare @exist_rectord [int] = (select count(*) from [dbo].[Employee_Position]
	where [Treatment_Employee_Id] = @Treatment_Employee_Id and [Position_Id] = @Position_Id)
	if (@exist_rectord > 0 )
		print('Указанная должность уже имеется у сотрудника!')
	else
	insert into [dbo].[Employee_Position] ([Treatment_Employee_Id], [Position_Id])
	values (@Treatment_Employee_Id, @Position_Id)
go

create or alter procedure [dbo].[Employee_Position_Update]
@Id_Employee_Position [int], @Treatment_Employee_Id [int], @Position_Id [int]
as
	declare @exist_rectord [int] = (select count(*) from [dbo].[Employee_Position]
	where [Treatment_Employee_Id] = @Treatment_Employee_Id and [Position_Id] = @Position_Id)
	if (@exist_rectord > 0 )
		print('Указанная должность уже имеется у сотрудника!')
	else
		update [dbo].[Employee_Position] set
		[Treatment_Employee_Id] = @Treatment_Employee_Id,
		[Position_Id] = @Position_Id
		where
		[Id_Employee_Position] = @Id_Employee_Position
go

create or alter procedure [dbo].[Employee_Position_Delete]
@Id_Employee_Position [int]
as
	delete from [dbo].[Employee_Position]
	where [Id_Employee_Position] = @Id_Employee_Position
go

create or alter procedure [dbo].[Outpatient_Card_Employee_Insert]
@Treatment_Employee_Id [int], @Personal_Outpatient_Card_Id [int]
as
	declare @exist_rectord [int] = (select count(*) from [dbo].[Outpatient_Card_Employee]
	where [Treatment_Employee_Id] = @Treatment_Employee_Id and [Personal_Outpatient_Card_Id] = @Personal_Outpatient_Card_Id)
	if (@exist_rectord > 0 )
		print('Указанная карта уже имеется у сотрудника!')
	else
	insert into [dbo].[Outpatient_Card_Employee] ([Treatment_Employee_Id], [Personal_Outpatient_Card_Id])
	values (@Treatment_Employee_Id, @Personal_Outpatient_Card_Id)
go

create or alter procedure [dbo].[Outpatient_Card_Employee_Update]
@Id_Outpatient_Card_Employee [int], @Treatment_Employee_Id [int], @Personal_Outpatient_Card_Id [int]
as
	declare @exist_rectord [int] = (select count(*) from [dbo].[Outpatient_Card_Employee]
	where [Treatment_Employee_Id] = @Treatment_Employee_Id and [Personal_Outpatient_Card_Id] = @Personal_Outpatient_Card_Id)
	if (@exist_rectord > 0 )
		print('Указанная карта уже имеется у сотрудника!')
	else
		update [dbo].[Outpatient_Card_Employee] set
		[Treatment_Employee_Id] = @Treatment_Employee_Id,
		[Personal_Outpatient_Card_Id] = @Personal_Outpatient_Card_Id
		where
		[Id_ Outpatient_Card_Employee] = @Id_Outpatient_Card_Employee
go

create or alter procedure [dbo].[Outpatient_Card_Employee_Delete]
@Id_Outpatient_Card_Employee [int]
as
	delete from [dbo].[Outpatient_Card_Employee]
	where [Id_ Outpatient_Card_Employee] = @Id_Outpatient_Card_Employee
go