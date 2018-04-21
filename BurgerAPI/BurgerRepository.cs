using System.Collections.Generic;
using BurgerAPI.Models;
using MongoDB.Driver;
using MongoDB.Bson;


namespace BurgerAPI
{
    public class BurgerRepository
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;
        protected IMongoCollection<Burger> _collection;

        public BurgerRepository()
        {
            _client = new MongoClient();
            _database = _client.GetDatabase("BurgerDB");
            _collection = _database.GetCollection<Burger>("burgers");
        }

        public Burger Insert(Burger contact)
        {
            _collection.InsertOneAsync(contact);
            return Get(contact.Id.ToString());
        }

        public List<Burger> SelectAll()
        {
            var query = _collection.Find(new BsonDocument()).ToListAsync();
            return query.Result;
        }

        public ICollection<Burger> Search(FilterDefinition<Burger> filter)
        {
            return _collection.Find(filter).ToList();
        }

        public Burger Get(string id)
        {
            return _collection.Find(
                new BsonDocument
                {
                    { "Id", new ObjectId(id) }
                })
                .FirstAsync()
                .Result;
        }

        public Burger Update(string id, Burger postmodel)
        {
            postmodel.Id = new ObjectId(id);
            var filter = Builders<Burger>.Filter.Eq(s => s.Id, postmodel.Id);
            _collection.ReplaceOneAsync(filter, postmodel);

            return Get(id);
        }
    }
}
