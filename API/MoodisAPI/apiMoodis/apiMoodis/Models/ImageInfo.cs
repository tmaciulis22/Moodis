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
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ImagePath { get; set; }
        public string DateAsString { get; set; }
    
        public virtual ImageInfo ImageInfos1 { get; set; }
        public virtual ImageInfo ImageInfo1 { get; set; }
    }
}
