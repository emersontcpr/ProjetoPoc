using ApiTesteBanco.Dto;
using DataBaseEntity.Model;

namespace ApiTesteBanco.Service.Inerfaces
{
    public interface IClienteService
    {
        Task<RetornoLista<Cliente>> ListaAsync(GetPorPaginacao pagina);
        Task<RetornoObjeto> GetAsync(int id);
        Task<RetornoApi> DeletarAsync(int id);
        Task<RetornoApi> AtualizarAsync(ClienteDto dto);
        Task<RetornoObjeto> GravarAsync(ClienteDto dto);
    }
}
