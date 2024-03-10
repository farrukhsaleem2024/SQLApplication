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
using System.Data;

namespace sqlfunctionapp
{
    public static class AddProduct
    {
        [FunctionName("AddProduct")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Product product = JsonConvert.DeserializeObject<Product>(requestBody);
            SqlConnection conn = GetConnection();
            conn.Open();
            string Statement = "INSERT INTO Products(ProductID,ProductName,Quantity) VALUES (@param1,@param2,@param3)";

            using (SqlCommand cmd = new SqlCommand(Statement,conn)){
                cmd.Parameters.Add("@param1",SqlDbType.Int).Value = product.ProductID;
                cmd.Parameters.Add("@param2", SqlDbType.VarChar).Value = product.ProductName;
                cmd.Parameters.Add("@param3", SqlDbType.Int).Value = product.Quantity;
                cmd.CommandType=CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            return new OkObjectResult("Product has been added");
        }
        private static SqlConnection GetConnection()
        {
            string connectionString = "Server=tcp:webappdbsrvr.database.windows.net,1433;Initial Catalog=webappdb;Persist Security Info=False;User ID=webappdbsrvr;Password=WebAppDbServer2024;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            return new SqlConnection(connectionString);
        }
    }
}
