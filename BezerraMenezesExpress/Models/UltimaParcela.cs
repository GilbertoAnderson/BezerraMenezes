using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BezerraMenezesExpress.Models
{
    public class UltimaParcela
    {
        public String Matricula { get; set; }

        public String Nome { get; set; }

        public String Categoria { get; set; }

        public Int32 Ano { get; set; }

        public Int32 Mes { get; set; }

    }
}