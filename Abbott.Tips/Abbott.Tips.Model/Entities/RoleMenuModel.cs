﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Abbott.Tips.Model.Entities
{
    [Table("T_RoleMenu")]
    public class RoleMenuModel : TipsEntity
    {
        #region 导航属性

        public int MenuId { get; set; }

        public virtual MenuModel Menu { get; set; }

        public int RoleId { get; set; }

        public virtual RoleModel Role { get; set; }

        #endregion
    }
}
