using apiMoodis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace apiMoodis.Controllers
{
    public class UserController : ApiController
    {
        public HttpResponseMessage Get()
        {
            using (DBUserContext dbContext = new DBUserContext())
            {
                var users = dbContext.Users.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, users);
            }
        }

        public HttpResponseMessage Get(string id)
        {
            using (DBUserContext dbContext = new DBUserContext())
            {
                var entity = dbContext.Users.FirstOrDefault(user => user.Id == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with Id " + id + " Not found");
                }
            }
        }

        public HttpResponseMessage Post([FromBody] User user)
        {
            try
            {
                using (DBUserContext dbContext = new DBUserContext())
                {
                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, user);
                    message.Headers.Location = new Uri(Request.RequestUri +
                        user.Id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(string id, [FromBody]User user)
        {
            try
            {
                using (DBUserContext dbContext = new DBUserContext())
                {
                    var entity = dbContext.Users.FirstOrDefault(e => e.Id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "User with Id " + id.ToString() + " not found to update");
                    }
                    else
                    {
                        entity.Username = user.Username;
                        entity.Password = user.Password;
                        entity.GroupName = user.GroupName;
                        entity.PersonGroupId = user.PersonGroupId;
                        dbContext.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(string id)
        {
            try
            {
                using (DBUserContext dbContext = new DBUserContext())
                {
                    var entity = dbContext.Users.FirstOrDefault(e => e.Id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "User with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        dbContext.Users.Remove(entity);
                        dbContext.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}
