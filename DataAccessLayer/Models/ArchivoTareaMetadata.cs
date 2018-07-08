using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class ArchivoTareaMetadata
    {
        [Required(ErrorMessage = "Debe seleccionar un archivo.")]
        public string RutaArchivo { get; set; }
    }
}
