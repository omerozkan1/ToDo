#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["OmerOzkan.ToDo.Test/OmerOzkan.ToDo.Test.csproj", "OmerOzkan.ToDo.Test/"]
COPY ["OmerOzkan.ToDo.Web/OmerOzkan.ToDo.Web.csproj", "OmerOzkan.ToDo.Web/"]
COPY ["OmerOzkan.ToDo.Business/OmerOzkan.ToDo.Business.csproj", "OmerOzkan.ToDo.Business/"]
COPY ["OmerOzkan.ToDo.DataAccess/OmerOzkan.ToDo.DataAccess.csproj", "OmerOzkan.ToDo.DataAccess/"]
COPY ["OmerOzkan.ToDo.Entities/OmerOzkan.ToDo.Entities.csproj", "OmerOzkan.ToDo.Entities/"]
COPY ["OmerOzkan.ToDo.Dto/OmerOzkan.ToDo.Dto.csproj", "OmerOzkan.ToDo.Dto/"]
RUN dotnet restore "OmerOzkan.ToDo.Test/OmerOzkan.ToDo.Test.csproj"
COPY . .
WORKDIR "/src/OmerOzkan.ToDo.Test"
RUN dotnet build "OmerOzkan.ToDo.Test.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OmerOzkan.ToDo.Test.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OmerOzkan.ToDo.Test.dll"]
