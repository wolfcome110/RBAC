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
    
    public partial class Sys_Roles
    {
        public Sys_Roles()
        {
            this.Sys_Roles_Departments = new HashSet<Sys_Roles_Departments>();
            this.Sys_RoleNavBtns = new HashSet<Sys_RoleNavBtns>();
            this.Sys_Users_Roles = new HashSet<Sys_Users_Roles>();
        }
    
        public int Id { get; set; }
        public string RoleName { get; set; }
        public Nullable<bool> IsDefault { get; set; }
        public int SortNum { get; set; }
        public string Remark { get; set; }
        public System.DateTime AddTime { get; set; }
        public Nullable<int> ParentId { get; set; }
    
        public virtual ICollection<Sys_Roles_Departments> Sys_Roles_Departments { get; set; }
        public virtual ICollection<Sys_RoleNavBtns> Sys_RoleNavBtns { get; set; }
        public virtual ICollection<Sys_Users_Roles> Sys_Users_Roles { get; set; }
    }
}