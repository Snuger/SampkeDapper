using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Dapper;
using DapperOnNorthwind.Web.Models;
using System.Net.Http;
using Sampke.Dapper.Abstract;

namespace DapperOnNorthwind.Web.Pages
{
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
}
