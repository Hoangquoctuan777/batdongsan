using BDS_WEBAPI.IRespository;
using BDS_WEBAPI.Model;
using MongoDB.Driver;

namespace BDS_WEBAPI.Respository
{
    public class PropertiesRespository : Database, IPropertiesRespository
    {
        private IMongoCollection<Propeties>? Properties;
        public PropertiesRespository():base()
        {
            //this.database = new Database();
            if (_database != null)
                this.Properties = _database.GetCollection<Propeties>("Properties");
        }
        Task IPropertiesRespository.DeletebyId(string id)
        {
            Properties.DeleteOne(e => e._id == id);
            return Task.FromResult("đã xoá");
        }

       async Task<bool> IPropertiesRespository.Exits(Propeties entity)
        {
            var rs = await Properties.FindSync(p => p.Title == entity.Title || p._id == entity._id).FirstOrDefaultAsync();
            if (rs != null)
                return true;
            return false;
        }

        async Task<IEnumerable<Propeties>> IPropertiesRespository.GetAll()
        {
            return await Properties.Find(e => true).ToListAsync();
        }


        async Task<Propeties> IPropertiesRespository.GetbyId(string id)
        {
            return await Properties.Find(x => x._id == id).FirstOrDefaultAsync();
        }

        async Task<Propeties> IPropertiesRespository.Insert(Propeties entity)
        {
            Properties?.InsertOne(entity);
            return await Task.FromResult(entity);
        }

        async Task<Propeties> IPropertiesRespository.Update(Propeties entity)
        {
            Properties.ReplaceOne(x => x._id == entity._id, entity);
            return await Task.FromResult(entity);
        }
    }
}
