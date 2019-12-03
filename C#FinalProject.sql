/* Check whether the database already exists */
if exists(select
	1
from
	master.dbo.sysdatabases
where
	          name = 'Music_Store')
		begin
	drop database Music_Store
	print ''
	print '*** Dropping Database Music_Store'
end
go

print ''
print '*** Creating Database Music_Store'
create database Music_Store
go

use Music_Store
go


print ''
print '*** Creating Employee Table'
go

create table dbo.Employee
(
	EmployeeId   int           identity (100000, 1) not null,
	FirstName    nvarchar(50)  not null,
	LastName     nvarchar(50)  not null,
	PhoneNumber  nvarchar(11)  not null,
	Email        nvarchar(250) not null,
	PasswordHash nvarchar(100) not null
		default '9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E',
	Active       bit           not null default 1,
	constraint pk_EmployeeID
		primary key (EmployeeID asc),
	constraint ak_Email
		unique (Email asc)
)
go

print ''
print '*** Inserting sample Employees'
go

insert
into
	dbo.Employee
	( FirstName, LastName, PhoneNumber, Email )
values
	( 'Admin', 'System', '4443332222', 'Sys.Admin@music.com' ),
	( 'John', 'Smith', '1112223333', 'john.smith@music.com' ),
	( 'Saint', 'Nicholas', '3334442345', 'saint.nicholas@music.com' )
go

print ''
print '*** Creating sp_create_employee'
go

create procedure sp_create_employee
	(
	@FirstName   nvarchar(50),
	@LastName    nvarchar(50),
	@PhoneNumber nvarchar(11)
)
as
begin
	declare @Email nvarchar(50)
	set @Email = Lower(@FirstName + '.' + @LastName + '@music.com')

	insert
	into
		dbo.Employee
		( FirstName, LastName, PhoneNumber, Email )
	values
		( @FirstName, @LastName, @PhoneNumber, @Email )
	return SCOPE_IDENTITY()
end
go


print ''
print '*** Creating sp_authenticate_employee'
go

create procedure sp_authenticate_employee
	(
	@Email        nvarchar(50),
	@PasswordHash nvarchar(100)
)
as
begin
	select
		count(EmployeeID)
	from
		dbo.Employee
	where
		  Email = @Email
		and PasswordHash = @PasswordHash
		and Active = 1
	return @@RowCount
end
go


print ''
print '*** Creating sp_retrieve_employee_by_email'
go

create procedure sp_retrieve_employee_by_email
	(
	@Email nvarchar(50)
)
as
begin
	select
		EmployeeID,
		FirstName,
		LastName,
		PhoneNumber
	from
		dbo.Employee
	where
		  Email = @Email
		and Active = 1
end
go

print ''
print '*** Creating sp_retrieve_employee_by_employeeID'
go

create procedure sp_retrieve_employee_by_employeeID
	(
	@EmployeeID int
)
as
begin
	select
		FirstName,
		LastName,
		Phonenumber,
		Email
	from
		dbo.Employee
	where
		  EmployeeID = @EmployeeID
		and Active = 1
end
go

print ''
print '*** Creating sp_select_all_employees'
go

create procedure sp_select_all_employees
	(
	@Active bit
)
as
begin
	select
		EmployeeID,
		FirstName,
		LastName,
		PhoneNumber,
		Email
	from
		dbo.Employee
	where Active = @Active
	order by LastName
end
go

print ''
print '*** Creating sp_select_employee_by_last_name_like'
go

create procedure sp_select_employee_by_last_name_like
	(
	@LastNameLike nvarchar(50)
)
as
begin
	select
		EmployeeID,
		FirstName,
		LastName,
		PhoneNumber,
		Email
	from
		dbo.Employee
	where
		  LastName like '%' + @LastNameLike + '%'
		and Active = 1
end
go

print ''
print '*** Creating sp_update_employee_profile'
go

create procedure sp_update_employee_profile
	(
	@EmployeeID     int,
	@OldFirstName   nvarchar(50),
	@OldLastName    nvarchar(50),
	@OldEmail       nvarchar(50),
	@OldPhoneNumber nvarchar(11),
	@NewFirstName   nvarchar(50),
	@NewLastName    nvarchar(50),
	@NewEmail       nvarchar(50),
	@NewPhoneNumber nvarchar(11)
)
as
begin
	update dbo.Employee
	set
		FirstName   = @NewFirstName,
		LastName    = @NewLastName,
		Email       = @NewEmail,
		PhoneNumber = @NewPhoneNumber
	where
		  EmployeeID = @EmployeeID
		and Email = @OldEmail
		and FirstName = @OldFirstName
		and LastName = @OldLastName
		and PhoneNumber = @OldPhoneNumber
		and Active = 1
