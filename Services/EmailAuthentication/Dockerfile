FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["./ModsenOnlineStore.EmailAuthentication.API/ModsenOnlineStore.EmailAuthentication.API.csproj", "ModsenOnlineStore.EmailAuthentication.API/"]
COPY ["./ModsenOnlineStore.EmailAuthentication.Application/ModsenOnlineStore.EmailAuthentication.Application.csproj", "ModsenOnlineStore.EmailAuthentication.Application/"]
COPY ["./ModsenOnlineStore.EmailAuthentication.Domain/ModsenOnlineStore.EmailAuthentication.Domain.csproj", "ModsenOnlineStore.EmailAuthentication.Domain/"]
COPY ["./ModsenOnlineStore.EmailAuthentication.Infrastructure/ModsenOnlineStore.EmailAuthentication.Infrastructure.csproj", "ModsenOnlineStore.EmailAuthentication.Infrastructure/"]
RUN dotnet restore "./ModsenOnlineStore.EmailAuthentication.API/ModsenOnlineStore.EmailAuthentication.API.csproj"
COPY . .
WORKDIR "/src/ModsenOnlineStore.EmailAuthentication.API"
RUN dotnet build "ModsenOnlineStore.EmailAuthentication.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ModsenOnlineStore.EmailAuthentication.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ModsenOnlineStore.EmailAuthentication.API.dll"]
