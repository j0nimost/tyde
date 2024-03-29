﻿using System;
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
        public string TokenTagName { get; set; } = "token"; // default 
        /// <summary>
        /// Refresh Token TagName 
        /// it is only used when <see cref="HasRefreshToken"/> is set to True
        /// <br/>
        /// Default value is "refresh_token"
        /// </summary>
        public string RefreshTokenTagName { get; set; } = "refresh_token";
        /// <summary>
        /// ExpiresIn TagName i.e "expiresIn": 1800 <br/>
        /// This is the json response timespan from the Authorization API <br/>
        /// Default value is "expiresIn"
        /// </summary>
        public string ExpiresInTagName { get; set; } = "expiresIn";
        /// <summary>
        /// Any additional Headers needed for any other subsequent request <br/> <br/> example;
        /// "CLIENTID": "er3rfdqe8ADSH" <br/>
        /// You can pass empty values for the values to be picked from Authorization API Response, <br/>
        /// else the passed value will be used as they are.
        /// </summary>
        public Dictionary<string, string>? AdditionalHeaders { get; set; }
        /// <summary>
        /// Pass in all the necessary parameters needed to signin <br/>
        /// Example; Username, Password or any other parameter
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value
        public Dictionary<string, string> AuthorizingParameters { get; set; }
#pragma warning restore CS8618 
        /// <summary>
        /// Expected TimeSpan to expire.
        /// Default value is Zero
        /// </summary>
        public TimeSpan Expires_In { get; set; } = TimeSpan.Zero;
        /// <summary>
        /// The Authentication Url, where the service retrives the tokens
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value
        public Uri AuthenticationUrl { get; set; }

#pragma warning restore CS8618
        /// <summary>
        /// If you're using Refresh Tokens set this to True. 
        /// <see cref="RefreshTokenTagName"/> has to be set with the correct Tag Name to deserialize to.
        /// </summary>
        public bool HasRefreshToken { get; set; }


    }
}
