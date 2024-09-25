using ApiTesteBanco.Dto.Enum;

namespace ApiTesteBanco.Dto
{
    public class LougradouroPorClienteGetPorPaginacao : GetPorPaginacao
    {
        public int IdCliente { get; set; }

        public RetornoApi ValidadarCliente()
        {
            if (this.IdCliente == 0) return new RetornoApi()
            {
                Codigo = (int)EnumRetorno.FAIL,
                Mensagem = "Id Cliente Invalido"
            };
            return null;
        }
    }
}
