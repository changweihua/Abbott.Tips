using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Abbott.Tips.Model.Query
{
    /// <summary>
    /// 用户登录模型
    /// </summary>
    public class UserLoginQueryModel
    {
        [Required(ErrorMessage = "登录名称不能为空")]
        public string LoginName { get; set; }

        [Required]
        public string LoginPwd { get; set; }
    }
}
