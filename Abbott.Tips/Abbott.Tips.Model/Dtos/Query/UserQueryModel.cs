using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Model.Query
{
    public class UserCreateModel
    {
        public string LoginName { get; set; }

        public string LoginPwd { get; set; }

        public string UserName { get; set; }
    }

    /// <summary>
    /// 用户列表查询模型
    /// </summary>
    public class UserListQueryModel : LayTablePagerParameterQueryModel
    {
        public string UserName { get; set; }
    }
}
