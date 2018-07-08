using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    [MetadataType(typeof(UsuarioLoginMetadata))]
    public partial class UsuarioLogin
    {
        public string Email { get; set; }
        public string Contrasenia { get; set; }
        public bool Recordarme { get; set; }        
    }
    
}
