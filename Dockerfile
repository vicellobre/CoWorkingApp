# Etapa de Restaurar dependencias (base)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /src

# Copiar los archivos necesarios para restaurar dependencias
COPY *.sln ./
COPY CoWorkingApp.API/*.csproj ./CoWorkingApp.API/
COPY CoWorkingApp.Application/*.csproj ./CoWorkingApp.Application/
COPY CoWorkingApp.Core/*.csproj ./CoWorkingApp.Core/
COPY CoWorkingApp.Infrastructure/*.csproj ./CoWorkingApp.Infrastructure/
COPY CoWorkingApp.Persistence/*.csproj ./CoWorkingApp.Persistence/
COPY CoWorkingApp.Presentation/*.csproj ./CoWorkingApp.Presentation/

# Restaurar dependencias
RUN dotnet restore ./CoWorkingApp.API/CoWorkingApp.API.csproj

# Etapa de construcción (build)
FROM base AS build
WORKDIR /src

# Copiar el resto del código fuente
COPY . ./

# Instalar herramientas EF Core en la etapa final
RUN dotnet tool install --global dotnet-ef

# Agregar el directorio de herramientas globales a la PATH
ENV PATH="$PATH:/root/.dotnet/tools"

# Construir y publicar la aplicación
RUN dotnet publish ./CoWorkingApp.API/CoWorkingApp.API.csproj -c Release -o /app/publish --no-restore

# Etapa final (final)
FROM build AS final
WORKDIR /app

# Crear los directorios necesarios antes de copiar
COPY --from=build /app/publish ./

# Copiar el script de arranque
COPY entrypoint.sh /app/entrypoint.sh

# Hacer el script ejecutable
RUN chmod +x /app/entrypoint.sh

# Usar el script como entrypoint
ENTRYPOINT ["/app/entrypoint.sh"]
