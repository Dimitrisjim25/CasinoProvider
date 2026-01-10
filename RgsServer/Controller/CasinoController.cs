using Microsoft.AspNetCore.Mvc;

namespace RgsServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CasinoController : ControllerBase
{
    // Server Balance only
    // static = While Server is running only, because its demo
    private static decimal _serverBalance = 1000m; 

    [HttpGet("balance")]
    public IActionResult GetBalance()
    {
        return Ok(new { balance = _serverBalance });
    }

    [HttpPost("spin")]
    public IActionResult Spin([FromBody] SpinRequest request)
    {
        // 1. Validation
        if (request.Amount <= 0) return BadRequest("Το ποσό πρέπει να είναι θετικό.");
        if (_serverBalance < request.Amount) return BadRequest("Δεν επαρκεί το υπόλοιπο.");

        // 2. RNG Logic
        var symbols = new[] { "🍒", "🍋", "🍊", "🍉", "🔔", "⭐️", "7️⃣" };
        int r1 = Random.Shared.Next(symbols.Length);
        int r2 = Random.Shared.Next(symbols.Length);
        int r3 = Random.Shared.Next(symbols.Length);

        decimal win = 0;
        if (r1 == r2 && r2 == r3) win = request.Amount * 10;
        else if (r1 == r2 || r2 == r3 || r1 == r3) win = request.Amount * 1.5m;

        // 3. Server-Authoritative for Balance
        _serverBalance = _serverBalance - request.Amount + win;

        string message = win > 0 ? $"WINNER! +{win}€" : "No luck, try again!";

        return Ok(new
        {
            r1, r2, r3,
            win,
            message,
            currentBalance = _serverBalance // Server sends the balance
        });
    }
}

public class SpinRequest
{
    public decimal Amount { get; set; }

}
