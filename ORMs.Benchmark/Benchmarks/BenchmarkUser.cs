using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ORMs.Benchmark.Benchmarks
{
    [HtmlExporter]
    public class BenchmarkUser
    {
        [Params(100, 200)]
        public int IterationCount;

        private readonly RestClient _restClient = new ();

        //[Benchmark]
        //public async Task RestGetUsersWithMessagesDapper()
        //{
        //    for (int i = 0; i < IterationCount; i++)
        //    {
        //        await _restClient.GetGetUsersWithMessagesDapper();
        //    }
        //}

        //[Benchmark]
        //public async Task RestGetUsersWithMessagesAdoNet()
        //{
        //    for (int i = 0; i < IterationCount; i++)
        //    {
        //        await _restClient.GetGetUsersWithMessagesAdoNet();
        //    }
        //}

        //[Benchmark]
        //public async Task RestCreateUserAdoNet()
        //{
        //    for (int i = 0; i < IterationCount; i++)
        //    {
        //        await _restClient.CreateUserAdoNet(new User { Username = $"user{Guid.NewGuid()}@gmail.com" });
        //    }
        //}

        //[Benchmark]
        //public async Task RestCreateUserDapper()
        //{
        //    for (int i = 0; i < IterationCount; i++)
        //    {
        //        await _restClient.CreateUserDapper(new User { Username = $"user{Guid.NewGuid()}@gmail.com" });
        //    }
        //}

        [Benchmark]
        public async Task GetWhereUsernameLikeDapper()
        {
            for (int i = 0; i < IterationCount; i++)
            {
                await _restClient.GetWhereUsernameLikeDapper("@");
            }
        }

        [Benchmark]
        public async Task GetWhereUsernameLikeAdoNet()
        {
            for (int i = 0; i < IterationCount; i++)
            {
                await _restClient.GetWhereUsernameLikeAdoNet("@");
            }
        }
    }

    public class User
    {
        public User()
        {
            UserFolders = new List<UserFolder>();
            Folders = new List<Folder>();
        }

        public Guid Id { get; set; }
        public string Username { get; set; }

        public ICollection<UserFolder> UserFolders { get; set; }
        public ICollection<Folder> Folders { get; set; }
    }

    public class UserFolder
    {
        public Guid UserId { get; set; }
        public Guid FolderId { get; set; }

        public User User { get; set; }
        public Folder Folder { get; set; }
    }

    public class Folder
    {
        public Folder()
        {
            UserFolders = new List<UserFolder>();
            Messages = new List<Message>();
            Users = new List<User>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<UserFolder> UserFolders { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<User> Users { get; set; }
    }

    public class Message
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid FolderId { get; set; }

        public Folder Folder { get; set; }
    }
}
