using eddo.csa.git.Interfaces;

namespace eddo.csa.git.Model
{
    public class GitSettings : IGitSettings
    {
        #region Properties
        public string Alias { get; set; }
        public string PatchFileRegexPattern { get; set; }
        public GitBranch[] Branches { get; set; }
        #endregion Properties
    }
}
