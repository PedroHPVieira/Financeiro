using Financeiro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Financeiro.Dtos;
using AutoMapper;
using Financeiro.ViewModels;

namespace Financeiro.Controllers
{
    public class TransacoesController : Controller
    {
        ApplicationDbContext _context;

        public TransacoesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Transacoes
        public ViewResult Index()
        {
            var transacoes = _context.Transacoes.Include(c => c.Empresa).ToList();

            return View(transacoes);
        }

        public ActionResult New()
        {
            var transacaoViewModel = new TransacaoFormViewModel();

            return View("TransacaoForm", transacaoViewModel);
        }

        public ActionResult Edit(int id)
        {
            var transacao = _context.Transacoes.SingleOrDefault(c => c.Id == id);
            var empresa = _context.Empresas.SingleOrDefault(c => c.Id == transacao.EmpresaId);

            if (transacao == null)
            {
                return HttpNotFound();
            }

            var viewModel = new TransacaoFormViewModel
            {
                Id = transacao.Id,
                DataInvestimento = transacao.DataInvestimento,
                DataRecebimento = transacao.DataRecebimento,
                EmpresaId = transacao.EmpresaId,
                Empresa = transacao.Empresa,
                Ativo = transacao.Ativo,
                IsPago = transacao.IsPago,
                QtdDias = transacao.QtdDias,
                TaxaJuros = transacao.TaxaJuros,
                ValorInvestido = transacao.ValorInvestido,
                valorJuros = transacao.valorJuros,
                ValorReceber = transacao.ValorReceber
            };

            return View("TransacaoForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(TransacaoDto transacaoDto)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (!ModelState.IsValid)
                return View("TransacaoForm");

            if (transacaoDto.Id == 0)
            {
                var transacao = Mapper.Map<TransacaoDto, Transacao>(transacaoDto);
                _context.Transacoes.Add(transacao);
            }
            else
            {
                var transacaoInDb = _context.Transacoes.Include(c => c.Empresa)
                                .SingleOrDefault(c => c.Id == transacaoDto.Id);

                if (transacaoInDb == null)
                    return HttpNotFound();

                Mapper.Map(transacaoDto, transacaoInDb);
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Transacoes");
        }
    }
}