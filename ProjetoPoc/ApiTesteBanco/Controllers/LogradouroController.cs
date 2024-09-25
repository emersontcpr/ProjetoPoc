using ApiTesteBanco.Dto;
using ApiTesteBanco.Dto.Enum;
using ApiTesteBanco.Service;
using ApiTesteBanco.Service.Inerfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiTesteBanco.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class LogradouroController : ControllerBase
    {
        private readonly ILogradouroService _logradouroService;

        public LogradouroController(ILogradouroService logradouroService)
        {
            _logradouroService = logradouroService;
        }

        [HttpPost("PorCliente")]
        public async Task<IActionResult> GetLogradouroPorClienteAsync([FromBody] LougradouroPorClienteGetPorPaginacao pagina)
        {
            var retorno = await _logradouroService.ListaPorClienteAsync(pagina);
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
        [HttpGet]
        public async Task<IActionResult> GetLogradouroAsync(GetPorPaginacao pagina)
        {
            var retorno = await _logradouroService.ListaAsync(pagina);

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
        public async Task<IActionResult> GetProdutosAsync([FromBody]  int id)
        {
            var retorno = await _logradouroService.GetAsync(id);
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
        public async Task<IActionResult> UpdateProdutoAsync([FromBody] LogradouroDto dto)
        {
            var retorno = await _logradouroService.AtualizarAsync(dto);
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
            var retorno = await _logradouroService.DeletarAsync(id);
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
        public async Task<IActionResult> CreateProdutoAsync([FromBody] LogradouroDto dto)
        {
            var retorno = await _logradouroService.GravarAsync(dto);
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
