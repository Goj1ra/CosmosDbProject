
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.IO;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace CosmosDbProject
{
    public class CosmosDbConnection
    {
        private static string _connectionString;
        private static CosmosClient _client;
        private static Database _database;
        private static Container _container;

        public CosmosDbConnection()
        {
            //config = new ConfigurationBuilder()
            //     .AddJsonFile("Config.json")
            //     .AddEnvironmentVariables()
            //     .Build();
            _connectionString = "AccountEndpoint=https://4a93a0be-0ee0-4-231-b9ee.documents.azure.com:443/;AccountKey=U7BfTZPkEudrA5fgtXHJox05LeC4yu53C8ZdOL77jsdLzyYzMbwF20Ek6GVG10yQ6rpC1YPj321VACDbJPDgjQ==";/*config.GetSection("ConnectionString").Value*/
            _client = new CosmosClient(_connectionString);
            _database = _client.GetDatabase("TestDatabase");
            _container = _database.GetContainer("TestDatabase");
        }

        #region Create Operation

        public async Task CreateUserAsync()            
        {
            var user = new 
            {           
                id = Guid.NewGuid().ToString(),
                name = "Sergei",
                surname = "Cooleer",
                email = "stas_cool@2003",
                password = "stas2003"
            };
            Console.WriteLine("Adding user...");
            var result = await _container.CreateItemAsync(user);
            Console.WriteLine(result.StatusCode);
            Console.ReadLine();
        }
        #endregion

        #region Update Operation

        public async Task UpdateUserAsync()
        {
            var user = new User
            {
                Id = "4e41c183-f684-4074-9a02-db9693b2453b",
                Name = "Artemio",
                Surname = "Cooler",
                Email = "artemio_cooler@2004",
                Password = "artemio2004"
            };
            Console.WriteLine("Updaiting user...");
            var result = await _container.ReplaceItemAsync(user, user.Id);
            Console.WriteLine(result.StatusCode);
            Console.ReadLine();
        }
        #endregion

        #region Delete Operation

        public async Task DeleteUserAsync()
        {
            try
            {
                Console.WriteLine("Deleting user...");
                var result = await _container.DeleteItemAsync<dynamic>("910e04a2-e896-4a95-bc21-bb67dd77b975", new PartitionKey("910e04a2-e896-4a95-bc21-bb67dd77b975"));
                Console.WriteLine(result.StatusCode);
                Console.ReadLine();
            }
            catch(Exception exp)
            {
                Console.WriteLine(exp);
            }
        }
        #endregion

        #region Query

        public async Task GetAppsAsync()
        {
            string query = "SELECT * FROM c";

            var usersIterator = _container.GetItemQueryIterator<dynamic>(query);
            var users = await usersIterator.ReadNextAsync();
            foreach( var user in users)
            {
                Console.WriteLine("Querying users...");
                Console.WriteLine($"{user.name} - {user.surname} - {user.email} - {user.password}");
            }
            Console.ReadLine();
        }
        #endregion
    }
}
