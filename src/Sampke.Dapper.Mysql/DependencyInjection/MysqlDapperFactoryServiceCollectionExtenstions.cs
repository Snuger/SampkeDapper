using Sampke.Dapper.Abstract;
using Sampke.Dapper.Abstract.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MysqlDapperFactoryServiceCollectionExtenstions
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
            clinet.Connection =new MySqlConnection(clinet.NameOrConnectstring);
            return clinet;
        }
        
    }
}
