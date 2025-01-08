using ExcelDna.Integration;
using Microsoft.Extensions.Hosting;

namespace eddo.csa.exceldna.hosting
{
    internal class ExcelRibbonLoader : IHostedService
    {
        #region Internals
        private readonly IEnumerable<HostedExcelRibbon> _excelRibbons;
        #endregion Internals


        #region Constructors & Destructors
        public ExcelRibbonLoader( IEnumerable<HostedExcelRibbon> excelRibbons )
        {
            _excelRibbons = excelRibbons;
        }
        #endregion Constructors & Destructors


        #region Properties
        internal Action<ExcelComAddIn> LoadComAddIn { get; set; } = ExcelComAddInHelper.LoadComAddIn;
        #endregion Properties


        #region Implements Interface IHostedService
        public Task StartAsync( CancellationToken cancellationToken )
        {
            foreach( HostedExcelRibbon excelRibbon in _excelRibbons )
            {
                LoadComAddIn( excelRibbon );
            }

            return Task.CompletedTask;
        }

        public Task StopAsync( CancellationToken cancellationToken ) => Task.CompletedTask;
        #endregion Implements Interface IHostedService
    }
}
