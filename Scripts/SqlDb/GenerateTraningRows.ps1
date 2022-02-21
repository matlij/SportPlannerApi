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
    
    $body = [ordered]@{
        date      = $date
        eventType = 1
        address   = @{
            id = "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        }
        users     = [array][ordered]@{
            userId    = "0cafdfe9-614f-495b-b672-61508baa3dbc"
            userName  = "Matte"
            userReply = 1
            isOwner   = $true
        }
    }

    $response = Invoke-RestMethod -Uri 'https://sportplannerapi.azurewebsites.net/api/event' -Method 'POST' -Body (ConvertTo-Json $body -Depth 32) -ContentType 'application/json' -Headers @{ 'x-functions-key' = 'OlEfvbvK6YHrBpQa8tIycbz49AR/mgasjZryR3f96TdyXb/ZsEWwaA==' }

    Write-Output $response
}

for ($i = 0; $i -lt 25; $i++) {
    $date = Get-date
    $date = $date.AddDays(7 * $i + 2)

    Write-Output "Storing event with date: $date"

    PostToApi $date
}
