using eddo.csa.exceldna.hosting;
using eddo.csa.exceldna.Interfaces;
using ExcelDna.Integration.CustomUI;
using ExcelDna.Logging;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices;

namespace eddo.csa.exceldna.Ribbons
{
    [ComVisible( true )]
    public class QuickStartRibbon : HostedExcelRibbon
    {
        #region Fields
        private readonly IQuickStartService _quickStartService;
        private readonly ILogger<QuickStartRibbon> _logger;
        #endregion Fields


        #region Constructors & Destructors
        public QuickStartRibbon( IQuickStartService quickStartService, ILogger<QuickStartRibbon> logger )
        {
            _quickStartService = quickStartService;
            _logger = logger;
        }
        #endregion Constructors & Destructors


        #region Events
        public void OnButtonPressed( IRibbonControl control ) => _logger.LogInformation( _quickStartService.SayHello( control.Tag ) );

        public void OnLogDisplay( IRibbonControl control ) => LogDisplay.Show();
        #endregion Events


        #region Implements Abstract Class HostedExcelRibbon
        public override string GetCustomUI( string ribbonId )
        {
            return @"
<customUI xmlns='http://schemas.microsoft.com/office/2006/01/customui'>
  <ribbon>
    <tabs>
      <tab id='tab1' label='Quick Start'>
        <group id='group1' label='Hosting'>
          <button id='button1' label='Say Hello' tag='Ribbon' onAction='OnButtonPressed'/>
          <button id='button2' label='Show Log Display' onAction='OnLogDisplay'/>
        </group >
      </tab>
    </tabs>
  </ribbon>
</customUI>
";
        }
        #endregion Implements Abstract Class HostedExcelRibbon
    }
}
