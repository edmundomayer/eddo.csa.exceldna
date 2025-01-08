using eddo.csa.git.Model;

namespace eddo.csa.git.Interfaces
{
    public interface IGitService
    {
        IEnumerable<string> GetBrancheAliases();
        IEnumerable<PendingCommitFile> GetPendingCommitFilesByBranchName( Func<GitBranch, bool> branch, Func<PendingCommitFile, bool> filter = null );
        IEnumerable<PendingCommitFile> GetPendingCommitFilesByBranchName( string branchName, Func<PendingCommitFile, bool> filter = null );
        IEnumerable<PendingCommitFile> GetChangesFromPatchFile( string patchFileName );
    }
}