using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLion.Core
{
    public class PagedList<T>
    {
        public PagedList()
        {

        }

        public PagedList(IQueryable<T> source, int index = 0, int pageSize = 0)
        {
            Count = source.Count();
            PageSize = pageSize;
            PageIndex = index;

            //if (index <= 0 || index > TotalPages)
            //    index = 1;

            //if (pageSize > 0)
            if (index == -1)
            {
                PageSize = pageSize = Count;
                PageIndex = index = 1;
            }

            source = source.Skip((index - 1) * pageSize).Take(pageSize);

            Result = source.ToList();
        }

        public IEnumerable<T> Result { get; set; }

        public int TotalPages
        {
            get
            {
                if (PageIndex <= 0 || PageSize <= 0) return 1;
                var total = Count / PageSize;
                total = Count % PageSize > 0 ? total + 1 : total;
                return total;
            }
        }

        public int Count
        {
            get;
            set;
        }

        public int PageIndex
        {
            get;
            set;
        }

        public int PageSize
        {
            get;
            set;
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex * PageSize) < Count;
            }
        }

        public int LastIndex { get; set; }
    }

    public static class PagedListExtension
    {
        /// <summary>
        /// Convert query to PagedList
        /// </summary>
        /// <typeparam name="T">Entity or Model</typeparam>
        /// <param name="source">Query</param>
        /// <param name="pageSize">Size of the page(If all then 0)</param>
        /// <param name="index">Page Index</param>
        /// <returns></returns>
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, int index = 0, int pageSize = 0)
        {
            return new PagedList<T>(source, index, pageSize);
        }
    }
}
