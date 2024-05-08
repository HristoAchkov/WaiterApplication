using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using static WaiterApplication.Infrastructure.Constants.DataConstants;

namespace WaiterApplication.Controllers
{
    [Authorize(Roles = AdminRole)]
    public class BaseAdminController : Controller
    {
    }
}
