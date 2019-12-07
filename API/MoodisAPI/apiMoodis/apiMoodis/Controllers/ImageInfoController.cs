using apiMoodis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Text;

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
                if (imageInfos.Count != 0)
                {
                    return Ok(imageInfos);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpGet]
        public IHttpActionResult GetImageInfoById(string id)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                try
                {
                    var entity = dbContext.ImageInfos.Single(ImageInfo => ImageInfo.Id == id);
                    return Ok(entity);
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
        public IHttpActionResult PostImageInfo([FromBody] ImageInfo imageInfo)
        {
            try
            {
                using (DatabaseContext dbContext = new DatabaseContext())
                {
                    imageInfo.Id = Guid.NewGuid().ToString();
                    dbContext.ImageInfos.Add(imageInfo);
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
        public IHttpActionResult PutImageInfo(string id, [FromBody]ImageInfo imageInfo)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                try
                {
                    var entity = dbContext.ImageInfos.Single(e => e.Id == id);
                    entity.DateAsString = imageInfo.DateAsString;
                    entity.UserId = imageInfo.UserId;
                    dbContext.SaveChanges();
                    return Ok(entity);
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
