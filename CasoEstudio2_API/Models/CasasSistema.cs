//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CasoEstudio2_API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CasasSistema
    {
        public long IdCasa { get; set; }
        public string DescripcionCasa { get; set; }
        public decimal PrecioCasa { get; set; }
        public string UsuarioAlquiler { get; set; }
        public Nullable<System.DateTime> FechaAlquiler { get; set; }
    }
}
