using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Core.Utils
{
    public class StringUtils
    {
        public static string FirstCharToUpper(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            return $"{char.ToUpper(input[0])}{input[1..]}";
        }
    }
}
