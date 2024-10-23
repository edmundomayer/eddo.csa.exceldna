using ExcelDna.Integration;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace eddo.csa.exceldna.hosting
{
    public abstract class HostedExcelAddIn : IExcelAddIn
    {
        #region Internals
        private IHost _host;
        #endregion Internals


        #region Methods
        protected virtual void AutoOpen( IHost host )
        { }

        protected virtual void AutoClose( IHost host )
        { }

        protected virtual void OnException( Exception e )
        {
            Debug.WriteLine( e );
        }

        protected abstract IHostBuilder CreateHostBuilder();
        #endregion Methods


        #region Implements Interface IExcelAddIn
        void IExcelAddIn.AutoOpen()
        {
            try
            {
                _host = CreateHostBuilder().Build();
                _host.StartAsync().GetAwaiter().GetResult();
                AutoOpen( _host );
            }
            catch( Exception e )
            {
                OnException( e );
                throw;
            }
        }

        void IExcelAddIn.AutoClose()
        {
            try
            {
                AutoClose( _host );
                _host.StopAsync().GetAwaiter().GetResult();
                _host.Dispose();
            }
            catch( Exception e )
            {
                OnException( e );
                throw;
            }
        }
        #endregion Implements Interface IExcelAddIn
    }
}
