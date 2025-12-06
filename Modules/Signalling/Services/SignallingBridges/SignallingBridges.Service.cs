using EMPIRIAN.Database;
using EMPIRIAN.Database.Signalling;

public class SignallingBridgesService : ISignallingBridgesService
{
    private readonly DatabaseContext db;

    public SignallingBridgesService(DatabaseContext db)
    {
        this.db = db;
    }

    public async Task<SignallingBridge> CreateBridge(Guid initiatorUserId)
    {
        var signallingBridge = new SignallingBridge(initiatorUserId);
        db.SignallingBridges.Add(signallingBridge);
        await db.SaveChangesAsync();
        return signallingBridge;
    }

    public async Task<SignallingBridge?> GetBridgeByID(Guid bridgeId)
        => await db.SignallingBridges.FindAsync(bridgeId);

    public async Task DeactivateBridge(SignallingBridge bridge)
    {
        if (!bridge.IsActive) return;
        
        bridge.IsActive = false;
        await db.SaveChangesAsync();
    }

    public async Task DeactivateBridge(Guid bridgeId)
    {
        var bridge = await GetBridgeByID(bridgeId);
        if (bridge != null)
        {
            await DeactivateBridge(bridge);
        }
    }
}