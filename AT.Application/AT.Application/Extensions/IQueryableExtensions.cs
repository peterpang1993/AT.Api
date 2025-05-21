using AT.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AT.Application.Extensions
{
    public static class IQueryableExtensions
    {
        public async static Task<PaginatedResult<TResult>> GetPagedAsync<T, TResult>(this IQueryable<T> query,
            int page,
            int pageSize,
            Expression<Func<T, TResult>> expression)
        {
            var count = await query.CountAsync();

            var result = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(expression)
                .ToListAsync();

            return new PaginatedResult<TResult>
            {
                Items = result,
                Page = page,
                PageSize = pageSize,
                TotalCount = count
            };
        }
    }
}
