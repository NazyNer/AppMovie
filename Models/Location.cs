using System.ComponentModel.DataAnnotations;

namespace AppMovie.Models
{
    public class Location
    {   
        [Key]

        
        public int LocationId { get; set; }

        [Display (Name = "Ubicación")]
        public string? LocationName { get; set;}

        [Display (Name = "País")]
        public int CountryID {get; set;}

        [Display (Name = "País")]
        public virtual Country? Country {get; set;}

        [Display (Name = "País")]
        public virtual ICollection<Partner>? Partner { get; set; }
    }
    
}