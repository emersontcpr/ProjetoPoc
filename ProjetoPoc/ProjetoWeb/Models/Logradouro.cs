using System.ComponentModel.DataAnnotations;

namespace ProjetoWeb.Models
{
    public class Logradouro
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O Descricao é obrigatório")]
        public string Descricao { get; set; }
        public int IdCliente { get; set; }
    }
}