end
go

print ''
print '*** Creating sp_update_employee_password'
go

create procedure sp_update_employee_password
	(
	@EmployeeID      int,
	@OldPasswordHash nvarchar(100),
	@NewPasswordHash nvarchar(100)
)
as
begin
	update dbo.Employee
	set PasswordHash = @NewPasswordHash
	where
		  EmployeeId = @EmployeeID
		and PasswordHash = @OldPasswordHash
		and Active = 1
	return @@RowCount
end
go

print ''
print '*** Creating sp_update_employee_password_by_email'
go

create procedure sp_update_employee_password_by_email
	(
	@Email           nvarchar(50),
	@NewPasswordHash nvarchar(100)
)
as
begin
	update dbo.Employee
	set PasswordHash = @NewPasswordHash
	where
		  Email = @Email
		and Active = 1
	return @@RowCount
end
go

print ''
print '*** Creating sp_delete_employee'
go

create procedure sp_delete_employee
	(
	@EmployeeID int,
	@FirstName  nvarchar(50),
	@LastName   nvarchar(50)
)
as
begin
	declare @employee_to_delete int

	select
		@employee_to_delete = (
			select
			count(*)
		from
			dbo.Employee
		where
				  EmployeeID = @EmployeeID
			and FirstName = @FirstName
			and LastName = @LastName
		)

	if @employee_to_delete <> 1
			raiserror (15600, -1, -1, 'sp_delete_employee')
		else
			
			delete
			from dbo.EmployeeRole
			where EmployeeID = @EmployeeID

	delete
	from dbo.Employee
	where
		  EmployeeID = @EmployeeID
		and FirstName = @FirstName
		and LastName = @LastName
	return @@RowCount
end
go

print ''
print '*** Creating sp_deactivate_employee'
go

create procedure sp_deactivate_employee
	(
	@EmployeeID int,
	@FirstName  nvarchar(50),
	@LastName   nvarchar(50)
)
as
begin
	declare @user_to_deactivate int

	select
		@user_to_deactivate = (
			select
			count(*)
		from
			dbo.Employee
		where
				  EmployeeID = @EmployeeID
			and FirstName = @FirstName
			and LastName = @LastName
		)

	if @user_to_deactivate <> 1
			raiserror (15600, -1, -1, 'sp_delete_user')
		else
			update dbo.Employee
			set Active = 0
			where
				  EmployeeID = @EmployeeID
		and FirstName = @FirstName
		and LastName = @LastName
		and Active = 1
	return @@RowCount
end
go

print ''
print '*** Creating sp_reactivate_employee'
go

create procedure sp_reactivate_employee
	(
	@EmployeeID int,
	@FirstName  nvarchar(50),
	@LastName   nvarchar(50)
)
as
begin
	declare @user_to_reactivate int

	select
		@user_to_reactivate = (
			select
			count(*)
		from
			dbo.Employee
		where
				  EmployeeID = @EmployeeID
			and FirstName = @FirstName
			and LastName = @LastName
		)

	if @user_to_reactivate <> 1
			raiserror (15600, -1, -1, 'sp_reactivate_user')
		else
			update dbo.Employee
			set Active = 1
			where
				  EmployeeID = @EmployeeID
		and FirstName = @FirstName
		and LastName = @LastName
		and Active = 0
	return @@RowCount
end
go

print ''
print '*** Creating Role Table'
go

create table dbo.Role
(
	RoleID      nvarchar(50)  not null,
	Description nvarchar(250) null,
	constraint pk_RoleID
	primary key (RoleID asc)
)
go

print ''
print '*** Inserting Roles'
go

insert
into
	dbo.Role
	(
	RoleID
	)
values
	( 'admin' ),
	( 'supervisor' ),
	( 'employee' ),
	( 'customer' )
go

print ''
print '*** Creating sp_create_role'
go

create procedure sp_create_role
	(
	@RoleID      nvarchar(50),
	@Description nvarchar(250)
)
as
begin
	insert
	into
		dbo.Role
		( RoleID, Description )
	values
		( @RoleID, @Description )
