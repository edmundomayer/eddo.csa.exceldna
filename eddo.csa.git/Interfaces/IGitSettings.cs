using eddo.csa.git.Model;

namespace eddo.csa.git.Interfaces
{
    public interface IGitSettings
    {
        string Alias { get; set; }
        string PatchFileRegexPattern { get; set; }
        GitBranch[] Branches { get; set; }
    }
}