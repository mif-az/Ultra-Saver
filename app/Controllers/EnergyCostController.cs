using Microsoft.AspNetCore.Mvc;

namespace Ultra_Saver.Controllers;

[ApiController]
[Route("[controller]")]
public class EnergyCostController : ControllerBase
{
    private readonly AppDatabaseContext _db;

    public EnergyCostController(AppDatabaseContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult CalculateEnergyCost()
    {
        var res = _db.Recipes.Find(1);
        if (res == null)
        {
            return StatusCode(500);
        }

        return Ok(CalculateKiloWattHours(res.Minutes, res.Wattage) * 2); //ignitis standartas 
    }

    private float CalculateKiloWattHours(float minutes, float wattage) // returns kWh of recipe
    {
        float kw = wattage / 1000;
        float hours = minutes / 60;

        return kw / hours;
    }
}
// 30 9999