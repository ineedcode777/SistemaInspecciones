namespace SistemaInspecciones.Mobile.Services
{
    public class HttpsClientHandlerService
    {
        public HttpMessageHandler? GetPlatformMessageHandler()
        {
#if ANDROID
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            return handler;
#else
            return null;
#endif
        }
    }
}