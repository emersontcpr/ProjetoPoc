using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseEntity.Model
{
    public class Cliente
    {
        public int Id { get; set; }               // Mapeia o campo 'Id'
        public string Nome { get; set; }          // Mapeia o campo 'Nome'
        public string Email { get; set; }         // Mapeia o campo 'Email'
        public byte[] Logotipo { get; set; }      // Mapeia o campo 'Logotipo'

        // Relacionamento com a tabela Logradouro (1 cliente pode ter vários logradouros)
        public ICollection<Logradouro> Logradouros { get; set; }
    }
}
