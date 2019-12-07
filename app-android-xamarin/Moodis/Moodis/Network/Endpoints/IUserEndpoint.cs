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

        [Post("/user")]
        public Task RegisterUser([Body] User user);

        [Delete("/user/{id}")]
        public Task DeleteUser(string userId);
    }
}