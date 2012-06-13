using System.Security.Cryptography.X509Certificates;
using System.Web;
using NeosIT.IIS.Authentication.PkiAuthentication.Module;

namespace NeosIT.IIS.Authentication.PkiAuthentication.Service
{
    public class PkiAuthenticationService : IAuthenticationService
    {
        #region IAuthenticationService Members

        public bool IsAuthenticated(HttpClientCertificate httpClientCertificate)
        {
            var certificate = new X509Certificate2(httpClientCertificate.Certificate);
            return !certificate.Archived && certificate.Verify();
        }

        #endregion
    }
}