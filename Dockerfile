# Etapa de Restaurar dependencias (base)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /src

# Copiar los archivos necesarios para restaurar dependencias
COPY *.sln ./
COPY CoWorkingApp.API/*.csproj ./CoWorkingApp.API/
COPY CoWorkingApp.Core/*.csproj ./CoWorkingApp.Core/

# Restaurar dependencias
RUN dotnet restore ./CoWorkingApp.API/CoWorkingApp.API.csproj

# Etapa de construcci�n (build)
FROM base AS build
WORKDIR /src

# Copiar el resto del c�digo fuente
COPY . ./

# Instalar herramientas EF Core
RUN dotnet tool install --global dotnet-ef

# Agregar el directorio de herramientas globales a la PATH
ENV PATH="$PATH:/root/.dotnet/tools"

# Construir y publicar la aplicaci�n
RUN dotnet publish ./CoWorkingApp.API/CoWorkingApp.API.csproj -c Release -o /app/publish --no-restore

# Etapa final (final)
FROM build AS final
WORKDIR /app
COPY --from=build /app/publish ./

# Comando de entrada: Aplicar las migraciones antes de iniciar la aplicaci�n
CMD dotnet ef database update --project /src/CoWorkingApp.API/CoWorkingApp.API.csproj && dotnet CoWorkingApp.API.dll
