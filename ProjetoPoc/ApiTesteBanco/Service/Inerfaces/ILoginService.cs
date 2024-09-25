using ApiTesteBanco.Dto;

namespace ApiTesteBanco.Service.Inerfaces
{
    public interface ILoginService
    {
        Task<string> LogarApiAsync(Login login);
    }
}