end
go

print ''
print '*** Creating sp_delete_role'
go

create procedure sp_delete_role
	(
	@RoleID nvarchar(50)
)
as
begin
	delete
	from dbo.Role
	where RoleID = @RoleID
end
go

print ''
print '*** Creating EmployeeRole Table'
go

create table dbo.EmployeeRole
(
	EmployeeID int          not null,
	RoleID     nvarchar(50) not null,
	constraint pk_EmployeeID_RoleID
	primary key (EmployeeID asc, RoleID asc),
	constraint fk_Employee_EmployeeID
		foreign key (EmployeeID)
			references dbo.Employee (EmployeeID),
	constraint fk_Role_RoleID
		foreign key (RoleID)
			references dbo.Role (RoleID)
			on update cascade
			on delete cascade,
)

print ''
print 'Inserting Sample EmployeeRole records'
go

insert
into
	dbo.EmployeeRole
	( EmployeeID, RoleID )
values
	( 100000, 'admin' ),
	( 100001, 'employee' ),
	( 100002, 'supervisor' ),
	( 100002, 'employee' )
go

print ''
print '*** Creating sp_add_roles_for_employeeID'
go

create procedure sp_add_roles_for_employeeID
	(
	@EmployeeID int,
	@RoleID     nvarchar(50)
)
as
begin
	insert
	into
		dbo.EmployeeRole
		( EmployeeID, RoleID )
	values
		( @EmployeeID, @RoleID )
end
go

print ''
print '*** Creating sp_select_all_roles_for_employeeID'
go

create procedure sp_select_all_roles_for_employeeID
	(
	@EmployeeID int
)
as
begin
	select
		RoleID
	from
		dbo.EmployeeRole
	where EmployeeID = @EmployeeID
end
go


print ''
print '*** Creating sp_update_role_for_employeeID'
go

create procedure sp_update_role_for_employeeID
	(
	@EmployeeID int,
	@OldRoleID  nvarchar(50),
	@NewRoleID  nvarchar(50)
)
as
begin
	update dbo.EmployeeRole
	set RoleID = @NewRoleID
	where
		  EmployeeID = @EmployeeID
		and RoleID = @OldRoleID
end
go

print ''
print '*** Creating sp_remove_role_for_employeeID'
go

create procedure sp_remove_role_for_employeeID
	(
	@EmployeeID int,
	@RoleID     nvarchar(50)
)
as
begin
	delete
	from dbo.EmployeeRole
	where
		  EmployeeId = @EmployeeID
		and RoleID = @RoleID
end
go

print ''
print '*** Creating Customer Table'
go

create table dbo.Customer
(
	CustomerID  int          identity (100000, 1) not null,
	FirstName   nvarchar(50) not null,
	LastName    nvarchar(50) not null,
	PhoneNumber nvarchar(11) not null,
	Email       nvarchar(50) not null,
	-- (Will be used for Web only Not used for Desktop version)
	-- PasswordHash nvarchar(100) not null,
	Active      bit          not null default 1,
	constraint pk_CustomerID
		primary key (CustomerID asc)
)
go

print ''
print '*** Inserting sample Customer records'
go

insert
into
	dbo.Customer
	( FirstName, LastName, PhoneNumber, Email )
values
	( 'Daniel', 'Taylor', '3334442222', 'daniel.taylor@gmail.com' ),
	( 'Stephanie', 'Rodrigez', '1234567890', 'stephanie.Rodrigez@outlook.com' ),
	( 'Jim', 'Glasgow', '1987654321', 'jim.glasgow@whatever.com' )
go

print ''
print '*** Creating sp_create_customer'
go

create procedure sp_create_customer
	(
	@FirstName   nvarchar(50),
	@LastName    nvarchar(50),
	@PhoneNumber nvarchar(11),
	@Email       nvarchar(50)
)
as
begin
	insert
	into
		dbo.Customer
		( FirstName, LastName, PhoneNumber, Email )
	values
		( @FirstName, @LastName, @PhoneNumber, @Email )
	return scope_identity()
end
go

print ''
print '*** Creating sp_select_customer_by_customer_id'
go

create procedure sp_select_customer_by_customer_id
	(
	@CustomerID int
)
as
begin
	select
		FirstName,
		LastName,
		PhoneNumber,
		Email
	from
		dbo.customer
	where
		  CustomerID = @CustomerID
		and Active = 1
