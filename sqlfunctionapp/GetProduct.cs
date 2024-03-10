using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace sqlfunctionapp
{
    public static class GetProduct
    {
        [FunctionName("GetProduct")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get",Route = null)] HttpRequest req,
            ILogger log)
        {
            SqlConnection conn = GetConnection();
            List<Product> _products = new List<Product>();
            string statement = "SELECT ProductID,ProductName,Quantity from Products";
            conn.Open();
            SqlCommand cmd = new SqlCommand(statement, conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };
                    _products.Add(product);
                }
            }
            return new OkObjectResult(_products);

        }
        private static SqlConnection GetConnection() {
            string connectionString = "Server=tcp:webappdbsrvr.database.windows.net,1433;Initial Catalog=webappdb;Persist Security Info=False;User ID=webappdbsrvr;Password=WebAppDbServer2024;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            return new SqlConnection(connectionString);
        }
    }
}
