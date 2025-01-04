using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BezerraMenezesExpress.Models
{
    public class ExtratoReport
    {
        public Int32 idExtrato { get; set;}

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Data { get; set; }

        public String Subconta { get; set; }

        public String Descricao { get; set; }

        public Decimal Valor { get; set; }

        public Decimal Saldo { get; set; }       

    }
}