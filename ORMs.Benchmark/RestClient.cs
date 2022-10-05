using ORMs.Benchmark.Benchmarks;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ORMs.Benchmark
{
    public class RestClient
    {
        private static readonly HttpClient client = new ();

        public async Task<IEnumerable<User>> GetGetUsersWithMessagesAdoNet()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return await client.GetFromJsonAsync<IEnumerable<User>>($"https://localhost:44379/api/ado-net/user/all");
        }

        public async Task<IEnumerable<User>> GetGetUsersWithMessagesDapper()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return await client.GetFromJsonAsync<IEnumerable<User>>($"https://localhost:44379/api/dapper/user/all");
        }

        public async Task CreateUserAdoNet(User user)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var userJson = JsonContent.Create(user);

            await client.PostAsync($"https://localhost:44379/api/ado-net/user", userJson);
        }

        public async Task CreateUserDapper(User user)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var userJson = JsonContent.Create(user);

            await client.PostAsync($"https://localhost:44379/api/dapper/user", userJson);
        }

        public async Task<IEnumerable<User>> GetWhereUsernameLikeAdoNet(string username)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return await client.GetFromJsonAsync<IEnumerable<User>>($"https://localhost:44379/api/ado-net/user?username={username}");
        }

        public async Task<IEnumerable<User>> GetWhereUsernameLikeDapper(string username)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return await client.GetFromJsonAsync< IEnumerable<User>>($"https://localhost:44379/api/dapper/user?username={username}");
        }
    }
}
