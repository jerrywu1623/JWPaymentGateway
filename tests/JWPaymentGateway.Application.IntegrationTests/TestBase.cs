using System.IO;
using System.Threading.Tasks;
using JWPaymentGateway.Web;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace JWPaymentGateway.Application.IntegrationTests
{
    public class TestBase
    {
        private IConfigurationRoot _configuration;
        private static IServiceScopeFactory _scopeFactory;
        
        [OneTimeSetUp]
        public void Setup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);

            _configuration = builder.Build();

            var useInMemory = _configuration.GetValue<bool>("UseInMemoryDatabase");
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            
            var startUp = new Startup(_configuration);
            var services = new ServiceCollection();

            services.AddLogging(config => config.AddConsole());
            startUp.ConfigureServices(services);
            
            services.AddTransient<IHttpContextAccessor>(_ => SetupMockHttpContextAccessor());
            
            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();
        }
        
        private IHttpContextAccessor SetupMockHttpContextAccessor()
        {
            var fakeHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
            var httpContext = new DefaultHttpContext();
            fakeHttpContextAccessor.HttpContext.Returns(httpContext);

            return fakeHttpContextAccessor;
        }
        
        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(request);
        }

        public T GetInstance<T>()
        {
            using var scope = _scopeFactory.CreateScope();
            var result = scope.ServiceProvider.GetService<T>();
            return result;
        }
    }
}