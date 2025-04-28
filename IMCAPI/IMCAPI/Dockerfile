# Etapa 1: Build y pruebas (usando SDK de .NET)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia los archivos del proyecto y restaura dependencias
COPY ["IMCAPI.csproj", "."]
RUN dotnet restore "IMCAPI.csproj"

# Copia el resto del código
COPY . .

# Ejecuta pruebas unitarias (opcional, descomenta si quieres ejecutarlas en el build)
# RUN dotnet test --logger "trx;LogFileName=test-results.trx" --results-directory /TestResults

# Publica la aplicación para producción
RUN dotnet publish "IMCAPI.csproj" -c Release -o /app/publish

# Etapa 2: Runtime (imagen ligera sin SDK)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Configura variables de entorno para EF Core
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ConnectionStrings__DefaultConnection="Server=sqlserver;Database=IMCDb;User=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true;"

# Copia los archivos publicados desde la etapa 'build'
COPY --from=build /app/publish .

# Expone el puerto que usa tu API (ajusta si es necesario)
EXPOSE 80
EXPOSE 443

# Comando de inicio
ENTRYPOINT ["dotnet", "IMCAPI.dll"]