using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    // Specifiy api/"the controller name"
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        
    }
}