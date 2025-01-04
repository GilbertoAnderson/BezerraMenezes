using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BezerraMenezesExpress.Models
{
    public class Relatorios
    {

        [Display(Name = "Data Inicio")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DtInicio { get; set; }


        [Display(Name = "Data Fim")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DtFim { get; set; }


        [Required]
        [Range(1, 12, ErrorMessage = "A conta é obrigatoria!")]
        public int Conta { get; set; }

        [StringLength(240)]
        public string Filtro { get; set; }


    }
}