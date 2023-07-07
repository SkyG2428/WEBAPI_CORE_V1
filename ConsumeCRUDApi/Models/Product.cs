using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConsumeCRUDApi.Models
{
	public class Product
	{
		//[JsonPropertyName("id")]
		public int Id { get; set; }
		//[JsonPropertyName("name")]
		[Required]
		[DisplayName("Product Name")]
		public string Name { get; set; }
		//[JsonPropertyName("price")]
		[Required]
		public double Price { get; set; }
		//[JsonPropertyName("quantity")]
		[Required]
		public int Quantity { get; set; }
	}
}
