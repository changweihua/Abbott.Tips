using Abbott.Tips.EntityFrameworkCore.UnitOfWork.PagedList;
using Abbott.Tips.Framework.Dependency;
using Abbott.Tips.Model.Entities;
using Abbott.Tips.Model.Result;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abbott.Tips.Application.Users
{
    /// <summary>
    /// 用户服务接口类
    /// </summary>
    public interface IUserService : IDependency
    {
        /// <summary>
        /// 实现登录
        /// </summary>
        /// <param name="loginName">用户名</param>
        /// <param name="loginPwd">用户密码</param>
        /// <returns></returns>
        UserModel Login(string loginName, string loginPwd);

        /// <summary>
        /// 查询对应用户票据信息
        /// </summary>
        /// <param name="userID">用户编号，优先使用</param>
        /// <param name="adName">用户域帐号</param>
        /// <returns></returns>
        UserIdentityModel GetUserIdentity(int userID, string adName = "");

        /// <summary>
        /// 查询当前用户的所有菜单及权限
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        IList<UserMenuPermissionModel> GetUserMenuPermissions(int userId);

        /// <summary>
        /// 查询当前用户的所有权限
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        IList<UserPermissionModel> GetUserPermissions(int userId);

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<int> CreateUser(UserModel user);

        Task<IPagedList<UserModel>> GetPagedUsers(int pageIndex, int pageSize, string userName);

        UserModel GetSingleUser(int userId);
    }
}
