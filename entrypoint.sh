#!/bin/bash
set -e

# Ejecutar las migraciones de EF Core
echo "Aplicando migraciones de la base de datos..."
if dotnet ef database update --verbose --startup-project /src/CoWorkingApp.API --project /src/CoWorkingApp.Persistence ; then
    echo "Migraciones aplicadas correctamente."
else
    echo "Error al aplicar migraciones."
    exit 1
fi

# Iniciar la aplicación
echo "Iniciando la aplicación..."
exec dotnet CoWorkingApp.API.dll
