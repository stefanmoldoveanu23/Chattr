using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Helpers.UnitOfWork
{
    internal interface IUnitOfWork: IDisposable
    {
        Task SaveAsync();
    }
}
