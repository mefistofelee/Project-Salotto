using System.Collections.Generic;
using System.Linq;

namespace Salotto.App.Common.Helpers
{
    public class ColorHelper
    {
        public static List<string> Colors => new List<string> { "#198754", "#0DCAF0", "#FFC107", "#0D6EFD", "#DC3545", "#6C757D" };

        public static string FromIndex(int index)
        {
            if (!Colors.Any())
                return null;
            
            var k = index < Colors.Count ? index : (index % Colors.Count - 1);            
            return Colors[k];
        }
    }
}
