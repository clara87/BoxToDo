using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class CarpetaMetadata
    {
        [Required(ErrorMessage = "Complete el nombre.")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres.")]
        public string Nombre { get; set; }


        [StringLength(200, ErrorMessage = "Máximo 200 caracteres.")]
        public string Descripcion { get; set; }
    }
}