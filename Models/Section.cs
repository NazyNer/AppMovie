using System.ComponentModel.DataAnnotations;

namespace AppMovie.Models
{
    public class Section
    {   
        [Key]
        public int SectionId { get; set; }
        [Display(Name = "Nombre de la seccion")]
        [Required(ErrorMessage = "Este valor es obligatorio.")]
        public string SectionName { get; set;}

        public virtual ICollection<Movie>? Movie {get; set;}
    }
}