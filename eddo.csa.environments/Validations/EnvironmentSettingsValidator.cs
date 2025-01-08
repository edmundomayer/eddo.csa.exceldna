using eddo.csa.environments.Helpers;
using eddo.csa.environments.Settings;
using FluentValidation;

namespace eddo.csa.environments.Validations
{
    internal class EnvironmentSettingsValidator : AbstractValidator<EnvironmentSettings>
    {
        public EnvironmentSettingsValidator()
        {
            // TODO: Improve Validations (sets?)


            // MSBuild
            // 
            RuleFor( envSetting => envSetting.MSBuild )
                .Cascade( CascadeMode.Stop )
                .NotNull()
                .WithMessage( "Cannot be Null." )
                .WithErrorCode( "MSBuildNull" )
                .Must( ( envSetting, value ) => envSetting.MSBuild.Trim() != string.Empty )
                .WithMessage( "Cannot be Empty." )
                .WithErrorCode( "MSBuildEmpty" )
                .Must( ( envSetting, value ) => envSetting.MSBuild.PathIsFile() )
                .WithMessage( "MSBuild.exe command is not found in the specified path." )
                .WithErrorCode( "MSBuildInvalidPath" );


            //RuleFor( envSetting => envSetting.MSBuild )
            //    .SetValidator( new FilePathValidator(), ruleSets:new string[]{ "uno","dos"} );


            // PublishTemplate
            // 
            RuleFor( envSetting => envSetting.PublishTemplate )
                .NotNull()
                .WithMessage( "Cannot be Null." )
                .WithErrorCode( "PublishTemplateNull" )
                .NotEmpty()
                .Must( ( envSetting, value ) => envSetting.PublishTemplate.Trim() != string.Empty )
                .WithMessage( "Cannot be Empty." )
                .WithErrorCode( "PublishTemplateEmpty" )
                .Must( ( envSetting, value ) => Path.GetDirectoryName( envSetting.PublishTemplate ).PathIsDirectory() )
                .WithMessage( "A publication configuration template file is required to publish the different components. Indicate the path and name of the configuration template, if it does not exist, will be created in the indicated path." )
                .WithErrorCode( "PublishTemplateInvalidPath" );

            //Path.GetDirectoryName( "C:\\path\\to\\file.txt" );
            //    .WithSeverity( Severity.Warning )
            //    .WithMessage( "A publication configuration template file is required to publish the different components. Indicate the path and name of the configuration template, if it does not exist, will be created in the indicated path." );

            RuleFor( envSetting => envSetting.PublishTemplate )
                .Must( ( envSetting, value ) => File.Exists( envSetting.PublishTemplate ) )
                .WithSeverity( Severity.Warning )
                .WithMessage( ( envSetting, value ) => string.Format( "Configuration template file will be automatically generated on '{0}'", value ) );



            // Branches
            // 
            RuleFor( envSetting => envSetting.Branches )
                .Must( items => items == null || items.Length == 0 )
                .WithMessage( "No Branches defined." );
        }
    }
}
