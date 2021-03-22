using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        // always make DB call async to process data requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(){
            // It goest to datacontext class, access the user table and then access the data inside
            // ToListAsync is async method comes from EF Core
            return await _context.Users.ToListAsync();
            
        }

        //  Get users with id's with api/users/id
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id){
            // It goest to datacontext class, access the user table and then access the data inside
            return await _context.Users.FindAsync(id);
        }
    }
}