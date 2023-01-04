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

        public Settings Settings { get; set; }

        public User()
        {
            FirstFriend = new List<Friendship>();
            SecondFriend = new List<Friendship>();
            Servers = new List<MemberOfServer>();
            Role = Roles.User;
        }

        public ICollection<MemberOfServer> Servers { get; set; }

        public ICollection<Friendship> FirstFriend { get; set; }
        public ICollection<Friendship> SecondFriend { get; set; }

        public ICollection<FriendLog> FriendMessages { get; set; }
        public ICollection<ChatLog> ThreadMessages { get; set; }
    }
}
