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
        //public async Task GetWhereUsernameLikeDapper()
        //{
        //    for (int i = 0; i < IterationCount; i++)
        //    {
        //        await _restClient.GetWhereUsernameLikeDapper("@");
        //    }
        //}

        //[Benchmark]
        //public async Task GetWhereUsernameLikeAdoNet()
        //{
        //    for (int i = 0; i < IterationCount; i++)
        //    {
        //        await _restClient.GetWhereUsernameLikeAdoNet("@");
        //    }
        //}

        //[Benchmark]
        //public async Task GetUserDapper()
        //{
        //    for (int i = 0; i < IterationCount; i++)
        //    {
        //        await _restClient.GetUserDapper(Guid.Parse("9979C25A-C566-4205-8435-8DD95CFF0CB7"));
        //    }
        //}

        //[Benchmark]
        //public async Task GetUserAdoNet()
        //{
        //    for (int i = 0; i < IterationCount; i++)
        //    {
        //        await _restClient.GetUserAdoNet(Guid.Parse("9979C25A-C566-4205-8435-8DD95CFF0CB7"));
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

        //[Benchmark]
        //public async Task RestCreateUsersAdoNet()
        //{
        //    for (int i = 0; i < IterationCount; i++)
        //    {
        //        await _restClient.CreateUsersAdoNet(new List<User> { new User { Username = $"user{Guid.NewGuid()}@gmail.com" } });
        //    }
        //}

        //[Benchmark]
        //public async Task RestCreateUsersDapper()
        //{
        //    for (int i = 0; i < IterationCount; i++)
        //    {
        //        await _restClient.CreateUsersDapper(new List<User> { new User { Username = $"user{Guid.NewGuid()}@gmail.com" } });
        //    }
        //}

        //[Benchmark]
        //public async Task RestUpdateUserAdoNet()
        //{
        //    for (int i = 0; i < IterationCount; i++)
        //    {
        //        await _restClient.UpdateUserAdoNet(new User { Id = Guid.Parse("623F22F4-17D5-48A2-9E68-63D620E3D809"), Username = $"user{Guid.NewGuid()}@gmail.com" });
        //    }
        //}

        //[Benchmark]
        //public async Task RestUpdateUserDapper()
        //{
        //    for (int i = 0; i < IterationCount; i++)
        //    {
        //        await _restClient.UpdateUserDapper(new User { Id = Guid.Parse("623F22F4-17D5-48A2-9E68-63D620E3D809"), Username = $"user{Guid.NewGuid()}@gmail.com" });
        //    }
        //}

        //[Benchmark]
        //public async Task RestUpdateUsersAdoNet()
        //{
        //    for (int i = 0; i < IterationCount; i++)
        //    {
        //        await _restClient.UpdateUsersAdoNet(new List<User> { new User { Id = Guid.Parse("623F22F4-17D5-48A2-9E68-63D620E3D809"), Username = $"user{Guid.NewGuid()}@gmail.com" } });
        //    }
        //}

        //[Benchmark]
        //public async Task RestUpdateUsersDapper()
        //{
        //    for (int i = 0; i < IterationCount; i++)
        //    {
        //        await _restClient.UpdateUsersDapper(new List<User> { new User { Id = Guid.Parse("623F22F4-17D5-48A2-9E68-63D620E3D809"), Username = $"user{Guid.NewGuid()}@gmail.com" } });
        //    }
        //}

        [Benchmark]
        public async Task ResDeleteUserAdoNet()
        {
            for (int i = 0; i < IterationCount; i++)
            {
                await _restClient.DeleteUserAdoNet(Guid.Parse("623F22F4-17D5-48A2-9E68-63D620E3D809"));
            }
        }

        [Benchmark]
        public async Task RestDeleteUserDapper()
        {
            for (int i = 0; i < IterationCount; i++)
            {
                await _restClient.DeleteUserDapper(Guid.Parse("623F22F4-17D5-48A2-9E68-63D620E3D809"));
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
