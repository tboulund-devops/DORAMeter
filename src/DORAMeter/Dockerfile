FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/DORAMeter/DORAMeter.csproj", "src/DORAMeter/"]
RUN dotnet restore "src/DORAMeter/DORAMeter.csproj"
COPY . .
WORKDIR "/src/src/DORAMeter"
RUN dotnet build "DORAMeter.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DORAMeter.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DORAMeter.dll"]
