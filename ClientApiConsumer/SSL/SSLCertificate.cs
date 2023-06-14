namespace ClientApiConsumer.SSL
{
    public class SSLCertificate
    {
        public static void sslcertificate()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (hrm, cert, cetChain, policyErrors) => true;

            HttpClient client = new HttpClient(handler);
        }
    }
}
