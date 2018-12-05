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
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.InputDetails = new HashSet<InputDetail>();
            this.OrderDetails = new HashSet<OrderDetail>();
            this.Pictures = new HashSet<Picture>();
        }
    
        public int Id { get; set; }
        public string BarCode { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string Unit { get; set; }
        public decimal InputPrice { get; set; }
        public decimal OutputPrice { get; set; }
        public Nullable<int> PictureId { get; set; }
        public decimal Discount { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InputDetail> InputDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Picture> Pictures { get; set; }
        public virtual Picture Picture { get; set; }
    }
}
