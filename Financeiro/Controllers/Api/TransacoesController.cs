using AutoMapper;
using Financeiro.Dtos;
using Financeiro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace Financeiro.Controllers.Api
{
    public class TransacoesController : ApiController
    {
        ApplicationDbContext _context;

        public TransacoesController()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<TransacaoDto> GetTransacoes(string query = null)
        {
            IQueryable<Transacao> transacoesQuery = _context.Transacoes.Include(c => c.Empresa);

            if (!String.IsNullOrWhiteSpace(query))
            {
                transacoesQuery = transacoesQuery.Where(c => c.Empresa.Nome.Contains(query));
            }

            return transacoesQuery.ToList().Select(Mapper.Map<Transacao, TransacaoDto>);
        }

        [HttpPost]
        public IHttpActionResult CreateTransacao(TransacaoDto transacaoDto)
        {
            //var errors = ModelState.Values.SelectMany(v => v.Errors);

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}

            //var transacao = Mapper.Map<TransacaoDto, Transacao>(transacaoDto);

            var empresa = _context.Empresas.Single(c => c.Id == transacaoDto.EmpresaId);

            var transacao = new Transacao()
            {
                Empresa = empresa,
                DataInvestimento = transacaoDto.DataInvestimento,
                DataRecebimento = transacaoDto.DataRecebimento,
                TaxaJuros = transacaoDto.TaxaJuros,
                QtdDias = transacaoDto.QtdDias,
                ValorInvestido = transacaoDto.ValorInvestido,
                valorJuros = transacaoDto.valorJuros,
                ValorReceber = transacaoDto.ValorReceber,
                IsPago = transacaoDto.IsPago,
                Ativo = transacaoDto.Ativo

            };

            _context.Transacoes.Add(transacao);
            _context.SaveChanges();

            transacaoDto.Id = transacao.Id;

            return Created(new Uri(Request.RequestUri + "/" + transacao.Id), transacaoDto);
        }

        [HttpPut]
        public IHttpActionResult UpdateTransacao(int id, TransacaoDto transacaoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var transacaoInDb = _context.Transacoes.Include(c => c.Empresa).SingleOrDefault(c => c.Id == id);

            if (transacaoInDb == null)
                return NotFound();

            Mapper.Map(transacaoDto, transacaoInDb);

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteTransacao(int id)
        {
            var transacaoInDb = _context.Transacoes.Include(c => c.Empresa).SingleOrDefault(c => c.Id == id);

            if (transacaoInDb == null)
                return NotFound();

            _context.Transacoes.Remove(transacaoInDb);
            _context.SaveChanges();

            return Ok();

        }
    }
}
