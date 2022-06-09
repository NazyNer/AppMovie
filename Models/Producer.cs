using System.ComponentModel.DataAnnotations;

namespace AppMovie.Models
{
    public class Producer
    {   
        [Key]
        public int ProducerId { get; set; }

        [Display (Name = "Productor")]
        public string ProducerName { get; set;}

        public virtual ICollection<Movie>? Movies {get; set;}
    }
    
}