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
    
    public partial class StockStExternalEntities1 : DbContext
    {
        //public StockStExternalEntities1()
        //    : base("name=StockStExternalEntities1")
        //{
        //}
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BrokerSecurity> BrokerSecurities { get; set; }
        public virtual DbSet<TradeExecution> TradeExecutions { get; set; }
        public virtual DbSet<ExternalBlock> ExternalBlocks { get; set; }
    }
}
