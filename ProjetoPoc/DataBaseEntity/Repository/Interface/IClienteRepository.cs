using DataBaseEntity.Model;
using DataBaseEntity.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseEntity.Repository.Interface
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<bool> AtualizarCampoAsync(int id, string nomeCampo, string dado);
        Task<bool> IsEmailJaCadastradoAsync(string Email, int id = 0);
        Task<PaginacaoLista<Cliente>> ListaAsync(int pagina, int totalItens);
    }
}
