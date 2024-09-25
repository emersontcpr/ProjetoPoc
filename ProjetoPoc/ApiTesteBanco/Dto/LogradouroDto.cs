using ApiTesteBanco.Dto.Enum;

namespace ApiTesteBanco.Dto
{
    public class LogradouroDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int IdCliente { get; set; }

        public RetornoApi Validadar(bool IsAtualizar = false)
        {
            if (IsAtualizar && this.Id == 0)
                return new RetornoApi()
                {
                    Codigo = (int)EnumRetorno.FAIL,
                    Mensagem = "Id Inválido"
                };
            if (string.IsNullOrWhiteSpace(this.Descricao))
                return new RetornoApi()
                {
                    Codigo = (int)EnumRetorno.FAIL,
                    Mensagem = "Nome Cliente Vazio"
                };

            if (this.IdCliente == 0)
                return new RetornoApi()
                {
                    Codigo = (int)EnumRetorno.FAIL,
                    Mensagem = "Id Cliente Inválido"
                };
            return null;
        }


    }
}
