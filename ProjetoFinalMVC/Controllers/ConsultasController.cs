using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoFinalMVC.Services;
using ProjetoFinalMVC.Models;
using ProjetoFinalMVC.Models.ViewModels;
using System.Diagnostics;
using ProjetoFinalMVC.Services.Exceptions;

namespace ProjetoFinalMVC.Controllers
{
    public class ConsultasController : Controller
    {
        private readonly ConsultasService _consultasService;
        private readonly DoutorService _doutorService;

        public ConsultasController(ConsultasService consultasService, DoutorService doutorService)
        {
            _consultasService = consultasService;
            _doutorService = doutorService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> BuscaSimples(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)//caso o usuário não digite nenhuma data específica, a busca começa no primeira dia do ano e vai até o dia atual
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var resultado = await _consultasService.EncontrarDataAsync(minDate, maxDate);
            return View(resultado);
        }

        public async Task<IActionResult> BuscaGrupo(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");


            var resultado = await _consultasService.EncontrarGrupoAsync(minDate, maxDate);


            return View(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var doutores = await _doutorService.RetornaDoutoresAsync();
            var viewModel = new ConsultFormViewModel { Doutores = doutores };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Consulta consulta)
        {
            if (!ModelState.IsValid)// caso o cadastro não cumpra os requisitos, ele é acionado novamente
            {
                var doutores = await _doutorService.RetornaDoutoresAsync();
                var viewModel = new ConsultFormViewModel { consulta = consulta, Doutores = doutores };

                return View(viewModel);
            }

            await _consultasService.InserirConsultaAsync(consulta);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "O id não foi fornecido" });
            }

            var consulta = await _consultasService.EncontrarIdAsync(id.Value);

            if (consulta == null)
            {
                return RedirectToAction(nameof(Error), new { message = "O Id não foi encontrado" });
            }

            return View(consulta);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "O Id não foi fornecido" });
            }

            var consulta = await _consultasService.EncontrarIdAsync(id.Value);

            if (consulta == null)
            {
                return RedirectToAction(nameof(Error), new { message = "O Id não foi encontrado" });
            }

            List<Doutor> doutores = await _doutorService.RetornaDoutoresAsync();
            ConsultFormViewModel viewmodel = new ConsultFormViewModel { consulta = consulta, Doutores = doutores };

            return View(viewmodel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Consulta consulta)
        {
            if (!ModelState.IsValid)
            {
                var doutores = await _doutorService.RetornaDoutoresAsync();
                var viewModel = new ConsultFormViewModel { consulta = consulta, Doutores = doutores };

                return View(viewModel);
            }

            if (id != consulta.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Os Ids não correspondem" });
            }

            try
            {
                await _consultasService.AtualizarConsultaAsync(consulta);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "O Id não foi fornecido" });
            }

            var consulta = await _consultasService.EncontrarIdAsync(id.Value);

            if (consulta == null)
            {
                return RedirectToAction(nameof(Error), new { message = "O Id não foi encontrado" });
            }

            return View(consulta);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _consultasService.RemoverConsultaAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier// Comando do framework para pegar o id interno da requisição
            };

            return View(viewModel);
        }
    }
}