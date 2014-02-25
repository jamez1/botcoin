using CCXSharp.BaseClasses;
using RestSharp;

namespace CCXSharp.Interfaces
{
    public interface ICCXRestSharp
    {
        T GetResponse<T>(string endpoint, Method method, object parameters, AccessType accessType);
    }
}
