namespace eddo.csa.environments.Settings
{
    public class Configuration
    {
        #region Properties
        public int Order { get; set; }
        public string Type { get; set; }
        public string Component { get; set; }
        public string ProjectPath { get; set; }
        public string ProjectName { get; set; }
        public string PublishProfile { get; set; }
        public bool ClearBeforeBuild { get; set; }
        public bool Build { get; set; }
        public AdditionalCopy[] AdditionalConfigurationCopy { get; set; }
        #endregion Properties
    }
}
