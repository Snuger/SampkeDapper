using Sampke.Dapper.Abstract;
using System;
using Npgsql;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class PostgresqlDapperFactoryServiceCollectionExtenstions
    {
        public static DapperClinet UsePostgresql(this DapperClinet clinet, Action<DapperClinet> action)
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
            clinet.Connection =new NpgsqlConnection(clinet.NameOrConnectstring);
            return clinet;
        }
        
    }
}
