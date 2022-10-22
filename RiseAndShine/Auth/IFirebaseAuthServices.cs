using System.Threading.Tasks;
using RiseAndShine.Auth.Models;

namespace RiseAndShine.Auth
{
    public interface IFirebaseAuthService
    {
        Task<FirebaseUser> Login(Credentials credentials);
        Task<FirebaseUser> Register(Registration registration);
    }
}