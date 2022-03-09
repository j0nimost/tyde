using System;

using System.Text.Json;
using System.Threading.Tasks;

namespace Tyde.Shared.Configurations
{
    public class TydeJsonProperties
    {
        public static Type  JsonDeserailizatingToType { get; set; } = new Dictionary<string, string>().GetType();
        public static JsonSerializerOptions JsonSerializerOptions { get; set; }
    }
}
