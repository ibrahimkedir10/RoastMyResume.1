using MongoDB.Driver;
using resume_image.IRepository;
using resume_image.Models;

namespace resume_image.Repository
{
    public class UserRepository : IUserRepository
    {
        private MongoClient _mongoClient = null;
        private IMongoDatabase _database = null;
        private IMongoCollection<User> _userTable = null;
        
        public UserRepository() { 
            _mongoClient = new MongoClient("mongodb+srv://ibrahimkedir31206:4169027739imk@resumereview.j6p8klk.mongodb.net/");
            _database = _mongoClient.GetDatabase("officeDB");
            _userTable = _database.GetCollection<User>("User");
        }
        public User GetSaveUser()
        {
            return _userTable.Find(FilterDefinition<User>.Empty).ToList().FirstOrDefault();
        }

        public User Save(User user)
        {
            var useObj = _userTable.Find(x => x.Id == user.Id).FirstOrDefault();
            if (useObj == null) {
                _userTable.InsertOne(user);
            }
            else 
            {
                _userTable.ReplaceOne(x => x.Id == user.Id, user);

            }
            return user;
        }
    }
}
