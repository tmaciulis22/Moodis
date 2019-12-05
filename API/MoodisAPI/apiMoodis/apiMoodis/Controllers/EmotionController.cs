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
        [HttpGet]
        public IHttpActionResult GetAllEmotions()
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var emotions = dbContext.Emotions.ToList();
                if (emotions.Count() == 0)
                {
                    return BadRequest("There are no emotions");
                }
                else
                {
                    return Ok(emotions);
                }
            }
        }

        [Route("api/emotion/getbyImageId/{imageId}")]
        public IHttpActionResult GetByUsernameUser(string imageId)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var emotions = dbContext.Emotions.Where(emotion => emotion.ImageId == imageId).ToList();
                if (emotions.Count() == 0)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(emotions);
                }
            }
        }

        [HttpGet]
        public IHttpActionResult GetByIdEmotion(string id)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var entity = dbContext.Emotions.Single(emotion => emotion.Id == id);
                return Ok(entity);
            }
        }

        [HttpPost]
        public IHttpActionResult PostEmotion([FromBody] Emotion emotion)
        {
            try
            {
                using (DatabaseContext dbContext = new DatabaseContext())
                {
                    dbContext.Emotions.Add(emotion);
                    dbContext.SaveChanges();
                    return Ok(emotion);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        public IHttpActionResult PutEmotion(string id, [FromBody]Emotion emotion)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var entity = dbContext.Emotions.Single(e => e.Id == id);
                entity.ImageId = emotion.ImageId;
                entity.Name = emotion.Name;
                entity.Confidence = emotion.Confidence;
                dbContext.SaveChanges();
                return Ok();
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteEmotion(string id)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var entity = dbContext.Emotions.Single(e => e.Id == id);
                dbContext.Emotions.Remove(entity);
                dbContext.SaveChanges();
                return Ok();
            }
        }
    }
}
