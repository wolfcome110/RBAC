﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Sys_Groups
    {
        public IEnumerable<Sys_Groups> children { get; set; }
    }
}
