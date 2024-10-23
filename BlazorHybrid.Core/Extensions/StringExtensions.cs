using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHybrid.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ToBlazorUpper(this string value)
        {
            return value.ToUpper();
        }
    }
}
