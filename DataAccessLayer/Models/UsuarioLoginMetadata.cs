using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    class UsuarioLoginMetadata
    {
        [Required]
        [StringLength(50)]
        [EmailAddress(ErrorMessage = "El email ingresado es invalido")]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Contrasenia { get; set; }
    }
}
