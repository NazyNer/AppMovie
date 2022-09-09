using System.ComponentModel.DataAnnotations;

namespace AppMovie.Models
{
    public class Movie
    {
        [Key]
        
        public int MovieID { get; set; }


        [Display(Name = "Nombre de la Película")]
        [Required(ErrorMessage = "Este valor es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El largo maximo es de {0} caracteres.")]
        public string? MovieName { get; set; }


        [Display(Name = "Descripcion de la Película")]
        public string? MovieDescription { get; set; }

        public bool IsDeleted { get; set; }


        [Display(Name = "Fecha de lanzamineto")]
        [DataType(DataType.Date)]
        public DateTime MovieDate { get; set; }

        [Display(Name = "Seccion")]
        public int SectionID { get; set; }

        [Display(Name = "Seccion")]

        public virtual Section? Section { get; set; }

        [Display(Name = "Género")]
        public int GenderID { get; set; }

        [Display(Name = "Género")]

        public virtual Gender? Gender { get; set; }
        [Display(Name = "Productor")]
        public int ProducerID { get; set; }

        [Display(Name = "Productor")]
        public virtual Producer? Producer { get; set; }

        public bool estaAlquilada { get; set; }
    }
}