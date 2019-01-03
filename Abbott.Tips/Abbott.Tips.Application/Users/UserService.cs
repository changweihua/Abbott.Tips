using Abbott.Tips.EntityFrameworkCore.UnitOfWork;
using Abbott.Tips.EntityFrameworkCore.UnitOfWork.PagedList;
using Abbott.Tips.Model.Entities;
using Abbott.Tips.Model.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Abbott.Tips.Application.Users
{
    /// <summary>
    /// 用户服务类
    /// </summary>
    public class UserService : IUserService
    {
        public IUnitOfWork unitOfWork { get; set; }

        /// <summary>
        /// 实现登录
        /// </summary>
        /// <param name="loginName">用户名</param>
        /// <param name="loginPwd">用户密码</param>
        /// <returns></returns>
        public UserModel Login(string loginName, string loginPwd)
        {
            if (string.IsNullOrEmpty(loginName))
            {
                throw new FormatException("loginName can not be empty !");
            }

            Func<IQueryable<UserModel>, IOrderedQueryable<UserModel>> orderBy = (user) => user.OrderBy(_ => _.Id);
            Expression<Func<UserModel, bool>> predicate = user => !user.IsDeleted && user.LoginName == loginName;
            Func<IQueryable<UserModel>, IIncludableQueryable<UserModel, object>> include = (user) => user.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).ThenInclude(r => r.RoleMenus).ThenInclude(rm => rm.Menu);

            //var cfgs = unitOfWork.SqlQuery<ConfigurationListModel>("select * from T_Configuration");

            return unitOfWork.GetRepository<UserModel>().GetFirstOrDefault(predicate: predicate, include: include, orderBy: orderBy);
        }

        /// <summary>
        /// 查询对应用户票据信息
        /// </summary>
        /// <param name="userID">用户编号，优先使用</param>
        /// <param name="adName">用户域帐号</param>
        /// <returns></returns>
        public UserIdentityModel GetUserIdentity(int userID, string adName = "")
        {
            Func<IQueryable<UserModel>, IOrderedQueryable<UserModel>> orderBy = (user) => user.OrderBy(_ => _.Id);
            Expression<Func<UserModel, bool>> predicate = user => !user.IsDeleted && user.Id == userID;
            if (userID == 0)
            {
                predicate = user => !user.IsDeleted && user.LoginName == adName;
            }
            Func<IQueryable<UserModel>, IIncludableQueryable<UserModel, object>> include = (user) => user.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).ThenInclude(r => r.RoleMenus).ThenInclude(rm => rm.Menu);

            var currentUser = unitOfWork.GetRepository<UserModel>().GetFirstOrDefault(predicate: predicate, include: include, orderBy: orderBy);

            if (currentUser == null)
            {
                return null;
            }

            return new UserIdentityModel
            {
                ADName = @"SFTK\Lance.Chang",
                UserID = currentUser.Id,
                UserName = currentUser.UserName,
                UserRoles = currentUser.UserRoles.Select(ur => new UserRolePlainInfoModel
                {
                    RoleID = ur.RoleId,
                    UserID = ur.UserId,
                    RoleName = ur.Role.RoleName,
                    UserRoleMenus = ur.Role.RoleMenus.Select(rm => new UserRoleMenuPlainInfoModel
                    {
                        MenuID = rm.MenuId,
                        RoleID = rm.RoleId,
                        MenuName = rm.Menu.MenuName
                    }).ToList()
                }).ToList()
            };
        }

        /// <summary>
        /// 查询当前用户的所有菜单及权限
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public IList<UserMenuPermissionModel> GetUserMenuPermissions(int userId)
        {
            SqlParameter[] param = new SqlParameter[] {
                new SqlParameter{ ParameterName = "@UserID", Value = userId }
            };

            var permissions = unitOfWork.SqlQuery<UserMenuPermissionModel>("exec sp_GetUserMenuPermissions @UserID", param).ToList();

            return permissions;
        }

        /// <summary>
        /// 查询当前用户的所有权限
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public IList<UserPermissionModel> GetUserPermissions(int userId)
        {
            SqlParameter[] param = new SqlParameter[] {
                new SqlParameter{ ParameterName = "@UserID", Value = userId }
            };

            var permissions = unitOfWork.SqlQuery<UserPermissionModel>("exec sp_GetUserPermissions @UserID", param).ToList();

            return permissions;
        }


        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<int> CreateUser(UserModel user)
        {
            await unitOfWork.GetRepository<UserModel>().InsertAsync(user);

            return await unitOfWork.SaveChangesAsync();
        }

        public async Task<IPagedList<UserModel>> GetPagedUsers(int pageIndex, int pageSize, string userName)
        {
            Func<IQueryable<UserModel>, IOrderedQueryable<UserModel>> orderBy = (b) => b.OrderBy(_ => _.Id);

            //Func<IQueryable<UserModel>, IIncludableQueryable<UserModel, object>> include = (user) => user.Include(r => r.RoleMenus);

            Expression<Func<UserModel, bool>> predicate = null;
            if (!string.IsNullOrEmpty(userName))
            {
                predicate = user => user.UserName.Contains(userName) || user.LoginName.Contains(userName);
            }

            return await unitOfWork.GetRepository<UserModel>().GetPagedListAsync(predicate: predicate, pageIndex: pageIndex, pageSize: pageSize);
        }

        public UserModel GetSingleUser(int userId)
        {
            return unitOfWork.GetRepository<UserModel>().GetFirstOrDefault(predicate: g => g.Id == userId);
        }
    }
}
