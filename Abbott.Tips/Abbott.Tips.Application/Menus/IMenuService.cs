using Abbott.Tips.EntityFrameworkCore.UnitOfWork.PagedList;
using Abbott.Tips.Framework.Dependency;
using Abbott.Tips.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Application.Menus
{
    /// <summary>
    /// 菜单服务接口类
    /// </summary>
    public interface IMenuService : IDependency
    {
        /// <summary>
        /// 获取系统当前所有菜单
        /// </summary>
        /// <returns></returns>
        IList<MenuModel> GetSystemMenus();

        /// <summary>
        /// 分页获取系统全部菜单
        /// </summary>
        /// <returns></returns>
        IPagedList<MenuModel> GetPagedMenus(int pageIndex, int pageSize, string menuName = "");

        int UpdateMenu(MenuModel menu);
    }
}
