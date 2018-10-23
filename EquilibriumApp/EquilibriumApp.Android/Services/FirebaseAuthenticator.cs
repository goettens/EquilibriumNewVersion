using System.Threading.Tasks;
using EquilibriumApp.Services;
using Firebase.Auth;
using Xamarin.Forms;
using Firebase;

[assembly: Dependency(typeof(EquilibriumApp.Droid.Services.FirebaseAuthenticator))]
namespace EquilibriumApp.Droid.Services
{
    public class FirebaseAuthenticator : IFirebaseAuthenticator
    {
        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            var user = await FirebaseAuth.Instance.
                            SignInWithEmailAndPasswordAsync(email, password);
            var token = await user.User.GetIdTokenAsync(false);
            return token.Token;
        }

        public async Task<string> CreateLogin(string email, string nome, string password, string confirmPassword)
        {
            var user = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
            var token = await user.User.GetIdTokenAsync(false);
            return token.Token;
        }
    }
}