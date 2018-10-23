using System.Threading.Tasks;

namespace EquilibriumApp.Services
{
    public interface IFirebaseAuthenticator
    {
        Task<string> LoginWithEmailPassword(string email, string password);
        Task<string> CreateLogin(string email, string nome, string password, string confirmPassword);
    }
}
