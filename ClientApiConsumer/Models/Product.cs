using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcommApi.Models
{
    public class Product
    {
        
        public int id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("price")]
        public double Price { get; set; }
    }
}