end
go

print ''
print '*** Creating sp_select_customer_by_first_or_Last_name'
go

create procedure sp_select_customer_by_first_or_Last_name
	(
	@FirstName nvarchar(50),
	@LastName  nvarchar(50)
)
as
begin
	select
		CustomerID,
		FirstName,
		LastName,
		PhoneNumber,
		Email
	from
		dbo.Customer
	where
		 FirstName = @FirstName
		or LastName = @LastName
		and Active = 1
end
go

print ''
print '*** Creating sp_select_customer_by_first_last_name_like'
go

create procedure sp_select_customer_by_first_last_name_like
	(
	@FirstName nvarchar(50),
	@LastName  nvarchar(50)
)
as
begin
	select
		CustomerID,
		FirstName,
		LastName,
		PhoneNumber,
		Email
	from
		dbo.Customer
	where
		 FirstName like '%' + @FirstName + '%'
		or LastName like '%' + @LastName + '%'
		and Active = 1
end
go

print ''
print '*** Creating sp_select_all_customers'
go

create procedure sp_select_all_customers
	(
	@Active bit
)
as
begin
	select
		CustomerID,
		FirstName,
		LastName,
		PhoneNumber,
		Email
	from
		dbo.Customer
	where Active = @Active
end
go

print ''
print '*** creating sp_select_customer_by_email_like'
go

create procedure sp_select_customer_by_email_like
	(
	@Email nvarchar(50)
)
as
begin
	select
		CustomerID,
		FirstName,
		LastName,
		PhoneNumber,
		Email
	from
		dbo.Customer
	where
		 Email like @Email + '%'
		and Active = 1
end
go

print ''
print '*** Creating sp_update_customer_profile'
go

create procedure sp_update_customer_profile
	(
	@CustomerID     int,
	@OldLastName    nvarchar(50),
	@OldPhoneNumber nvarchar(11),
	@OldEmail       nvarchar(50),
	@NewLastName    nvarchar(50),
	@NewPhoneNumber nvarchar(11),
	@NewEmail       nvarchar(50)
)
as
begin
	update dbo.customer
	set
		LastName    = @NewLastName,
		PhoneNumber = @NewPhoneNumber,
		Email       = @NewEmail
	where
		  CustomerId = @CustomerID
		and LastName = @OldLastName
		and PhoneNumber = @OldPhoneNumber
		and Email = @OldEmail
		and Active = 1
end
go

print ''
print '*** Creating sp_deactivate_customer'
go

create procedure sp_deactivate_customer
	(
	@CustomerID int,
	@FirstName  nvarchar(50),
	@LastName   nvarchar(50)
)
as
begin
	declare @customer_to_deactivate int

	select
		@customer_to_deactivate = (
			select
			count(*)
		from
			dbo.Customer
		where
				  CustomerID = @CustomerID
			and FirstName = @FirstName
			and LastName = @LastName
		)

	if @customer_to_deactivate <> 1
			raiserror (15600, -1, -1, 'sp_deactivate_customer')
		else
			update dbo.Customer
			set Active = 0
			where
				  CustomerID = @CustomerID
		and FirstName = @FirstName
		and LastName = @LastName
		and Active = 1
	return @@RowCount
end
go

print ''
print '*** Creating sp_delete_customer'
go

create procedure sp_delete_customer
	(
	@CustomerID int,
	@FirstName  nvarchar(50),
	@LastName   nvarchar(50)
)
as
begin
	declare @customer_to_delete int

	select
		@customer_to_delete = (
			select
			count(*)
		from
			dbo.Customer
		where
				  CustomerID = @CustomerID
			and FirstName = @FirstName
			and LastName = @LastName
		)

	if @customer_to_delete <> 1
			raiserror (15600, -1, -1, 'sp_delete_customer')
		else
			delete
			from dbo.Customer
			where
				  CustomerID = @CustomerID
		and FirstName = @FirstName
		and LastName = @LastName
	return @@RowCount
end
go

print ''
print '*** Creating InstrumentStatus table'
go

create table dbo.InstrumentStatus
(
	InstrumentStatusID nvarchar(50)  not null,
	Description        nvarchar(250) null,
	constraint pk_InstrumentStatusID
	primary key (InstrumentStatusID asc)
)
go

