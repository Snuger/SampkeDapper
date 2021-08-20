using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampke.Dapper.Abstract.DependencyInjection
{
    public interface IDapperClientBuilderOptions
    {    
       public IDapperClientBuilder Build();
       
    }
}
