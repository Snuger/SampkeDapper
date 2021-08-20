using Sampke.Dapper.Abstract;
using System;
using Oracle.ManagedDataAccess.Client;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class OracleDapperFactoryServiceCollectionExtenstions
    {
        public static DapperClinet UseOracle(this DapperClinet clinet, Action<DapperClinet> action)
        {
            if (clinet is null)
            {
                throw new ArgumentNullException(nameof(clinet));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            action.Invoke(clinet);
            clinet.Connection =new OracleConnection(clinet.NameOrConnectstring);
            return clinet;
        }
        
    }
}
