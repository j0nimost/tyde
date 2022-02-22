using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tyde.src.Tyde.Cache
{
    public class TydeCache: TokenCache
    {
        /// <summary>
        /// Stores the Session Token Cache
        /// </summary>
        public static ConcurrentDictionary<string, string>? TokenCache { get; private set; }

        public TydeCache()
        {
            TokenCache = new ConcurrentDictionary<string, string>();
        }

    }
    
}
