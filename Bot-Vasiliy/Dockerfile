#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Bot-Vasiliy/Bot-Vasiliy.csproj", "Bot-Vasiliy/"]
RUN dotnet restore "Bot-Vasiliy/Bot-Vasiliy.csproj"
COPY . .
WORKDIR "/src/Bot-Vasiliy"
RUN dotnet build "Bot-Vasiliy.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bot-Vasiliy.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bot-Vasiliy.dll"]