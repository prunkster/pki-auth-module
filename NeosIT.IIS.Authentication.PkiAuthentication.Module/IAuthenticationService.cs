using System.Web;

namespace NeosIT.IIS.Authentication.PkiAuthentication.Module
{
    public interface IAuthenticationService
    {
        bool IsAuthenticated(HttpClientCertificate httpClientCertificate);
    }
}