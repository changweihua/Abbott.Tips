using Abbott.Tips.EntityFrameworkCore.UnitOfWork;
using Abbott.Tips.EntityFrameworkCore.UnitOfWork.PagedList;
using Abbott.Tips.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Abbott.Tips.Application.Groups
{
    /// <summary>
    /// 用户组服务类
    /// </summary>
    public class GroupService : IGroupService
    {
        public IUnitOfWork unitOfWork { get; set; }

        /// <summary>
        /// 获取系统全部角色
        /// </summary>
        /// <returns></returns>
        public IList<GroupModel> GetGroups()
        {
            Func<IQueryable<GroupModel>, IOrderedQueryable<GroupModel>> orderBy = (b) => b.OrderBy(_ => _.Id);
            Expression<Func<GroupModel, bool>> predicate = null;
            Func<IQueryable<GroupModel>, IIncludableQueryable<GroupModel, object>> include = (group) => group.Include(r => r.SubGroups);

            return unitOfWork.GetRepository<GroupModel>().Get(orderBy: orderBy, predicate: predicate, include: include).ToList();
        }

        public GroupModel GetSingleGroup(int id)
        {
            return unitOfWork.GetRepository<GroupModel>().GetFirstOrDefault(predicate: g => g.Id == id, include: g => g.Include(r => r.ParentGroup).Include(r => r.GroupRoles));
        }

        /// <summary>
        /// 分页获取系统全部角色
        /// </summary>
        /// <returns></returns>
        public IPagedList<GroupModel> GetPagedGroups(int pageIndex, int pageSize, int pageStart = 0, int pageEnd = 0, string groupName = "")
        {
            Func<IQueryable<GroupModel>, IOrderedQueryable<GroupModel>> orderBy = (b) => b.OrderBy(_ => _.Id);
            Expression<Func<GroupModel, bool>> predicate = null;
            if (!string.IsNullOrEmpty(groupName))
            {
                predicate = group => group.GroupName.Contains(groupName);
            }
            Func<IQueryable<GroupModel>, IIncludableQueryable<GroupModel, object>> include = (group) => group.Include(r => r.ParentGroup);

            return unitOfWork.GetRepository<GroupModel>().GetPagedList(predicate: predicate, orderBy: orderBy, include: include, pageIndex: pageIndex, pageSize: pageSize);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public int CreateGroup(GroupModel group)
        {
            unitOfWork.GetRepository<GroupModel>().Insert(group);

            unitOfWork.SaveChanges();

            return 0;
        }

   /// <summary>
   /// 
   /// </summary>
   /// <param name="group"></param>
   /// <returns></returns>
        public int EditGroup(GroupModel group)
        {
            var estGroup = unitOfWork.GetRepository<GroupModel>().GetFirstOrDefault(predicate: (r => !r.IsDeleted && r.Id == group.Id));

            if (estGroup != null)
            {
                estGroup.GroupName = group.GroupName;
                estGroup.GroupDescription = group.GroupDescription;
                estGroup.ParentID = group.ParentID;
                estGroup.IsInherited = group.IsInherited;

                unitOfWork.GetRepository<GroupModel>().Update(estGroup);

                unitOfWork.SaveChanges();
            }

            return 0;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public int DeleteGroup(int groupId)
        {
            var group = unitOfWork.GetRepository<GroupModel>().GetFirstOrDefault(predicate: (r => !r.IsDeleted && r.Id == groupId), include: r => r.Include(ir => ir.SubGroups));

            if (group != null)
            {
                if(group.SubGroups != null && group.SubGroups.Count() > 0)

                group.IsDeleted = true;
                group.UpdatedBy = 1;
                unitOfWork.GetRepository<GroupModel>().Update(group);

                unitOfWork.SaveChanges();
            }

            return 0;
        }

        public int AssignRole(int groupId, List<int> roleIds)
        {
            var group = unitOfWork.GetRepository<GroupModel>().GetFirstOrDefault(predicate: (r => !r.IsDeleted && r.Id == groupId), include: r => r.Include(ir => ir.GroupRoles));

            if (group != null)
            {
                
                var existedRoles = group.GroupRoles.Where(_ => !_.IsDeleted).ToList();
                var existedRoleIds = existedRoles.Select(_ => _.RoleID).ToList();

                var toDeleteRoles = existedRoles.Where(em => !roleIds.Contains(em.RoleID)).Select(em =>
                {
                    em.IsDeleted = true;
                    em.UpdatedBy = 2;
                    return em;
                }).ToList();

                var toAddRoles = roleIds.Except(existedRoleIds).Select(id => new GroupRoleModel
                {
                    CreatedBy = 1,
                    RoleID = id,
                    GroupID = group.Id
                }).ToList();

                if (toDeleteRoles.Count > 0)
                {
                    unitOfWork.GetRepository<GroupRoleModel>().Update(toDeleteRoles);
                }
                if (toAddRoles.Count > 0)
                {
                    unitOfWork.GetRepository<GroupRoleModel>().Insert(toAddRoles);
                }

                return unitOfWork.SaveChanges();
            }

            return 0;
        }

    }
}
