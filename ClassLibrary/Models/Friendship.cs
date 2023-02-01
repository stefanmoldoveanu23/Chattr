using Discord_Copycat.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Discord_Copycat.Models
{
    public class Friendship : BaseEntity
    {

        public Guid? User1Id { get; set; }
        public User User1 { get; set; }

        public Guid? User2Id { get; set;}
        public User User2 { get; set; }

        public ICollection<FriendLog> Logs { get; set; }
    }
}
