using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Model.Query
{
    /// <summary>
    /// 角色新增模型
    /// </summary>
    public class RoleMenuCreateModel
    {
        public string RoleName { get; set; }
        public int ParentID { get; set; }
        public bool IsInherited { get; set; }
        public string RoleMenu { get; set; }
    }

    /// <summary>
    /// 角色编辑模型
    /// </summary>
    public class RoleMenuEditModel
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public int ParentID { get; set; }
        public bool IsInherited { get; set; }
        public string RoleMenu { get; set; }
    }

    /// <summary>
    /// 角色列表查询模型
    /// </summary>
    public class RoleListQueryModel : LayTablePagerParameterQueryModel
    {
        public string RoleName { get; set; }
    }

}
