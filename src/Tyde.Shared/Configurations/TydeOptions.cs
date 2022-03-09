using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tyde.Shared.Configurations
{
    public class TydeOptions
    {
        public TydeOptions()
        {

        }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
    }
}

// https://docs.microsoft.com/en-us/dotnet/core/extensions/options-library-authors