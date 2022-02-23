using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tyde.Shared.Configurations
{
    public class TydeConfiguration
    {
        /// <summary>
        /// Token TagName i.e "token": "EQ4ahSESaDFE".
        /// Default value is "token"
        /// </summary>
        public static string TagName { get; set; } = "token"; // default 
        /// <summary>
        /// Expected TimeSpan to expire.
        /// Default value is 1hr
        /// </summary>
        public static TimeSpan Expires_In { get; set; } = TimeSpan.FromSeconds(3600);
        /// <summary>
        /// Expected DateTimeOffset of to time to expire.
        /// Optional, if not provided package will use <see cref="Expires_In"/>
        /// </summary>
        public static DateTimeOffset? Expires_At { get; set; }
        /// <summary>
        /// The Authentication Url, where the service retrives the tokens
        /// </summary>
        public static Uri? AuthenticationUrl { get; set; }
        
    }
}
