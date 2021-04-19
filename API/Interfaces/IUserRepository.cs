using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    // The idea behind this repo interface is we are going to put methods that are used by another class
    // It helps reducing the redundancy of code
    public interface IUserRepository
    {
        // All methods with task should be async
       void Update(AppUser user);
       Task<bool> SaveAllAsync();
       Task<IEnumerable<AppUser>> GetUsersAsync();
       Task<AppUser> GetUserByIdAsync(int id);
       Task<AppUser> GetUserByUsernameAsync(string username);
       Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);
       Task<MemberDto> GetMemberAsync(string username);
    }
}