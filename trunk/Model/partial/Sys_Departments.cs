using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Sys_Departments
    {
        public IEnumerable<Sys_Departments> children { get; set; }
    }
}
