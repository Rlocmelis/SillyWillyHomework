using Microsoft.AspNetCore.Mvc;

namespace SillyWillyHomework.Controllers
{
    /// <summary>
    /// Defining base controller class for a consistent route convention with the [ApiController] attribute
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : Controller
    {

    }
}
