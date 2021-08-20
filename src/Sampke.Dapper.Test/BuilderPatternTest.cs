using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sampke.Dapper.Test.CarBuilderFactoryServiceCollectionExtensions;

namespace Sampke.Dapper.Test
{
    [TestClass]
    public class BuilderPatternTest
    {
        [TestMethod]
        public void TestOne() { 
        
            IServiceCollection services = new ServiceCollection();           
            services.AddCar("瑞虎七", Options =>
            {
                Options.AT = "百里杨";
                Options.Brand = "奇瑞7";
                Options.Engine = "1.5T QTEXDT";
                Options.Chassis = "奇瑞";
            });

            services.AddCar("瑞虎八", Options =>
            {
                Options.AT = "百里杨";
                Options.Brand = "奇瑞八";
                Options.Engine = "1.5T QTEXDT";
                Options.Chassis = "奇瑞";
            });

            services.AddCar("红旗", opt => opt.UssHongQi(at => { at.Brand = "红旗"; at.Chassis = "德国"; at.AT = "无极变速"; }));
            var provider = services.BuildServiceProvider();
            var factory= provider.GetRequiredService<ICarFactory>();
            var TigerEight = factory.CreateCar("瑞虎八");
            Assert.IsTrue(TigerEight != null);
            var TigerSevenOne = factory.CreateCar("瑞虎七");
            Assert.IsTrue(TigerSevenOne != null);
            var TigerSevenTwo = factory.CreateCar("瑞虎七");
            Assert.IsTrue(TigerSevenTwo != null);
            Assert.IsTrue(TigerSevenOne?.GetHashCode() != TigerSevenTwo?.GetHashCode());
            var hongQiOne = factory.CreateCar("红旗");
            Assert.IsTrue(hongQiOne != null);
            Assert.IsTrue(hongQiOne.AT == "无极变速");
        }

    }


    public class Car {

        public Car()
        {

        }

        public string Brand { get; set; }

        public string AT { get; set; }

        public string Engine { get; set; }

        public string Chassis { get; set; }

    }


    public interface ICarBuilder {
      
        string Name { get; }
    
        IServiceCollection Services { get; }
    }


    public class DefaultCarBuilder: ICarBuilder
    {
        public DefaultCarBuilder(IServiceCollection services, string name)
        {
            Name = name;
            Services = services;
        }

        public string Name { get; set; }


        public IServiceCollection Services { get; set; }

    }


    public static class CarBuilderExtensions
    {
        public static ICarBuilder ConfigureCar(this ICarBuilder builder, Action<Car> configureCar) { 
            if(builder == null)            
                throw new ArgumentNullException(nameof(builder));
            if (configureCar==null)             
                throw new ArgumentNullException(nameof(configureCar));
            builder.Services.Configure<CarFactoryOptions>(builder.Name,options=>options.CarActions.Add(configureCar));   
            return builder;
        }
    }



    public static class CarBuilderFactoryServiceCollectionExtensions {

        public static IServiceCollection AddCar(this IServiceCollection services) {
            if (services == null)            
                throw new ArgumentNullException(nameof(services));                  
            services.AddOptions();
            services.TryAddSingleton<DefaultCarFactory>();
            services.TryAddSingleton<ICarFactory>(serviceProvider => serviceProvider.GetRequiredService<DefaultCarFactory>());
            services.TryAddTransient(s =>
            {
                return s.GetRequiredService<ICarFactory>().CreateCar(string.Empty);
            });
            return services;
        }


       public static ICarBuilder AddCar(this IServiceCollection services, string name, Action<Car> configureCar) {
            if(services == null)
                 throw new ArgumentNullException(nameof(services));
            if(string.IsNullOrWhiteSpace(name))
                 throw new ArgumentNullException(nameof(name));
            if(configureCar == null)
                throw new ArgumentNullException(nameof(configureCar));
            AddCar(services);
            var builder = new DefaultCarBuilder(services, name);
            builder.ConfigureCar(configureCar);
            return builder;
        }


       public interface ICarFactory
        {

           Car CreateCar(string name);

        }


        public class DefaultCarFactory : ICarFactory
        {
            private readonly IOptionsMonitor<CarFactoryOptions> _optionsMonitor;

            private readonly IServiceProvider _services;

            public DefaultCarFactory(IOptionsMonitor<CarFactoryOptions> optionsMonitor, IServiceProvider services)
            {
                _optionsMonitor = optionsMonitor;
                _services = services;
            }

            public Car CreateCar(string name)
            { 
                var car = new Car();
                CarFactoryOptions options = _optionsMonitor.Get(name);
                for (int i = 0; i < options.CarActions.Count; i++)
                {
                    options.CarActions[i](car);
                }
                return car;
            }
        }


        public class CarFactoryOptions
        {
            public IList<Action<Car>> CarActions { get; } = new List<Action<Car>>();

        }      

    }

    public static class HongQiCarExtentions
    {
        public static Car UssHongQi(this Car car, Action<Car> options)
        {
            if (options is null)
                throw new ArgumentNullException(nameof(options));
            options.Invoke(car);
            return car;
        }

    }

}
