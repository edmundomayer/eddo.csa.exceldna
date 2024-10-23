﻿using eddo.csa.exceldna.hosting.Interfaces;
using ExcelDna.Registration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace eddo.csa.exceldna.hosting.Helpers
{
    public static class ServiceCollectionExtensions
    {
        #region Methods
        public static IServiceCollection AddExcelFunctions<T>(this IServiceCollection services)
            where T : class
        {
            services.AddTransient<IExcelFunctionsProvider, ExcelFunctionsProvider>();
            services.AddHostedService<ExcelFunctionsRegistrar>();

            services.AddSingleton<T>();
            services.TryAddEnumerable(
                ServiceDescriptor.Singleton<IExcelFunctionsDeclaration, ExcelFunctionsDeclaration<T>>());

            return services;
        }

        public static IServiceCollection AddExcelRibbon<T>(this IServiceCollection services)
            where T : HostedExcelRibbon
        {
            services.AddHostedService<ExcelRibbonLoader>();
            services.TryAddEnumerable(ServiceDescriptor.Singleton<HostedExcelRibbon, T>());
            return services;
        }

        [Obsolete("Please use AddExcelFunctionsProcessor<TProcessor>()")]
        public static IServiceCollection AddExcelFunctionsProcessor(this IServiceCollection services, Func<IEnumerable<ExcelFunctionRegistration>, IEnumerable<ExcelFunctionRegistration>> process)
        {
            return services.AddSingleton<IExcelFunctionsProcessor>(new ExcelFunctionsProcessor(process));
        }

        [Obsolete("Please use AddExcelFunctionsProcessor<TProcessor>()")]
        public static IServiceCollection AddExcelFunctionsProcessor(this IServiceCollection services, Func<IEnumerable<ExcelFunctionRegistration>, IServiceProvider, IEnumerable<ExcelFunctionRegistration>> process)
        {
            return services.AddSingleton<IExcelFunctionsProcessor>(provider => new ExcelFunctionsProcessor(functions => process(functions, provider)));
        }

        public static IServiceCollection AddExcelFunctionsProcessor<TProcessor>(this IServiceCollection services)
            where TProcessor : class, IExcelFunctionsProcessor
        {
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IExcelFunctionsProcessor, TProcessor>());
            return services;
        }
        #endregion Methods
    }
}
