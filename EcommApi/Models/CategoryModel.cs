using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommApi.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int DisplayOrder { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        [NotMapped]
        public IFormFile CategoryImage { get; set; }

        public string? CategoryImagePath { get; set; }
    }
}
