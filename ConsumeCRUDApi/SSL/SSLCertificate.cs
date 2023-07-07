namespace ConsumeCRUDApi.SSL
{
	public class SSLCertificate
	{
		public HttpClient client;
		public SSLCertificate()
		{
            HttpClientHandler handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (hrm, cert, cetChain, policyErrors) => true;

            client = new HttpClient(handler);
        }
  //      public void sslcertificate()
		//{
		//	HttpClientHandler handler = new HttpClientHandler();
		//	handler.ClientCertificateOptions = ClientCertificateOption.Manual;
		//	handler.ServerCertificateCustomValidationCallback =
		//		(hrm, cert, cetChain, policyErrors) => true;

		//	client = new HttpClient(handler);

		//}
	}
}
