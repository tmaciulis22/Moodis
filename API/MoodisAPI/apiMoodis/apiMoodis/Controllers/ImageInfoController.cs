using apiMoodis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace apiMoodis.Controllers
{
    public class ImageInfoController : ApiController
    {
        public HttpResponseMessage Get()
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var imageInfos = dbContext.ImageInfos.ToList();
                if (imageInfos.Count() == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There are no ImageInfos");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, imageInfos);
                }
            }
        }

        public HttpResponseMessage Get(string id)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var entity = dbContext.ImageInfos.FirstOrDefault(ImageInfo => ImageInfo.Id == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "ImageInfo with Id " + id + " Not found");
                }
            }
        }

        public HttpResponseMessage Post([FromBody] ImageInfo imageInfo)
        {
            try
            {
                using (DatabaseContext dbContext = new DatabaseContext())
                {
                    dbContext.ImageInfos.Add(imageInfo);
                    dbContext.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, imageInfo);
                    message.Headers.Location = new Uri(Request.RequestUri +
                        imageInfo.Id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(string id, [FromBody]ImageInfo imageInfo)
        {
            try
            {
                using (DatabaseContext dbContext = new DatabaseContext())
                {
                    var entity = dbContext.ImageInfos.FirstOrDefault(e => e.Id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Group with Id " + id.ToString() + " not found to update");
                    }
                    else
                    {
                        entity.DateAsString = imageInfo.DateAsString;
                        entity.UserId = imageInfo.UserId;
                        entity.ImagePath = imageInfo.ImagePath;
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
                    var entity = dbContext.ImageInfos.FirstOrDefault(e => e.Id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "ImageInfo with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        dbContext.ImageInfos.Remove(entity);
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
