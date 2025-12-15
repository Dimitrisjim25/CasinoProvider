using Microsoft.AspNetCore.Mvc;

namespace RgsServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CasinoController : ControllerBase
{
    [HttpPost("spin")]
    public IActionResult Spin([FromBody] SpinRequest request)
    {
        var symbols = new[] { "🍒", "🍋", "🍊", "🍉", "🔔", "⭐️", "7️⃣" };

        int r1 = Random.Shared.Next(symbols.Length);
        int r2 = Random.Shared.Next(symbols.Length);
        int r3 = Random.Shared.Next(symbols.Length);

        decimal win = 0;

        if (r1 == r2 && r2 == r3)
        {
            win = request.Amount * 10;
        }
        else if (r1 == r2 || r2 == r3 || r1 == r3)
        {
            win = request.Amount * 1.5m;
        }

        decimal newBalance = request.CurrentBalance - request.Amount + win;

        string message = win > 0 ? $"WINNER! +{win}€" : "No luck, try again!";

        return Ok(new
        {
            r1,
            r2,
            r3,
            win,
            message,
            currentBalance = newBalance
        });
    }
}

public class SpinRequest
{
    public decimal Amount { get; set; }
    public decimal CurrentBalance { get; set; }
}