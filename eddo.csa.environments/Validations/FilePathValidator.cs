using eddo.csa.environments.Helpers;
using FluentValidation;

namespace eddo.csa.environments.Validations
{
    internal class FilePathValidator : AbstractValidator<string>
    {
        public FilePathValidator()
        {
            // MSBuild
            // 
            RuleFor( envSetting => envSetting )
                .Cascade( CascadeMode.Stop )
                .NotNull()
                .WithMessage( "Cannot be Null." )
                .WithErrorCode( "MSBuildNull" )
                .Must( ( envSetting, value ) => envSetting.Trim() != string.Empty )
                .WithMessage( "Cannot be Empty." )
                .WithErrorCode( "MSBuildEmpty" )
                .Must( ( envSetting, value ) => envSetting.PathIsFile() )
                .WithMessage( "MSBuild.exe command is not found in the specified path." )
                .WithErrorCode( "MSBuildInvalidPath" );
        }
    }
}
