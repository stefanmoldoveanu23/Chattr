﻿using ClassLibrary.Repositories.GenericRep;
using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Repositories.ChatRep
{
    public interface IChatRepository: IGenericRepository<Chat>
    {
        Task<Chat?> GetWithLogs(Guid id);

        Task<Chat?> GetWithUsers(Guid id);
    }
}
