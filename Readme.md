
docker run --name SQLLNX01 -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Tacna.2019' -p 1433:1433 -d mcr.microsoft.com/mssql/server

dotnet ef database update

-- SELECT * FROM Cliente
-- INSERT Cliente VALUES ('JUAN','PEREZ','PEREZ','10101010')
-- INSERT Cliente VALUES ('MARIA','DIAZ','PEREZ','20202020')

-- SELECT * FROM Cuenta
-- INSERT Cuenta VALUES (1,3000)
-- INSERT Cuenta VALUES (1,4000)
-- INSERT Cuenta VALUES (5,5000)