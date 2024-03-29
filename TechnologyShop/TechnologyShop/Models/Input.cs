//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TechnologyShop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Input
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Input()
        {
            this.InputDetails = new HashSet<InputDetail>();
        }
    
        public int Id { get; set; }
        public string InputCode { get; set; }
        public System.DateTime InputDate { get; set; }
        public int UserId { get; set; }
        public int SupplierId { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public InputStatus Status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InputDetail> InputDetails { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual User User { get; set; }
    }
}
