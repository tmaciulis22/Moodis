using System;

namespace apiMoodis.Models
{
    public class ImageInfoFE
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public string HighestEmotion { get; set; }
    }
}