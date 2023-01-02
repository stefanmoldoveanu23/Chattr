using Discord_Copycat.Models.Base;
using Discord_Copycat.Models.Enums;

namespace Discord_Copycat.Models
{
    public class User : BaseEntity
    {
        public Roles Role { get; set; }
        public String Username { get; set; } = "";
        public String Password { get; set; } = "";
        public String Email { get; set; } = "";

        public ICollection<MemberOfServer> Servers { get; set; }

        public ICollection<Friendship> Friends { get; set; }

        public ICollection<FriendLog> FriendMessages { get; set; }
        public ICollection<ThreadLog> ThreadMessages { get; set; }
    }
}
