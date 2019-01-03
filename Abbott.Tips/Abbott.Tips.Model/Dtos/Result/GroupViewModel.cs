using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Abbott.Tips.Model.Result
{
    public class GroupListModel
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public string GroupDescription { get; set; }

        public int ParentID { get; set; }

        public string ParentName { get; set; }

        public string CreatedUser { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedUser { get; set; }

        public DateTime UpdatedDate { get; set; }
    }


    public class GroupRoleCheckModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public bool IsChecked { get; set; }
    }
}
