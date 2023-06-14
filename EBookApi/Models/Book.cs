using System.ComponentModel.DataAnnotations.Schema;

namespace EcommApi.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? BookUrl { get; set; }
        public bool Trending { get; set; }
        public DateTime CreatedDate { get; set; }= DateTime.Now;
        public string ISBNNumber { get; set;}
        [NotMapped]
        public IFormFile BookFile { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public int? BookCoverId { get; set; }
        public BookCover BookCover { get; set; }
        public int? BookWriterId { get; set; }
        public BookWriter BookWriter { get; set; }
    }
}
