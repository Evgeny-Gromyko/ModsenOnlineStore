FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["./TokenAuthentication/ModsenOnlineStore.Login.API/ModsenOnlineStore.Login.API.csproj", "TokenAuthentication/ModsenOnlineStore.Login.API/"]
COPY ["./ModsenOnlineStore.Common/ModsenOnlineStore.Common.csproj", "ModsenOnlineStore.Common/"]
COPY ["./TokenAuthentication/ModsenOnlineStore.Login.Application/ModsenOnlineStore.Login.Application.csproj", "TokenAuthentication/ModsenOnlineStore.Login.Application/"]
COPY ["./TokenAuthentication/ModsenOnlineStore.Login.Domain/ModsenOnlineStore.Login.Domain.csproj", "TokenAuthentication/ModsenOnlineStore.Login.Domain/"]
COPY ["./TokenAuthentication/ModsenOnlineStore.Login.Infrastructure/ModsenOnlineStore.Login.Infrastructure.csproj", "TokenAuthentication/ModsenOnlineStore.Login.Infrastructure/"]
RUN dotnet restore "TokenAuthentication/ModsenOnlineStore.Login.API/ModsenOnlineStore.Login.API.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "TokenAuthentication/ModsenOnlineStore.Login.API/ModsenOnlineStore.Login.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TokenAuthentication/ModsenOnlineStore.Login.API/ModsenOnlineStore.Login.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ModsenOnlineStore.Login.API.dll"]
