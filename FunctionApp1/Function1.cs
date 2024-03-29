using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace FunctionApp1
{
    public class Function1
    {
        
        private static SqlConnection GetConnection()
        {
            //string connectionString = Environment.GetEnvironmentVariable("SQLAZURECONNSTR_SQLConnectionString");
            string connectionString = "Server=tcp:webappdbsrvr.database.windows.net,1433;Initial Catalog=webappdb;Persist Security Info=False;User ID=webappdbsrvr;Password=WebAppDbServer2024;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            return new SqlConnection(connectionString);

        }       

        [Function("GetNewProducts")]
        public static async Task<IActionResult> RunProducts(
           [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
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
            //return new OkObjectResult(_products);
            return new OkObjectResult(JsonConvert.SerializeObject(_products));
        }
        [Function("GetProduct")]
        public static async Task<IActionResult> RunProduct(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            int productId = int.Parse(req.Query["id"]);
            SqlConnection conn = GetConnection();
            List<Product> _products = new List<Product>();
            string statement = String.Format("SELECT ProductID,ProductName,Quantity from Products WHERE ProductID={0}", productId);
            conn.Open();

            SqlCommand cmd = new SqlCommand(statement, conn);

            Product product = new Product();

            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    product.ProductID = reader.GetInt32(0);
                    product.ProductName = reader.GetString(1);
                    product.Quantity = reader.GetInt32(2);
                    var response = product;
                    return new OkObjectResult(response);
                }
            }
            catch (Exception ex)
            {
                var response = "No Response found";
                conn.Close();
                return new OkObjectResult(response);
            }
        }
    }
}
