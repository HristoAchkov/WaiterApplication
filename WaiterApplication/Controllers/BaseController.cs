using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WaiterApplication.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
    }
}
