FROM mcr.microsoft.com/dotnet/aspnet:6.0 as base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY . /src
WORKDIR /src
RUN ls
RUN dotnet restore .
RUN dotnet build "EasyIntern-Backend/EasyIntern-Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EasyIntern-Backend/EasyIntern-Backend.csproj" -c Release -o /app/publish

FROM publish AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN dotnet tool install -g dotnet-ef
ENV PATH $PATH:/root/.dotnet/tools

ENTRYPOINT ["dotnet", "EasyIntern-Backend.dll"]
