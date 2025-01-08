namespace eddo.csa.environments.Settings
{
    public class Branch
    {
        #region Properties
        public string BranchName { get; set; }
        public string BaseBranchPath { get; set; }
        public string BaseConfigurationPath { get; set; }
        public string PublishType { get; set; }
		public string BasePublishPath { get; set; }
        public string ResultCommandFile { get; set; }
        public Section[] Sections { get; set; }
        #endregion Properties
    }
}
