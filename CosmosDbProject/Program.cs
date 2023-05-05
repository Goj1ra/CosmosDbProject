using Microsoft.Azure.Cosmos;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CosmosDbProject
{
    class Program
    {
        static async Task Main()
        {
            CosmosDbConnection db = new CosmosDbConnection();
            string command = "";
            do
            {
                Console.WriteLine("Write the CRUD command: ");
                command = Console.ReadLine();
                switch (command.ToLower().TrimEnd().TrimStart())
                {

                    case "create":
                        await db.CreateUserAsync();
                        break;
                    case "delete":
                        await db.DeleteUserAsync();
                        break;
                    case "update":
                        await db.UpdateUserAsync();
                        break;
                    case "query":
                        await db.GetAppsAsync();
                        break;
                }
            }
            while (command.ToLower().TrimEnd().TrimStart() != "stop");
        }
    }
}
