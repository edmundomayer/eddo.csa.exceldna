using eddo.csa.exceldna.Interfaces;
using eddo.csa.exceldna.Settings;
using Microsoft.Extensions.Options;

namespace eddo.csa.exceldna.Services
{
    public class QuickStartService : IQuickStartService
    {
        #region Fields
        private IOptions<ContainerOptions> _optionsRegisteredByAddOptions;
        #endregion Fields


        #region Constructors & Destructors
        public QuickStartService( IOptions<ContainerOptions> optionsRegisteredByAddOptions )
        {
            _optionsRegisteredByAddOptions = optionsRegisteredByAddOptions;
        }
        #endregion Constructors & Destructors


        #region Implements Interface IQuickStartService
        public string SayHello( string name ) => $"From ctor {_optionsRegisteredByAddOptions.Value.Name} and Hello {name}!";
        #endregion Implements Interface IQuickStartService
    }
}
