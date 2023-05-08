using BDS_WEBAPI.IRespository;
using BDS_WEBAPI.Model;
using MongoDB.Driver;

namespace BDS_WEBAPI.Respository
{
    public class NewsRespository :Database, INewsRespository
    {
        private IMongoCollection<News>? News;
        public NewsRespository():base()
        {
            if(this._database!=null)
                News=this._database.GetCollection<News>("News");
        }
        public Task DeletebyId(string id)
        {
            News.DeleteOne(e => e._id == id);
            return Task.FromResult("đã xoá");
        }

        public async Task<bool> Exits(News entity)
        {
            var rs = await News.FindSync(p => p.Title == entity.Title || p._id == entity._id).FirstOrDefaultAsync();
            if (rs != null)
                return true;
            return false;
        }

        public async Task<bool> Exits(string id)
        {
            var rs = await News.FindSync(p => p._id == id).FirstOrDefaultAsync();
            if (rs != null)
                return true;
            return false;
        }

        public async Task<IEnumerable<News>> GetAll()
        {
            return await  News.Find(e => true).ToListAsync();
        }

        public async Task<News> GetbyId(string id)
        {
          return  await News.Find(x => x._id == id).FirstOrDefaultAsync();
        }
        public async Task<News> Insert(News entity)
        {
            News?.InsertOne(entity);
            return await Task.FromResult(entity);
        }

        public async Task<News> Update(News entity)
        {
            News.ReplaceOne(x => x._id == entity._id, entity);
            return await Task.FromResult(entity);
        }

      
    }
}
