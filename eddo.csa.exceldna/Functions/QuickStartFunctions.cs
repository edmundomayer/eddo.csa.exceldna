using eddo.csa.exceldna.Interfaces;
using ExcelDna.Integration;

namespace eddo.csa.exceldna.Functions
{
    public class QuickStartFunctions
    {
        #region Fields
        private readonly IQuickStartService _quickStartService;
        #endregion Fields


        #region Constructors & Destructors
        public QuickStartFunctions(IQuickStartService quickStartService) => _quickStartService = quickStartService;
        #endregion Constructors & Destructors


        #region Excel Public Functions
        [ExcelFunction(Description = "Say hello to somebody")]
        public string SayHelloHosted([ExcelArgument(Name = "Name", Description = "The name to say hello to")] string name) => _quickStartService.SayHello(name);
        #endregion Excel Public Functions
    }
}
