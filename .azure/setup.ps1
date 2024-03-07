param (
    $resourceBaseName="carsmanagerorleans$( Get-Random -Maximum 1000)",
    $location='westeurope'
)

Write-Host 'Compiling app code' -ForegroundColor Cyan

dotnet build

Write-Host 'Building Silo' -ForegroundColor Cyan
dotnet publish CarsManager.Orleans.Silo\CarsManager.Orleans.Silo.csproj

Write-Host 'Building Web' -ForegroundColor Cyan
dotnet publish CarsManager.Orleans.Web\CarsManager.Orleans.Web.csproj

Write-Host 'Building Orleans Dashboard' -ForegroundColor Cyan
dotnet publish CarsManager.Orleans.Dashboard\CarsManager.Orleans.Dashboard.csproj

Write-Host 'Building Devices GPS messages simulator' -ForegroundColor Cyan
dotnet publish CarsManager.Orleans.Devices.Signal.Gen\CarsManager.Orleans.Devices.Signal.Gen.csproj

Write-Host 'Creating resource group' -ForegroundColor Cyan
az group create -l $location -n $resourceBaseName

Write-Host 'Creating Orleans Cluster and deploying code to it ' -ForegroundColor Cyan
az deployment group create --resource-group $resourceBaseName --template-file 'deploy/main.bicep'

Write-Host 'Deploying code ' -ForegroundColor Cyan
az webapp deploy -n "$($resourceBaseName)-silo" -g $resourceBaseName --src-path silo.zip
az webapp deploy -n "$($resourceBaseName)-web" -g $resourceBaseName --src-path web.zip
az webapp deploy -n "$($resourceBaseName)-dashboard" -g $resourceBaseName --src-path dashboard.zip
az webapp deploy -n "$($resourceBaseName)-devices" -g $resourceBaseName --src-path devices.zip

Write-Host 'Orleans Cluster deployed.' -ForegroundColor Cyan
az webapp restart -n "$($resourceBaseName)-silo" -g $resourceBaseName
az webapp restart -n "$($resourceBaseName)-web" -g $resourceBaseName
az webapp restart -n "$($resourceBaseName)-dashboard" -g $resourceBaseName
az webapp restart -n "$($resourceBaseName)-devices" -g $resourceBaseName

Write-Host 'Orleans Cluster deployed.' -ForegroundColor Cyan
az webapp browse -n "$($resourceBaseName)-silo" -g $resourceBaseName
az webapp restart -n "$($resourceBaseName)-web" -g $resourceBaseName
az webapp browse -n "$($resourceBaseName)-dashboard" -g $resourceBaseName
az webapp browse -n "$($resourceBaseName)-devices" -g $resourceBaseName