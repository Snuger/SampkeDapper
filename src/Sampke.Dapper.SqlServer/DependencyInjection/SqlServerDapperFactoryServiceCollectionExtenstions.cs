using Sampke.Dapper.Abstract;
using System;
using System.Data.SqlClient;


namespace Microsoft.Extensions.DependencyInjection
{
    public static class SqlServerDapperFactoryServiceCollectionExtenstions
    {
        public static DapperClinet UseMysql(this DapperClinet clinet, Action<DapperClinet> action)
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
            clinet.Connection =new SqlConnection(clinet.NameOrConnectstring);
            return clinet;
        }
        
    }
}
