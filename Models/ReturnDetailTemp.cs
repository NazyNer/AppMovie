using System.ComponentModel.DataAnnotations;

namespace AppMovie.Models
{
    public class ReturnDetailTemp
    {
        [Key]
        public int ReturnDetailTempID { get; set; }

        public int MovieID {get; set;}

        public string? MovieName {get; set;}

    }
}