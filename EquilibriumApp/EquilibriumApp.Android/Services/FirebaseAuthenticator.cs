using System.Threading.Tasks;
using EquilibriumApp.Services.Common.Authentication;
using Firebase.Auth;
using Xamarin.Forms;

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

        public async Task<bool> ChangePassword(string email)
        {
            try
            {
                await FirebaseAuth.Instance.SendPasswordResetEmailAsync(email);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}