--Delete from [EventsUsers]
--Delete from [Events]
--Delete from [Users]


--SET VARIABLES
DECLARE @GameAddressId AS UNIQUEIDENTIFIER = '55e1482c-3fc8-41ba-94a3-ee8a12b70abf'
DECLARE @UserMatteId AS UNIQUEIDENTIFIER = '0cafdfe9-614f-495b-b672-61508baa3dbc'
DECLARE @EventDate AS DateTime

--USERS
insert into dbo.Users(Id, Name) 
select @UserMatteId, 'Matte'
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

--EVENTS GAMES
Set @EventDate = '2022-01-31 17:00'
insert into [Events]
select NEWID(), @EventDate, 2, @GameAddressId
where not exists (select 1 from [Events] where Date = @EventDate and EventType = 1 and AddressId = @GameAddressId)

Set @EventDate = '2022-02-14 19:00'
insert into [Events]
select NEWID(), @EventDate, 2, @GameAddressId
where not exists (select 1 from [Events] where Date = @EventDate and EventType = 1 and AddressId = @GameAddressId)

Set @EventDate = '2022-02-28 17:00'
insert into [Events]
select NEWID(), @EventDate, 2, @GameAddressId
where not exists (select 1 from [Events] where Date = @EventDate and EventType = 1 and AddressId = @GameAddressId)

Set @EventDate = '2022-03-07 19:00'
insert into [Events]
select NEWID(), @EventDate, 2, @GameAddressId
where not exists (select 1 from [Events] where Date = @EventDate and EventType = 1 and AddressId = @GameAddressId)

Set @EventDate = '2022-03-21 19:00'
insert into [Events]
select NEWID(), @EventDate, 2, @GameAddressId
where not exists (select 1 from [Events] where Date = @EventDate and EventType = 1 and AddressId = @GameAddressId)

--EVENTS TRANINGS
Set @EventDate = '2022-01-04 20:00'
insert into [Events]
select NEWID(), @EventDate, 1, @GameAddressId
where not exists (select 1 from [Events] where Date = @EventDate and EventType = 1 and AddressId = @GameAddressId)

Set @EventDate = '2022-01-11 20:00'
insert into [Events]
select NEWID(), @EventDate, 1, @GameAddressId
where not exists (select 1 from [Events] where Date = @EventDate and EventType = 1 and AddressId = @GameAddressId)

Set @EventDate = '2022-01-18 20:00'
insert into [Events]
select NEWID(), @EventDate, 1, @GameAddressId
where not exists (select 1 from [Events] where Date = @EventDate and EventType = 1 and AddressId = @GameAddressId)

Set @EventDate = '2022-01-25 20:00'
insert into [Events]
select NEWID(), @EventDate, 1, @GameAddressId
where not exists (select 1 from [Events] where Date = @EventDate and EventType = 1 and AddressId = @GameAddressId)

Set @EventDate = '2022-02-01 20:00'
insert into [Events]
select NEWID(), @EventDate, 1, @GameAddressId
where not exists (select 1 from [Events] where Date = @EventDate and EventType = 1 and AddressId = @GameAddressId)

Set @EventDate = '2022-02-08 20:00'
insert into [Events]
select NEWID(), @EventDate, 1, @GameAddressId
where not exists (select 1 from [Events] where Date = @EventDate and EventType = 1 and AddressId = @GameAddressId)

Set @EventDate = '2022-02-15 20:00'
insert into [Events]
select NEWID(), @EventDate, 1, @GameAddressId
where not exists (select 1 from [Events] where Date = @EventDate and EventType = 1 and AddressId = @GameAddressId)

Set @EventDate = '2022-02-22 20:00'
insert into [Events]
select NEWID(), @EventDate, 1, @GameAddressId
where not exists (select 1 from [Events] where Date = @EventDate and EventType = 1 and AddressId = @GameAddressId)

Set @EventDate = '2022-03-01 20:00'
insert into [Events]
select NEWID(), @EventDate, 1, @GameAddressId
where not exists (select 1 from [Events] where Date = @EventDate and EventType = 1 and AddressId = @GameAddressId)

