using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Abbott.Tips.Model.Result
{
    public class RoleListModel
    {
        [NotMapped]
        public int RoleId { get; set; }

        [Column("RoleName")]
        public string RoleName { get; set; }

        [Column("RoleDescription")]
        public string RoleDescription { get; set; }

        [NotMapped]
        public int ParentID { get; set; }

        [NotMapped]
        public string ParentName { get; set; }

        [NotMapped]
        public string CreatedUser { get; set; }

        [NotMapped]
        public DateTime CreatedDate { get; set; }

        [NotMapped]
        public string UpdatedUser { get; set; }

        [NotMapped]
        public DateTime UpdatedDate { get; set; }
    }
}
