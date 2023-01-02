using Discord_Copycat.Models.Base;
using Discord_Copycat.Models.Enums;

namespace Discord_Copycat.Models
{
    public class Thread : BaseEntity
    {
        public String Name { get; set; } = "General";
        public Roles Role { get; set; }

        public Guid ServerId { get; set; }
        public Server Server { get; set; }

        public ICollection<ThreadLog> Log { get; set; }
    }
}
