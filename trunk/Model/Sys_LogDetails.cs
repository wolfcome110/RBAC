//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sys_LogDetails
    {
        public int Id { get; set; }
        public int LogId { get; set; }
        public string ColumnName { get; set; }
        public string ColumnText { get; set; }
        public string ColumnDataType { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string Remark { get; set; }
        public System.DateTime AddTime { get; set; }
    
        public virtual Sys_Logs Sys_Logs { get; set; }
    }
}
