using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EF.RBAC.Models
{
    public class ComboTree
    {
        public int id { get; set; }
        public string text { get; set; }
        public IEnumerable<ComboTree> children { get; set; }
    }
}