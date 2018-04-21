using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BurgerAPI.Models
{
    public class Burger
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public float Calories { get; set; }
    }
}
