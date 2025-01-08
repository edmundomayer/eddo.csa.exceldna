using Autofac;
using eddo.csa.environments.Interfaces;
using eddo.csa.environments.Services;
using eddo.csa.environments.Settings;
using eddo.csa.environments.Validations;
using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace eddo.csa.environments.Configurations
{
    public class EnvironmentApplicationModule : Module
    {
        #region Implements Abstract Class Module
        protected override void Load( ContainerBuilder builder )
        {
            // Settings
            //
            builder.Register<EnvironmentSettings>( ctx => ctx.Resolve<IConfiguration>().GetSection( "EnvironmentSettings" ).Get<EnvironmentSettings>()! ).SingleInstance();

            // Validators
            //
            builder.RegisterType<EnvironmentSettingsValidator>()
                    .As<IValidator<EnvironmentSettings>>()
                    .InstancePerDependency();

            // Services
            //
            builder.RegisterType<EnvironmentService>()
                    .As<IEnvironmentService>()
                    .InstancePerDependency();
        }
        #endregion Implements Abstract Class Module
    }
}
