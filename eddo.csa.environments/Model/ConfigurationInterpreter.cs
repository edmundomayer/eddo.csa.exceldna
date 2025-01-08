using eddo.csa.environments.Interfaces;
using eddo.csa.environments.Settings;

namespace eddo.csa.environments.Model
{
    internal class ConfigurationInterpreter : BranchInterpreter, IConfigurationInterpreter
    {
        #region Fields
        private Section _section;
        private Configuration _configuration;
        #endregion Fields


        #region Constructors & Destructors
        public ConfigurationInterpreter( EnvironmentSettings environmentSetting,
                      Branch branch,
                      Section section,
                      Configuration configuration )
            : base( environmentSetting, branch )
        {
            _section = section;
            _configuration = configuration;
        }
        #endregion Constructors & Destructors


        #region Properties
        public string MSBuildCommand => _environmentSetting.MSBuild;

        public string ConfigurationTitle => string.Format( "{0}/{1}/{2}/{3}", BranchNameTitle,
                                                                            _section.SectionName,
                                                                            _configuration.Type,
                                                                            _configuration.Component );

        public string ResultCommandFile => _branch.ResultCommandFile;

        public string ProjectFile => Path.Combine( _branch.BaseBranchPath,
                                                    _configuration.ProjectPath );

        public string ProjectDirectory => Path.GetDirectoryName( ProjectFile );
        public string PublishDirectory => Path.Combine( _branch.BasePublishPath,
                                                        _branch.BranchName,
                                                        _section.SectionName,
                                                        _configuration.Type,
                                                        _configuration.Component );

        public string ConfigurationPublishTemplate => _environmentSetting.PublishTemplate;
        public string ConfigurationFolder => Path.Combine( _branch.BaseConfigurationPath,
                                                                _branch.BranchName,
                                                                _section.SectionName,
                                                                _configuration.Type,
                                                                _configuration.Component );
        public string ConfigurationPublishFile => Path.Combine( ConfigurationFolder,
                                                                _configuration.PublishProfile );

        public bool ClearBeforeBuild => _configuration.ClearBeforeBuild;
        public bool Build => _configuration.Build;
        public bool HasAdditionalConfigurationCopy => _configuration.AdditionalConfigurationCopy.Length > 0;
        #endregion Properties
    }
}
