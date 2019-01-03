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

namespace Abbott.Tips.Application.Menus
{
    /// <summary>
    /// 菜单服务类
    /// </summary>
    public class MenuService : IMenuService
    {
        public IUnitOfWork unitOfWork { get; set; }

        /// <summary>
        /// 获取系统当前所有菜单
        /// </summary>
        /// <returns></returns>
        public IList<MenuModel> GetSystemMenus()
        {
            Func<IQueryable<MenuModel>, IOrderedQueryable<MenuModel>> orderBy = (b) => b.OrderBy(_ => _.Id);
            Expression<Func<MenuModel, bool>> predicate = menu => !menu.IsDeleted;
            Func<IQueryable<MenuModel>, IIncludableQueryable<MenuModel, object>> include = (menu) => menu.Include(m => m.SubMenus);

            return unitOfWork.GetRepository<MenuModel>().Get(orderBy: orderBy, include: include, predicate: predicate).ToList();
        }

        /// <summary>
        /// 分页获取系统全部菜单
        /// </summary>
        /// <returns></returns>
        public IPagedList<MenuModel> GetPagedMenus(int pageIndex, int pageSize, string menuName = "")
        {
            Func<IQueryable<MenuModel>, IOrderedQueryable<MenuModel>> orderBy = (b) => b.OrderBy(_ => _.Id);
            Expression<Func<MenuModel, bool>> predicate = null;
            //if (!string.IsNullOrEmpty(roleName))
            //{
            //    predicate = role => role.RoleName.Contains(roleName);
            //}
            //Func<IQueryable<MenuModel>, IIncludableQueryable<MenuModel, object>> include = (role) => role.Include(r => r.ParentRole);

            return unitOfWork.GetRepository<MenuModel>().GetPagedList(predicate: predicate, orderBy: orderBy, pageIndex: pageIndex, pageSize: pageSize);
        }

        public int UpdateMenu(MenuModel menu)
        {
            var estMenu = unitOfWork.GetRepository<MenuModel>().GetFirstOrDefault(predicate: (r => !r.IsDeleted && r.Id == menu.Id));

            if (estMenu != null)
            {
                estMenu.MenuName = menu.MenuName;
                estMenu.MenuController = menu.MenuController;
                estMenu.MenuAction = menu.MenuAction;
                estMenu.MenuLink = menu.MenuLink;
                estMenu.MenuPermission = menu.MenuPermission;

                unitOfWork.GetRepository<MenuModel>().Update(estMenu);

                return unitOfWork.SaveChanges();
            }

            return 0;
        }
    }
}
