#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DaySchedulerApp.Api/DaySchedulerApp.Api.csproj", "DaySchedulerApp.Api/"]
COPY ["DaySchedulerApp.Application/DaySchedulerApp.Application.csproj", "DaySchedulerApp.Application/"]
COPY ["DaySchedulerApp.Domain/DaySchedulerApp.Domain.csproj", "DaySchedulerApp.Domain/"]
COPY ["DaySchedulerApp.Identity/DaySchedulerApp.Identity.csproj", "DaySchedulerApp.Identity/"]
COPY ["DaySchedulerApp.Infrastructure/DaySchedulerApp.Infrastructure.csproj", "DaySchedulerApp.Infrastructure/"]
COPY ["DaySchedulerApp.Persistance/DaySchedulerApp.Persistance.csproj", "DaySchedulerApp.Persistance/"]
RUN dotnet restore "DaySchedulerApp.Api/DaySchedulerApp.Api.csproj"
COPY . .
WORKDIR "/src/DaySchedulerApp.Api"
RUN dotnet build "DaySchedulerApp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DaySchedulerApp.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DaySchedulerApp.Api.dll"]