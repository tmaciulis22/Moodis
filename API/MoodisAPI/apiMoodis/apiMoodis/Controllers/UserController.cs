using apiMoodis.Models;
using System;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using apiMoodis.Encryption;
using apiMoodis.Requests;
using System.Data.Entity.Validation;
using System.Text;

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
                var users = dbContext.Users.Include(item => item.ImageInfos).Select(user => new UserFE() { 
                    Id = user.Id,
                    GroupId = user.GroupId,
                    Username = user.Username,
                    Password = user.Password,
                    IsDoctor = user.IsDoctor,
                    PersonGroupId = user.PersonGroupId,
                    PersonId = user.PersonId
                }).ToList();
                if (users.Count != 0)
                {
                    return Ok(users);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpGet]
        public IHttpActionResult GetByIdUser(string id)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                try
                {
                    var entity = dbContext.Users.Include(item => item.ImageInfos).Single(u => u.Id == id);
                    var user = new UserFE()
                    {
                        Id = entity.Id,
                        GroupId = entity.GroupId,
                        Username = entity.Username,
                        Password = entity.Password,
                        IsDoctor = entity.IsDoctor,
                        PersonGroupId = entity.PersonGroupId,
                        PersonId = entity.PersonId
                    };
                    return Ok(user);
                }
                catch (InvalidOperationException)
                {
                    return NotFound();
                }
                catch (Exception e)
                {
                    return InternalServerError(e);
                }
            }
        }

        [HttpGet]
        public IHttpActionResult GetUserByUsername(string username)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                try
                {
                    var entity = dbContext.Users.Include(item => item.ImageInfos).Single(u => u.Username == username);
                    var user = new UserFE()
                    {
                        Id = entity.Id,
                        GroupId = entity.GroupId,
                        Username = entity.Username,
                        Password = entity.Password,
                        IsDoctor = entity.IsDoctor,
                        PersonGroupId = entity.PersonGroupId,
                        PersonId = entity.PersonId
                    };
                    return Ok(user);
                }
                catch (InvalidOperationException)
                {
                    return NotFound();
                }
                catch (Exception e)
                {
                    return InternalServerError(e);
                }
            }
        }

        [HttpGet]
        [Route("api/user/login")]
        public IHttpActionResult LoginUser([FromBody] LoginRequest request)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                try
                {
                    var encryptedPass = Crypto.CalculateMD5Hash(request.Password);
                    var entity = dbContext.Users.Include(item => item.ImageInfos)
                        .Single(u => u.Username == request.Username && u.Password == encryptedPass);
                    var user = new UserFE()
                    {
                        Id = entity.Id,
                        GroupId = entity.GroupId,
                        Username = entity.Username,
                        Password = entity.Password,
                        IsDoctor = entity.IsDoctor,
                        PersonGroupId = entity.PersonGroupId,
                        PersonId = entity.PersonId
                    };
                    return Ok(user);
                }
                catch (InvalidOperationException)
                {
                    return NotFound();
                }
                catch (Exception e)
                {
                    return InternalServerError(e);
                }
            }
        }

        [HttpPost]
        [Route("api/user/register")]
        public IHttpActionResult PostUser([FromBody] User user)
        {
            try
            {
                using (DatabaseContext dbContext = new DatabaseContext())
                {
                    var isAlreadyRegistered = dbContext.Users.Any(u => u.Username == user.Username);
                    if (isAlreadyRegistered)
                    {
                        return BadRequest("User already exists");
                    }

                    user.Id = Guid.NewGuid().ToString();
                    user.PersonGroupId = Guid.NewGuid().ToString();
                    user.Password = Crypto.CalculateMD5Hash(user.Password);
                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();
                    return Ok(user);
                }
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var entityError in ex.EntityValidationErrors)
                {
                    var generalMessage = "Entity of type \"" + entityError.Entry.Entity.GetType().Name + "\" in state \"" + entityError.Entry.State + "\" has the following validation errors:";
                    stringBuilder.AppendLine(generalMessage);
                    foreach (var validationError in entityError.ValidationErrors)
                    {
                        var propertyErrors = "- Property: \"" + validationError.PropertyName + "\", Value: \"" + entityError.Entry.CurrentValues.GetValue<object>(validationError.PropertyName) + "\", Error: \"" + validationError.ErrorMessage + "\"";
                        stringBuilder.AppendLine(propertyErrors);
                    }
                }
                return BadRequest(stringBuilder.ToString());
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut]
        public IHttpActionResult PutUser([FromBody] User user)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                try
                {
                    var entity = dbContext.Users.Single(e => e.Id == user.Id);
                    entity.Username = user.Username;
                    entity.Password = Crypto.CalculateMD5Hash(user.Password);
                    entity.GroupId = user.GroupId;
                    entity.PersonId = user.PersonId;
                    entity.PersonGroupId = user.PersonGroupId;
                    dbContext.SaveChanges();
                    return Ok();
                }
                catch (InvalidOperationException)
                {
                    return NotFound();
                }
                catch (DbEntityValidationException ex)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (var entityError in ex.EntityValidationErrors)
                    {
                        var generalMessage = "Entity of type \"" + entityError.Entry.Entity.GetType().Name + "\" in state \"" + entityError.Entry.State + "\" has the following validation errors:";
                        stringBuilder.AppendLine(generalMessage);
                        foreach (var validationError in entityError.ValidationErrors)
                        {
                            var propertyErrors = "- Property: \"" + validationError.PropertyName + "\", Value: \"" + entityError.Entry.CurrentValues.GetValue<object>(validationError.PropertyName) + "\", Error: \"" + validationError.ErrorMessage + "\"";
                            stringBuilder.AppendLine(propertyErrors);
                        }
                    }
                    return BadRequest(stringBuilder.ToString());
                }
                catch (Exception e)
                {
                    return InternalServerError(e);
                }
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteUser(string id)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                try
                {
                    var entity = dbContext.Users.Single(e => e.Id == id);
                    dbContext.Users.Remove(entity);
                    dbContext.SaveChanges();
                    return Ok();
                }
                catch (InvalidOperationException)
                {
                    return NotFound();
                }
                catch (Exception e)
                {
                    return InternalServerError(e);
                }
            }
        }
    }
}
