using DataBaseEntity.Context;
using DataBaseEntity.Model;
using DataBaseEntity.Repository.Interface;
using DataBaseEntity.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseEntity.Repository
{
    public class LogradouroRepository : Repository<Logradouro>, ILogradouroRepository
    {
        public LogradouroRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<bool> AtualizarCampoAsync(int id, string nomeCampo, string Valor)
        {

            var result = await _context.Database.ExecuteSqlRawAsync("EXEC UpdateCamposLogradouro  @Id = {0}, @Campo = {1} ,@Valor= {2}", id, nomeCampo, Valor);
            return result == 1 ? true : false;
        }

        public async Task<PaginacaoLista<Logradouro>> ListaAsync(int pagina, int totalItens)
        {

            IQueryable<Logradouro> query = _context.Logradouros.AsQueryable();
            var result = await GerarListaPaginada.GetPagedListAsync(query,
                                                                             pagina,
                                                                             totalItens);
            return result;
        }

        public async Task<PaginacaoLista<Logradouro>> ListaPorClienteAsync(int idCliente, int pagina, int totalItens)
        {
           
            IQueryable<Logradouro> query = _context.Logradouros.Where(x => x.IdCliente == idCliente).AsQueryable();
            var result = await GerarListaPaginada.GetPagedListAsync(query,
                                                                             pagina,
                                                                             totalItens);
            return result;
        }

        public async Task<IList<Logradouro>> ListaPorClienteAsync(int idCliente)
        {

            return _context.Logradouros.Where(x => x.IdCliente == idCliente).ToList();
            
        }
    }
}
