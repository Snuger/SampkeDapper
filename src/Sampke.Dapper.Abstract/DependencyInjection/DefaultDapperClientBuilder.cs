using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampke.Dapper.Abstract.DependencyInjection
{
    public sealed class DefaultDapperClientBuilder : IDapperClientBuilder
    {
        public DefaultDapperClientBuilder(string name, IServiceCollection services)
        {
            Name = name;
            Services = services;
        }

        public string Name { get; }

        public IServiceCollection Services { get; }
    }
}
