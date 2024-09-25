using ApiTesteBanco.Dto;
using DataBaseEntity.Model;

namespace ApiTesteBanco.Service.Inerfaces
{
    public interface ILogradouroService
    {
        Task<RetornoLista<Logradouro>> ListaAsync(GetPorPaginacao pagin);
        Task<RetornoLista<Logradouro>> ListaPorClienteAsync(LougradouroPorClienteGetPorPaginacao pagina);
        Task<RetornoObjeto> GetAsync(int id);
        Task<RetornoApi> DeletarAsync(int id);
        Task<RetornoApi> AtualizarAsync(LogradouroDto dto);
        Task<RetornoApi> GravarAsync(LogradouroDto dto);

    }
}
