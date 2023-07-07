using System.ComponentModel.DataAnnotations;

namespace CreudOperationWebApi.Models
{
	public class ProductModel
	{
		public int Id { get; set; }
		[Required]	
		
		public string Name { get; set; }
		[Required]
		public double Price { get; set; }
		[Required]
		public int Quantity { get; set; }

	}
}
