using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Common
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectList<T>(this IEnumerable<T> source,
            Func<T, string> value,
            Func<T, string> text,
            Func<T, bool> selectedValue = null)
            where T : class
        {
            //Func<object, object, bool> areEqual = (a,b) => a == b;

            return source.Select(s => new SelectListItem
            {
                Text = text(s),
                Value = value(s),
                Selected = selectedValue != null ? selectedValue(s) : false
            });
        }

        public static IEnumerable<TSource> Prepend<TSource>(this IEnumerable<TSource> source, TSource item)
        {
            yield return item;
            foreach (TSource data in source)
            {
                yield return data;
            }
        }
    }
}
