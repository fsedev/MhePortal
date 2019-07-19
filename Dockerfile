FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5001

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["mheportal.csproj", "./"]
RUN dotnet restore "./mheportal.csproj"
COPY . .
WORKDIR /src/.
RUN dotnet build "mheportal.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "mheportal.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY ./mheportalcert.pfx .
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "mheportal.dll"]
