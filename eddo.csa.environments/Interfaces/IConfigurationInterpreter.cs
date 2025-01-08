namespace eddo.csa.environments.Interfaces
{
    internal interface IConfigurationInterpreter : IBranchInterpreter
    {
        string MSBuildCommand { get; }

        string ConfigurationTitle { get; }

        string ResultCommandFile { get; }

        string ProjectFile { get; }
        string ProjectDirectory { get; }
        string PublishDirectory { get; }


        string ConfigurationPublishTemplate { get; }
        string ConfigurationFolder { get; }
        string ConfigurationPublishFile { get; }

        bool ClearBeforeBuild { get; }
        bool Build { get; }
        bool HasAdditionalConfigurationCopy { get; }
    }
}