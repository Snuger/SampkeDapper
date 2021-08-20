using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampke.Dapper.Abstract
{
    public class DapperClinet : IDisposable
    {
        bool _disposed;
        public DapperClinet()
        {
            Dispose(false);
        }

        public string NameOrConnectstring { get; set; }

        public IDbConnection Connection { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return; 
            if (disposing)
            {
                if(Connection != null&& Connection.State == ConnectionState.Open)                
                    Connection.Close();                
                Connection.Dispose();
            }
            _disposed = true;
        }


    }
}
