namespace EmailConsumer.Models
{
	public class Email
	{
		 public string ToEmail { get; set; }
		 public string EmailSubject { get; set; }
		public string EmailBody { get; set; }
		public bool IsBodyHtml { get; set; }
	}
}
