using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoFinalMVC.Services;
using ProjetoFinalMVC.Models;
using ProjetoFinalMVC.Models.ViewModels;
using ProjetoFinalMVC.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ProjetoFinalMVC.Controllers
{
    public class DoutoresController : Controller
    {
        private readonly DoutorService _doutorService;
        private readonly EspecializacaoService _especializacaoService;
        public DoutoresController(DoutorService doutorService, EspecializacaoService especializacaoService)
        {
            _doutorService = doutorService;
            _especializacaoService = especializacaoService;
        }
        public async Task<IActionResult> Index()
        {
            var listaDoutor = await _doutorService.RetornaDoutoresAsync();


            return View(listaDoutor);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var especializacoes = await _especializacaoService.RetornaEspecializacoesAsync();
            var viewModel = new DoutorFormViewModel { Especializacoes = especializacoes };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Doutor doutor)
        {
            if (!ModelState.IsValid) // caso o cadastro não cumpra os requisitos, ele é acionado novamente
            {
                var especializacoes = await _especializacaoService.RetornaEspecializacoesAsync();
                var viewModel = new DoutorFormViewModel { Doutor = doutor, Especializacoes = especializacoes };
                return View(viewModel);
            }

            await _doutorService.InserirDoutorAsync(doutor);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)// se cair na exceção, o programa vai para a tela de erro personalizada
            {
                return RedirectToAction(nameof(Error), new { message = "O Id não foi fornecido" });
            }

            var dr = await _doutorService.EncontrarIdAsync(id.Value);
            if (dr == null)
            {
                return RedirectToAction(nameof(Error), new { message = "O Id não foi encontrado" });
            }

            return View(dr);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _doutorService.RemoverDoutorAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "O Id não foi fornecido" });
            }

            var dr = await _doutorService.EncontrarIdAsync(id.Value);
            if (dr == null)
            {
                return RedirectToAction(nameof(Error), new { message = "O Id não foi encontrado" });
            }

            return View(dr);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "O Id não foi fornecido" });
            }

            var dr = await _doutorService.EncontrarIdAsync(id.Value);
            if (dr == null)
            {
                return RedirectToAction(nameof(Error), new { message = "O Id não foi encontrado" });
            }

            List<Especializacao> especializacoes = await _especializacaoService.RetornaEspecializacoesAsync();
            DoutorFormViewModel viewmodel = new DoutorFormViewModel { Doutor = dr, Especializacoes = especializacoes };

            return View(viewmodel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Doutor doutor)
        {
            if (!ModelState.IsValid)
            {
                var especializacoes = await _especializacaoService.RetornaEspecializacoesAsync();
                var viewModel = new DoutorFormViewModel { Doutor = doutor, Especializacoes = especializacoes };
                return View(viewModel);
            }

            if (id != doutor.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Os Ids não correspondem" });
            }

            try
            {
                await _doutorService.AtualizarDoutorAsync(doutor);

                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
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