#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
COPY bin/Release/net6.0/publish app/
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://*:5000
ENTRYPOINT ["dotnet", "StimulationAppAPI.dll"]

#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /src
#COPY ["StimulationAppAPI.csproj", "StimulationAppAPI/"]
#COPY ["StimulationAppAPI.BLL/*.csproj", "StimulationAppAPI.BLL/"]
#COPY ["StimulationApp.DAL/*.csproj", "StimulationApp.DAL/"]
#RUN dotnet restore "StimulationAppAPI/StimulationAppAPI.csproj"
#COPY . .
#WORKDIR "/src/StimulationAppAPI"
#RUN dotnet build "StimulationAppAPI.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "StimulationAppAPI.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "StimulationAppAPI.dll"]