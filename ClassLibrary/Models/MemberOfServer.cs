using ClassLibrary.Models.Base;
using ClassLibrary.Models.Enums;

namespace ClassLibrary.Models
{
    public class MemberOfServer
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid ServerId { get; set; }
        public Server Server { get; set; }

        public Roles Role { get; set; }
    }
}
