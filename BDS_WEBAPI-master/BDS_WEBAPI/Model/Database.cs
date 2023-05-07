using MongoDB.Driver;

namespace BDS_WEBAPI.Model
{
    public class Database
    {
        private MongoClient? Mongodb = null;
        public IMongoDatabase? _database = null;
        //private IMongoCollection<SinhVien> sinhviens;
        public Database()
        {
            this.Mongodb = new MongoClient("mongodb+srv://bdslvlanh:Lanh1234@bdsdatabase.syeksks.mongodb.net/?retryWrites=true&w=majority");//kết nối mongodb
            this._database = Mongodb.GetDatabase("DATABASE_BDS");//chọn database trong mongodb (ở đây tên là db)
            //this.sinhviens = _database.GetCollection<SinhVien>("sinhvien");//lấy danh sách các bản ghi trong bảng SinhVien
        }
    }
}
