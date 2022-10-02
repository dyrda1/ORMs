using ORMs.Domain.Entities;
using Z.Dapper.Plus;

namespace ORM.Dapper.Common
{
    public class MessengerContext : DapperPlusContext
    {
        public MessengerContext()
        {
            Entity<User>()
                .Table("users")
                .Key(x => x.Id, "id")
                .Map(x => x.Username, "username")
                .IgnoreOnMergeInsert(x => x.Id)
                .Output(x => x.Id, "id");
        }
    }
}
