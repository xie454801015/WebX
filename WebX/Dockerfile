FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["WebX/WebX.csproj", "WebX/"]
RUN dotnet restore "WebX/WebX.csproj"
COPY . .
WORKDIR "/src/WebX"
RUN dotnet build "WebX.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WebX.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebX.dll"]