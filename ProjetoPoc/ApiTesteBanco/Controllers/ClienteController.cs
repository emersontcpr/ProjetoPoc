using ApiTesteBanco.Dto;
using ApiTesteBanco.Dto.Enum;
using ApiTesteBanco.Service.Inerfaces;
using DataBaseEntity.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiTesteBanco.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }


        [HttpPost("Listar")]
        public async Task<IActionResult> GetListaClienteAsync(GetPorPaginacao pagina)
        {
            var retorno = await _clienteService.ListaAsync(pagina);

            switch ((EnumRetorno)retorno.Codigo)
            {
                case EnumRetorno.OK:
                    return Ok(retorno);
                    break;
                case EnumRetorno.FAIL:
                    return BadRequest(retorno);
                    break;
                case EnumRetorno.ERROR:
                    return StatusCode(500, retorno);
                    break;
                default:
                    return StatusCode(500, "Processo não concluído");
            };

        }
        [HttpGet("Obter")]
        public async Task<IActionResult> GetClienteAsync([FromBody] int id)
        {
            var retorno = await _clienteService.GetAsync(id);

            switch ((EnumRetorno)retorno.Codigo)
            {
                case EnumRetorno.OK:
                    return Ok(retorno);
                    break;
                case EnumRetorno.FAIL:
                    return BadRequest(retorno);
                    break;
                case EnumRetorno.ERROR:
                    return StatusCode(500, retorno);
                    break;
                default:
                    return StatusCode(500, "Processo não concluído");
            };

            return Ok(retorno);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProdutoAsync([FromBody] ClienteDto dto)
        {
            var retorno = await _clienteService.AtualizarAsync(dto);
            switch ((EnumRetorno)retorno.Codigo)
            {
                case EnumRetorno.OK:
                    return Ok(retorno);
                    break;
                case EnumRetorno.FAIL:
                    return BadRequest(retorno);
                    break;
                case EnumRetorno.ERROR:
                    return StatusCode(500, retorno);
                    break;
                default:
                    return StatusCode(500, "Processo não concluído");
            };

            return Ok(retorno);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteProdutoAsync([FromBody] int id)
        {
            var retorno = await _clienteService.DeletarAsync(id);
            switch ((EnumRetorno)retorno.Codigo)
            {
                case EnumRetorno.OK:
                    return Ok(retorno);
                    break;
                case EnumRetorno.FAIL:
                    return BadRequest(retorno);
                    break;
                case EnumRetorno.ERROR:
                    return StatusCode(500, retorno);
                    break;
                default:
                    return StatusCode(500, "Processo não concluído");
            };

            return Ok(retorno);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateProdutoAsync([FromBody] ClienteDto dto)
        {

            var retorno = await _clienteService.GravarAsync(dto);

            switch ((EnumRetorno)retorno.Codigo)    
            {
                case EnumRetorno.OK:
                    return Ok(retorno);
                    break;
                case EnumRetorno.FAIL:
                    return BadRequest(retorno);
                    break;
                case EnumRetorno.ERROR:
                    return StatusCode(500, retorno);
                    break;
                default:
                    return StatusCode(500, "Processo não concluído");
            };
            return Ok(retorno);
        }
    }
}
