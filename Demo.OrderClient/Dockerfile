#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Demo.OrderClient/Demo.OrderApi.csproj", "Demo.OrderClient/"]
COPY ["DiuDiu.Client/DiuDiu.Client.csproj", "DiuDiu.Client/"]
RUN dotnet restore "Demo.OrderClient/Demo.OrderApi.csproj"
COPY . .
WORKDIR "/src/Demo.OrderClient"
RUN dotnet build "Demo.OrderApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Demo.OrderApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Demo.OrderApi.dll"]
