using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using System.Data;
using Sampke.Dapper.Abstract;
using Dapper;

namespace Sampke.Dapper.Test
{

    [TestClass]
    public class MysqlExtenstionsTest
    {
        [TestMethod]
        public void MysqlInitTestOne() {
             var service =new ServiceCollection();
            service.AddDapperClient("default", opt => opt.UseMysql(conf => { conf.NameOrConnectstring = "server=172.22.110.198;userid=root;password=admin;database=northwind;Charset=utf8;Allow Zero Datetime=True; Pooling=true; Max Pool Size=512;sslmode=none;Allow User Variables=True;"; }));
            var provider = service.BuildServiceProvider();
            var factory = provider.GetRequiredService<IDapperClientFactory>();
            var client= factory.CreateClinet("default");
            Assert.IsNotNull(client);
            Assert.IsNotNull(client.Connection);
            var Customers = client.Connection.Query<Customer>("select * from customers limit 10");
            Assert.IsTrue(Customers.Count() > 0);

        }

        [TestMethod]
        public void MysqlInitTestTwo()
        {
            var service = new ServiceCollection();
            service.AddDapperClient("default", opt => opt.UseMysql(conf => { conf.NameOrConnectstring = "server=172.22.110.198;userid=root;password=admin;database=northwind;Charset=utf8;Allow Zero Datetime=True; Pooling=true; Max Pool Size=512;sslmode=none;Allow User Variables=True;"; }));
            var provider = service.BuildServiceProvider();
            var factory = provider.GetRequiredService<IDapperClientFactory>();
            for(var i = 0;i<10000;i++)
            {
                var client = factory.CreateClinet("default");
                Assert.IsNotNull(client);
                Assert.IsNotNull(client.Connection);
                var Customers = client.Connection.Query<Customer>("select * from customers limit 10");
                Assert.IsTrue(Customers.Count() > 0);
            }
          

        }

        public class Customer
        {
            //id, company, last_name, first_name, email_address, job_title, business_phone, home_phone, mobile_phone, fax_number, address, city, state_province, zip_postal_code, country_region, web_page, notes, attachments
            public int Id { get; set; }

            public string Company { get; set; }

            public string Last_Name { get; set; }


            public string First_Name { get; set; }

            public string Email_Address { get; set; }
        }
    }

}
