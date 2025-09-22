@echo off
setlocal

REM === CONFIG ===
REM
REM set CONNECTION_STRING=User Id=HR;Password=HR;Data Source=ORCL
set CONNECTION_STRING=Data Source = (DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=MY_DB)));User ID= MY_USER; Password = PSWD;Pooling=false;
set NAMESPACE=DBDataLibrary.Tables
set OUTPUT_DIR=C:\DBClassGenerator\DBDataLibrary\Tables
set TABLE_NAME=%%MY_FILTER%%

REM === EXECUTION ===
dotnet run --project DBClassGenerator.csproj -- -cs "%CONNECTION_STRING%" -ns %NAMESPACE% -out "%OUTPUT_DIR%" -tn "%TABLE_NAME%"

pause
