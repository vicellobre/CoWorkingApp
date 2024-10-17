# Etapa 1: Restaurar dependencias (restore)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS restore
WORKDIR /src

# Copiar los archivos necesarios para restaurar dependencias
COPY *.sln .
COPY CoWorkingApp.API/*.csproj ./CoWorkingApp.API/
COPY CoWorkingApp.Core/*.csproj ./CoWorkingApp.Core/
#COPY CoWorkingApp.Tests/*.csproj ./CoWorkingApp.Tests/

# Restaurar dependencias
RUN dotnet restore ./CoWorkingApp.API/CoWorkingApp.API.csproj

# Etapa 2: Compilar y publicar la aplicaci�n (build y publish)
FROM restore AS build
WORKDIR /src

# Copiar el resto del c�digo fuente una vez que las dependencias han sido restauradas
COPY . .

# Construir y publicar la aplicaci�n en una �nica capa
RUN dotnet publish ./CoWorkingApp.API/CoWorkingApp.API.csproj -c Release -o /app/publish --no-restore

# Etapa 3: Ejecutar la aplicaci�n (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Establecer el entorno como Development para habilitar Swagger y la configuraci�n de autenticaci�n
ENV ASPNETCORE_ENVIRONMENT=Development

# Copiar los archivos publicados desde la etapa de build
COPY --from=build /app/publish .

# Exponer los puertos que usar� la aplicaci�n
EXPOSE 8080
EXPOSE 8081

# Comando para ejecutar la aplicaci�n
ENTRYPOINT ["dotnet", "CoWorkingApp.API.dll"]
