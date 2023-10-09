use [GKP_DataBase]
go

--I
select top(3) [Second_Name_Empl]+' '+[First_Name_Empl]+' '+[Middle_Name_Empl] as '��� �����', [Name_Medical_Degree] as '�������', count([Treatment_Employee_Id]) as '���-�� �������' from [dbo].[Ticket] 
inner join [dbo].[Treatment_Employee] on [Treatment_Employee_Id] = [Id_Treatment_Employee]
inner join [dbo].[Medical_Degree] on [Medical_Degree_Id] = [Id_Medical_Degree]
where [Name_Medical_Degree] in ('������') 
group by [Treatment_Employee_Id], [Second_Name_Empl], [First_Name_Empl], [Middle_Name_Empl], [Name_Medical_Degree]
order by max([Treatment_Employee_Id]) DESC
go

--II
select [Second_Name_Cl]+' '+[First_Name_Cl]+' '+[Middle_Name_Cl] as "��� �������", count([Id_Sick_List]) as '���������� ����������' from [dbo].[Sick_List]
inner join [dbo].[Ticket] on [Ticket_Id] = [Id_Ticket]
inner join [dbo].[Personal_Outpatient_Card] on [Personal_Outpatient_Card_Id] = [Id_Personal_Outpatient_Card]
inner join [dbo].[Client] on [Client_Id] = [Id_Client]
group by [Second_Name_Cl], [First_Name_Cl], [Middle_Name_Cl], [Ticket_Id]
go

--III
select [Second_Name_Cl]+' '+[First_Name_Cl]+' '+[Middle_Name_Cl] as "��� �������", [Personal_Outpatient_Card_Number] as '����� �����', concat(upper([Unique_Diagnosis_Number]), ' | ', lower([Name_Of_Diagnosis])) as '�������' from [dbo].[Diagnosis]
inner join [dbo].[Personal_Outpatient_Card] on [Personal_Outpatient_Card_Id] = [Id_Personal_Outpatient_Card]
inner join [dbo].[Client] on [Client_Id] = [Id_Client]
order by [Unique_Diagnosis_Number] DeSC
go

--IV
select [Second_Name_Empl]+' '+[First_Name_Empl]+' '+[Middle_Name_Empl] as '��� �����', [Name_Medical_Degree] as '�������', [Name_Position] as '���������', [Name_Medical_Departments] as '���������' from [dbo].[Employee_Position] 
inner join [dbo].[Treatment_Employee] on [Treatment_Employee_Id] = [Id_Treatment_Employee]
inner join [dbo].[Medical_Degree] on [Medical_Degree_Id] = [Id_Medical_Degree]
inner join [dbo].[Medical_Departments] on [Medical_Departments_Id] = [Id_Medical_Departments]
inner join [dbo].[Position] on [Position_Id] = [Id_Position]
go

--V
select [Name_Medical_Departments] as '��������', count([Id_Ticket]) as '���-�� �������', '������� ������������' = case
when count([Id_Ticket]) > 4 then '������ ������������'
when count([Id_Ticket]) between 2 and 4 then '������� ������������'
when count([Id_Ticket]) < 2 then '����� ������������'
end from [dbo].[Ticket]
inner join [dbo].[Treatment_Employee] on [Treatment_Employee_Id] = [Id_Treatment_Employee]
inner join [dbo].[Medical_Departments] on [Medical_Departments_Id] = [Id_Medical_Departments]
group by [Name_Medical_Departments]
go

--VI
select [Name_Medical_Departments] as '���������', count([Id_Treatment_Employee]) as '���-�� �����������', string_agg([Second_Name_Empl]+' '+[First_Name_Empl]+' '+[Middle_Name_Empl], ', ') as '���' from [dbo].[Treatment_Employee]
inner join [dbo].[Medical_Departments] on [Medical_Departments_Id] = [Id_Medical_Departments]
group by [Name_Medical_Departments]
go

--VII
select ROW_NUMBER() over (order by [Ticket_Number]) as '� ������',[Code_Of_Service] as '��� ������', [Unique_Diagnosis_Number] as '��� ��������', [Name_Of_Diagnosis] as '��������' from [dbo].[Service_Diagnosis] 
inner join [dbo].[Service] on [Service_Id] = [Id_Service]
inner join [dbo].[Diagnosis] on [Diagnosis_Id] = [Id_Diagnosis]
inner join [dbo].[Ticket] on [Ticket_Id] = [Id_Ticket]
go

--VIII
select [Exercise_Program] as '����������', [Name_Taking_Medications] as '��������' from [dbo].[Recommended_Taking_Medications]
inner join [dbo].[Recommended_Treatment] on [Recommended_Treatment_Id] = [Id_Recommended_Treatment]
inner join [dbo].[Taking_Medications] on [Taking_Medications_Id] = [Id_Taking_Medications]
go

--IX
create or alter function [dbo].[Sum_Client](@Name_Medical_Departments [varchar] (max))
returns [int]
with execute as caller
as
begin
	return(select count([Id_Ticket]) from [dbo].[Ticket]
	inner join [dbo].[Treatment_Employee] on [Treatment_Employee_Id] = [Id_Treatment_Employee]
	inner join [dbo].[Medical_Departments] on [Medical_Departments_Id] = [Id_Medical_Departments]
	where [Name_Medical_Departments] = @Name_Medical_Departments)
end
go
select [dbo].[Sum_Client]('��������������� ��������� �1 �������� ��. ������')
go

--X
create or alter function [dbo].[Card_Diagnosis](@Card_Number [varchar] (13))
returns table
as
		return(select [Personal_Outpatient_Card_Number] as "� �����", 
		STRING_AGG('���: '+[Unique_Diagnosis_Number]+'��������: '+[Name_Of_Diagnosis], ' ') as "�������", [Code_Of_Service] as "������" from [dbo].[Sick_List]
		inner join [dbo].[Service] on [Service_Id] = [Id_Service]
		inner join [dbo].[Diagnosis] on [Diagnosis_Id] = [Id_Diagnosis]
		 inner join [dbo].[Personal_Outpatient_Card] on [Personal_Outpatient_Card_Id] = [Id_Personal_Outpatient_Card]
		 inner join [dbo].[Ticket] on [Ticket_Id] = [Id_Ticket]
		 where [Personal_Outpatient_Card_Number] = @Card_Number
		 group by [Personal_Outpatient_Card_Number], [Code_Of_Service])
go
select * from [dbo].[Card_Diagnosis]('0000000005')