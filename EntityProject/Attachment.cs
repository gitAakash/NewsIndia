//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntityProject
{
    using System;
    using System.Collections.Generic;
    
    public partial class Attachment
    {
        public Attachment()
        {
            this.SubCategoryDataAttachments = new HashSet<SubCategoryDataAttachment>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    
        public virtual ICollection<SubCategoryDataAttachment> SubCategoryDataAttachments { get; set; }
    }
}