Set @EventDate = '2022-03-08 20:00'
insert into [Events]
select NEWID(), @EventDate, 1, @GameAddressId
where not exists (select 1 from [Events] where Date = @EventDate and EventType = 1 and AddressId = @GameAddressId)

Set @EventDate = '2022-03-15 20:00'
insert into [Events]
select NEWID(), @EventDate, 1, @GameAddressId
where not exists (select 1 from [Events] where Date = @EventDate and EventType = 1 and AddressId = @GameAddressId)

Set @EventDate = '2022-03-22 20:00'
insert into [Events]
select NEWID(), @EventDate, 1, @GameAddressId
where not exists (select 1 from [Events] where Date = @EventDate and EventType = 1 and AddressId = @GameAddressId)

Set @EventDate = '2022-03-29 20:00'
insert into [Events]
select NEWID(), @EventDate, 1, @GameAddressId
where not exists (select 1 from [Events] where Date = @EventDate and EventType = 1 and AddressId = @GameAddressId)

Set @EventDate = '2022-04-05 20:00'
insert into [Events]
select NEWID(), @EventDate, 1, @GameAddressId
where not exists (select 1 from [Events] where Date = @EventDate and EventType = 1 and AddressId = @GameAddressId)

Set @EventDate = '2022-04-12 20:00'
insert into [Events]
select NEWID(), @EventDate, 1, @GameAddressId
where not exists (select 1 from [Events] where Date = @EventDate and EventType = 1 and AddressId = @GameAddressId)

Set @EventDate = '2022-04-19 20:00'
insert into [Events]
select NEWID(), @EventDate, 1, @GameAddressId
where not exists (select 1 from [Events] where Date = @EventDate and EventType = 1 and AddressId = @GameAddressId)

Set @EventDate = '2022-04-26 20:00'
insert into [Events]
select NEWID(), @EventDate, 1, @GameAddressId
where not exists (select 1 from [Events] where Date = @EventDate and EventType = 1 and AddressId = @GameAddressId)

Set @EventDate = '2022-05-03 20:00'
insert into [Events]
select NEWID(), @EventDate, 1, @GameAddressId
where not exists (select 1 from [Events] where Date = @EventDate and EventType = 1 and AddressId = @GameAddressId)

Set @EventDate = '2022-05-10 20:00'
insert into [Events]
select NEWID(), @EventDate, 1, @GameAddressId
where not exists (select 1 from [Events] where Date = @EventDate and EventType = 1 and AddressId = @GameAddressId)

Set @EventDate = '2022-05-17 20:00'
insert into [Events]
select NEWID(), @EventDate, 1, @GameAddressId
where not exists (select 1 from [Events] where Date = @EventDate and EventType = 1 and AddressId = @GameAddressId)

Set @EventDate = '2022-05-24 20:00'
insert into [Events]
select NEWID(), @EventDate, 1, @GameAddressId
where not exists (select 1 from [Events] where Date = @EventDate and EventType = 1 and AddressId = @GameAddressId)

Set @EventDate = '2022-05-31 20:00'
insert into [Events]
select NEWID(), @EventDate, 1, @GameAddressId
where not exists (select 1 from [Events] where Date = @EventDate and EventType = 1 and AddressId = @GameAddressId)

Set @EventDate = '2022-06-07 20:00'
insert into [Events]
select NEWID(), @EventDate, 1, @GameAddressId
where not exists (select 1 from [Events] where Date = @EventDate and EventType = 1 and AddressId = @GameAddressId)

Set @EventDate = '2022-06-14 20:00'
insert into [Events]
select NEWID(), @EventDate, 1, @GameAddressId
where not exists (select 1 from [Events] where Date = @EventDate and EventType = 1 and AddressId = @GameAddressId)

Set @EventDate = '2022-06-21 20:00'
insert into [Events]
select NEWID(), @EventDate, 1, @GameAddressId
where not exists (select 1 from [Events] where Date = @EventDate and EventType = 1 and AddressId = @GameAddressId)

--ADD MATTE AS OWNER TO EVENTS
insert into [EventUsers]
select Id, @UserMatteId, 1, 0
from Events e
where not exists (select 1 from [EventUsers] where EventId = e.Id and UserId = @UserMatteId)

select * from Events e
left join EventUsers eu on e.Id = eu.EventId