FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore hhh.webapi.admin/hhh.webapi.admin.csproj
RUN dotnet publish hhh.webapi.admin/hhh.webapi.admin.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
ENV TZ=Asia/Taipei
ENV ASPNETCORE_URLS=http://0.0.0.0:6000
EXPOSE 6000
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "hhh.webapi.admin.dll"]