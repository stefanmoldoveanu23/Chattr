using ClassLibrary.Repositories.GenericRep;
using Discord_Copycat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Repositories.ServerRep
{
    internal interface IServerRepository: IGenericRepository<Server>
    {
        Task<Server> GetWithUsers(Guid id);

        Task<Server> GetWithChats(Guid id);
    }
}
