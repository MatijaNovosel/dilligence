using System.Threading.Tasks;
using tvz2api.Models;

namespace tvz2api.Data 
{
    public interface IAuthRepository 
    {
        Task<Korisnik> Register(Korisnik user, string password);
        Task<Korisnik> Login(string username, string password);
        Task<bool> userExists(string username);
    }
}