using apiMoodis.Models;
using System;
using System.Linq;
using System.Web.Http;
using System.Data.Entity.Validation;
using System.Text;

namespace apiMoodis.Controllers
{
    public class EmotionController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetAllEmotions()
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var emotions = dbContext.Emotions.Select(emotion => new EmotionFE()
                {
                    Id = emotion.Id,
                    ImageId = emotion.ImageId,
                    Name = emotion.Name,
                    Confidence = emotion.Confidence
                }).ToList();
                if (emotions.Count != 0)
                {
                    return Ok(emotions);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [Route("api/emotion/getbyImageId/{imageId}")]
        public IHttpActionResult GetEmotionsByImageId(string imageId)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var entities = dbContext.Emotions.Where(emotion => emotion.ImageId == imageId).ToList();
                var emotions = entities.Select(e => new EmotionFE() { Id = e.Id, ImageId = e.ImageId, Name = e.Name, Confidence = e.Confidence }).ToList();
                if (emotions.Count != 0)
                {
                    return Ok(emotions);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpGet]
        public IHttpActionResult GetByIdEmotion(string id)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                try
                {
                    var entity = dbContext.Emotions.Single(e => e.Id == id);
                    var emotion = new EmotionFE() { Id = entity.Id, ImageId = entity.ImageId, Name = entity.Name, Confidence = entity.Confidence };
                    return Ok(emotion);
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
        public IHttpActionResult PostEmotion([FromBody] Emotion emotion)
        {
            try
            {
                using (DatabaseContext dbContext = new DatabaseContext())
                {
                    emotion.Id = Guid.NewGuid().ToString();
                    dbContext.Emotions.Add(emotion);
                    dbContext.SaveChanges();
                    return Ok();
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
        public IHttpActionResult PutEmotion(string id, [FromBody] Emotion emotion)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                try
                {
                    var entity = dbContext.Emotions.Single(e => e.Id == id);
                    entity.ImageId = emotion.ImageId;
                    entity.Name = emotion.Name;
                    entity.Confidence = emotion.Confidence;
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
        public IHttpActionResult DeleteEmotion(string id)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                try
                {
                    var entity = dbContext.Emotions.Single(e => e.Id == id);
                    dbContext.Emotions.Remove(entity);
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
