function SqlStatement {
    param (
        [datetime]$date
    )

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

function PostToApi {
    param (
        [datetime]$date
    )
    
    $body = @{
        id        = (New-Guid).Guid
        date      = Get-Date -Month $date.Month -Day $date.Day -Year $date.Year -Hour 20 -Minute 0 -Second 0 -Millisecond 0
        eventType = 1
        address   = @{
            id = "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        }
    }

    $url = 'https://bandydosapi.azurewebsites.net/api/event?code=booHUFPzV2zTqvMim2JNioFSV8Q751OLTix_lRDOUKHUAzFusPWHCA=='
    # $url = 'http://localhost:7071/api/event'
    $bodyJson = (ConvertTo-Json $body -Depth 32)

    Write-Output "Storing event: $bodyJson"    
    $response = Invoke-RestMethod -Uri $url -Method 'POST' -Body $bodyJson -ContentType 'application/json'
}

function GetNextTuesday {
    $nextTuesday = Get-date
    while ($nextTuesday.DayOfWeek -ne 'Tuesday') {
        $nextTuesday = $nextTuesday.AddDays(1)
    }

    return (Get-Date -Year $nextTuesday.Year -Month $nextTuesday.Month -Day $nextTuesday.Day -Hour 20 -Minute 0 -Second 0)
}

$date = GetNextTuesday

for ($i = 0; $i -lt 8; $i++) {    
    PostToApi $date
    $date = $date.AddDays(7)
}
