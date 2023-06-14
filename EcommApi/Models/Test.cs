using EcommApi.Validations;
using System.ComponentModel.DataAnnotations;

namespace EcommApi.Models
{
    public class Test
    {
        public int Id { get; set; }
        [Required]
        [StringLength(25)]
        [Uppercase]
        public string Title { get; set; }
        [Required]
        [Range(18, 110)]
        public int Age { get; set; }
        [Required]
        [Url]
        public string Url { get; set; }
        [Required]
        [CreditCard] 
        public string CreditCard { get; set; }
    }
}
