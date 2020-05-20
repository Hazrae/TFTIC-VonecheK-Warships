using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Util.Store;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectWarships_Web.Utils
{
    public class GoogleToken : IGoogleToken
    {
        IConfiguration _config;
        public string Id
        {
            get { return _config.GetValue<string>("Google:Id"); }
        }

        public string Secret
        {
            get { return _config.GetValue<string>("Google:Secret"); }
        }

        public string Mail
        {
            get { return _config.GetValue<string>("Google:Mail"); }
        }
        public GoogleToken(IConfiguration config)
        {
            _config = config;
        }
        public async Task<SaslMechanismOAuth2> Token()
        {   
            var clientSecrets = new ClientSecrets
            {
                ClientId = Id,
                ClientSecret = Secret
            };

            var codeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                DataStore = new FileDataStore("CredentialCacheFolder", false),
                Scopes = new[] { "https://mail.google.com/" },
                ClientSecrets = clientSecrets
            });

            var codeReceiver = new LocalServerCodeReceiver();
            var authCode = new AuthorizationCodeInstalledApp(codeFlow, codeReceiver);
            var credential = await authCode.AuthorizeAsync(Mail, CancellationToken.None);

            //if (authCode.ShouldRequestAuthorizationCode(credential.Token))
            //    await credential.RefreshTokenAsync(CancellationToken.None);
            if (credential.Token.IsExpired(Google.Apis.Util.SystemClock.Default))
            {
                await credential.RefreshTokenAsync(CancellationToken.None);
            }

            var oauth2 = new SaslMechanismOAuth2(credential.UserId, credential.Token.AccessToken);
            return oauth2;
        }
    }
}
