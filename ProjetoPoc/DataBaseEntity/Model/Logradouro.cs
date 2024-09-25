using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseEntity.Model
{
    public class Logradouro
    {
        public int Id { get; set; }               // Mapeia o campo 'Id'
        public string Descricao { get; set; }     // Mapeia o campo 'Descricao'

        // Chave estrangeira
        public int IdCliente { get; set; }       // Mapeia o campo 'Id_Cliente'
        public Cliente Cliente { get; set; }      // Referência à entidade 'Cliente'
    }

}
