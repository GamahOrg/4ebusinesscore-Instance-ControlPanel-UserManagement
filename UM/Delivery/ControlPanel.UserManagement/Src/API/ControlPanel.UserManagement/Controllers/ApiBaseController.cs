using Microsoft.AspNetCore.Mvc;

namespace UserManagement.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ApiBaseController : ControllerBase
    {
    }
}