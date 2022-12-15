using System.Text;
using System.Text.Json;

namespace gitinder
{
    internal class ApiController
    {
        private const string url = "http://localhost:80";
        public static async Task<IEnumerable<Repository>> GetPublicRepositories()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{url}/publicrepos");
            response.EnsureSuccessStatusCode();
            

            string responseString = await response.Content.ReadAsStringAsync();

            Console.WriteLine(responseString);
            Repository[] repositories = JsonSerializer.Deserialize<Repository[]>(responseString);
            var repoList = repositories.ToList();
            repoList.ForEach(r =>
             {
                 if (r.primaryLanguage == null)
                {
                    r.primaryLanguage = new Language() {  name = "None"};
                }
            });

            return repoList;
        }
        public static async Task AddRepository(Repository repository)
        {
            HttpClient client = new HttpClient();
            var stringContent = new StringContent(JsonSerializer.Serialize(repository), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"{url}/matches/add", stringContent);
            response.EnsureSuccessStatusCode();
            Console.WriteLine("Reaches end");
        }

        public static async Task RemoveRepository(Repository repository)
        {
            HttpClient client = new HttpClient();
            var stringContent = new StringContent(JsonSerializer.Serialize(repository), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{url}/matches/remove");
            request.Content = stringContent;
            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public static async Task<IEnumerable<Repository>> GetMatches()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{url}/matches");
            response.EnsureSuccessStatusCode();
            

            string responseString = await response.Content.ReadAsStringAsync();

            Console.WriteLine(responseString);
            Repository[] repositories = JsonSerializer.Deserialize<Repository[]>(responseString);

            var repoList = repositories.ToList();
            repoList.ForEach(r =>
             {
                 if (r.primaryLanguage == null)
                {
                    r.primaryLanguage = new Language() {  name = "None"};
                }
            });


            return repoList;

        }
    }
}
