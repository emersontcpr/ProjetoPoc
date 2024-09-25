using ApiTesteBanco.Dto.Enum;

namespace ApiTesteBanco.Dto
{
    public class GetPorPaginacao
    {
        public int Pagina { get; set; }
        public int ItemPorPagina { get; set; }

        public RetornoApi Validadar()
        {
            if (this.ItemPorPagina == 0) return new RetornoApi() {
                Codigo = (int)EnumRetorno.FAIL,
                Mensagem = "Não há Itens por Paginas"
            };

           
            if (this.Pagina == 0) return new RetornoApi()
            {
                Codigo = (int)EnumRetorno.FAIL,
                Mensagem = "Não há numero de Pagina"
            };
            return null;

        }
    }
}
