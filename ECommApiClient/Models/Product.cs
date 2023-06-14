using System.Text.Json.Serialization;

namespace ECommApiClient.Models
{
    public class Product
    {
       
        public int id { get; set; }
        
        public string Title { get; set; }
        public double Price { get; set; }
    }
}
