using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection serviceCollecton,
            ICoreModule[] coreModules) 
        {
            foreach (var module in coreModules)
            {
                module.Load(serviceCollecton);
            }
            return ServiceTool.Create(serviceCollecton);
        }
    }
}
