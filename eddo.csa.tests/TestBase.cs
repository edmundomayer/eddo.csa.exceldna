using Autofac;
using Autofac.Core.Registration;
using Autofac.Extensions.DependencyInjection;
using eddo.csa.environments.Configurations;
using eddo.csa.exceldna.Configurations;
using eddo.csa.exceldna.Interfaces;
using eddo.csa.exceldna.Settings;
using eddo.csa.git.Configurations;
using eddo.csa.git.Interfaces;
using eddo.csa.git.Model;
using eddo.csa.git.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace eddo.csa.tests
{
    public abstract class TestBase
    {
        #region Fields
        private IHost _testHost;
        #endregion Fields


        #region Constructors & Destructors
        public TestBase()
        {
            _testHost = CreateHostBuilder().Build();
            _testHost.StartAsync().GetAwaiter().GetResult();
        }
        #endregion Constructors & Destructors


        #region Properties
        protected IHost GetHost() => _testHost;
        #endregion Properties


        #region Methods
        //protected virtual Action<ContainerBuilder> OverrideRegister => null;

        private IHostBuilder CreateHostBuilder() => Host.CreateDefaultBuilder()
                .UseServiceProviderFactory( new AutofacServiceProviderFactory() )
                .ConfigureAppConfiguration( ( hostingContext, config ) =>
                {
                    var basePath = Path.GetDirectoryName( AppContext.BaseDirectory );
                    //var configFile = Path.Combine( basePath, "appsettings.json" );

                    //config.SetBasePath( basePath );
                    //config.AddJsonFile( configFile, optional: false, reloadOnChange: true );

                    config.SetBasePath( basePath );

                    // Load appsettings
                    config.AddJsonFile( Path.Combine( basePath, "appsettings.json" ), optional: false, reloadOnChange: true );

                    // Load git-settings
                    config.AddJsonFile( Path.Combine( basePath, "git-settings.json" ), optional: false, reloadOnChange: true );

                    // Load environment-settings
                    config.AddJsonFile( Path.Combine( basePath, "environments-settings.json" ), optional: false, reloadOnChange: true );


                    var env = hostingContext.HostingEnvironment;

                    Console.WriteLine( $"Environment name: {env.EnvironmentName}" );
                } )
                //.ConfigureAppConfiguration( ( hostingContext, app ) =>
                //{
                //    var env = hostingContext.HostingEnvironment;

                //    Console.WriteLine( $"Environment name: {env.EnvironmentName}" );
                //} )
                .ConfigureLogging( logging =>
                {
                    // TODO: Implement custom Loging

                    //logging.AddLogDisplay( options =>
                    //{
                    //    options.TimestampFormat = "G";
                    //} );
                } )
                .ConfigureContainer<ContainerBuilder>( ( context, builder ) =>
                {
                    // Share Configuration with the rest of Modules
                    //
                    builder.RegisterInstance<IConfiguration>( context.Configuration );


                    // Register ExcelDna module
                    builder.RegisterModule<ExcelDnaApplicationModule>();


                    // Register Git module
                    builder.RegisterModule<GitApplicationModule>();


                    // Register Environments module
                    builder.RegisterModule<EnvironmentApplicationModule>();

                    //if( OverrideRegister != null ) { OverrideRegister( builder ); }
                } )
                .ConfigureServices( ( context, services ) =>
                {

                    //services.AddSingleton<Worker>();

                    // This will register an IOptions<ContainerOptions> object, not a simple ContainerOptions object.
                    services.AddOptions<ContainerOptions>().BindConfiguration( "Settings:Container" );

                    // This will also register an IOptions<OrderOptions> object.
                    // It requires access to the HostBuilderContext.Configuration property.
                    // Note that the nested AddressOptions object will also be populated at the same time.
                    services.Configure<OrderOptions>( context.Configuration.GetSection( "Settings:Order" ) );

                    // This allows us to register an IServerOptions object, without wrapping it in IOptions.
                    // It also requires access to the HostBuilderContext.Configuration property.
                    // If we wanted we could have registered it as a concrete type, rather than as the implementation of an 
                    // interface.
                    ServerOptions serverOptions = context.Configuration.GetSection( "Settings:Server" ).Get<ServerOptions>();
                    services.AddSingleton<IServerOptions>( serverOptions );

                    // Git
                    //
                    GitSettings gitSettings = context.Configuration.GetSection( "GitSettings" ).Get<GitSettings>();
                    services.AddSingleton<IGitSettings>( gitSettings );

                    services.AddTransient<IGitService, GitServices>();


                    //services.AddTransient<IQuickStartService, QuickStartService>();

                    // TODO: is AddExcelFunctionsProcessor required???

                    //services.AddExcelFunctionsProcessor( functions => functions
                    //    .ProcessParamsRegistrations()
                    //    .ProcessAsyncRegistrations()
                    //);

                    //services.AddExcelRibbon<QuickStartRibbon>();

                    //services.AddExcelFunctions<QuickStartFunctions>();
                    //services.AddExcelFunctions<TagsHelper>();
                } );
        #endregion Methods
    }
}
