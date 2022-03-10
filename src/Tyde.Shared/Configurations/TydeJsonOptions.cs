using System;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace Tyde.Shared.Configurations
{
    public class TydeJsonOptions
    {
        public Type JsonDeserailizatingToType { get; set; } = new Dictionary<string, string>().GetType(); // default is Dictionary to deserialize to
        public JsonSerializerOptions? JsonSerializerOptions { get; set; }

        private void TypeInferred()
        {
            #region
            // handle getting generic type
            PropertyInfo propertyInfo = typeof(TydeJsonOptions).GetProperty(nameof(TydeJsonOptions.JsonDeserailizatingToType));
            //PropertyInfo genericProperty = propertyInfo.Make
            #endregion


        }
    }

}
