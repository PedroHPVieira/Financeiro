using Financeiro.Models;
using Financeiro.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Financeiro.Controllers
{
    public class EmpresasController : Controller
    {
        private ApplicationDbContext _context;

        public EmpresasController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Empresas
        public ActionResult Index()
        {
            var empresas = _context.Empresas.ToList();

            return View();
        }

        public ActionResult Edit(int id)
        {
            var empresa = _context.Empresas.SingleOrDefault(c => c.Id == id);

            if (empresa == null)
                return HttpNotFound();

            var empresaViewModel = new EmpresaFormViewModel(empresa);

            return View("EmpresaForm", empresaViewModel);
        }

        public ActionResult Details(int id)
        {
            var empresa = _context.Empresas.SingleOrDefault(c => c.Id == id);

            if (empresa == null)
                return HttpNotFound();

            return View(empresa);
        }

        public ActionResult New()
        {
            var empresaViewModel = new EmpresaFormViewModel();

            return View("EmpresaForm", empresaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Empresa empresa)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (!ModelState.IsValid)
            {
                return View("EmpresaForm");
            }

            if (empresa.Id == 0)
            {
                empresa.DataCadastro = DateTime.Now;
                empresa.DataAtualizacao = DateTime.Now;

                _context.Empresas.Add(empresa);
            }
            else
            {
                var empresaInDb = _context.Empresas.Single(c => c.Id == empresa.Id);

                empresaInDb.Nome = empresa.Nome;
                empresaInDb.DataAtualizacao = DateTime.Now;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Empresas");
        }
    }
}