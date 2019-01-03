using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Model.Query
{
    public class GroupCreateModel
    {
        public string GroupName { get; set; }
        public int ParentID { get; set; }
        public bool IsInherited { get; set; }
    }

    public class GroupEditModel
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int ParentID { get; set; }
        public bool IsInherited { get; set; }
    }

    public class GroupRoleAssignModel
    {
        public int GroupId { get; set; }
        public string GroupRole { get; set; }
    }
}
