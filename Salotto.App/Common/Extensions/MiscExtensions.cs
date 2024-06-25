///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//




using Salotto.App.Common.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Salotto.App.Common.Extensions
{
    public static class MiscExtensions
    {
        /// <summary>
        /// Different string based on Boolean value
        /// </summary>
        /// <param name="theValue"></param>
        /// <param name="yes"></param>
        /// <param name="no"></param>
        /// <returns></returns>
        public static string ToDefault(this bool theValue, string yes = "yes", string no = "")
        {
            return theValue
                ? yes
                : no;
        }

        public static bool Value(this bool? input)
        {
            if (input == null)
                return false;
            return (bool) input;
        }

        public static List<DonutSerie> GroupBy_ToChart<T>(this ICollection<T> values, string propertyName)
        {
            values = values.Where(e => GetPropertyValue(e, propertyName) != null).ToList();
            var groups = values.GroupBy(e => GetPropertyValue(e, propertyName)).ToList();

            var toReturn = new List<DonutSerie>();

            for (int i = 0; i < groups.Count; i++)
            {
                toReturn.Add(new DonutSerie(groups[i].Key.ToString(), groups[i].Count(), ColorHelper.FromIndex(i)));
            }
            return toReturn;
        }

        public static object GetPropertyValue(object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName).GetValue(obj, null);
        }
    }
}