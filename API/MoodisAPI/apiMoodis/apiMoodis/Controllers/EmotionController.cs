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
    public class EmotionController : ApiController
    {
        public HttpResponseMessage Get()
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var emotions = dbContext.Emotions.ToList();
                if (emotions.Count() == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There are no emotions");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, emotions);
                }
            }
        }

        public HttpResponseMessage Get(string id)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var entity = dbContext.Emotions.FirstOrDefault(emotion => emotion.Id == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Emotion with Id " + id + " Not found");
                }
            }
        }

        public HttpResponseMessage Post([FromBody] Emotion emotion)
        {
            try
            {
                using (DatabaseContext dbContext = new DatabaseContext())
                {
                    dbContext.Emotions.Add(emotion);
                    dbContext.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, emotion);
                    message.Headers.Location = new Uri(Request.RequestUri +
                        emotion.Id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(string id, [FromBody]Emotion emotion)
        {
            try
            {
                using (DatabaseContext dbContext = new DatabaseContext())
                {
                    var entity = dbContext.Emotions.FirstOrDefault(e => e.Id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Emotion with Id " + id.ToString() + " not found to update");
                    }
                    else
                    {
                        entity.ImageId = emotion.ImageId;
                        entity.Name = emotion.Name;
                        entity.Confidence = emotion.Confidence;
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
    }
}
