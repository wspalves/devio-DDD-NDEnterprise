using Microsoft.AspNetCore.Mvc;

namespace DDDNerdStore.WebApp.MVC.Controllers;

public class ControllerBase : Controller
{
    protected Guid ClienteId = Guid.Parse("00000000-0000-0000-0000-000000000001");
}