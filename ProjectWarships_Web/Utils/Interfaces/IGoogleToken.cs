using MailKit.Security;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace ProjectWarships_Web.Utils
{
    public interface IGoogleToken
    {
        Task<SaslMechanismOAuth2> Token();
    }
}