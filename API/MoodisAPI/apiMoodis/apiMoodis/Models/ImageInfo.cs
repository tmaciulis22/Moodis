//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace apiMoodis.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ImageInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ImageInfo()
        {
            this.Emotions = new HashSet<Emotion>();
        }
    
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ImagePath { get; set; }
        public string DateAsString { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Emotion> Emotions { get; set; }
        public virtual User User { get; set; }
    }
}