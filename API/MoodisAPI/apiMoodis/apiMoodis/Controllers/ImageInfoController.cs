using apiMoodis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace apiMoodis.Controllers
{
    public class ImageInfoController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetAllImageInfos()
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var imageInfos = dbContext.ImageInfos.Include(e => e.Emotions).ToList();
                if (imageInfos.Count() == 0)
                {
                    return BadRequest("There are no ImageInfos");
                }
                else
                {
                    return Ok(imageInfos);
                }
            }
        }

        [HttpGet]
        public IHttpActionResult GetImageInfoById(string id)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var entity = dbContext.ImageInfos.Single(ImageInfo => ImageInfo.Id == id);
                return Ok(entity);
            }
        }

        [HttpPost]
        public IHttpActionResult PostImageInfo([FromBody] ImageInfo imageInfo)
        {
            try
            {
                using (DatabaseContext dbContext = new DatabaseContext())
                {
                    dbContext.ImageInfos.Add(imageInfo);
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
        public IHttpActionResult PutImageInfo(string id, [FromBody]ImageInfo imageInfo)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var entity = dbContext.ImageInfos.Single(e => e.Id == id);
                entity.DateAsString = imageInfo.DateAsString;
                entity.UserId = imageInfo.UserId;
                entity.ImagePath = imageInfo.ImagePath;
                dbContext.SaveChanges();
                return Ok(entity);
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteImageInfo(string id)
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
