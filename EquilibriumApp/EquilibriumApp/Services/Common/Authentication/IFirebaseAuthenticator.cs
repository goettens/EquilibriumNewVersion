using System.Threading.Tasks;

namespace EquilibriumApp.Services.Common.Authentication
{
    public interface IFirebaseAuthenticator
    {
        Task<string> LoginWithEmailPassword(string email, string password);
        Task<bool> CreateLogin(string email, string password);
        Task<bool> ChangePassword(string email);
    }
}
