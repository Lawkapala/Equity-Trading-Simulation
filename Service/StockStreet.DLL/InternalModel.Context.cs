﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class StockStInternalEntities : DbContext
    {
        //public StockStInternalEntities()
        //    : base("name=StockStInternalEntities")
        //{
        //}
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Block> Blocks { get; set; }
        public virtual DbSet<Market> Markets { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Portfolio> Portfolios { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }
        //public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
    }
}