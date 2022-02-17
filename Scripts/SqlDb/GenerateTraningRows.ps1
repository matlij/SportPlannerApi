
for ($i = 0; $i -lt 25; $i++) {
    $date = Get-date
    $date = $date.AddDays(7 * $i)
    if ($date.Day -lt 10) {
        $day = "0$($date.Day)"
    }
    else {
        $day = $date.Day
    }

    Write-Host "Set @EventDate = '$($date.year)-0$($date.Month)-$day 20:00'" 
    Write-Host "insert into [Events]" 
    Write-Host "select NEWID(), @EventDate, 1, @GameAddressId" 
    Write-Host "where not exists (select 1 from [Events] where Date = @EventDate and EventType = 1 and AddressId = @GameAddressId)" 
    Write-Host
}