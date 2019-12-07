using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apiMoodis.Models
{
    public class EmotionFE
    {
        public string Id { get; set; }
        public string ImageId { get; set; }
        public string Name { get; set; }
        public double Confidence { get; set; }
    }
}