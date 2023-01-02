using Discord_Copycat.Models.Base;

namespace Discord_Copycat.Models
{
    public class MemberOfServer
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid ServerId { get; set; }
        public Server Server { get; set; }
    }
}
