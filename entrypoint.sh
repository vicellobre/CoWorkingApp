#!/bin/bash

# Ejecutar las migraciones de EF Core
dotnet ef database update --project /src/CoWorkingApp.API/CoWorkingApp.API.csproj

# Iniciar la aplicación
dotnet CoWorkingApp.API.dll