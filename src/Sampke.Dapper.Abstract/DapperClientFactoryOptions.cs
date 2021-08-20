using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sampke.Dapper.Abstract
{
    public  class DapperClientFactoryOptions
    {
        public IList<Action<DapperClinet>> DapperClientActions { get; } = new List<Action<DapperClinet>>();

    }
}
