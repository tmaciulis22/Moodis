using apiMoodis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using apiMoodis.Encryption;
using apiMoodis.Requests;

namespace apiMoodis.Controllers
{
    [Route("api/user")]
    public class UserController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetAllUsers()
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var users = dbContext.Users.Include(item => item.ImageInfos).ToList();
                return Ok(users);
            }
        }

        [HttpGet]
        public IHttpActionResult GetByIdUser(string id)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var entity = dbContext.Users.Include(item => item.ImageInfos).Single(user => user.Id == id);
                return Ok(entity);
            }
        }

        [Route("api/user/login")]
        public IHttpActionResult GetUser([FromBody] LoginRequest request)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                try
                {
                    var encryptedPass = Crypto.CalculateMD5Hash(request.Password);
                    var entity = dbContext.Users.Include(item => item.ImageInfos)
                        .Single(user => user.Username == request.Username && user.Password == encryptedPass);
                    return Ok(entity);
                }
                catch
                {
                    return NotFound();
                }
            }
        }

        [HttpPost]
        public IHttpActionResult PostUser([FromBody] User user)
        {
            try
            {
                using (DatabaseContext dbContext = new DatabaseContext())
                {
                    user.Id = Guid.NewGuid().ToString();
                    user.Password = Crypto.CalculateMD5Hash(user.Password);
                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        public IHttpActionResult PutUser(string id, [FromBody]User user)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var entity = dbContext.Users.Single(e => e.Id == id);
                entity.Username = user.Username;
                entity.Password = Crypto.CalculateMD5Hash(user.Password);
                entity.GroupName = user.GroupName;
                entity.PersonGroupId = user.PersonGroupId;
                dbContext.SaveChanges();
                return Ok();

            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteUser(string id)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var entity = dbContext.Users.Single(e => e.Id == id);
                dbContext.Users.Remove(entity);
                dbContext.SaveChanges();
                return Ok();
            }
        }
    }
}
