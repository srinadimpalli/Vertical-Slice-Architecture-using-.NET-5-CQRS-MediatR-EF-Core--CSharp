
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["VerticalArchApp.API/VerticalArchApp.API.csproj", "VerticalArchApp.API/"]
RUN dotnet restore "VerticalArchApp.API/VerticalArchApp.API.csproj"
COPY . .
WORKDIR "/src/VerticalArchApp.API"
RUN dotnet build "VerticalArchApp.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VerticalArchApp.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VerticalArchApp.API.dll"]