using Caliburn.Micro;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.UI.Helpers
{
    /// <summary>
    /// Custom Extensions Class
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Converts an IEnumerable<typeparamref name="T"/>  to an ObervableCollection<typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns>ObservableCollection<typeparamref name="T"/></returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
        {
            var col = new ObservableCollection<T>();
            foreach (var cur in enumerable)
            {
                col.Add(cur);
            }
            return col;
        }

        /// <summary>
        /// Converts an IEnumerable<typeparamref name="T"/> To a BindableCollection<typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns>BindableCollection<typeparamref name="T"/></returns>
        public static BindableCollection<T> ToBindableCollection<T>(this IEnumerable<T> enumerable)
        {
            var col = new BindableCollection<T>();
                col.AddRange(enumerable);
           
            return col;
        }

        /// <summary>
        /// Called to await Async Tasks
        /// </summary>
        /// <param name="task"></param>
        /// <param name="errorCallBack"></param>
        public  async static void Await(this Task task, Action<Exception>  errorCallBack)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                errorCallBack?.Invoke(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="failures"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GroupByProperty(this IEnumerable<ValidationFailure> failures)
        {
            return failures.GroupBy(error => error.PropertyName)
                .ToDictionary(group => group.Key,
                              group => String.Join(Environment.NewLine,
                                           group.Select(error => error.ErrorMessage)));
        }
    }

}
