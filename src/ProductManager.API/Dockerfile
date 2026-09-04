# 1. Etapa de compilación (SDK de .NET 10)
FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src

# Copiar archivos de proyecto (.csproj) para restaurar dependencias y aprovechar la caché
COPY ["ProductManager.API/ProductManager.API.csproj", "ProductManager.API/"]
COPY ["ProductManager.Infrastructure/ProductManager.Infrastructure.csproj", "ProductManager.Infrastructure/"]
COPY ["ProductManager.Core/ProductManager.Core.csproj", "ProductManager.Core/"]

# Restaurar paquetes NuGet
RUN dotnet restore "ProductManager.API/ProductManager.API.csproj"

# Copiar el resto del código fuente de la solución
COPY . .

# Compilar y publicar la aplicación en modo Release
WORKDIR "/src/ProductManager.API"
RUN dotnet publish "ProductManager.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 2. Etapa de ejecución (Runtime optimizado)
FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS final
WORKDIR /app

# Puerto interno en el que escuchará la API dentro del contenedor
EXPOSE 8080

# Copiar los binarios publicados desde la etapa de compilación
COPY --from=build /app/publish .

# Comando de entrada para iniciar la Web API
ENTRYPOINT ["dotnet", "ProductManager.API.dll"]