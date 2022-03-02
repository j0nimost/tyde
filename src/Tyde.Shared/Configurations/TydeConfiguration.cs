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
        /// Token TagName i.e "token": "EQ4ahSESaDFE". <br/>
        /// Default value is "token"
        /// </summary>
        public static string TokenTagName { get; set; } = "token"; // default 
        /// <summary>
        /// ExpiresIn TagName i.e "expiresIn": 1800 <br/>
        /// This is the json response timespan from the Authorization API <br/>
        /// Default value is "expiresIn"
        /// </summary>
        public static string ExpiresInTagName { get; set; } = "expiresIn";
        /// <summary>
        /// Any additional Headers needed for any other subsequent request <br/> <br/> example;
        /// "CLIENTID": "er3rfdqe8ADSH" <br/>
        /// You can pass empty values for the values to be picked from Authorization API Response, <br/>
        /// else the passed value will be used as they are.
        /// </summary>
        public static Dictionary<string, string>? AdditionalHeaders { get; set; }
        /// <summary>
        /// Expected TimeSpan to expire.
        /// Default value is Zero
        /// </summary>
        public static TimeSpan Expires_In { get; set; } = TimeSpan.Zero;
        /// <summary>
        /// The Authentication Url, where the service retrives the tokens
        /// </summary>
        public static Uri? AuthenticationUrl { get; set; }
        
    }
}
