#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.





#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.



FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Covid19_World/Covid_World.csproj", "Covid19_World/"]
COPY ["EFDataAccessLibrary/EFDataAccessLibrary.csproj", "EFDataAccessLibrary/"]
COPY ["MyDLL", "MyDLL"]
RUN dotnet restore "Covid19_World/Covid_World.csproj"
COPY . .
WORKDIR "/src/Covid19_World"
RUN dotnet build "Covid_World.csproj" -c Release -o /app/build 

FROM build AS publish
RUN dotnet publish "Covid_World.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Covid_World.dll"]


