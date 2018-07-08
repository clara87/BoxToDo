using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class ComentarioTareaMetadata
    {
        [Required(ErrorMessage = "Debe escribir un comentario.")]
        public string Texto { get; set; }
    }
}
