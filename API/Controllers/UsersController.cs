using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        // Get all the users
        // always make DB call async to process data requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(){
            // It goes to datacontext class, access the user table and then access the data inside
            // ToListAsync is async method comes from EF Core
            return await _context.Users.ToListAsync();
            
        }
        // Authorization for endpoints
        [Authorize]
        //  Get users with id's with api/users/id
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id){
            // It goest to datacontext class, access the user table and then access the data inside
            return await _context.Users.FindAsync(id);
        }
    }
}