
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{

    public class QueryHelper<T> where T : class
    {

        public PaginationParameters? PaginationParams { get; set; } = null;

        public Expression<Func<T, bool>>? Filter { get; set; } = null;

        public Expression<Func<T, object>>[]? Includes { get; set; } = null;

        public Expression<Func<T, T>> Selector { get; set; } = null!;

        public string[] SelectedFields { get; set; } = null!;
    }

    public class QueryHelper<TSource, TResult> : QueryHelper<TSource> where TSource : class where TResult : class
    {

        public new Expression<Func<TSource, TResult>> Selector { get; set; } = null!;
    }

}
