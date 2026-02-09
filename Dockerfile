# Etapa 1: Compilación usando SDK 10
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar archivos de proyectos
COPY ["BookingEventos.Api/BookingEventos.Api.csproj", "BookingEventos.Api/"]
COPY ["BookingEventos.Application/BookingEventos.Application.csproj", "BookingEventos.Application/"]
COPY ["BookingEventos.Domain/BookingEventos.Domain.csproj", "BookingEventos.Domain/"]
COPY ["BookingEventos.Infrastructure/BookingEventos.Infrastructure.csproj", "BookingEventos.Infrastructure/"]

RUN dotnet restore "BookingEventos.Api/BookingEventos.Api.csproj"

# Copiar todo el código y compilar
COPY . .
WORKDIR "/src/BookingEventos.Api"
RUN dotnet build "BookingEventos.Api.csproj" -c Release -o /app/build

# Etapa 2: Publicación
FROM build AS publish
RUN dotnet publish "BookingEventos.Api.csproj" -c Release -o /app/publish

# Etapa 3: Imagen Final (Runtime) usando ASPNET 10
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookingEventos.Api.dll"]