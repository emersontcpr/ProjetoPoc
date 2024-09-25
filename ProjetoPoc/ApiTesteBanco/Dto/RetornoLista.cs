using DataBaseEntity.Util;

namespace ApiTesteBanco.Dto
{
    public class RetornoLista<T> : RetornoApi
    {
        public List<T> Items { get; set; } = new List<T>();
        public int TotalItems { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }

        public RetornoLista(List<T> itens, int totalItems, int pageNumber, int pageSize, int totalPages,int codigo, string mensagem ){
            this.Items = itens;
            this.TotalItems = totalItems;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.TotalPages = totalPages;
            this.Codigo = codigo;
            this.Mensagem = mensagem;
        }
        public RetornoLista( int codigo, string mensagem)
        {
            this.Codigo = codigo;
            this.Mensagem = mensagem;
        }

        public RetornoLista()
        {
        }
    }
}
