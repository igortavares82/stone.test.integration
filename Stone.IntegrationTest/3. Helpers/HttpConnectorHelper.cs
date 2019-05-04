using Stone.Framework.Http.Abstractions;
using Stone.Framework.Http.Concretes;

namespace Stone.IntegrationTest.Helpers
{
    public static class HttpConnectorHelper
    {
        public static IHttpConnector GetChargeConnector()
        {
            IHttpConnector connector = new HttpConnector();
            connector.SetAddress("http://localhost:59761/api/charge");

            return connector;
        }

        public static IHttpConnector GetClientConnector()
        {
            IHttpConnector connector = new HttpConnector();
            connector.SetAddress("http://localhost:51095/api/charge");

            return connector;
        }
    }
}
