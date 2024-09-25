using DataBaseEntity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseEntity.Repository.Interface
{
  public interface ILogradouroRepository : IRepository<Logradouro>
    {
        Task<bool> AtualizarCampoAsync(int id, string nomeCampo, string dado);
        Task<Util.PaginacaoLista<Logradouro>> ListaAsync(int pagina, int itemPorPagina);
        Task<Util.PaginacaoLista<Logradouro>> ListaPorClienteAsync(int idCliente, int pagina, int itemPorPagina);
        Task<IList<Logradouro>> ListaPorClienteAsync(int idCliente);
    }
}
