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
        public IConfiguration _config { get; set; }
        private string Id
        {
            get { return _config.GetValue<string>("Id"); }
        }

        private string Secret
        {
            get { return _config.GetValue<string>("Secret"); }
        }

        private string Mail
        {
            get { return _config.GetValue<string>("Mail"); }
        }
       
        public GoogleToken(IConfiguration config)
        {
            _config = config;
        }
        public async Task<SaslMechanismOAuth2> Token()
        {        
            ClientSecrets clientSecrets = new ClientSecrets
            {
                // Stockés dans mon app.settings
                ClientId = Id,
                ClientSecret = Secret
            };

            GoogleAuthorizationCodeFlow codeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                DataStore = new FileDataStore("CredentialCacheFolder", false),
                Scopes = new[] { "https://mail.google.com/" },
                ClientSecrets = clientSecrets
            });

            LocalServerCodeReceiver codeReceiver = new LocalServerCodeReceiver();
            AuthorizationCodeInstalledApp authCode = new AuthorizationCodeInstalledApp(codeFlow, codeReceiver);
            UserCredential credential = await authCode.AuthorizeAsync(Mail, CancellationToken.None);

            if (authCode.ShouldRequestAuthorizationCode(credential.Token))
                await credential.RefreshTokenAsync(CancellationToken.None);

            SaslMechanismOAuth2 oauth2 = new SaslMechanismOAuth2(credential.UserId, credential.Token.AccessToken);
            return oauth2;
        }
    }
}
