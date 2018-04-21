using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BurgerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace BurgerAPI.Controllers
{
    [Route("api/[controller]")]
    public class BurgerController : Controller
    {
        protected BurgerRepository _repository;
        public BurgerController()
        {
            _repository = new BurgerRepository();
        }

        [HttpGet]
        [Route("GetAll")]
        public List<Burger> GetAll()
        {
            return _repository.SelectAll();
        }

        [HttpGet]
//        [Route("searchbyname/{name}")]
        public ICollection<Burger> Search([FromQuery(Name = "price")] string price = "0", [FromQuery(Name = "callories")] string callories = "0", [FromQuery(Name = "name")] string name = "")
        {
            var filter = new FilterDefinitionBuilder<Burger>().Where(t => t.Calories == float.Parse(callories) || t.Price == decimal.Parse(price) || t.Name == name);
            return _repository.Search(filter);
        }

        [HttpPost]
        public Burger Post(Burger postModel, string id = "")
        {
            if (id != "") return _repository.Update(id, postModel);
            {
                return _repository.Insert(postModel);
            }
        }
    }
}
