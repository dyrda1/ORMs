using ORMs.Benchmark.Benchmarks;
using System;
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

        public async Task<IEnumerable<User>> GetGetUsersWithMessagesEntityFramework()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return await client.GetFromJsonAsync<IEnumerable<User>>($"https://localhost:44379/api/entity-framework/user/all");
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

            return await client.GetFromJsonAsync<IEnumerable<User>>($"https://localhost:44379/api/dapper/user?username={username}");
        }

        public async Task<IEnumerable<User>> GetWhereUsernameLikeEntityFramework(string username)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return await client.GetFromJsonAsync<IEnumerable<User>>($"https://localhost:44379/api/entity-framework/user?username={username}");
        }

        public async Task<User> GetUserAdoNet(Guid id)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return await client.GetFromJsonAsync<User>($"https://localhost:44379/api/ado-net/user/{id}");
        }

        public async Task<User> GetUserDapper(Guid id)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return await client.GetFromJsonAsync<User>($"https://localhost:44379/api/dapper/user/{id}");
        }

        public async Task<User> GetUserEntityFramework(Guid id)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return await client.GetFromJsonAsync<User>($"https://localhost:44379/api/entity-framework/user/{id}");
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

        public async Task CreateUserEntityFramework(User user)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var userJson = JsonContent.Create(user);

            await client.PostAsync($"https://localhost:44379/api/entity-framework/user", userJson);
        }

        public async Task CreateUsersAdoNet(List<User> users)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var userJson = JsonContent.Create(users);

            await client.PostAsync($"https://localhost:44379/api/ado-net/user/some", userJson);
        }

        public async Task CreateUsersDapper(List<User> users)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var userJson = JsonContent.Create(users);

            await client.PostAsync($"https://localhost:44379/api/dapper/user/some", userJson);
        }

        public async Task CreateUsersEntityFramework(List<User> users)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var userJson = JsonContent.Create(users);

            await client.PostAsync($"https://localhost:44379/api/entity-framework/user/some", userJson);
        }

        public async Task UpdateUserAdoNet(User user)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var userJson = JsonContent.Create(user);

            await client.PutAsync($"https://localhost:44379/api/ado-net/user", userJson);
        }

        public async Task UpdateUserDapper(User user)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var userJson = JsonContent.Create(user);

            await client.PutAsync($"https://localhost:44379/api/dapper/user", userJson);
        }

        public async Task UpdateUserEntityFramework(User user)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var userJson = JsonContent.Create(user);

            await client.PutAsync($"https://localhost:44379/api/entity-framework/user", userJson);
        }

        public async Task UpdateUsersAdoNet(List<User> users)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var userJson = JsonContent.Create(users);

            await client.PutAsync($"https://localhost:44379/api/ado-net/user/some", userJson);
        }

        public async Task UpdateUsersDapper(List<User> users)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var userJson = JsonContent.Create(users);

            await client.PutAsync($"https://localhost:44379/api/dapper/user/some", userJson);
        }

        public async Task UpdateUsersEntityFramework(List<User> users)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var userJson = JsonContent.Create(users);

            await client.PutAsync($"https://localhost:44379/api/entity-framework/user/some", userJson);
        }

        public async Task DeleteUserAdoNet(Guid id)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            await client.DeleteAsync($"https://localhost:44379/api/ado-net/user/{id}");
        }

        public async Task DeleteUserDapper(Guid id)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            await client.DeleteAsync($"https://localhost:44379/api/dapper/user/{id}");
        }
        public async Task DeleteUserEntityFramework(Guid id)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            await client.DeleteAsync($"https://localhost:44379/api/entity-framework/user/{id}");
        }
    }
}
