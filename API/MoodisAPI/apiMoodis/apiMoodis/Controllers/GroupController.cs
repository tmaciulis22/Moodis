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
        [HttpGet]
        public IHttpActionResult GetAllGroups()
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var groups = dbContext.Groups.ToList();
                if(groups.Count() == 0)
                {
                    return BadRequest("There are no groups");
                }
                else
                {
                    return Ok(groups);
                }
            }
        }

        [HttpGet]
        public IHttpActionResult GetByIdGroup(string id)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var entity = dbContext.Groups.Single(group => group.Id == id);
                return Ok(entity);
            }
        }

        [HttpPost]
        public IHttpActionResult PostGroup([FromBody] Group group)
        {
            try
            {
                using (DatabaseContext dbContext = new DatabaseContext())
                {
                    dbContext.Groups.Add(group);
                    dbContext.SaveChanges();
                    return Ok(group);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        public IHttpActionResult Put(string id, [FromBody]Group group)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var entity = dbContext.Groups.Single(e => e.Id == id);
                entity.GroupName = group.GroupName;
                entity.MembersInString = group.MembersInString;
                dbContext.SaveChanges();
                return Ok(entity);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(string id)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var entity = dbContext.Groups.Single(e => e.Id == id);
                dbContext.Groups.Remove(entity);
                dbContext.SaveChanges();
                return Ok();
            }
        }
    }
}
