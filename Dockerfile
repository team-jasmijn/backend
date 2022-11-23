#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.


#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /src
#COPY ["EasyIntern-Backend/EasyIntern-Backend.csproj", "EasyIntern-Backend/"]
#COPY ../Data ./Data
#RUN dotnet restore "EasyIntern-Backend/EasyIntern-Backend.csproj"
#COPY . .
#WORKDIR "/src/EasyIntern-Backend"
#RUN dotnet build "EasyIntern-Backend.csproj" -c Release -o /app/build
#RUN dotnet publish "EasyIntern-Backend.csproj" -c Release -o /app/publish

#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#COPY --from=build /app/publish .
#ENTRYPOINT ["dotnet", "EasyIntern-Backend.dll"]


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

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EasyIntern-Backend.dll"]
