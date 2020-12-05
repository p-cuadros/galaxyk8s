
docker run --name SQLLNX01 -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Tacna.2019' -p 1433:1433 -d mcr.microsoft.com/mssql/server

dotnet ef database update

-- SELECT * FROM Cliente
-- INSERT Cliente VALUES ('JUAN','PEREZ','PEREZ','10101010')
-- INSERT Cliente VALUES ('MARIA','DIAZ','PEREZ','20202020')

-- SELECT * FROM Cuenta
-- INSERT Cuenta VALUES (1,3000)
-- INSERT Cuenta VALUES (1,4000)
-- INSERT Cuenta VALUES (5,5000)



C:\Users\pcuadros_dev\.nuget\packages\grpc.tools\2.34.0\tools\windows_x64\protoc.exe --proto_path=Protos --grpc_out=Protos --csharp_out=Protos --csharp_opt=file_extension=.g.cs Banca.proto --plugin=protoc-gen-grpc=C:\Users\pcuadros_dev\.nuget\packages\grpc.tools\2.34.0\tools\windows_x64\grpc_csharp_plugin.exe