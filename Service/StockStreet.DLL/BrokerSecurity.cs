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
    
    public partial class BrokerSecurity
    {
        public int securityId { get; set; }
        public string securitySymbol { get; set; }
        public string securityName { get; set; }
        public decimal tradePrice { get; set; }
        public int maxSpread { get; set; }
        public int maxExeOrder { get; set; }
        public int maxInterval { get; set; }
        public int perFullyExe { get; set; }
    }
}