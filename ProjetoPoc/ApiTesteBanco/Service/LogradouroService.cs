using ApiTesteBanco.Dto;
using ApiTesteBanco.Dto.Enum;
using ApiTesteBanco.Service.Inerfaces;
using DataBaseEntity.Model;
using DataBaseEntity.Repository;
using DataBaseEntity.Repository.Interface;

namespace ApiTesteBanco.Service
{
    public class LogradouroService : ILogradouroService
    {
        private readonly ILogradouroRepository _logradouroRepository;

        public LogradouroService(ILogradouroRepository logradouroRepository)
        {
            _logradouroRepository = logradouroRepository;
        }

        public async Task<RetornoApi> AtualizarAsync(LogradouroDto dto)
        {

            RetornoApi retonar = null;
            try
            {
                retonar = dto.Validadar(true);
                if (retonar == null)
                {
                    var logradouro = await _logradouroRepository.GetByIdAsync(dto.Id);
                    if (logradouro != null)
                    {
                        if (!logradouro.Descricao.Equals(dto.Descricao))
                            await _logradouroRepository.AtualizarCampoAsync(dto.Id, "Descricao", dto.Descricao);

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
                        Mensagem = "Id Logradouro Inválido"
                    };
                else
                {
                    var info = await _logradouroRepository.GetByIdAsync(id);
                    if (info == null)
                        retonar = new RetornoObjeto()
                        {
                            Codigo = (int)EnumRetorno.FAIL,
                            Mensagem = "Logradouro não encontrado"
                        };
                    else
                    {
                        _logradouroRepository.Delete(info);
                        await _logradouroRepository.SaveChangesAsync();

                        retonar = new RetornoObjeto()
                        {
                            Codigo = (int)EnumRetorno.OK,
                            Mensagem = "Logradouro deletado com sucesso"
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
                        Mensagem = "Id Logradouro Inválido"
                    };
                else
                {
                    var info = await _logradouroRepository.GetByIdAsync(id);
                    if (info == null)
                        retonar = new RetornoObjeto()
                        {
                            Codigo = (int)EnumRetorno.FAIL,
                            Mensagem = "Logradouro não encontrado"
                        };
                    else
                        retonar = new RetornoObjeto()
                        {
                            Codigo = (int)EnumRetorno.OK,
                            Mensagem = "Logradouro encontrado com sucesso",
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

        public async Task<RetornoApi> GravarAsync(LogradouroDto dto)
        {
            RetornoApi retonar = null;
            try
            {
                retonar = dto.Validadar();
                if (retonar == null)
                {

                    var info = new Logradouro()
                    {
                        IdCliente = dto.IdCliente,
                        Descricao = dto.Descricao,
                    };
                    await _logradouroRepository.AddAsync(info);
                    await _logradouroRepository.SaveChangesAsync();

                    retonar = new RetornoApi()
                    {
                        Codigo = (int)EnumRetorno.OK,
                        Mensagem = "Gravação concluída com Sucesso"
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

        public async Task<RetornoLista<Logradouro>> ListaAsync(GetPorPaginacao pagina)
        {
            RetornoLista<Logradouro> retonar = null;
            try
            {
                var result = (RetornoLista<Logradouro>)pagina.Validadar();
              
                if (result != null)
                {
                    retonar = new RetornoLista<Logradouro>()
                    {
                        Codigo = result.Codigo,
                        Mensagem = result.Mensagem
                    };
                }


                if (retonar == null)
                {
                    var retornoConsulta = await _logradouroRepository.ListaAsync(pagina.Pagina, pagina.ItemPorPagina);

                    if (retornoConsulta.Items == null || retornoConsulta.Items.Count == 0)
                        retonar = new RetornoLista<Logradouro>(
                              (int)EnumRetorno.FAIL,
                              "Não há dados"
                        );
                    else
                    {

                        return new RetornoLista<Logradouro>(
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
                retonar = new RetornoLista<Logradouro>(
                      (int)EnumRetorno.ERROR,
                      ex.Message
                );

            }
            return retonar;
        }

        public async Task<RetornoLista<Logradouro>> ListaPorClienteAsync(LougradouroPorClienteGetPorPaginacao pagina)
        {
            RetornoLista<Logradouro> retonar = null;

            try
            {

             var    result =  pagina.Validadar();

                if (result != null)
                {
                    retonar = new RetornoLista<Logradouro>()
                    {
                        Codigo = result.Codigo,
                        Mensagem = result.Mensagem
                    };
                }

                if (retonar != null)
                    return retonar;


                result = pagina.ValidadarCliente();
                if (result != null)
                {
                    retonar = new RetornoLista<Logradouro>()
                    {
                        Codigo = result.Codigo,
                        Mensagem = result.Mensagem
                    };
                }

                if (retonar == null)
                {
                    var retornoConsulta = await _logradouroRepository.ListaPorClienteAsync(pagina.IdCliente, pagina.Pagina, pagina.ItemPorPagina);

                    if (retornoConsulta.Items == null || retornoConsulta.Items.Count == 0)
                        retonar = new RetornoLista<Logradouro>(
                              (int)EnumRetorno.FAIL,
                              "Não há dados"
                        );
                    else
                    {

                        return new RetornoLista<Logradouro>(
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
                retonar = new RetornoLista<Logradouro>(
                      (int)EnumRetorno.ERROR,
                      ex.Message
                );

            }
            return retonar;
        }
    }
}
