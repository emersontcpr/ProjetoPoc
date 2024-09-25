using DataBaseEntity.Context;
using DataBaseEntity.Model;
using DataBaseEntity.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseEntity.Repository
{
    public class TokenRepository : Repository<Token>, ITokenRepository
    {
        public TokenRepository(AppDbContext context) : base(context)
        {
        }
    }
}
