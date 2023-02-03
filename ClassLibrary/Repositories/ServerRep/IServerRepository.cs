using ClassLibrary.Repositories.GenericRep;
using Discord_Copycat.Models;
using Discord_Copycat.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Repositories.ServerRep
{
    public interface IServerRepository: IGenericRepository<Server>
    {
        Task<Roles?> GetUserRole(Guid id, Guid userId);

        Task<Server?> GetWithUsers(Guid id);

        Task<Server?> GetWithChats(Guid id);
    }
}
