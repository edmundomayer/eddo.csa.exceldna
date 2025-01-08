namespace eddo.csa.environments.Settings
{
    public class EnvironmentSettings
    {
        #region Properties
        public string MSBuild { get; set; }
        public string PublishTemplate { get; set; }
        public Branch[] Branches { get; set; }
        #endregion Properties
    }
}
