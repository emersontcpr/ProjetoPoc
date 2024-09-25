using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseEntity.Util
{
    public class GerarListaPaginada
    {
        public static  async Task<PaginacaoLista<T>> GetPagedListAsync<T>(IQueryable<T> source,int pageNumber,int pageSize) where T : class
        {
          
            var totalItems = await source.CountAsync();

            var items = await source.Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            return new PaginacaoLista<T>
            {
                Items = items,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
            };
        }
    }
}
