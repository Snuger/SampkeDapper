using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampke.Dapper.Abstract
{
    public class DefaultDapperClientFactory : IDapperClientFactory
    {

        private readonly IServiceProvider _services;

        private readonly IOptionsMonitor<DapperClientFactoryOptions> _optionsMonitor;

        public DefaultDapperClientFactory(IServiceProvider services, IOptionsMonitor<DapperClientFactoryOptions> optionsMonitor)
        {
            _services = services;
            _optionsMonitor = optionsMonitor;
        }


        public DapperClinet CreateClinet(string name)
        {
            if(string.IsNullOrEmpty(name))
                throw new ArgumentNullException(name);    

          DapperClinet clinet = new DapperClinet();
          DapperClientFactoryOptions options= _optionsMonitor.Get(name);
          for(int i = 0; i < options.DapperClientActions.Count; i++)
          {
                options.DapperClientActions[i](clinet);
          }
            return clinet;
        }
    }
}
