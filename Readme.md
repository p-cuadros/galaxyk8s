
docker run --name SQLLNX01 -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Tacna.2019' -p 1433:1433 -d mcr.microsoft.com/mssql/server

dotnet ef database update

-- SELECT * FROM Cliente
-- INSERT Cliente VALUES ('JUAN','PEREZ','PEREZ','10101010')
-- INSERT Cliente VALUES ('MARIA','DIAZ','PEREZ','20202020')

-- SELECT * FROM Cuenta
-- INSERT Cuenta VALUES (1,3000)
-- INSERT Cuenta VALUES (1,4000)
-- INSERT Cuenta VALUES (5,5000)


> dotnet add package Grpc.core
> dotnet add package grpc.aspnetcore
> dotnet add package grpc.net.client

C:\Users\pcuadros_dev\.nuget\packages\grpc.tools\2.34.0\tools\windows_x64\protoc.exe --proto_path=Protos --grpc_out=Protos --csharp_out=Protos --csharp_opt=file_extension=.g.cs Banca.proto --plugin=protoc-gen-grpc=C:\Users\pcuadros_dev\.nuget\packages\grpc.tools\2.34.0\tools\windows_x64\grpc_csharp_plugin.exe


# Generacion de las imagenes

#### Dentro de cada carpeta de la solucion de VIsual Studio generar las imagenes

> docker build -t apiuxbanca ./../ -f Dockerfile
> docker build -t apiseguridad ./../ -f Dockerfile
> docker build -t apiopbanca ./../ -f Dockerfile
> docker build -t apiuxcustomer ./../ -f Dockerfile
> docker build -t apiopcliente ./../ -f Dockerfile
#### Le ponemos un tag con el usuario del Hub.docker

> docker tag apiseguridad patrickcuadros/apiseguridad
> docker tag apiopbanca patrickcuadros/apiopbanca
> docker tag apiuxbanca patrickcuadros/apiuxbanca
> docker tag apiopcliente patrickcuadros/apiopcliente
> docker tag apiuxcustomer patrickcuadros/apiuxcustomer

#### Subimos la imagen hacia el repositorio de imagenes (hubDocker)

> docker push patrickcuadros/apiseguridad
> docker push patrickcuadros/apiopbanca
> docker push patrickcuadros/apiuxbanca
> docker push patrickcuadros/apiopcliente
> docker push patrickcuadros/apiuxcustomer

# Configurando Istio

#### Descargar instaladores 
https://github.com/istio/istio/releases/tag/1.8.0
Descargamos istio-1.8.0-win.zip para el contenido del Istio
Descargamos istioctl-1.8.0-win.zip para uso del IstioCTL
Descomprimir los archivos en carpeta independientes

#### Dentro de la carpeta IstioCTL Ejecutamos el comando 
> istioctl install
> istioctl install --set profile=demo

#### Revisar documentacion de componentes complementarios

Revision de documentacion https://kiali.io/documentation/latest/quick-start/

> kubectl apply -f ${ISTIO_HOME}/samples/addons/kiali.yaml

#### Agregamos Prometheus, Grafana, Jaeger, Kiali
> kubectl apply -f prometheus.yaml
> kubectl apply -f grafana.yaml
> kubectl apply -f jaeger.yaml
> kubectl apply -f kiali.yaml

#### Indicamos que el namespace *default* aplique el pod de control
> kubectl label namespace default istio-injection=enabled


# Desplegar la aplicacion en Kubernetes

#### Generamos los archivos k8s.yml para cada api en su carpeta

> kubectl apply -f k8s.yml

#### En caso de querer eliminar puedes eliminarlo por el objecto o por todo el archivo yaml
> kubectl delete -f k8s.yml

#### Si queremos mapear un puerto del k8s con el puerto local
> kubectl port-forward services/apiuxbanca 8888:8888


# Uso de Prometheus

#### Link de referencia 
https://www.weave.works/docs/cloud/latest/tasks/monitor/configuration-k8s/

#### Debemos agregar secciones en el yml
>       annotations:
        prometheus.io/scrape: "true"
        prometheus.io/port: "8090"