print ''
print '*** Inserting Sample InstrumentStatus records'

insert
into
	dbo.InstrumentStatus
	( InstrumentStatusID, Description )
values
	( 'All', 'Used for only viewing all Instruments' ),
	( 'Available', 'Available for any time of transaction' ),
	( 'For Sale', 'For Sale ONLY' ),
	( 'For Rent', 'For Rent ONLY' ),
	( 'For Rent To Own', 'For Rent To Own ONLY' ),
	('Rent To Own', 'Currenly under Rent to Own contract'),
	( 'Rented', 'Is currently rented' ),
	( 'Sold', 'Instrument has been sold' )
go

print ''
print '*** Creating sp_select_all_statuses'
go

create procedure sp_select_all_statuses
as
begin
	select
		InstrumentStatusID
	from
		InstrumentStatus
end
go


print ''
print '*** Creating sp_create_InstrumentStatusID'
go

create procedure sp_create_InstrumentStatusID
	(
	@InstrumentStatusID nvarchar(50),
	@Description        nvarchar(250)
)
as
begin
	insert
	into
		dbo.InstrumentStatus
		( InstrumentStatusID, Description )
	values
		( @InstrumentStatusID, @Description )
end
go

print ''
print '*** Creating sp_select_all_instrument_Status_IDs'
go

create procedure sp_select_all_instrument_Status_IDs
as
begin
	select
		InstrumentStatusID
	from
		InstrumentStatus
end
print ''
print '*** Creating sp_delete_InstrumentStatusID'
go

create procedure sp_delete_InstrumentStatusID
	(
	@InstrumentStatusID nvarchar(50)
)
as
begin
	delete
	from dbo.InstrumentStatus
	where InstrumentStatusID = @InstrumentStatusID
end
go

print ''
print '*** Creating sp_select_all_InstrumentStatusID'
go

create procedure sp_select_all_InstrumentStatusID
as
begin
	select
		InstrumentStatusID
	from
		dbo.InstrumentStatus
end
go

print ''
print '*** Creating RentalTerm Table'
go

create table dbo.RentalTerm
(
	RentalTermID nvarchar(50) not null,
	TermLength   int          not null,
	RentalCost   money        not null,
	constraint pk_RentalTermID
	primary key (RentalTermID asc)
)
go

print ''
print '*** Inserting sample RentalTerm Records'
go

insert
into
	dbo.RentalTerm
	( RentalTermID, TermLength, RentalCost )
values
	( '30 Day', 30, 15.00 ),
	( '90 Day', 90, 45.00 ),
	( '12 Months', 12, 90.00 ),
	( '24 Months', 24, 180.00 )
go

print ''
print '*** Creating sp_create_RentalTerm'
go

create procedure sp_create_RentalTerm
	(
	@RentalTermID nvarchar(50),
	@TermLength   int,
	@RentalCost   money
)
as
begin
	insert
	into
		dbo.RentalTerm
		( RentalTermID, TermLength, RentalCost )
	values
		( @RentalTermID, @TermLength, @RentalCost )
end
go

print ''
print '*** Creating sp_delete_RentalTerm'
go

create procedure sp_delete_RentalTerm
	(
	@RentalTermID nvarchar(50),
	@TermLength   int,
	@RentalCost   money
)
as
begin
	delete
	from dbo.RentalTerm
	where
		  RentalTermID = @RentalTermID
		and TermLength = @TermLength
		and RentalCost = @RentalCost
end
go

print ''
print '*** Creating table PrepList'
go

create table dbo.PrepList
(
	PrepListID  int           identity (100000, 1) not null,
	Description nvarchar(250) null,
	constraint pk_PrepListID
	primary key (PrepListID asc)
)

print ''
print '*** Inserting Sample PrepList records'
go

insert
into
	dbo.PrepList
	( Description )
values
	( 'Check Tuning' ),
	( 'Check for dents' ),
	( 'Check slides' ),
	( 'Check corks, springs, and pads' ),
	( 'check, string, tuning pegs, bridge, and neck' ),
	( 'Check head tightness, tuning, overall condition' )
go

print ''
print '*** Creating sp_create_PrepListID'
go

create procedure sp_create_PrepListID
	(
	@Description nvarchar(250)
)
as
begin
	insert
	into
		dbo.PrepList
		( Description )
	values
		( @Description )
	return scope_identity()
end
go

