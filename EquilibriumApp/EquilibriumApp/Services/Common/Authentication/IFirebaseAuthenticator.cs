using System.Threading.Tasks;

namespace EquilibriumApp.Services
{
    public interface IFirebaseAuthenticator
    {
        Task<string> LoginWithEmailPassword(string email, string password);
    }
}
