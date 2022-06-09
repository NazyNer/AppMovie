using System.ComponentModel.DataAnnotations;

namespace AppMovie.Models
{
    public class Country
    {   
        [Key]
        public int CountryId { get; set; }
        
        [Required(ErrorMessage = "Este valor es obligatorio.")]
        [Display (Name = "País")]
        public string CountryName { get; set;}

        public virtual ICollection<Location>? Location { get; set; }
    }
    
}