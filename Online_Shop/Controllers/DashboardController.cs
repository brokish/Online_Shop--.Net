using Microsoft.AspNetCore.Mvc;
using Online_Shop.services;

namespace Online_Shop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController(OnlineShopServices shopServices) : ControllerBase
{
    [HttpGet("sequential")]
    public async Task<IActionResult> GetDashboardSequential()
    {
        var result = await shopServices.GetSequentialDashboardAsync();
        return Ok(result);
    }

    [HttpGet("parallel")]
    public async Task<IActionResult> GetDashboardParallel()
    {
        var result = await shopServices.GetParallelDashboardAsync();
        return Ok(result);
    }
}