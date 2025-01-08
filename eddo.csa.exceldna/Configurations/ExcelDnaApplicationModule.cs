using Autofac;
using eddo.csa.exceldna.Git;
using eddo.csa.exceldna.Git.Interfaces;
using eddo.csa.exceldna.Interfaces;
using eddo.csa.exceldna.Services;
using eddo.csa.exceldna.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace eddo.csa.exceldna.Configurations
{
    internal class ExcelDnaApplicationModule : Module
    {
        #region Implements Abstract Class Module
        protected override void Load( ContainerBuilder builder )
        {
            // Options
            //
            builder.Register<IOptions<ContainerOptions>>( ctx => new OptionsWrapper<ContainerOptions>( ctx.Resolve<IConfiguration>().GetSection( "Settings:Container" ).Get<ContainerOptions>()! ) ).SingleInstance();
            builder.Register<IOptions<OrderOptions>>( ctx => new OptionsWrapper<OrderOptions>( ctx.Resolve<IConfiguration>().GetSection( "Settings:Order" ).Get<OrderOptions>()! ) ).SingleInstance();
            builder.Register<IOptions<ServerOptions>>( ctx => new OptionsWrapper<ServerOptions>( ctx.Resolve<IConfiguration>().GetSection( "Settings:Server" ).Get<ServerOptions>()! ) ).SingleInstance();
            builder.Register<IOptions<Dictionary<string, TableMappers>>>( ctx => new OptionsWrapper<Dictionary<string, TableMappers>>( ctx.Resolve<IConfiguration>().GetSection( "Settings:TableMappers" ).Get<Dictionary<string, TableMappers>>()! ) ).SingleInstance();

            // Services
            //
            builder.RegisterType<Worker>().SingleInstance();
            builder.RegisterType<QuickStartService>().As<IQuickStartService>().InstancePerLifetimeScope();

            // Panels
            //
            builder.RegisterType<GitPanel>().As<IGitPanel>().InstancePerLifetimeScope();
        }
        #endregion Implements Abstract Class Module
    }
}