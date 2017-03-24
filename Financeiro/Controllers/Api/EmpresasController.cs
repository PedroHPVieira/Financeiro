using AutoMapper;
using Financeiro.Dtos;
using Financeiro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Financeiro.Controllers.Api
{
    public class EmpresasController : ApiController
    {
        ApplicationDbContext _context;

        public EmpresasController()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<EmpresaDto> GetEmpresas(string query = null)
        {
            IQueryable<Empresa> empresasQuery = _context.Empresas;

            if (!String.IsNullOrWhiteSpace(query))
            {
                empresasQuery = empresasQuery.Where(c => c.Nome.Contains(query));
            }

            return empresasQuery.ToList().Select(Mapper.Map<Empresa, EmpresaDto>);
        }

        [HttpDelete]
        public IHttpActionResult DeleteEmpresa(int id)
        {
            var empresaInDb = _context.Empresas.SingleOrDefault(c => c.Id == id);

            if (empresaInDb == null)
                return NotFound();

            _context.Empresas.Remove(empresaInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
