using Discord_Copycat.Models.Base;
using System.ComponentModel.Design;

namespace Discord_Copycat.Models
{
    public class Server : BaseEntity
    {
        public String Name { get; set; }

        public String Description { get; set; }

        public ICollection<MemberOfServer> Users { get; set; }

        public Guid CreatorId { get; set; }
        public User Creator { get; set; }

        public Server(Guid CreatorId, String Name = "", String Description = "")
        {
            this.Name = (Name == "" ? $"{CreatorId}'s server" : Name);
            this.Description = (Description == "" ? $"$Hi, this is {CreatorId}'s server" : Description);

            this.CreatorId = CreatorId;
        }
    }
}
