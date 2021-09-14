using ExampleApp.Shared.Models;
using System.Threading.Tasks;

namespace ExampleApp.Shared
{
    public interface ITokenHelper
    {
        Task<AuthUser> GetAccessTokenAsync();
    }
}