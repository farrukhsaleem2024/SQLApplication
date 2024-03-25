//using Microsoft.FeatureManagement;
using System.Text.Json;
using MySql.Data.MySqlClient;
using MySQLApp.Models;


namespace MySQLApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration _configuration;
        //private readonly IFeatureManager _featureManager;
        //private static string db_source = "trainingazuredb.database.windows.net";
        //private static string db_user = "trainingazuredb";
        //private static string db_password = "Tr@iningAzuredb";
        //private static string db_database = "TrainingAzureDB";
        public ProductService(IConfiguration configuration)//, IFeatureManager featureManager)
        {
            _configuration = configuration;
             //_featureManager = featureManager;
        }

        //public async Task<bool> IsBeta()
        //{
        //    if (await _featureManager.IsEnabledAsync("beta"))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //private MySqlConnection GetConnection()
        //{
            //var _builder = new MySqlConnectionStringBuilder();
            //_builder.Database = "";
            //_builder.UserID = db_user;
            //_builder.Password = db_password;
            //_builder. = db_database;
            //return new MySqlConnection(_builder.ConnectionString);
            // return new SqlConnection(_configuration.GetConnectionString("ConnectionString"));
            //return new MySqlConnection(_configuration["ConnectionString"]);
       // }
        public List<Product> GetProducts()
        //public async Task<List<Product>> GetProducts()
        {
            //    string FunctionURL = "https://myappfn.azurewebsites.net/api/GetProducts?code=eLquOHTPcvE---JcQltMFwbs9ux7vQbx80lPb6I-AjLuAzFu9dq1vg==";

            //    using (HttpClient client = new HttpClient())
            //    {
            //        HttpResponseMessage response = await client.GetAsync(FunctionURL);
            //        string content = await response.Content.ReadAsStringAsync();
            //        return JsonSerializer.Deserialize<List<Product>>(content);
            //    }
            //MySqlConnection conn = GetConnection();
            MySqlConnection conn = new MySqlConnection("Server='mysqldbsrvr.mysql.database.azure.com';UserID ='adminusr';Password='Lnxvmpswd2024';Database='appdb';");
            List<Product> _products = new List<Product>();
            string statement = "SELECT ProductID,ProductName,Quantity from Products";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(statement, conn);

            using (MySqlDataReader reader = cmd.ExecuteReader())
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
            return _products;
        }
    }
}
