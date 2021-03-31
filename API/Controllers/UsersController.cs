using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    // All methods are protected by authorize method
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        // Get all the users
        // always make DB call async to process data requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            // It goes to datacontext class, access the user table and then access the data inside
            // ToListAsync is async method comes from EF Core
            var users = await _userRepository.GetMembersAsync();
            return Ok(users);

        }
        //  Get users with id's with api/users/id
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            // It goest to datacontext class, access the user table and then access the data inside
            return await _userRepository.GetMemberAsync(username); 
        }
    }
}