using DataBaseEntity.Context;
using DataBaseEntity.Model;
using DataBaseEntity.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseEntity.Repository
{
    public class LoginRepository : Repository<Login>, ILoginRepository
    {
        public LoginRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> IsLoginValidoAsync(string username, string password)
        {
            return await _context.Logins.AnyAsync(c => c.NomeLogin    == username
                            && c.Senha == password  && c.StatusUsuario==true);

        }
    }
}
