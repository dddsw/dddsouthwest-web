using DDDSouthWest.Domain;
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
                        scanner.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<>));
                        scanner.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
                        scanner.ConnectImplementationsToTypesClosing(typeof(IAsyncRequestHandler<>));
                        scanner.ConnectImplementationsToTypesClosing(typeof(IAsyncRequestHandler<,>));
                        scanner.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
                        scanner.ConnectImplementationsToTypesClosing(typeof(IAsyncNotificationHandler<>));
                    });

                    cfg.For<ClientConfigurationOptions>().Use(() => new ClientConfigurationOptions
                    {
                        Database = new Database
                        {
                            ConnectionString =
                                "Host=localhost;Username=dddsouthwest_user;Password=letmein;Database=dddsouthwest"
                        }
                    }).Singleton();
                    
                    cfg.For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
                    cfg.For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));
                    cfg.For<IMediator>().Use<Mediator>();
                });

                return container.GetInstance<IMediator>();
            }
        }
    }
}