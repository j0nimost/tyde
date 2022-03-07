
namespace Tyde.Core.AuthHandler
{
    public interface ITydeAuthHander
    {
        Task<T> SendRequestAsync<T>();
        Task SendAuthRequestAsync();
    }
}
