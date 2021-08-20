# SampkeDapper

#### 基本使用方法

~~~C#


public void ConfigureServices(IServiceCollection services)
{
	services.AddDapperClient("default", opt => opt.UseMysql(config => config.NameOrConnectstring = Configuration.GetConnectionString("default")));
  
}



public class IndexModel : PageModel
{
	private readonly ILogger<IndexModel> _logger;

	private readonly IDbConnection _connection; 

	public IEnumerable<Customer> Customers { get; set; }

	public IndexModel(ILogger<IndexModel> logger, IDapperClientFactory factory)
	{
		_logger = logger;
		_connection = factory.CreateClinet("default")?.Connection;
	}

	public void OnGet()
	{
		Customers = _connection.Query<Customer>($"select * from customers limit {new Random().Next(1,100)}");         
	}
}


~~~