using API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [ApiController]
    // Specifiy api/"the controller name"
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        
    }
}