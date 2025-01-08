using eddo.csa.environments.Interfaces;
using eddo.csa.environments.Settings;

namespace eddo.csa.environments.Model
{
    internal class BranchInterpreter : IBranchInterpreter
    {
        #region Fields
        protected EnvironmentSettings _environmentSetting;
        protected Branch _branch;
        #endregion Fields


        #region Constructors & Destructors
        public BranchInterpreter( EnvironmentSettings environmentSetting,
                                    Branch branch )
        {
            _environmentSetting = environmentSetting;
            _branch = branch;
        }
        #endregion Constructors & Destructors


        #region Properties
        public string BranchNameTitle => _branch.BranchName;
        #endregion Properties
    }
}
