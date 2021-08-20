using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampke.Dapper.Test
{

    [TestClass]
    public  class OptionsMonitorTest
    { 


        [TestMethod]
        public void TestOptionsSetting() {
            ServiceCollection service = new ServiceCollection();
            service.Configure<SettingsOptions>(opt => { opt.Name = "张三"; opt.Version = "1.9"; });      
            service.Configure<SettingsOptions>("admin", opt => { opt.Name = "ct"; opt.Version = "1.1"; });
            var provider = service.BuildServiceProvider();
            var options = provider.GetRequiredService<IOptions<SettingsOptions>>();
            Assert.IsNotNull(options);
            Assert.IsTrue(options.Value.Name == "张三");
            var om=  provider.GetRequiredService<IOptionsMonitor<SettingsOptions>>();
            Assert.IsNotNull(om.Get("admin"));
            Assert.IsTrue(om.Get("admin").Name == "ct");

            var ot = provider.GetRequiredService<IOptionsSnapshot<SettingsOptions>>();
            Assert.IsNotNull(ot.Get("admin"));
            Assert.IsTrue(ot.Get("admin").Name == "ct");

        }
}


    public class SettingsOptions
    {
        public string Name { get; set; }

        public string Version { get; set; }

       
    }
}
