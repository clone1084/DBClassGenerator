@echo off
setlocal

REM === CONFIG ===
REM
REM set CONNECTION_STRING=User Id=HR;Password=HR;Data Source=ORCL
set CONNECTION_STRING=Data Source = (DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.16.11.218)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ROEN)));User ID= WMS_ROEN; Password = tabe;Pooling=false;
set NAMESPACE=DBDataLibrary.Tables
set OUTPUT_DIR=C:\Users\Beta80\source\repos\DBClassGenerator\DBDataLibrary\Tables
set TABLE_NAME=MFC_CONV%
set GENERATE_CRUD=N

REM === EXECUTION ===
dotnet run --project DBClassGenerator.csproj -- -cs "%CONNECTION_STRING%" -ns %NAMESPACE% -out "%OUTPUT_DIR%" -tn "%TABLE_NAME%" -gc %GENERATE_CRUD%

pause

REM -cs "Data Source = (DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.16.11.218)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ROEN)));User ID= WMS_ROEN; Password = tabe;Pooling=false;"
REM -ns DBDataLibrary.Tables
REM -out "C:\Users\Beta80\source\repos\DBClassGenerator\DBDataLibrary\Tables"
REM -tn "MFC_CONV%"
REM -gc Y