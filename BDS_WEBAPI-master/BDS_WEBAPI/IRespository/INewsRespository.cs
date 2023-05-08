using BDS_WEBAPI.Model;

namespace BDS_WEBAPI.IRespository
{
    public interface INewsRespository
    {
        Task<IEnumerable<News>> GetAll();
      
        Task<News> GetbyId(string id);
        Task DeletebyId(string id);
        Task<News> Insert(News entity);
        Task<News> Update(News entity);
        Task<bool> Exits(News entity);
        Task<bool> Exits(string id);
    }
}
