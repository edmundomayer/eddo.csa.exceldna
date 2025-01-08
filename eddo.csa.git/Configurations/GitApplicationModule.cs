using Autofac;
using eddo.csa.git.Interfaces;
using eddo.csa.git.Model;
using eddo.csa.git.Services;
using Microsoft.Extensions.Configuration;

namespace eddo.csa.git.Configurations
{
    public class GitApplicationModule : Module
    {
        #region Implements Abstract Class Module
        protected override void Load( ContainerBuilder builder )
        {
            //var basePath = Path.GetDirectoryName( System.Reflection.Assembly.GetExecutingAssembly().Location );
            //var configFile = Path.Combine( basePath, "appsettings.json" );

            //var configuration = new ConfigurationBuilder()
            //                        .AddJsonFile( configFile, optional: false, reloadOnChange: true )
            //                        .Build();

            //// Settings
            ////
            //builder.Register( ctx =>
            //{
            //    var gitSettings = configuration.GetSection( "GitSettings" ).Get<GitSettings>();

            //    return gitSettings;
            //} )
            //.As<IGitSettings>()
            //.SingleInstance();


            // Settings
            //
            builder.Register<GitSettings>( ctx => ctx.Resolve<IConfiguration>().GetSection( "GitSettings" ).Get<GitSettings>()! ).As<IGitSettings>().SingleInstance();

            // Services
            //
            builder.RegisterType<GitServices>()
                    .As<IGitService>()
                    .InstancePerDependency();
        }
        #endregion Implements Abstract Class Module
    }
}