FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5188

ENV ASPNETCORE_URLS=http://+:5188

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Adasofttest/Adasofttest.csproj", "Adasofttest/"]
RUN dotnet restore "Adasofttest\Adasofttest.csproj"
COPY . .
WORKDIR "/src/Adasofttest"
RUN dotnet build "Adasofttest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Adasofttest.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Adasofttest.dll"]
