namespace eddo.csa.git.Model
{
    public class GitBranch
    {
        #region Properties
        public string Name { get; set; }
        public string Alias { get; set; }
        public int Order { get; set; }
        public string BranchPath { get; set; }
        public string OutputPath { get; set; }
        public GitBranchConfigurationItem[] Configuration { get; set; }
        #endregion Properties
    }
}
