using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Sys_Navigations
    {
        public IEnumerable<Sys_Navigations> children { get; set; }
        public string iconCls { get { return this.IconCls; } }
    }
}
