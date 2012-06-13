using System;
using System.Web;

namespace NeosIT.IIS.Authentication.PkiAuthentication.Module
{
    public class PkiAuthenticationModule : IHttpModule
    {
        public PkiAuthenticationModule(IAuthenticationService authenticationService)
        {
            AuthenticationService = authenticationService;
        }

        public IAuthenticationService AuthenticationService { get; protected set; }

        #region IHttpModule Members

        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += AuthenticateRequestHandler;
        }


        public void Dispose()
        {
        }

        #endregion

        private void AuthenticateRequestHandler(object sender, EventArgs e)
        {
            if (null == AuthenticationService)
            {
                throw new HttpException(401, "AuthenticationService nicht initialisiert.");
            }

            var app = (HttpApplication) sender;
            HttpContext context = app.Context;

            if (context.Request.ClientCertificate.IsPresent)
            {
                if (AuthenticationService.IsAuthenticated(context.Request.ClientCertificate))
                {
                    // do cool stuff here ;)
                    // too lazy to write something more...
                }
                else
                {
                    throw new HttpException(401, "Benutzer nicht authentifiziert.");
                }
            }
            else
            {
                throw new HttpException(401, "Clientzertifikat nicht vorhanden.");
            }
        }
    }
}