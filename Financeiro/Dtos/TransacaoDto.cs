using Financeiro.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Financeiro.Dtos
{
    public class TransacaoDto
    {
        public int Id { get; set; }

        public Empresa Empresa { get; set; }

        [Required]
        [Display(Name = "Empresa")]
        public int EmpresaId { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "0:dd/MM/yyyy")]
        [DataType(DataType.Date, ErrorMessage = "Data em formado inválido")]
        public DateTime DataInvestimento { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "0:dd/MM/yyyy")]
        [DataType(DataType.Date, ErrorMessage = "Data em formado inválido")]
        public DateTime DataRecebimento { get; set; }

        [Required]
        [Display(Name = "Taxa de juros")]
        public decimal TaxaJuros { get; set; }

        [Required]
        [Display(Name = "Quantidade de dias investido")]
        public int QtdDias { get; set; }

        [Required]
        [Display(Name = "Valor a investir")]
        public decimal ValorInvestido { get; set; }

        [Required]
        [Display(Name = "Valor do rendimento/juros")]
        public decimal valorJuros { get; set; }

        [Required]
        [Display(Name = "Valor a receber")]
        public decimal ValorReceber { get; set; }

        [Required]
        [Display(Name = "Pago?")]
        public bool IsPago { get; set; }

        [Required]
        public bool Ativo { get; set; }
    }
}