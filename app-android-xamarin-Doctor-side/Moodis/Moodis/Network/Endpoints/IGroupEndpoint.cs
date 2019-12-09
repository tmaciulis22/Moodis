using System.Collections.Generic;
using System.Threading.Tasks;
using Moodis.Feature.Group;
using Moodis.Network.Requests;
using Refit;

namespace Moodis.Network.Endpoints
{
    interface IGroupEndpoint
    {
        [Get("/group?Id={id}")]
        public Task<Group> GetByIdGroup(string id);

        [Get("/group?doctorId={doctorid}")]
        public Task<List<Group>> GetDoctorGroups(string doctorid);

        [Get("/group")]
        public Task<List<Group>> GetAllGroups();

        [Post("/group")]
        public Task CreateGroup([Body] Group group);

        [Delete("/group?id={id}")]
        public Task DeleteGroup(string id);
    }
}