using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BezerraMenezesExpress.Models
{
    public class LivrosMesmaNotaFiscal
    {
        public Int32 idCarrinhoEntrada { get; set; }
        public Int32 ISBN { get; set; }
        public String Titulo { get; set; }
        public Int32 Qtde { get; set; }
        public decimal Valor { get; set; }

    }
}

