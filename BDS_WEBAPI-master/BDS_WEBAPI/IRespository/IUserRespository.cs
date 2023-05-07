using BDS_WEBAPI.Model;

namespace BDS_WEBAPI.IRespository
{
    public interface IUserRespository
    {
        Task<IEnumerable<Users>> GetAll();
        Task<Users> GetbyId(string id);
        Task<Users> GetbyUsername(string id);
        Task DeletebyId(string id);
        Task<Users> Insert(Users entity);
        Task<Users> Update(Users entity);
        Task<bool> Exits(Users entity);
        Task<bool> Exits(string id);
    }
}
