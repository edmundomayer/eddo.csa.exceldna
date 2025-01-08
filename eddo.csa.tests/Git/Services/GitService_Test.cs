//using eddo.csa.git.Interfaces;
//using Microsoft.Extensions.DependencyInjection;

//namespace eddo.csa.tests.Git.Services
//{
//    public class GitService_Test : TestBase
//    {
//        #region Fields
//        private IGitService _gitService;
//        #endregion Fields


//        #region Constructors & Destructors
//        public GitService_Test()
//        {
//            _gitService = base.GetHost().Services.GetRequiredService<IGitService>();
//        }
//        #endregion Constructors & Destructors


//        #region Methods
//        [Test]
//        public void GetPendingCommitFiles_WhenRepoIsDirty_Test()
//        {
//            // Act
//            //
//            var result = _gitService.GetPendingCommitFilesByBranchName( "hotfix" ).ToList();

//            Assert.Pass();
//        }

//        [Test]
//        public void GetUpdatesFromPatchFile_WhenPatchFileIsProvided_Test()
//        {
//            // Act
//            //
//            var result = _gitService.GetChangesFromPatchFile( "C:\\GIT\\CSA\\fiba.csa.environments\\GitPatches\\Hotfix\\00-Data-EDDO-only.diff" ).ToList();

//            Assert.Pass();
//        }
//        #endregion Methods
//    }
//}