print ''
print '*** Creating sp_update_PrepListID'
go

create procedure sp_update_PrepListID
	(
	@PrepListID     int,
	@OldDescription nvarchar(250),
	@NewDescription nvarchar(250)
)
as
begin
	update dbo.PrepList
	set Description = @OldDescription
	where
		  PrepListID = @PrepListID
		and Description = @NewDescription
	return @@RowCount
end
go

print ''
print '*** Creating sp_delete_PrepListID'
go

create procedure sp_delete_PreplistID
	(
	@PrepListID  int,
	@Description nvarchar(250)
)
as
begin
	delete
	from dbo.PrepList
	where
		  PrepListID = @PrepListID
		and Description = @Description
	return @@RowCount
end
go

print ''
print '*** Creating sp_select_all_PrepList'
go

create procedure sp_select_all_PrepList
as
begin
	select
		PrepListID,
		Description
	from
		dbo.PrepList
end
go

print ''
print '*** Creating InstrumentFamily Table'
go

create table instrumentFamily
(
	InstrumentFamilyID nvarchar(50) not null,
	constraint pk_instrumentFamilyID
	primary key (InstrumentFamilyID asc)
)
go

print ''
print 'inserting data for instrumentFamily table'
go

insert
into
	instrumentFamily
	( instrumentFamilyID )
values
	( 'Brass' ),
	( 'Percussion' ),
	( 'String' ),
	( 'Woodwind' )
go

print ''
print '*** Creating sp_select_all_Instrument_family'
go

create procedure sp_select_all_instrument_family
as
begin
	select
		instrumentFamilyID
	from
		instrumentFamily
end
go

print ''
print '*** Creating InstrumentType Table'
go

create table InstrumentType
(
	InstrumentTypeID   nvarchar(50) not null,
	RentalTermID       nvarchar(50) not null,
	PrepListID         int          null,
	InstrumentFamilyID nvarchar(50) not null,
	constraint pk_InstrumentTypeID
		primary key (InstrumentTypeID asc),
	constraint fk_InstrumentType_RentalID
		foreign key (RentalTermID)
			references RentalTerm (RentalTermID),
	constraint fk_InstrumentType_PrepList
		foreign key (PrepListID)
			references PrepList (PrepListId),
	constraint fk_InstrumentType_InstrumentFamilyID
		foreign key (InstrumentFamilyID)
			references InstrumentFamily (InstrumentFamilyID)
)
go

print ''
print '*** Inserting sample data into InstrumentType Table'
go

insert
into
	dbo.InstrumentType
	( InstrumentTypeID, RentalTermID, PrepListID, InstrumentFamilyID )
values
	( 'Tenor Saxophone', '12 Months', 100003, 'Woodwind' ),
	( 'Alto Saxophone', '12 Months', 100003, 'Woodwind' ),
	( 'Clarinet', '90 Day', 100003, 'Woodwind' ),
	( 'Cello', '24 Months', 100004, 'String' ),
	( 'Bongo', '12 Months', 100005, 'Percussion' ),
	( 'Cornet', '24 Months', 100002, 'Brass' ),
	( 'Trumpet', '24 Months', 100002, 'Brass' ),
	( 'Cymbal', '90 Day', 100005, 'Percussion' ),
	( 'Double Bass', '24 Months', 100004, 'String' ),
	( 'Flute', '24 Months', 100003, 'Woodwind' ),
	( 'French Horn', '24 Months', 100002, 'Brass' ),
	( 'Oboe', '24 Months', 100003, 'Woodwind' ),
	( 'Tuba', '24 Months', 100002, 'Brass' ),
	( 'Violin', '12 Months', 100004, 'String' ),
	( 'Piano', '24 Months', 100004, 'String' ),
	( 'Marimba', '12 Months', 100005, 'Percussion' ),
	( 'Bassoon', '12 Months', 100003, 'Woodwind' )
go

print ''
print '*** Creating sp_select_all_InstrumentType'
go

create procedure sp_select_all_InstrumentType
as
begin
	select
		InstrumentTypeID
	from
		dbo.InstrumentType
end
go

print ''
print '*** crating sp_select_instrument_type_by_InstrumentTypeID'
go

