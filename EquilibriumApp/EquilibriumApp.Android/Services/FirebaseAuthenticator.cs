using System;
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

        public async Task<bool> CreateLogin(string email, string password)
        {
            try
            {
                var user = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                return true;
            }
            catch
            {
                return false;
            }            
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