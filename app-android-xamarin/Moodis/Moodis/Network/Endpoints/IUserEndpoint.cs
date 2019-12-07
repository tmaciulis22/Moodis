using System.Threading.Tasks;
using Moodis.Feature.Login;
using Moodis.Network.Requests;
using Refit;

namespace Moodis.Network.Endpoints
{
    interface IUserEndpoint
    {
        [Get("/user/login")]
        public Task<User> LoginUser([Body] LoginRequest request);

        [Get("/user/{username}")]
        public Task<User> GetUser(string username);

        [Post("/user/register")]
        public Task<User> RegisterUser([Body] User user);

        [Delete("/user/{id}")]
        public Task DeleteUser(string id);

        [Put("/user")]
        public Task UpdateUser([Body] User user);
    }
}