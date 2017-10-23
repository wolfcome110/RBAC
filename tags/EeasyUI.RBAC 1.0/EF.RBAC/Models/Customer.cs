using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EF.RBAC.Models
{
    public class Customer
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public int TaxNo { get; set; }
    }
}