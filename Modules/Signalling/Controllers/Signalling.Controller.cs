using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("signalling")]
public class SignallingController : ControllerBase
{
    private readonly ISignallingBridgesService signallingBridgesService;

    public SignallingController(ISignallingBridgesService signallingBridgesService)
    {
        this.signallingBridgesService = signallingBridgesService;
    }

    [HttpPost]
    [Route("bridge")]
    public async Task<IActionResult> CreateSignallingBridge([FromQuery] Guid userId)
    {
        var signallingBridge = await signallingBridgesService.CreateBridge(userId);
        return Ok(new { BridgeID = signallingBridge.ID });
    }
}