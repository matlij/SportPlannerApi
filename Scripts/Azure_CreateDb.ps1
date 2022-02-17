# Set variables
$rg = 'sp-rg-weu'
$server = 'sportplannersqlserver'
$location = 'westeurope'
$database = 'sportplannersqldb'
$adminSqlLogin = 'matlij'
$password = Read-Host "Enter password for db user $user" -MaskInput
Write-Output "Connecting user"
Connect-AzAccount

Write-Output "Creating $server in $location..."
$credentials= $(New-Object -TypeName System.Management.Automation.PSCredential -ArgumentList $adminSqlLogin, $(ConvertTo-SecureString -String $password -AsPlainText -Force))
New-AzSqlServer -location $location -ResourceGroupName $rg -ServerName $server -SqlAdministratorCredentials $credentials

$myIpAddress= (Invoke-WebRequest -uri "http://ifconfig.me/ip").Content
Write-Output "Adding IP $myIpAddress to $server..."
New-AzSqlServerFirewallRule -ResourceGroupName $rg -ServerName $server -FirewallRuleName "MattiasL" -StartIpAddress $myIpAddress -EndIpAddress $myIpAddress

Write-Output "Creating $database on $server..."
$database = New-AzSqlDatabase -ResourceGroupName $rg `
    -ServerName $server `
    -DatabaseName $database `
    -RequestedServiceObjectiveName "Basic"

Write-Output "Migrating the database '$database'"
$connectionString = "Server=tcp:$server.database.windows.net,1433;Initial Catalog=sportplannersqldb;Persist Security Info=False;User ID=$adminSqlLogin;Password=$password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
dotnet ef database update --connection $connectionString -p ..\SportPlanner.DataLayer\SportPlanner.DataLayer.csproj --startup-project ..\SportPlannerApi\SportPlannerApi.csproj