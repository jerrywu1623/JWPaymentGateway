FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
ENV ASPNETCORE_URLS=https://+:5001;http://+:5000
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/JWPaymentGateway.Web/JWPaymentGateway.Web.csproj", "src/JWPaymentGateway.Web/"]
COPY ["src/JWPaymentGateway.Application/JWPaymentGateway.Application.csproj", "src/JWPaymentGateway.Application/"]
COPY ["src/JWPaymentGateway.Domain/JWPaymentGateway.Domain.csproj", "src/JWPaymentGateway.Domain/"]
COPY ["src/JWPaymentGateway.Infrastructure/JWPaymentGateway.Infrastructure.csproj", "src/JWPaymentGateway.Infrastructure/"]
RUN dotnet restore "src/JWPaymentGateway.Web/JWPaymentGateway.Web.csproj"
COPY . .
WORKDIR "/src/src/JWPaymentGateway.Web"
RUN dotnet build "JWPaymentGateway.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JWPaymentGateway.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JWPaymentGateway.Web.dll"]
