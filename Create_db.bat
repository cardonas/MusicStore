echo off

rem batch file to run a script to create a db
rem 9/5/2019

rem sqlcmd -S localhost -E -i BicycleDB_AM.sql
rem sqlcmd -S localhost\mssqlserver -E -i BicycleDB_AM.sql
sqlcmd -S localhost\sqlexpress -E -i C#FinalProject.sql
rem sqlcmd -S 10.132.11.23 -U SA -P Unipres15 -i BicycleDB_AM.sql


ECHO .
ECHO if no error messages appear DB was created
PAUSE