create procedure sp_select_instrument_type_by_InstrumentTypeID
	(
	@InstrumentTypeID nvarchar(50)
)
as
begin
	select
		InstrumentType.RentalTermID,
		RentalTerm.RentalCost,
		InstrumentFamilyID
	from
		InstrumentType
		join RentalTerm on InstrumentType.RentalTermID = RentalTerm.RentalTermID
	where InstrumentTypeID = @InstrumentTypeID
end
go

print ''
print '*** Creating InstrumentBrand Table'
go

create table InstrumentBrand
(
	InstrumentBrandID nvarchar(50) not null,
	constraint pk_InstrumnetBrandID
	primary key (InstrumentBrandID asc)
)

print ''
print '*** Creating InstrumentBrand Data'
go

insert
into
	InstrumentBrand
	( InstrumentBrandID )
values
	( 'Selmer' ),
	( 'Yamaha' ),
	( 'King' ),
	( 'P.Mauriat' ),
	( 'Buescher' ),
	( 'Conn' ),
	( 'Jupiter' ),
	( 'Antigua' ),
	( 'Conn-Selmer' ),
	( 'Eastman' ),
	( 'Chateau' ),
	( 'Yanagisawa' ),
	( 'Buffet Crampon' ),
	( 'Bundy' ),
	( 'Martin' )
go

print ''
print '*** Creating sp_select_all_instrument_brands'
go

create procedure sp_select_all_instrument_brands
as
begin
	select
		InstrumentBrandID
	from
		InstrumentBrand
end
go

print ''
print '*** Creating Instrument Table'
go

create table Instrument
(
	InstrumentID       nvarchar(50) not null,
	InstrumentTypeID   nvarchar(50) not null,
	InstrumentStatusID nvarchar(50) not null default 'Available',
	InstrumentBrandID  nvarchar(50) not null,
	Price              money        not null,
	constraint pk_InstrumentID
		primary key (InstrumentID asc),
	constraint fk_Instrument_InstrumentTypeID
		foreign key (InstrumentTypeID)
			references InstrumentType (InstrumentTypeID),
	constraint fk_Instrument_InstrumentStatusID
		foreign key (InstrumentStatusID)
			references InstrumentStatus (InstrumentStatusID),
	constraint fk_Instrument_InstrumentBrandID
		foreign key (InstrumentBrandID)
			references InstrumentBrand (InstrumentBrandID)
)
go

print ''
print '*** Creating sample data for Instrument Table'
go

insert
into
	dbo.Instrument
	( InstrumentID, InstrumentTypeID, InstrumentBrandID, Price )
values
	( '125584', 'Tenor Saxophone', 'Selmer', 1500.00 ),
	( '443323', 'Clarinet', 'Bundy', 600.00 ),
	( '432312', 'Trumpet', 'Conn-Selmer', 2000.00 )
go

print ''
print '*** Creating sp_select_all_Instruments'
go

create procedure sp_select_all_Instruments
as
begin
	select
		InstrumentID,
		instrument.InstrumentTypeID,
		InstrumentFamily.InstrumentFamilyID,
		InstrumentStatusID,
		InstrumentBrandID,
		Price,
		RentalTerm.RentalTermID,
		RentalTerm.RentalCost,
		PrepList.Description
	from
		dbo.Instrument
		join InstrumentType
		on Instrument.InstrumentTypeID = InstrumentType.InstrumentTypeID
		join RentalTerm
		on InstrumentType.RentalTermID = RentalTerm.RentalTermID
		join PrepList
		on InstrumentType.PrepListID = PrepList.PrepListID
		join InstrumentFamily
		on InstrumentType.InstrumentFamilyID = InstrumentFamily.InstrumentFamilyID
	where
		 InstrumentStatusID = 'Available'
		or InstrumentStatusID = 'For Sale'
		or InstrumentStatusID = 'For Rent'
		or InstrumentStatusID = 'For Rent To Own'
end
go

print ''
print '*** Creating sp_update_instrument_status'
go

create procedure sp_update_instrumentStatus
	(
	@InstrumentID nvarchar(50),
	@OldStatus    nvarchar(50),
	@OldPrice     money,
	@NewStatus    nvarchar(50),
	@NewPrice     money
)
as
begin
	update dbo.Instrument
	set
		InstrumentStatusID = @NewStatus,
		Price              = @NewPrice
	where
		  InstrumentID = @InstrumentID
		and InstrumentStatusID = @OldStatus
		and Price = @OldPrice
end
go

print ''
print '*** Creating sp_insert_instrument'
go

