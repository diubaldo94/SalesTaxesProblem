if not exist "workdir\Input" mkdir workdir\Input
if not exist "workdir\Output" mkdir workdir\Output
if not exist "workdir\Error" mkdir workdir\Error
if not exist "workdir\Backup" mkdir workdir\Backup
cd SalesTaxesCalculation/SalesTaxesCalculation.Application
dotnet run --project SalesTaxesCalculation.Application.csproj
pause