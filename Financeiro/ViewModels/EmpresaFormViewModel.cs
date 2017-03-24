using Financeiro.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Financeiro.ViewModels
{
    public class EmpresaFormViewModel
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime DataAtualizacao { get; set; }

        public string Title
        {
            get
            {
                return Id != 0 ? "Editar Empresa" : "Nova Empresa";
            }
        }

        public EmpresaFormViewModel()
        {
            Id = 0;
        }

        public EmpresaFormViewModel(Empresa empresa)
        {
            this.Id = empresa.Id;
            this.Nome = empresa.Nome;
        }
    }
}