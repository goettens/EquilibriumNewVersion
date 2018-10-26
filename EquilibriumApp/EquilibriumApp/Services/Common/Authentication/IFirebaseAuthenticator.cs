using System.Threading.Tasks;

namespace EquilibriumApp.Services.Common.Authentication
{
    public interface IFirebaseAuthenticator
    {
        Task<string> LoginWithEmailPassword(string email, string password);
        Task<string> CreateLogin(string email, string nome, string password, string confirmPassword);
        Task<bool> ChangePassword(string email);
    }
}
