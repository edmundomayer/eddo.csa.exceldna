using Autofac;
using Autofac.Extensions.DependencyInjection;
using eddo.csa.environments.Configurations;
using eddo.csa.environments.Interfaces;
using eddo.csa.exceldna.Configurations;
using eddo.csa.exceldna.Functions;
using eddo.csa.exceldna.Functions.Snow;
using eddo.csa.exceldna.Git.Ribbons;
using eddo.csa.exceldna.hosting;
using eddo.csa.exceldna.hosting.Helpers;
using eddo.csa.exceldna.Ribbons;
using eddo.csa.exceldna.Settings;
using eddo.csa.git.Configurations;
using eddo.csa.git.Interfaces;
using ExcelDna.Integration;
using ExcelDna.IntelliSense;
using ExcelDna.Registration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace eddo.csa.exceldna
{
    public class QuickStartAddIn : HostedExcelAddIn
    {
        #region Fields
        private string _onDebugOpenDefaultExcelFile;
        #endregion Fields


        #region Implements Abstract Class HostedExcelAddIn
        protected override void AutoOpen( IHost host )
        {
#if DEBUG
            var container = host.Services.GetAutofacRoot();

            var workerInstance = container.Resolve<Worker>();
            var gitService = container.Resolve<IGitService>();
            var environmentService = container.Resolve<IEnvironmentService>();

            //environmentService.PrepareCommands();

            workerInstance.DoWork();
#endif


            IntelliSenseServer.Install();


#if DEBUG
            if( _onDebugOpenDefaultExcelFile != null )
            {
                var xlApp = ( Microsoft.Office.Interop.Excel.Application ) ExcelDnaUtil.Application;
                xlApp.Workbooks.Open( _onDebugOpenDefaultExcelFile );
            }
#endif
        }

        protected override void AutoClose( IHost host ) => IntelliSenseServer.Uninstall();

        protected override IHostBuilder CreateHostBuilder() => Host.CreateDefaultBuilder()
                .UseServiceProviderFactory( new AutofacServiceProviderFactory() )
                .ConfigureAppConfiguration( ( hostingContext, config ) =>
                {
                    var basePath = Path.GetDirectoryName( ExcelDnaUtil.XllPath );

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
                } )
                .ConfigureServices( ( context, services ) =>
                {
                    _onDebugOpenDefaultExcelFile = context.Configuration.GetValue<string>( "OnDebugOpenDefaultExcelFile" );

                    services.AddExcelFunctionsProcessor( functions => functions
                        .ProcessParamsRegistrations()
                        .ProcessAsyncRegistrations()
                    );

                    services.AddExcelRibbon<QuickStartRibbon>();
                    services.AddExcelRibbon<GitRibbon>();

                    services.AddExcelFunctions<QuickStartFunctions>();
                    services.AddExcelFunctions<DailyFunctions>();
                    services.AddExcelFunctions<TagsHelper>();
                } );
        #endregion Implements Abstract Class HostedExcelAddIn
    }
}
