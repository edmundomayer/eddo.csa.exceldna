using eddo.csa.git.Interfaces;
using eddo.csa.git.Model;
using LibGit2Sharp;
using System.Text.RegularExpressions;

namespace eddo.csa.git.Services
{
    public class GitServices : IGitService
    {
        #region Fields
        private IGitSettings _settings;
        #endregion Fields


        #region Constructors & Destructors
        public GitServices( IGitSettings settings )
        {
            _settings = settings;
        }
        #endregion Constructors & Destructors


        #region Implements Interface IGitService
        public IEnumerable<string> GetBrancheAliases()
        {
            return _settings.Branches
                    .OrderBy( x => x.Order )
                    .Select( x => x.Alias );
        }

        public IEnumerable<PendingCommitFile> GetPendingCommitFilesByBranchName( string branchName, Func<PendingCommitFile, bool> filter = null )
        {
            return GetPendingCommitFilesByBranchName( branch => branch.Name == branchName, filter );
        }

        public IEnumerable<PendingCommitFile> GetPendingCommitFilesByBranchName( Func<GitBranch, bool> branch, Func<PendingCommitFile, bool> filter = null )
        {
            if( branch == null )
                throw new ArgumentException( string.Format( "Parameter branch can't be null or empty." ), "branch" );

            string repoPath = null;

            try
            {
                repoPath = _settings.Branches.Single( x => branch( x ) )?.BranchPath;
            }
            catch( InvalidOperationException _error )
            {
                throw new ArgumentException( string.Format( "Requested Branch not found on settings file" ), _error );
            }
            catch( Exception _error )
            {
                throw _error;
            }

            List<PendingCommitFile> result = new List<PendingCommitFile>();

            using( var repo = new Repository( repoPath ) )
            {
                var repoStatus = repo.RetrieveStatus();

                if( repoStatus.IsDirty )
                    result.AddRange( repoStatus
                            .Select( x => new PendingCommitFile( fullFileName: string.Format( "{0}", Path.Combine( repoPath, x.FilePath.Replace( '/', '\\' ) ) ), type: x.State.ToString() ) ) );
            }

            if( filter != null )
                result = result.Where( x => filter( x ) ).ToList();

            return result;
        }

        public IEnumerable<PendingCommitFile> GetChangesFromPatchFile( string patchFileName )
        {
            if( string.IsNullOrEmpty( patchFileName ) )
                throw new ArgumentException( string.Format( "Parameter patchFileName can't be null or empty." ), patchFileName );


            string patchFileContent = null;

            try
            {
                patchFileContent = File.ReadAllText( patchFileName );
            }
            catch( Exception _error )
            {
                throw _error;
            }

            List<PendingCommitFile> result = new List<PendingCommitFile>();

            var pattern = _settings.PatchFileRegexPattern;

            Regex regex = new Regex( pattern );
            MatchCollection matchCollection = regex.Matches( patchFileContent );

            result.AddRange( matchCollection.Select( x => new PendingCommitFile( x.Value ) ) );

            return result.Distinct();
        }
        #endregion Implements Interface IGitService
    }
}
