--SET VARIABLES
DECLARE @GameAddressId AS UNIQUEIDENTIFIER
SET @GameAddressId = '55e1482c-3fc8-41ba-94a3-ee8a12b70abf'

--USERS
insert into dbo.Users(Id, Name) 
select '0cafdfe9-614f-495b-b672-61508baa3dbc', 'Matte'
where not exists (select 1 from Users where Name = 'Matte')

--ADDRESSES
insert into Addresses
select	'3fa85f64-5717-4562-b3fc-2c963f66afa6',
		'Brinellvägen 38, 114 28 Stockholm',
		59.35218566796535,
		18.069107869137287
where not exists (select 1 from Addresses where FullAddress = 'Brinellvägen 38, 114 28 Stockholm')
insert into Addresses
select	@GameAddressId,
		'Lilla Alby skola Humblegatan 19-21, 172 39 Sundbyberg',
		59.35881310264956,
		17.96805748336981
where not exists (select 1 from Addresses where FullAddress = 'Lilla Alby skola Humblegatan 19-21, 172 39 Sundbyberg')

--EVENTS
insert into [Events] values (NEWID(), '2022-01-31 17:00', 1, @GameAddressId)