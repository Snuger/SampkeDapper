using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Sampke.Dapper.Abstract
{
    public abstract class DapperConnectionBuilder
    {
        public DapperConnectionBuilder(string connectionString)
        {
            Options =  new DapperClientFactoryOptions(connectionString);
        }

        public DapperConnectionBuilder(DapperClientFactoryOptions options)
        {
            Options = options;
        }

        public DapperClientFactoryOptions Options { get; }

       
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract IDbConnection Builder();


    }
}
