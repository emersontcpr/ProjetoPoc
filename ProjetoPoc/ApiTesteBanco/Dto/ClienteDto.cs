using ApiTesteBanco.Dto.Enum;

namespace ApiTesteBanco.Dto
{
    public class ClienteDto
    {
        public int Id { get; set; }                
        public string Nome { get; set; }        
        public string Email { get; set; }         
        public byte[] Logotipo { get; set; }      

        public RetornoApi Validadar(bool IsAtualizar = false)
        {
            if (IsAtualizar && this.Id == 0)
                return new RetornoApi()
                {
                    Codigo = (int)EnumRetorno.FAIL,
                    Mensagem = "Id Cliente Inválido"
                };
            if (string.IsNullOrWhiteSpace(Nome))
                return new RetornoApi()
                {
                    Codigo = (int)EnumRetorno.FAIL,
                    Mensagem = "Nome Cliente Vazio"
                };

            if (string.IsNullOrWhiteSpace(Email))
                return new RetornoApi()
                {
                    Codigo = (int)EnumRetorno.FAIL,
                    Mensagem = "Email Vazio"
                };

            if (this.Logotipo == null || this.Logotipo.Length == 00)
                return new RetornoApi()
                {
                    Codigo = (int)EnumRetorno.FAIL,
                    Mensagem = "Logotipo Inválido"
                };
            return null;

        }
    }
}
