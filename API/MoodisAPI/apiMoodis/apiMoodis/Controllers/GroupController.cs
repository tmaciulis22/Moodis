using apiMoodis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace apiMoodis.Controllers
{
    public class GroupController : ApiController
    {
        public HttpResponseMessage Get()
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var groups = dbContext.Groups.ToList();
                if(groups.Count() == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There are no groups");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, groups.GetType());
                }
            }
        }

        public HttpResponseMessage Get(string id)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var entity = dbContext.Groups.FirstOrDefault(group => group.Id == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Group with Id " + id + " Not found");
                }
            }
        }

        public HttpResponseMessage Post([FromBody] Group group)
        {
            try
            {
                using (DatabaseContext dbContext = new DatabaseContext())
                {
                    dbContext.Groups.Add(group);
                    dbContext.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, group);
                    message.Headers.Location = new Uri(Request.RequestUri +
                        group.Id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(string id, [FromBody]Group group)
        {
            try
            {
                using (DatabaseContext dbContext = new DatabaseContext())
                {
                    var entity = dbContext.Groups.FirstOrDefault(e => e.Id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Group with Id " + id.ToString() + " not found to update");
                    }
                    else
                    {
                        entity.GroupName = group.GroupName;
                        entity.MembersInString = group.MembersInString;
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
                using (DatabaseContext dbContext = new DatabaseContext())
                {
                    var entity = dbContext.Groups.FirstOrDefault(e => e.Id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Group with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        dbContext.Groups.Remove(entity);
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
