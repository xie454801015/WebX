FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["WebX.Api/WebX.Api.csproj", "WebX.Api/"]
RUN dotnet restore "WebX.Api/WebX.Api.csproj"
COPY . .
WORKDIR "/src/WebX.Api"
RUN dotnet build "WebX.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WebX.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebX.Api.dll"]