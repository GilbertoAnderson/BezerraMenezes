//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BezerraMenezesExpress.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblCategorias
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblCategorias()
        {
            this.tblLivros = new HashSet<tblLivros>();
        }
    
        public int idCategoria { get; set; }
        public string Descricao { get; set; }
        public Nullable<bool> Ativo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblLivros> tblLivros { get; set; }
    }
}
