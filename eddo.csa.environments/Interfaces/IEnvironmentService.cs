using FluentValidation.Results;

namespace eddo.csa.environments.Interfaces
{
    public interface IEnvironmentService
    {
        void PrepareFolders( params string[] branchNames );

        List<string> PrepareCommands( params string[] branchNames );

        bool ValidateSettings( out ValidationResult validationResult );
    }
}
