using eddo.csa.exceldna.hosting.Interfaces;
using ExcelDna.Registration;
using Microsoft.Extensions.Hosting;

namespace eddo.csa.exceldna.hosting
{
    internal class ExcelFunctionsRegistrator : IHostedService
    {
        #region Internals
        private readonly IExcelFunctionsProvider _excelFunctionsProvider;
        private readonly IEnumerable<IExcelFunctionsProcessor> _excelFunctionsProcessors;
        #endregion Internals


        #region Constructors & Destructors
        public ExcelFunctionsRegistrator( IExcelFunctionsProvider excelFunctionsProvider, IEnumerable<IExcelFunctionsProcessor> excelFunctionsProcessors )
        {
            _excelFunctionsProvider = excelFunctionsProvider;
            _excelFunctionsProcessors = excelFunctionsProcessors;
        }
        #endregion Constructors & Destructors


        #region Properties
        internal Action<IEnumerable<ExcelFunctionRegistration>> RegisterFunctions { get; set; } = functions => functions.RegisterFunctions();
        #endregion Properties


        #region Implements Interface IHostedService
        public Task StartAsync( CancellationToken cancellationToken )
        {
            var functions = _excelFunctionsProcessors.Aggregate(
                _excelFunctionsProvider.GetExcelFunctions(),
                ( current, excelFunctionsProcessor ) => excelFunctionsProcessor.Process( current ) );
            RegisterFunctions( functions );
            return Task.CompletedTask;
        }

        public Task StopAsync( CancellationToken cancellationToken ) => Task.CompletedTask;
        #endregion Implements Interface IHostedService
    }
}
