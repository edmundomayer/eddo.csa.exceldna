﻿using ExcelDna.Registration;
using Microsoft.Extensions.Hosting;

namespace eddo.csa.exceldna.hosting
{
    internal class ExcelFunctionsRegistrar : IHostedService
    {
        private readonly IExcelFunctionsProvider _excelFunctionsProvider;
        private readonly IEnumerable<IExcelFunctionsProcessor> _excelFunctionsProcessors;

        public ExcelFunctionsRegistrar( IExcelFunctionsProvider excelFunctionsProvider, IEnumerable<IExcelFunctionsProcessor> excelFunctionsProcessors )
        {
            _excelFunctionsProvider = excelFunctionsProvider;
            _excelFunctionsProcessors = excelFunctionsProcessors;
        }

        internal Action<IEnumerable<ExcelFunctionRegistration>> RegisterFunctions { get; set; } = functions => functions.RegisterFunctions();

        public Task StartAsync( CancellationToken cancellationToken )
        {
            var functions = _excelFunctionsProcessors.Aggregate(
                _excelFunctionsProvider.GetExcelFunctions(),
                ( current, excelFunctionsProcessor ) => excelFunctionsProcessor.Process( current ) );
            RegisterFunctions( functions );
            return Task.CompletedTask;
        }

        public Task StopAsync( CancellationToken cancellationToken ) => Task.CompletedTask;
    }
}
