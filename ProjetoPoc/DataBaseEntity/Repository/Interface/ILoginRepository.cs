using DataBaseEntity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseEntity.Repository.Interface
{
    public interface   ILoginRepository : IRepository<Login>
    {
        Task<bool> IsLoginValidoAsync(string username, string password);
    }
}
