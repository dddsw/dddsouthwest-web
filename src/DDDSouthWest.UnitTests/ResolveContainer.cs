using DDDSouthWest.Domain;
using DDDSouthWest.Domain.Features.Account.RegisterNewUser;
using DDDSouthWest.Domain.Features.Public.Page;
using MediatR;
using StructureMap;

namespace DDDSouthWest.UnitTests
{
    public static class ResolveContainer
    {
        public static IMediator MediatR
        {
            get
            {
                var container = new Container(cfg =>
                {
                    cfg.Scan(scanner =>
                    {
                        scanner.AssemblyContainingType<GetPage.Query>();
                        scanner.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
                        scanner.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<>));
                        scanner.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
                    });

                    cfg.For<IRegistrationConfirmation>().Use<SendEmailConfirmation>();
                    cfg.For<ClientConfigurationOptions>().Use(() => new ClientConfigurationOptions
                    {
                        Database = new Database
                        {
                            ConnectionString =
                                "Host=localhost;Username=dddsouthwest_user;Password=letmein;Database=dddsouthwest"
                        },
                        WebsiteSettings = new WebsiteSettings
                        {
                            RequireNewAccountConfirmation = false
                        }
                    }).Singleton();
                    
                    cfg.For<ServiceFactory>().Use<ServiceFactory>(ctx => t => ctx.GetInstance(t));
                    cfg.For<IMediator>().Use<Mediator>();
                });

                return container.GetInstance<IMediator>();
            }
        }
    }
}