using BDS_WEBAPI.IRespository;
using BDS_WEBAPI.Model;
using MongoDB.Driver;

namespace BDS_WEBAPI.Respository
{
    public class SessionsRespository : Database,ISessionRespository
    {
        private IMongoCollection<Sessions>? Sessions=null;
        public SessionsRespository() : base()
        {
            if (_database != null)
                this.Sessions = _database.GetCollection<Sessions>("Sessions");
        }
        public Task DeletebyId(string id)
        {
            Sessions.DeleteOne(e => e._id == id);
            return Task.FromResult("đã xoá");
        }
        public async Task<bool> Exits(Sessions entity)
        {
            var rs = await Sessions.FindSync(p => p.Username == entity.Username || p._id == entity._id).FirstOrDefaultAsync();
            if (rs != null)
                return true;
            return false;
        }

        public async Task<IEnumerable<Sessions>> GetAll()
        {
            return await Sessions.Find(e => true).ToListAsync();
        }

        public async Task<Sessions> GetbyId(string id)
        {
            return await Sessions.Find(x => x._id == id).FirstOrDefaultAsync();
        }

        public async Task<Sessions> Insert(Sessions entity)
        {
            Sessions?.InsertOne(entity);
            return await Task.FromResult(entity);
        }
    }
}
