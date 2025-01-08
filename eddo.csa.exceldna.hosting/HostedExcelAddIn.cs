using Autofac;
using ExcelDna.Integration;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace eddo.csa.exceldna.hosting
{
    public abstract class HostedExcelAddIn : IExcelAddIn
    {
        #region Internals
        private IHost _host;
        //private IContainer _container;
        #endregion Internals


        #region Methods
        protected virtual void AutoOpen( IHost host )
        //protected virtual void AutoOpen( IContainer container )
        { }

        protected virtual void AutoClose( IHost host )
        //protected virtual void AutoClose( IContainer container )
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

                //var builder = new ContainerBuilder();
                //_container = builder.Build();
                //AutoOpen( _container );
            }
            catch( Exception _error )
            {
                OnException( _error );
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

                //AutoClose( _container );
                //_container.Dispose();
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
