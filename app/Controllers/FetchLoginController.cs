using Microsoft.AspNetCore.Mvc;

namespace Ultra_Saver.Controllers;

[ApiController]
[Route("[controller]")]
public class FetchLoginDataController : ControllerBase
{
    [HttpGet]
    public string GetPlaceholder()
    {
        return "placeholder";
    }
}
