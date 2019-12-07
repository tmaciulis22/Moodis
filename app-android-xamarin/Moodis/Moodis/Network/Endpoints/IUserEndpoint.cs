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

        [Get("/user?personId={personId}")]
        public Task<User> GetUser(string personId);

        [Post("/user/register")]
        public Task<User> RegisterUser([Body] RegisterRequest request);

        [Delete("/user?id={id}")]
        public Task DeleteUser(string id);

        [Put("/user")]
        public Task UpdateUser([Body] User user);
    }
}