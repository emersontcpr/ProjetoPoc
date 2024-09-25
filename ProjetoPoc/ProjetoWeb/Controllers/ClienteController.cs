using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoWeb.ConsultaApi;
using ProjetoWeb.Models;
using ProjetoWeb.Models.Json;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json.Nodes;

namespace ProjetoWeb.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly WebApiPoc _apiClient;

        public ClienteController(ILogger<ClienteController> logger, WebApiPoc apiClient)
        {
            _logger = logger;
            _apiClient = apiClient;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var json = new JsonPaginacao()
            {
                ItemPorPagina = 100,
                Pagina = 1
            };

            var token = Request.Cookies["AuthToken"];
            _apiClient.SetBearerToken(token);
            var response = await _apiClient.PostAsync<JsonPaginacao, JsonResultadoLista<Cliente>>("api/Cliente/Listar", json);
            if (response.Data != null)
            {
                if (response.Data.Codigo == (int)EnumCodigoRetornoApi.OK)
                    return View("Index", response.Data.Items);
                ViewBag.ErrorMessage = response.Data.Mensagem;
            }
            ViewBag.ErrorMessage = response.ErrorMessage;
            return View(null);
        }
        [Authorize]
        public IActionResult Novo()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Novo([Bind("Id,Nome,Email,LogotipoFile,Logradouros")] Cliente model)
        {


            if (model.LogotipoFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await model.LogotipoFile.CopyToAsync(memoryStream);
                    model.Logotipo = memoryStream.ToArray();
                }
                var token = Request.Cookies["AuthToken"];
                _apiClient.SetBearerToken(token);
                var response = await _apiClient.PostAsync<Cliente, JsonResultadoIdCliente>("api/Cliente/Create", model);
                if (response.Data != null)
                {
                    if (response.Data.Codigo == (int)EnumCodigoRetornoApi.OK)
                    {
                        if (model.Logradouros != null && model.Logradouros.Count() > 0)
                            foreach (var item in model.Logradouros)
                            {
                                if (!string.IsNullOrEmpty(item.Descricao))
                                {
                                    item.IdCliente = response.Data.resultado.Value;
                                    var responseLogradouro = await _apiClient.PostAsync<Logradouro, RetornoApi>("api/Logradouro/Create", item);
                                    if (responseLogradouro.Data != null)
                                    {
                                        if (responseLogradouro.Data.Codigo == (int)EnumCodigoRetornoApi.OK)
                                            TempData["SucessoMessage"] = string.Concat(TempData["SucessoMessage"], responseLogradouro.Data.Mensagem, "-");
                                        else
                                            TempData["ErrorMessage"] = string.Concat(TempData["ErrorMessage"], responseLogradouro.Data.Mensagem, "-");
                                    }
                                }
                            }
                        TempData["SucessoMessage"] = response.Data.Mensagem;
                        return RedirectToAction(nameof(Index));

                    }
                    else
                        ViewBag.ErrorMessage = response.Data.Mensagem;
                }
                else

                    ViewBag.ErrorMessage = response.ErrorMessage;

            }


            return View(model);
        }


        [Authorize]
        public async Task<IActionResult> Editar(int id)
        {
            var model = new Cliente();
            var token = Request.Cookies["AuthToken"];
            _apiClient.SetBearerToken(token);
            var response = await _apiClient.GetAsync<JsonResultadoObjetoCliente>("api/Cliente/Obter", id);
            if (response.Data != null)
            {
                if (response.Data.Codigo == (int)EnumCodigoRetornoApi.OK)
                {
                    model.Id = response.Data.resultado.Id;
                    model.Nome = response.Data.resultado.Nome;
                    model.Email = response.Data.resultado.Email;
                    model.Logotipo = response.Data.resultado.Logotipo;

                    var json = new JsonConsultaListaLogradouro()
                    {
                        IdCliente = model.Id,
                        Pagina = 1,
                        ItemPorPagina = 100,
                    };
                    var responseLogradouro = await _apiClient.PostAsync<JsonConsultaListaLogradouro, JsonResultadoLista<Logradouro>>("api/Logradouro/PorCliente", json);
                    if (responseLogradouro.Data != null)
                    {
                        if (responseLogradouro.Data.Codigo == (int)EnumCodigoRetornoApi.OK)
                        {
                            model.Logradouros = responseLogradouro.Data.Items;
                        }
                    }


                }
                else
                    TempData["ErrorMessage"] = response.Data.Mensagem;

            }
            else
                TempData["ErrorMessage"] = response.ErrorMessage;

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditarSalvar([Bind("Id,Nome,Email,LogotipoFile,Logradouros")] Cliente model)
        {


            if (model.LogotipoFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await model.LogotipoFile.CopyToAsync(memoryStream);
                    model.Logotipo = memoryStream.ToArray();
                }
            }
            else
            {
                model.Logotipo = Convert.FromBase64String(Request.Form["Logotipo"]);
            }
            var token = Request.Cookies["AuthToken"];
            _apiClient.SetBearerToken(token);
            var response = await _apiClient.PutAsync<Cliente, RetornoApi>("api/Cliente/Update", model);
            if (response.Data != null)
            {
                if (response.Data.Codigo == (int)EnumCodigoRetornoApi.OK)
                {
                    if (model.Logradouros != null && model.Logradouros.Count() > 0)
                        foreach (var item in model.Logradouros)
                        {
                            if (!string.IsNullOrEmpty(item.Descricao))
                            {
                                ApiResponse<RetornoApi> responseLogradouro;
                                if (item.Id == 0)
                                {
                                    item.IdCliente = model.Id;
                                      responseLogradouro = await _apiClient.PostAsync<Logradouro, RetornoApi>("api/Logradouro/Create", item);
                                }
                                else
                                {
                                      responseLogradouro = await _apiClient.PutAsync<Logradouro, RetornoApi>("api/Logradouro/Update", item);
                                }
                            
                                if (responseLogradouro.Data != null)
                                {
                                    if (responseLogradouro.Data.Codigo == (int)EnumCodigoRetornoApi.OK)
                                        TempData["SucessoMessage"] = string.Concat(TempData["SucessoMessage"], responseLogradouro.Data.Mensagem, "-");
                                    else
                                        TempData["ErrorMessage"] = string.Concat(TempData["ErrorMessage"], responseLogradouro.Data.Mensagem, "-");
                                }

                            }

                        }
                    TempData["SucessoMessage"] = response.Data.Mensagem;
                    return RedirectToAction(nameof(Editar), new { id = model.Id });

                }
                else
                    TempData["ErrorMessage"] = response.Data.Mensagem;
            }
            else

                TempData["ErrorMessage"] = response.ErrorMessage;




            return RedirectToAction(nameof(Editar), new { id = model.Id });
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var token = Request.Cookies["AuthToken"];
            _apiClient.SetBearerToken(token);
            var response = await _apiClient.DeleteAsync<RetornoApi>("api/Cliente/Delete/", id);
            if (response.Data != null)
            {
                if (response.Data.Codigo == (int)EnumCodigoRetornoApi.OK)
                    TempData["SucessoMessage"] = response.Data.Mensagem;
                else
                    TempData["ErrorMessage"] = response.Data.Mensagem;

            }
            else
                TempData["ErrorMessage"] = response.ErrorMessage;


            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<JsonResult> DeleteLogradouro(int id)
        {
            var token = Request.Cookies["AuthToken"];
            _apiClient.SetBearerToken(token);
            var response = await _apiClient.DeleteAsync<RetornoApi>("api/Logradouro/Delete/", id);

            if (response.Data != null)
            {
                if (response.Data.Codigo == (int)EnumCodigoRetornoApi.OK)
                    return Json(new { success = true });
                else
                    return Json(new { success = false, message = response.ErrorMessage });

            }
            return Json(new { success = false, message = response.ErrorMessage });
        }
    }
}
