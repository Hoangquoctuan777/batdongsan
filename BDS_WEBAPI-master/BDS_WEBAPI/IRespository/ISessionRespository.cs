using BDS_WEBAPI.Model;

namespace BDS_WEBAPI.IRespository
{
    public interface ISessionRespository
    {
        Task<IEnumerable<Sessions>> GetAll();
        Task<Sessions> GetbyId(string id);
        Task DeletebyId(string id);
        Task<Sessions> Insert(Sessions entity);
        Task<bool> Exits(Sessions entity);
    }
}
