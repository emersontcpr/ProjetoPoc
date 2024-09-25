using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseEntity.Model
{
    public class Login
    {
        public int Id { get; set; }               // Mapeia o campo 'Id'
        public string NomeLogin { get; set; }     // Mapeia o campo 'Login'
        public string Senha { get; set; }         // Mapeia o campo 'Senha'
        public bool StatusUsuario { get; set; }   // Mapeia o campo 'StatusUsuario' (bit para boolean)
    }

}
