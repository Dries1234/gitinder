using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace gitinder
{
    internal class ApiController
    {
        public static async Task<Repository[]> GetPublicRepositories()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://localhost:49153/PublicRepo");

            

            string responseString = await response.Content.ReadAsStringAsync();

            Console.WriteLine(responseString);
            Repository[] repositories = JsonSerializer.Deserialize<Repository[]>(responseString);

            return repositories;
        }

    }
}
