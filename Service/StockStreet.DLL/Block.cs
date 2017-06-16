//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StockStreet.DLL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Block
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Block()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }
    
        public int blockId { get; set; }
        public string userName { get; set; }
        public string symbol { get; set; }
        public string side { get; set; }
        public decimal price { get; set; }
        public Nullable<decimal> stopPrice { get; set; }
        public int totalQuantity { get; set; }
        public Nullable<int> openQuantity { get; set; }
        public Nullable<int> executedQuantity { get; set; }
        public string blockStatus { get; set; }
        public Nullable<System.DateTime> createTStamp { get; set; }
        public Nullable<System.DateTime> updatedTStamp { get; set; }
        public string orderType { get; set; }
    
        public virtual Market Market { get; set; }
        public virtual UserDetail UserDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
