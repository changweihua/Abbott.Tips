using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Model.Query
{
    public class MenuListQueryModel : PagerParameterQueryModel
    {
        public string MenuName { get; set; }
    }

    public class MenuEditModel
    {
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public string MenuLink { get; set; }
        public string MenuController { get; set; }
        public string MenuAction { get; set; }
        public string MenuPermission { get; set; }
    }
}