create procedure sp_insert_instrument
	(
	@InstrumentID      nvarchar(50),
	@InstrumentTypeID  nvarchar(50),
	@InstrumentBrandID nvarchar(50),
	@Price             money
)
as
begin
	insert
	into
		dbo.Instrument
		( InstrumentID, InstrumentTypeID, InstrumentBrandID, Price )
	values
		( @InstrumentID, @InstrumentTypeID, @InstrumentBrandID, @Price )
end
go

print ''
print '*** Creating sp_select_instruments_by_status'
go

create procedure sp_select_instruments_by_status
	(
	@Status nvarchar(50)
)
as
begin
	select
		InstrumentID,
		instrument.InstrumentTypeID,
		InstrumentFamily.InstrumentFamilyID,
		InstrumentStatusID,
		InstrumentBrandID,
		Price,
		RentalTerm.RentalTermID,
		RentalTerm.RentalCost,
		PrepList.Description
	from
		dbo.Instrument
		join InstrumentType
		on Instrument.InstrumentTypeID = InstrumentType.InstrumentTypeID
		join RentalTerm
		on InstrumentType.RentalTermID = RentalTerm.RentalTermID
		join PrepList
		on InstrumentType.PrepListID = PrepList.PrepListID
		join InstrumentFamily
		on InstrumentType.InstrumentFamilyID = InstrumentFamily.InstrumentFamilyID
	where InstrumentStatusID = @Status
end
go

print ''
print '*** Creating Cart table'
go

create table Cart
(
	InstrumentID       nvarchar(50) not null,
	InstrumentTypeID   nvarchar(50) not null,
	InstrumentStatusID nvarchar(50) not null,
	InstrumentBrandID  nvarchar(50) not null,
	Price              money        not null,
	constraint pk_Cart_InstrumentID
		primary key (InstrumentID asc),
	constraint fk_Cart_InstrumentTypeID
		foreign key (InstrumentTypeID)
			references InstrumentType (InstrumentTypeID),
	constraint fk_Cart_InstrumentStatusID
		foreign key (InstrumentStatusID)
			references InstrumentStatus (InstrumentStatusID),
	constraint fk_Cart_InstrumentBrandID
		foreign key (InstrumentBrandID)
			references InstrumentBrand (InstrumentBrandID)
)
go

print ''
print '*** Creating sp_select_all_in_cart'
go

create procedure sp_select_all_in_cart
as
begin
	select
		InstrumentID,
		Cart.InstrumentTypeID,
		InstrumentFamily.InstrumentFamilyID,
		InstrumentStatusID,
		InstrumentBrandID,
		Price,
		RentalTerm.RentalTermID,
		RentalTerm.RentalCost,
		PrepList.Description
	from
		dbo.Cart
		join InstrumentType
		on Cart.InstrumentTypeID = InstrumentType.InstrumentTypeID
		join RentalTerm
		on InstrumentType.RentalTermID = RentalTerm.RentalTermID
		join PrepList
		on InstrumentType.PrepListID = PrepList.PrepListID
		join InstrumentFamily
		on InstrumentType.InstrumentFamilyID = InstrumentFamily.InstrumentFamilyID
end
go


print ''
print '*** creating sp_insert_cart_item'
go

create procedure sp_insert_cart_item
	(
	@InstrumentID       nvarchar(50),
	@InstrumentTypeID   nvarchar(50),
	@InstrumentStatusID nvarchar(50),
	@InstrumentBrandID  nvarchar(50),
	@Price              money
)
as
begin
	declare @newStatus nvarchar(50)
	declare @newPrice money
	set @newPrice = @Price

	insert
	into
		Cart
		( InstrumentID, InstrumentTypeID, InstrumentStatusID, InstrumentBrandID, Price )
	values
		( @InstrumentID, @InstrumentTypeID, @InstrumentStatusID, @InstrumentBrandID, @Price )

	if @InstrumentStatusID = 'For Rent'
			set @newStatus = 'Rented'
	if @InstrumentStatusID = 'For Rent To Own'
			set @newStatus = 'Rent To Own'
	if @InstrumentStatusID = 'For Sale' or @InstrumentStatusID = 'Available'
			set @newStatus = 'Sold'

	execute sp_update_instrumentStatus 
			@InstrumentID,
	        @InstrumentStatusID,
	        @Price,
	        @newStatus,
	        @newPrice

end
go



