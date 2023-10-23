using ClassLibrary.Models.Base;
using System.ComponentModel.Design;

namespace ClassLibrary.Models
{
    public class Server : BaseEntity
    {
        public String Name { get; set; } = "";

        public String Description { get; set; } = "";

        public ICollection<MemberOfServer> Users { get; set; }

        public ICollection<Chat> Chats { get; set; }
    }
}
