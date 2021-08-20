using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Xml.Linq;

namespace Sampke.Dapper.Test {

    [TestClass]
    public class UnitTest1
    {  
        [TestMethod]
        public void TestMethod3() {        
           
            IServiceCollection services = new ServiceCollection(); 
            services.AddComputer("mac", opt => { opt.Cpu = "intel"; opt.Memory = "32G"; });
            services.AddComputer("lenove", opt => opt.UseAmd(() => new ComputerOptions() { Memory = "32G" }));
            var factoty= services.BuildServiceProvider().GetService<IComputerFactoty>();
            Assert.IsNotNull(factoty);
            var mac= factoty.Create("mac");
            Assert.IsNotNull(mac);
            var lenove = factoty.Create("lenove");
            Assert.IsNotNull(lenove); 
        }
    }



    public class Computer {

        private  string cup;

        private string memory;

        private string mainboard;

        private string displayCard;

        private int usbCount { get; set; }

        public Computer(ComputerOptions options) {

            this.cup = options.Cpu;
            this.memory = options.Memory;
        }       

    }


    public class ComputerOptions {

        public string Cpu { get; set; }

        public string Memory { get; set; }
    

        public Action<Func<ComputerOptions>> UseAmd = opt =>
        {              
            opt();
        };

    }

    public interface IComputerBuilder
    {
        public void SetComputerOptions(ComputerOptions opt);

        public Computer Builder();

    }


    public class ComputerBuilder: IComputerBuilder
    {              

        public ComputerOptions options { get; private set; }

        public ComputerBuilder()
        {   
            options=new ComputerOptions();
        }

        public void SetComputerOptions(ComputerOptions opt ) {
           options = opt;
        }

        public  Computer Builder() {
            return new Computer(this.options);        
        }  
    
    }


    public interface IComputerFactoty {
 
        public Computer Create(string name);    
    }

    public class ComputerFactoty : IComputerFactoty
    {

        public ComputerFactoty()
        {
            ComputerBuilders = new Dictionary<string, ComputerBuilder>();
        }

        public Dictionary<string, ComputerBuilder> ComputerBuilders { get;  private set; }

        public void AddComputerBuilder(string name, ComputerBuilder computer)
        {
            if (!ComputerBuilders.ContainsKey(name))
                ComputerBuilders.Add(name, computer);
        }

        public Computer Create(string name)
        {
            if (!ComputerBuilders.ContainsKey(name))
                throw new ArgumentNullException(nameof(name));
            return ComputerBuilders[name].Builder();
        }
    }


    public static class ComputerBuilderExtions {

        public static IServiceCollection AddComputer(this IServiceCollection services) {          
            return services;
        }

        public static IServiceCollection AddComputer(this IServiceCollection services, string name,Action<ComputerOptions> options) {
            AddComputer(services);
            var provider = services.BuildServiceProvider();
            if(provider == null) 
                throw new ArgumentNullException(nameof(provider));         
            services.TryAddSingleton<ComputerFactoty>();
            var factory= services.BuildServiceProvider().GetRequiredService<ComputerFactoty>();
            factory.AddComputerBuilder(name, new ComputerBuilder().Config(options));
            services.TryAddSingleton<IComputerFactoty>(factory);         
            return services;     
        }



        public static ComputerBuilder ConfigHardware(this ComputerBuilder builder, Func<ComputerOptions> options)
        {

            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            builder.SetComputerOptions(options());
            return builder;
        }

        public static ComputerBuilder Config(this ComputerBuilder builder, Action<ComputerOptions> opt)
        {
            if (opt is null)
                throw new ArgumentNullException(nameof(opt));
            opt.Invoke(builder.options);
            return builder;
        } 
      }

}
