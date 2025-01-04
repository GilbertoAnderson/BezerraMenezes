using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BezerraMenezesExpress.Models
{
    public class Associados
    {
        [Key]
        public int idAssociado { get; set; }

        [Required(ErrorMessage = "Nome é campo obrigatório!")]
        [StringLength(240)]
        public string Nome { get; set; }

        [Required]
        [StringLength(240)]
        public string Email { get; set; }

        public string Celular { get; set; }

        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Nascimento { get; set; }

        public Boolean Ativo { get; set; }
        
        //.... chave estrangeira
        public List<Mensalidade> Mensalidades { get; set; }

    }
}