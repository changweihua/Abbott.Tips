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

namespace Abbott.Tips.Application.Roles
{
    /// <summary>
    /// 角色服务类
    /// </summary>
    public class RoleService : IRoleService
    {
        public IUnitOfWork unitOfWork { get; set; }

        /// <summary>
        /// 获取系统全部角色
        /// </summary>
        /// <returns></returns>
        public IList<RoleModel> GetRoles()
        {
            Func<IQueryable<RoleModel>, IOrderedQueryable<RoleModel>> orderBy = (b) => b.OrderBy(_ => _.Id);
            Expression<Func<RoleModel, bool>> predicate = null;
            Func<IQueryable<RoleModel>, IIncludableQueryable<RoleModel, object>> include = (role) => role.Include(r => r.SubRoles);

            return unitOfWork.GetRepository<RoleModel>().Get(orderBy: orderBy, predicate: predicate, include: include).ToList();
        }

        public RoleModel GetSingleRole(int id)
        {
            return unitOfWork.GetRepository<RoleModel>().GetFirstOrDefault(predicate: role => role.Id == id, include: role => role.Include(r => r.ParentRole));
        }

        /// <summary>
        /// 分页获取系统全部角色
        /// </summary>
        /// <returns></returns>
        public IPagedList<RoleModel> GetPagedRoles(int pageIndex, int pageSize, int pageStart = 0, int pageEnd = 0, string roleName = "")
        {
            Func<IQueryable<RoleModel>, IOrderedQueryable<RoleModel>> orderBy = (b) => b.OrderBy(_ => _.Id);
            Expression<Func<RoleModel, bool>> predicate = null;
            if (!string.IsNullOrEmpty(roleName))
            {
                predicate = role => role.RoleName.Contains(roleName);
            }
            Func<IQueryable<RoleModel>, IIncludableQueryable<RoleModel, object>> include = (role) => role.Include(r => r.ParentRole);

            return unitOfWork.GetRepository<RoleModel>().GetPagedList(predicate: predicate, orderBy: orderBy, include: include, pageIndex: pageIndex, pageSize: pageSize);
        }

        /// <summary>
        /// 新增角色及菜单
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="menuIds"></param>
        /// <returns></returns>
        public int CreateRole(string roleName, int parentId, bool isInherited, IList<int> menuIds)
        {
            var role = new RoleModel
            {
                RoleName = roleName,
                CreatedBy=1,
                 ParentID=parentId,
                 IsInherited=isInherited
            };

            unitOfWork.GetRepository<RoleModel>().Insert(role);

            unitOfWork.GetRepository<RoleMenuModel>().Insert(menuIds.Select(id => new RoleMenuModel
            {
                CreatedBy = 1,
                MenuID = id,
                RoleID = role.Id
            }));

            unitOfWork.SaveChanges();

            return 0;
        }

        /// <summary>
        /// 编辑角色菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="roleName"></param>
        /// <param name="menuIds"></param>
        /// <returns></returns>
        public int EditRole(int roleId, string roleName, int parentId, bool isInherited, IList<int> menuIds)
        {
            var role = unitOfWork.GetRepository<RoleModel>().GetFirstOrDefault(predicate: (r => !r.IsDeleted && r.Id == roleId), include: r => r.Include(ir => ir.RoleMenus));

            if (role != null)
            {
                role.RoleName = roleName;
                role.ParentID = parentId;
                role.IsInherited = isInherited;

                var existedMenus = role.RoleMenus.Where(_ => !_.IsDeleted).ToList();
                var existedMenuIds = existedMenus.Select(_ => _.MenuID).ToList();

                var toDeleteMenus = existedMenus.Where(em => !menuIds.Contains(em.MenuID)).Select(em =>
                {
                    em.IsDeleted = true;
                    em.UpdatedBy = 2;
                    return em;
                }).ToList();

                var toAddMenus = menuIds.Except(existedMenuIds).Select(id => new RoleMenuModel
                {
                    CreatedBy = 1,
                    MenuID = id,
                    RoleID = role.Id
                }).ToList();

                if (toDeleteMenus.Count > 0)
                {
                    unitOfWork.GetRepository<RoleMenuModel>().Update(toDeleteMenus);
                }
                if (toAddMenus.Count > 0)
                {
                    unitOfWork.GetRepository<RoleMenuModel>().Insert(toAddMenus);
                }

                //var toDeleteMenuIds = existedMenuIds.Except(menuIds);

                //var toAddMenuIds = menuIds.Except(existedMenuIds);

                //unitOfWork.GetRepository<RoleMenuModel>().Update(toDeleteMenuIds.Select(id => new RoleMenuModel
                //{
                //    UpdatedBy = 1,
                //    UpdatedTime = DateTime.Now,
                //    MenuID = id,
                //    RoleID = role.Id,
                //    IsDeleted = true
                //}));

                //unitOfWork.GetRepository<RoleMenuModel>().Insert(toAddMenuIds.Select(id => new RoleMenuModel
                //{
                //    CreatedBy = 1,
                //    MenuID = id,
                //    RoleID = role.Id
                //}));

                unitOfWork.GetRepository<RoleModel>().Update(role);

                unitOfWork.SaveChanges();
            }

            return 0;
        }

        /// <summary>
        /// 查询当前角色对应菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public Tuple<string, IList<RoleMenuListModel>> GetRoleMenus(int roleId)
        {
            var roleNameParam = new SqlParameter { ParameterName = "@RoleName", Value = "", Size = 50, Direction = System.Data.ParameterDirection.Output };

            SqlParameter[] param = new SqlParameter[] {
                new SqlParameter{ ParameterName = "@RoleID", Value = roleId },
               roleNameParam
            };

            var menus = unitOfWork.SqlQuery<RoleMenuListModel>("exec sp_GetRoleMenus @RoleID, @RoleName OUTPUT", param).ToList();

            return new Tuple<string, IList<RoleMenuListModel>>(roleNameParam.Value.ToString(), menus);
        }


        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public int DeleteRole(int roleId)
        {
            var role = unitOfWork.GetRepository<RoleModel>().GetFirstOrDefault(predicate: (r => !r.IsDeleted && r.Id == roleId), include: r => r.Include(ir => ir.RoleMenus));

            if (role != null)
            {
                var roleMenus = role.RoleMenus.Where(_ => !_.IsDeleted).Select(em =>
                {
                    em.IsDeleted = true;
                    em.UpdatedBy = 2;
                    return em;
                }).ToList();

                if (roleMenus.Count > 0)
                {
                    unitOfWork.GetRepository<RoleMenuModel>().Update(roleMenus);
                }

                role.IsDeleted = true;
                role.UpdatedBy = 1;
                unitOfWork.GetRepository<RoleModel>().Update(role);

                unitOfWork.SaveChanges();
            }

            return 0;
        }
    }
}
