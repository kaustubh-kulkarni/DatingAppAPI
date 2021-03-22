using System.Collections.Generic;
using System.Linq;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    // Specifiy api/"the controller name"
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }

        // Get all the users
        [HttpGet]
        public ActionResult<IEnumerable<AppUser>> GetUsers(){
            // It goest to datacontext class, access the user table and then access the data inside
            return _context.Users.ToList();
            
        }

        //  Get users with id's with api/users/id
        [HttpGet("{id}")]
        public ActionResult<AppUser> GetUser(int id){
            // It goest to datacontext class, access the user table and then access the data inside
            return _context.Users.Find(id);
        }
    }
}