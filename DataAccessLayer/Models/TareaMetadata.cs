using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    class TareaMetadata
    {
        [Required(ErrorMessage = "Complete el nombre.")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres.")]
        public string Nombre { get; set; }

        [StringLength(200, ErrorMessage = "Máximo 200 caracteres.")]
        public string Descripcion { get; set; }

        [RegularExpression(@"\d+(\,\d{2,2})?", ErrorMessage = "Solo se aceptan 2 decimales")]
        public Nullable<decimal> EstimadoHoras { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> FechaFin { get; set; }
    }
}
