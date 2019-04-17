using Abbott.Tips.Model;
using Abbott.Tips.Pager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.EntityFrameworkCore.UnitOfWork.PagedList
{
    /// <summary>
    /// Provides the interface(s) for paged list of any type.
    /// </summary>
    /// <typeparam name="T">The type for paging.</typeparam>
    public interface IPagedList<T> : IPager<T>
    {
        
    }
}
