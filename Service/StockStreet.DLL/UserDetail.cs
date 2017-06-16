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
    
    public partial class UserDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserDetail()
        {
            this.Blocks = new HashSet<Block>();
            this.Orders = new HashSet<Order>();
            this.Portfolios = new HashSet<Portfolio>();
        }
    
        public string userName { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string displayName { get; set; }
        public Nullable<bool> pmLoginStatus { get; set; }
        public Nullable<bool> etLoginStatus { get; set; }
        public string securityAnswer { get; set; }
        public string email { get; set; }
        public Nullable<int> phone { get; set; }
        public Nullable<System.DateTime> dob { get; set; }
        public Nullable<bool> active { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Block> Blocks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Portfolio> Portfolios { get; set; }
    }
}
