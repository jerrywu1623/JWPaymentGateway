# JWPaymentGateway
This is a RESTful API application which contains 2 endpoints:
1. /api/payments
2. /api/payments/{paymentId}

# Tech Stack
- .NET 5
- Entity Framework Core
- MediatR
- FluentValidation
- Mapster
- NUnit, NSubsitute
- Docker

# Docker Configuration
In order to get Docker working, you will need to add a temporary SSL cert and mount a volume to hold that cert.
- For Windows: The following will need to be executed from your terminal to create a cert `dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p Your_password123` `dotnet dev-certs https --trust`
- FOR macOS: `dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p Your_password123` `dotnet dev-certs https --trust`
- FOR Linux: `dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p Your_password123`

# Getting Started

1. Install the latest [Docker](https://docs.docker.com/get-docker/)
2. Open the terminal in the root of solution, run `docker-compose up -d --build`
3. Open the browser and navigate to [https://localhost:5001/swagger/index.html](https://localhost:5001/swagger/index.html), and view the swagger info
4. You shall add `x-api-key` and `merchant-id` in request headers, please find out the values in `ApplicationDbContextSeed`

