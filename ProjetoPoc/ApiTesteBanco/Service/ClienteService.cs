using ApiTesteBanco.Dto;
using ApiTesteBanco.Dto.Enum;
using ApiTesteBanco.Service.Inerfaces;
using ApiTesteBanco.Settings;
using DataBaseEntity.Model;
using DataBaseEntity.Repository;
using DataBaseEntity.Repository.Interface;
using Microsoft.Extensions.Options;
using System.Runtime.Serialization.Formatters;

namespace ApiTesteBanco.Service
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ILogradouroRepository _logradouroRepository;

        public ClienteService(IClienteRepository clienteRepository, ILogradouroRepository logradouroRepository)
        {
            _clienteRepository = clienteRepository;
            _logradouroRepository = logradouroRepository;
        }

        public async Task<RetornoApi> AtualizarAsync(ClienteDto dto)
        {
            RetornoApi retonar = null;
            try
            {
                retonar = dto.Validadar(true);
                if (retonar == null)
                {
                    var cliente = await _clienteRepository.GetByIdAsync(dto.Id);
                    if (cliente != null)
                    {
                        if (!cliente.Nome.Equals(dto.Nome))
                            await _clienteRepository.AtualizarCampoAsync(dto.Id, "Nome", dto.Nome);
                        if (!cliente.Email.Equals(dto.Email))
                        {
                            if (await _clienteRepository.IsEmailJaCadastradoAsync(dto.Email,dto.Id))
                                retonar = new RetornoApi()
                                {
                                    Codigo = (int)EnumRetorno.FAIL,
                                    Mensagem = "Cliente não Gravado Email já Utilizado"
                                };
                            else
                            {
                                await _clienteRepository.AtualizarCampoAsync(dto.Id, "Email", dto.Email);

                            }
                        }
                        if (cliente.Logotipo.Length != dto.Logotipo.Length)
                        {
                            cliente.Logotipo = dto.Logotipo;
                            await _clienteRepository.SaveChangesAsync();
                        }
                        retonar = new RetornoApi()
                        {
                            Codigo = (int)EnumRetorno.OK,
                            Mensagem = "Atualizado com Sucesso"
                        };

                    }
                    else
                        retonar = new RetornoApi()
                        {
                            Codigo = (int)EnumRetorno.FAIL,
                            Mensagem = "Não foi encontrado registro"
                        };
                }
            }
            catch (Exception ex)
            {
                retonar = new RetornoApi()
                {
                    Codigo = (int)EnumRetorno.ERROR,
                    Mensagem = ex.Message
                };

            }
            return retonar;
        }

        public async Task<RetornoApi> DeletarAsync(int id)
        {
            RetornoApi retonar = null;
            try
            {
                if (id == 0)
                    retonar = new RetornoObjeto()
                    {
                        Codigo = (int)EnumRetorno.FAIL,
                        Mensagem = "Id Cliente Inválido"
                    };
                else
                {
                    var info = await _clienteRepository.GetByIdAsync(id);
                    if (info == null)
                        retonar = new RetornoObjeto()
                        {
                            Codigo = (int)EnumRetorno.FAIL,
                            Mensagem = "Cliente não encontrado"
                        };
                    else
                    {
                        var listaLogadouro = await _logradouroRepository.ListaPorClienteAsync(info.Id);
                        if(listaLogadouro == null || listaLogadouro.Count() ==0)
                        {
                            _clienteRepository.Delete(info);
                            await _clienteRepository.SaveChangesAsync();

                            retonar = new RetornoObjeto()
                            {
                                Codigo = (int)EnumRetorno.OK,
                                Mensagem = "Cliente deletado com sucesso"
                            };
                        }else
                            retonar = new RetornoObjeto()
                            {
                                Codigo = (int)EnumRetorno.FAIL,
                                Mensagem = "Cliente Tem Lougradouros não pode ser Deletado!"
                            };

                    }

                }

            }
            catch (Exception ex)
            {
                retonar = new RetornoApi()
                {
                    Codigo = (int)EnumRetorno.ERROR,
                    Mensagem = ex.Message
                };

            }
            return retonar;
        }

        public async Task<RetornoObjeto> GetAsync(int id)
        {
            RetornoObjeto retonar = null;
            try
            {
                if (id == 0)
                    retonar = new RetornoObjeto()
                    {
                        Codigo = (int)EnumRetorno.FAIL,
                        Mensagem = "Id Cliente Inválido"
                    };
                else
                {
                    var info = await _clienteRepository.GetByIdAsync(id);
                    if (info == null)
                        retonar = new RetornoObjeto()
                        {
                            Codigo = (int)EnumRetorno.FAIL,
                            Mensagem = "Cliente não encontrado"
                        };
                    else
                        retonar = new RetornoObjeto()
                        {
                            Codigo = (int)EnumRetorno.OK,
                            Mensagem = "Cliente encontrado com sucesso",
                            resultado = info
                        };
                }

            }
            catch (Exception ex)
            {
                retonar = new RetornoObjeto()
                {
                    Codigo = (int)EnumRetorno.ERROR,
                    Mensagem = ex.Message
                };

            }
            return retonar;
        }

        public async Task<RetornoObjeto> GravarAsync(ClienteDto dto)
        {
            RetornoObjeto retonar = null;
            try
            {
                var result = dto.Validadar();

                if(result != null)
                {
                    retonar = new RetornoObjeto()
                    {
                        Codigo = result.Codigo,
                        Mensagem = result.Mensagem
                    };
                }
                if (retonar == null)
                {
                    if (await _clienteRepository.IsEmailJaCadastradoAsync(dto.Email))
                        retonar = new RetornoObjeto()
                        {
                            Codigo = (int)EnumRetorno.FAIL,
                            Mensagem = "Cliente não Gravado Email já Utilizado"
                        };
                    else
                    {
                        var info = new Cliente()
                        {
                            Email = dto.Email,
                            Logotipo = dto.Logotipo,
                            Nome = dto.Nome,
                        };
                        await _clienteRepository.AddAsync(info);
                        await _clienteRepository.SaveChangesAsync();

                        retonar = new RetornoObjeto()
                        {
                            Codigo = (int)EnumRetorno.OK,
                            resultado = info.Id,
                            Mensagem = "Gravação concluída com Sucesso"
                        };
                    }


                }
            }
            catch (Exception ex)
            {
                retonar = new RetornoObjeto()
                {
                    Codigo = (int)EnumRetorno.ERROR,
                    Mensagem = ex.Message
                };

            }
            return  retonar;
        }

        public async Task<RetornoLista<Cliente>> ListaAsync(GetPorPaginacao pagina)
        {
            RetornoLista<Cliente> retonar = null;
            try
            {
                retonar = (RetornoLista<Cliente>)pagina.Validadar();
                if (retonar == null)
                {
                    var retornoConsulta = await _clienteRepository.ListaAsync(pagina.Pagina, pagina.ItemPorPagina);

                    if (retornoConsulta.Items == null || retornoConsulta.Items.Count == 0)
                        retonar = new RetornoLista<Cliente>(
                              (int)EnumRetorno.FAIL,
                              "Não há dados"
                        );
                    else
                    {

                        return new RetornoLista<Cliente>(
                             retornoConsulta.Items,
                             retornoConsulta.TotalPages,
                             retornoConsulta.PageNumber,
                             retornoConsulta.PageSize,
                             retornoConsulta.TotalPages,
                             (int)EnumRetorno.OK,
                            null
                        );


                    }

                }


            }
            catch (Exception ex)
            {
                retonar = new RetornoLista<Cliente>(
                      (int)EnumRetorno.ERROR,
                      ex.Message
                );

            }
            return retonar;

        }
    }
}
