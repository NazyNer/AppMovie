using System.ComponentModel.DataAnnotations;

namespace AppMovie.Models
{
    public class Gender
    {   
        [Key]
        public int GenderId { get; set;}
        [Required(ErrorMessage = "Este valor es obligatorio.")]
        [Display(Name = "Nombre del Genero")]
        public string GenderName { get; set;}

        public virtual ICollection<Movie>? Movies {get; set;}
    }
    
}