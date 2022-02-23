using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tyde.Shared.Configurations
{
    public class TydeConfiguration
    {
        public static string TagName { get; set; } = String.Empty; // default 
        public static TimeSpan Expires_In { get; set; } = TimeSpan.FromSeconds(3600);
        public static Uri? AuthenticationUrl { get; set; }
    }
}
