using ClassLibrary.Repositories.GenericRep;
using Discord_Copycat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Repositories.ChatRep
{
    internal interface IChatRepository: IGenericRepository<Chat>
    {
        Task<Chat> GetWithLogs(Guid id);

        Task<Chat> GetWithUsers(Guid id);
    }
}
