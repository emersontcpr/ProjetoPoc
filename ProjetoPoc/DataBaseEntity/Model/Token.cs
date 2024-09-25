using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseEntity.Model
{
    public class Token
    {
        public int Id { get; set; }               // Mapeia o campo 'Id'
        public string ValorToken { get; set; }    // Mapeia o campo 'Token'
        public DateTime DataCadastro { get; set; } // Mapeia o campo 'DataCadastro'
        public DateTime DataExpiracao { get; set; } // Mapeia o campo 'DataExpiracao'
    }

}
