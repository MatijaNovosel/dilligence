using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tvz2api.Helpers
{
    public static class Helper
    {
        public static string Acronym(this string input) 
        {
            return string.Join(string.Empty, input.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).Select(s => s[0]));
        }
    }
}
