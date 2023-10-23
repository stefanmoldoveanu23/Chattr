using ClassLibrary.Models.Base;
using ClassLibrary.Models.Enums;

namespace ClassLibrary.Models
{
    public class Chat : BaseEntity
    {
        public String Name { get; set; } = "General";
        public Roles Role { get; set; }

        public Guid ServerId { get; set; }
        public Server Server { get; set; }

        public ICollection<ChatLog> Logs { get; set; }
    }
}
