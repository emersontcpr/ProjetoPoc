using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoWeb.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O Nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O Email é obrigatório")]
        public string Email { get; set; }

        [NotMapped]
        [Display(Name = "Logotipo")]
        [FileExtensions(Extensions = "jpg,jpeg,png,gif,ico", ErrorMessage = "Somente arquivos de imagem (jpg, jpeg, png, gif, ico) são permitidos.")]
        public IFormFile LogotipoFile { get; set; }

        public byte[] Logotipo { get; set; }

        public List<Logradouro> Logradouros { get; set; } = new List<Logradouro>();

    }
  


}

