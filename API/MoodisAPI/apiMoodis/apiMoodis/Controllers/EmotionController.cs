using apiMoodis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace apiMoodis.Controllers
{
    public class EmotionController : ApiController
    {
        public HttpResponseMessage Get()
        {
            using (EmotionDBContext dbContext = new EmotionDBContext())
            {
                var emotions = dbContext.Emotions.ToList();
                if (emotions.Count() == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There are no groups");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, emotions);
                }
            }
        }

        public HttpResponseMessage Get(string id)
        {
            using (EmotionDBContext dbContext = new EmotionDBContext())
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
                using (EmotionDBContext dbContext = new EmotionDBContext())
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
                using (EmotionDBContext dbContext = new EmotionDBContext())
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
