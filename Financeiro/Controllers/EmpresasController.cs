using Financeiro.Models;
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

        
    }
}