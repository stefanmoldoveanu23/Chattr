using Discord_Copycat.Models.Base;

namespace Discord_Copycat.Models
{
    public class ThreadLog : BaseEntity
    {
        public String Message { get; set; } = "";

        public Guid SenderId { get; set; }
        public User Sender { get; set; }

        public Guid ThreadId { get; set; }
        public Thread Thread { get; set; }
    }
}
