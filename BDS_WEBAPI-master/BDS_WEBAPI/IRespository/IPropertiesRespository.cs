using BDS_WEBAPI.Model;

namespace BDS_WEBAPI.IRespository
{
    public interface IPropertiesRespository
    {
        Task<IEnumerable<Propeties>> GetAll();
        Task<Propeties> GetbyId(string id);
        Task DeletebyId(string id);
        Task<Propeties> Insert(Propeties entity);
        Task<Propeties> Update(Propeties entity);
        Task<bool> Exits(Propeties entity);
    }
}
