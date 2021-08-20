using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sampke.Dapper.Abstract;
using Sampke.Dapper.Abstract.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public  static class DapperFactoryServiceCollectionExtenstions
    {
        public static IServiceCollection AddDapperClient(this IServiceCollection services)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));
            services.TryAddSingleton<DefaultDapperClientFactory>();
            services.TryAddSingleton<IDapperClientFactory>(serviceProvider=>serviceProvider.GetRequiredService<DefaultDapperClientFactory>());
            services.TryAddTransient(s =>
            {
                return s.GetRequiredService<IDapperClientFactory>().CreateClinet(string.Empty);
            });
            return services;
        }


        public static IDapperClientBuilder AddDapperClient(this IServiceCollection services, string name, Action<DapperClinet> action)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            AddDapperClient(services);
            var builder = new DefaultDapperClientBuilder(name, services);
            builder.Services.Configure<DapperClientFactoryOptions>(name, options => options.DapperClientActions.Add(action));
            return builder;
        }
    }
}
