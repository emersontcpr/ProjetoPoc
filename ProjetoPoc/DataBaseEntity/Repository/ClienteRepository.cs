using DataBaseEntity.Context;
using DataBaseEntity.Model;
using DataBaseEntity.Repository.Interface;
using DataBaseEntity.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseEntity.Repository
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> AtualizarCampoAsync(int id, string nomeCampo, string Valor)
        {

            var result = await _context.Database.ExecuteSqlRawAsync("EXEC UpdateCamposCliente @Id = {0}, @Campo = {1} ,@Valor = {2}", id, nomeCampo, Valor);
            return result == 1 ? true : false;
        }

        public async Task<bool> IsEmailJaCadastradoAsync(string Email, int id = 0)
        {
            if (id == 0)
                return await _context.Clientes.AnyAsync(c => c.Email == Email);
            else
                return await _context.Clientes.AnyAsync(c => c.Email == Email && c.Id != id);
        }

        public async Task<PaginacaoLista<Cliente>> ListaAsync(int pagina, int totalItens)
        {
            IQueryable<Cliente> query = _context.Clientes.AsQueryable();
            var result = await GerarListaPaginada.GetPagedListAsync(query,
                                                                             pagina,
                                                                             totalItens);
            return result;
        }
    }
}
